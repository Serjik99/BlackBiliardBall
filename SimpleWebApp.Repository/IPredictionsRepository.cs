using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleWebApp;




namespace SimpleWebApp.Repository
{
    public interface IPredictionsRepository
    {
        void SavePrediction(string prediction);
        PredictionDto GetPredictionById(int id);
        List<PredictionDto> GetAllPredictions();
        void RemovePrediction(string prediction);
        void UpdatePrediction(PredictionDto prediction);
    }
}
