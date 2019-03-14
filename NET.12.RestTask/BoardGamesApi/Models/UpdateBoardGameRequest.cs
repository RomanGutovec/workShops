using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGamesApi.Models
{
    public class UpdateBoardGameRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
