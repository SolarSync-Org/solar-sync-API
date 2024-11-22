using Microsoft.ML.Data;

namespace SolarSync_API.Domain.ML;

public class ClientReportPrediction
{
    [ColumnName("PredictedLabel")]
    public string PredictedLabel { get; set; }
}