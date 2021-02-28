using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApp
{
    public class PredictionManager
    {
        Random random = new Random();
        private List<string> predictions = new List<string>()
        {
            "1","2","3","4","5"
        };

        public void AddPrediction(string text)
        {
            predictions.Add(text);
        }

        public string GetRandomPrediction()
        {
            return predictions[random.Next(0,predictions.Count)];
        }
    }
}
