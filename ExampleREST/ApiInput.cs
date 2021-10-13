using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleREST
{
    public class ApiInput
    {

        [ColumnName("Label"), LoadColumn(0)]
        public string Label { get; set; }

        [ColumnName("ImageSource"), LoadColumn(1)]
        public string ImageSource { get; set; }
    }
}
