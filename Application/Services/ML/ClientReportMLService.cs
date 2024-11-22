using Microsoft.ML;
using System.Collections.Generic;
using System.Linq;
using SolarSync_API.Domain.ML;
using SolarSync_API.Models;

public class ClientReportMLService
{
    private readonly MLContext _mlContext;
    private ITransformer _model;

    public ClientReportMLService()
    {
        _mlContext = new MLContext();
    }

    public void Train(IEnumerable<ClientReport> clientReports)
    {
        // Verificar se todos os relatórios têm uma label
        if (clientReports.Any(cr => string.IsNullOrEmpty(cr.Label)))
            throw new InvalidOperationException("Todos os relatórios de treinamento devem ter uma Label definida.");

        var trainingData = clientReports.Select(cr => new ClientReportInput
        {
            ResidenceType = cr.ResidenceType,
            Rent = cr.Rent,
            Potential = cr.Potential,
            EnergyConsumption = cr.EnergyConsumption,
            Label = cr.Label // Certifique-se de passar a Label para o treinamento
        }).ToList();

        var dataView = _mlContext.Data.LoadFromEnumerable(trainingData);

        var pipeline = _mlContext.Transforms.Categorical.OneHotEncoding("ResidenceType")
            .Append(_mlContext.Transforms.Concatenate("Features", "ResidenceType", "Rent", "Potential", "EnergyConsumption"))
            .Append(_mlContext.Transforms.Conversion.MapValueToKey("Label"))
            .Append(_mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy())
            .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

        _model = pipeline.Fit(dataView);
    }


    public string Predict(ClientReportInput input)
    {
        if (_model == null)
            throw new InvalidOperationException("O modelo precisa ser treinado antes de realizar previsões.");

        // Cria um engine de previsão
        var predictionEngine = _mlContext.Model.CreatePredictionEngine<ClientReportInput, ClientReportPrediction>(_model);

        // Realiza a previsão
        var prediction = predictionEngine.Predict(input);
        return prediction.PredictedLabel;
    }
}