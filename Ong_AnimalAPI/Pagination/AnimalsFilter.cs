using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ong_AnimalAPI.Pagination
{
    public class AnimalsFilter : QueryStringParameters
    {
        public string Gender { get; set; } = string.Empty;
    }
}