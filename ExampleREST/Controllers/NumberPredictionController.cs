using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;
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

        private readonly PredictionEnginePool<ApiInput, ApiPrediction> _predictionEnginePool;

        public NumberPredictionController(PredictionEnginePool<ApiInput, ApiPrediction> predictionEnginePool)
        {
            _predictionEnginePool = predictionEnginePool;
        }

        // GET api/<NumberPredictionController>/5
        [HttpGet("{id}")]
        public ApiPrediction Get(string id)
        {

            var prediction = _predictionEnginePool.Predict(modelName:"MLModel", new ApiInput
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
