using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExampleREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumberPredictionController : ControllerBase
    {

        private static Lazy<PredictionEngine<ApiInput, ApiPrediction>> PredictionEngine = new Lazy<PredictionEngine<ApiInput, ApiPrediction>>(CreatePredictionEngine);

        // For more info on consuming ML.NET models, visit https://aka.ms/mlnet-consume
        // Method for consuming model in your app
        public static ApiPrediction Predict(ApiInput input)
        {
            ApiPrediction result = PredictionEngine.Value.Predict(input);
            return result;
        }

        public static PredictionEngine<ApiInput, ApiPrediction> CreatePredictionEngine()
        {
            // Create new MLContext
            MLContext mlContext = new MLContext();

            // Load model & create prediction engine
            string modelPath = @"C:\Technikum\SWEI\Uebung01\Uebung01\ExampleREST\MLModels\MLModel.zip";
            ITransformer mlModel = mlContext.Model.Load(modelPath, out var modelInputSchema);
            var predEngine = mlContext.Model.CreatePredictionEngine<ApiInput, ApiPrediction>(mlModel);

            return predEngine;
        }

        // GET api/<NumberPredictionController>/5
        [HttpGet("{id}")]
        public ApiPrediction Get(string id)
        {

            var predictionEngine = CreatePredictionEngine();
            var prediction = predictionEngine.Predict(new ApiInput
            {
                ImageSource = id
            });

            return (new ApiPrediction
            {
                Prediction = prediction.Prediction,
                Score = prediction.Score
            });
        }
    }
}
