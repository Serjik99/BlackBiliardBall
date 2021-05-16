using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SimpleWebApp.Repository
{
    public class PredictionsDatabaseRepository : IPredictionsRepository
    {
        public void SavePrediction(string prediction)
        {
            using (IDbConnection db = new MySqlConnection("Server=127.0.0.1;Database=myDataBase;Uid=root;Pwd=my-secret-pw;"))
            {
                string sqlQuery = "INSERT INTO predictions (PredictionText) Values(@PredictionText)";

                int rowsAffected = db.Execute(sqlQuery, prediction);

            }
        }

        public PredictionDto GetPredictionById(int id)
        {
            using (IDbConnection db = new MySqlConnection("Server=127.0.0.1;Database=myDataBase;Uid=root;Pwd=my-secret-pw;"))
            {
                return db.QueryFirst<PredictionDto>("SELECT * FROM predictions WHERE Id = @id", new { id });
            }
        }

        public List<PredictionDto> GetAllPredictions()
        {
            using (IDbConnection db = new MySqlConnection("Server=127.0.0.1;Database=myDataBase;Uid=root;Pwd=my-secret-pw;"))
            {
                return db.Query<PredictionDto>("SELECT * FROM predictions").ToList();
            }
        }

        public void RemovePrediction(string id)
        {
            using (IDbConnection db = new MySqlConnection("Server=127.0.0.1;Database=myDataBase;Uid=root;Pwd=my-secret-pw;"))
            {
                db.Query<PredictionDto>("DELETE FROM predictions WHERE PredictionText = @id", new { id }).ToList();
            }
        }

        public void UpdatePrediction(PredictionDto prediction)
        {
            using (IDbConnection db = new MySqlConnection("Server=127.0.0.1;Database=myDataBase;Uid=root;Pwd=my-secret-pw;"))
            {
                db.Query<PredictionDto>("UPDATE predictions SET PredictionText = @predictionText WHERE id = @id", prediction);
            }
        }

    }
}
