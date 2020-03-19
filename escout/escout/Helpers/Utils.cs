using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using escout.Models;
using Newtonsoft.Json;

namespace escout.Helpers
{
    class Utils
    {
        public static async Task<Image> GetImage(int imageId)
        {
            var img = new Image();
            var response = await RestConnector.GetObjectAsync(RestConnector.Image + "?id=" + imageId);
            if (!string.IsNullOrEmpty(response))
            {
                img = JsonConvert.DeserializeObject<Image>(response);
            }

            return img;
        }

        public static readonly List<string> AthleteFilter = new List<string>
        {
            {"Name"},
            {"Age"}
        };
    }
}
