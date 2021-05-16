using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleWebApp.Repository;

namespace SimpleWebApp
{
    public class PredictionManager
    {
        Random random = new Random();
        IPredictionsRepository _repository = new PredictionsDatabaseRepository();


        public PredictionManager(IPredictionsRepository repository)
        {
            _repository = repository;
        }

        public void AddPrediction(string prediction)
        {
            _repository.SavePrediction(prediction);
        }

        public List<Prediction> GetAllPredictions()
        {
            return _repository.GetAllPredictions().Select(dto => new Prediction(dto.PredictionText)).ToList();
            
        }

        public Prediction GetRandomPrediction()
        {
           var predictions = _repository.GetAllPredictions();
            return new Prediction(predictions[random.Next(0, predictions.Count())].PredictionText);
        }
        public void DeletPrediction(Prediction prediction)
        {
            _repository.RemovePrediction(prediction.PredictionString);
        }

        public void UpdatePrediction(int i,string text)
        {
            _repository.UpdatePrediction(new PredictionDto() { PredictionText = text, Id = i });
        }
    }
}
