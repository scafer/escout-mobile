using escout.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace escout.Models.GameData
{
    class Athlete : BaseModel
    {
        public string key { get; set; }
        public string name { get; set; }
        public string fullname { get; set; }
        public string birthDate { get; set; }
        public string birthPlace { get; set; }
        public string citizenship { get; set; }
        public double height { get; set; }
        public double weight { get; set; }
        public string position { get; set; }
        public string agent { get; set; }
        public string currentInternational { get; set; }
        public string status { get; set; }
        public int? clubId { get; set; }
        public int? imageId { get; set; }
        public string created { get; set; }
        public string updated { get; set; }
    }
}
