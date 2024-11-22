using System;

namespace SolarSync_API.Services
{
    public class PotentialCalculationService
    {
        /// <summary>
        /// Simula o cálculo do potencial de energia solar com base em parâmetros fornecidos.
        /// </summary>
        /// <param name="residenceType">Tipo de residência.</param>
        /// <param name="energyConsumption">Consumo médio mensal de energia em kWh.</param>
        /// <returns>Valor do potencial (0 a 1).</returns>
        public double CalculatePotential(string residenceType, double energyConsumption)
        {
            // Simulação de resposta mockada da API do Google de calorimetria
            Random random = new Random();
            
            double basePotential = residenceType.ToLower() switch
            {
                "casa" => 0.8,
                "apartamento" => 0.6,
                "fazenda" => 0.9,
                _ => 0.5 
            };
            
            double consumptionFactor = energyConsumption switch
            {
                < 200 => 0.5,
                < 500 => 0.7,
                _ => 0.9  
            };
            
            double randomFactor = random.NextDouble() * 0.2;
            
            return Math.Round((basePotential * consumptionFactor) + randomFactor, 2);
        }
    }
}