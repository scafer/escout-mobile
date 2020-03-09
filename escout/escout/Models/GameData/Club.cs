using escout.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace escout.Models.GameData
{
    class Club : BaseModel
    {
        public string key { get; set; }
        public string name { get; set; }
        public string fullname { get; set; }
        public string country { get; set; }
        public string founded { get; set; }
        public string colors { get; set; }
        public string members { get; set; }
        public string stadium { get; set; }
        public string address { get; set; }
        public string homepage { get; set; }
        public int? imageId { get; set; }
        public string created { get; set; }
        public string updated { get; set; }
    }
}
