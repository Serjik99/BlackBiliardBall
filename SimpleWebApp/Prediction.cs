namespace SimpleWebApp
{
    public  class Prediction
    {
        public int PredictionId { get; set; }
        public string PredictionString { get; set; }

        public Prediction()
        { }

        public Prediction(string text)
        {
            PredictionString = text;
        }

        public Prediction (int predictionId , string predictionString)
        {
            PredictionString = predictionString;
            PredictionId = predictionId;
        }

        public override bool Equals(object obj)
        {
            if ((obj) is Prediction)
                return (obj as Prediction).PredictionString == PredictionString;
            return base.Equals(obj);
        }

    }
}