using escout.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace escout.Models.GameData
{
    class Competition : BaseModel
    {
        public string key { get; set; }
        public string name { get; set; }
        public string edition { get; set; }
        public int sportId { get; set; }
        public int? imageId { get; set; }
        public string created { get; set; }
        public string updated { get; set; }
    }
}
