using escout.Models.Rest;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace escout.Helpers
{
    class Utils
    {
        public static async Task<Image> GetImage(int? imageId)
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
