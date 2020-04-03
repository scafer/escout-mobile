using escout.Models.Db;
using escout.Models.Rest;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using escout.Models;

namespace escout.Helpers
{
    public class Utils
    {
        public static DbGame DbGame;

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
            {"name"},
            {"age"}
        };

        public static readonly List<string> SoccerEventList = new List<string>
        {
            {string.Empty}, {"Soccer001"}, {"Soccer002"}, {"Soccer003"},
            {"Soccer004"}, {"Soccer005"}, {"Soccer006"}, {"Soccer007"},
            {"Soccer008"}, {"Soccer009"}, {"Soccer010"}, {"Soccer011"},
            {"Soccer012"}, {"Soccer013"}, {"Soccer014"}, {"Soccer015"},
            {"Soccer016"}, {"Soccer017"}, {"Soccer018"}, {"Soccer019"}, 
            {"Soccer020"}, {"Soccer021"}, {"Soccer022"}, {"Soccer023"},
            {"Soccer024"}, {"Soccer025"}, {"Soccer026"}, {"Soccer027"},
            {"Soccer028"}, {"Soccer029"}, {"Soccer030"},
        };


        public static Option GetSoccerEvent(string id)
        {
            return SoccerEvents[id];
        }

        public static readonly Dictionary<string, Option> SoccerEvents = new Dictionary<string, Option>
        {
            {SoccerEventList[0], new Option(string.Empty, string.Empty) },
            {SoccerEventList[1], new Option("Recupera��o de bola", "")},
            {SoccerEventList[2], new Option("Interrup��o", "") },
            {SoccerEventList[3], new Option("Perda de bola", "") },
            {SoccerEventList[4], new Option("Remate", "") },
            {SoccerEventList[5], new Option("Passe", "") },
            {SoccerEventList[6], new Option("Passe falhado", "") },
            {SoccerEventList[7], new Option("Passe concretizado", "") },
            {SoccerEventList[8], new Option("Assist�ncia Sim", "") },
            {SoccerEventList[9], new Option("Assist�ncia N�o", "") },
            {SoccerEventList[10], new Option("Para fora", "") },
            {SoccerEventList[11], new Option("Intercetado", "") },
            {SoccerEventList[12], new Option("� baliza", "") },
            {SoccerEventList[13], new Option("Bola parada", "") },
            {SoccerEventList[14], new Option("Lan�amento a favor", "") },
            {SoccerEventList[15], new Option("Falta cometida", "") },
            {SoccerEventList[16], new Option("Falta sofrida", "") },
            {SoccerEventList[17], new Option("Cart�o vermelho", "") },
            {SoccerEventList[18], new Option("Cart�o amarelo", "") },
            {SoccerEventList[19], new Option("Ao poste", "") },
            {SoccerEventList[20], new Option("Guarda-Redes Defendeu", "") },
            {SoccerEventList[21], new Option("Golo", "") },
            {SoccerEventList[22], new Option("Penalty", "") },
            {SoccerEventList[23], new Option("Livre", "") },
            {SoccerEventList[24], new Option("Falhou", "") },
            {SoccerEventList[25], new Option("Defesa", "") },
            {SoccerEventList[26], new Option("Pontap� de baliza", "") },
            {SoccerEventList[27], new Option("Posse de bola", "") },
            {SoccerEventList[28], new Option("Bola na equipa adversaria", "") },
            {SoccerEventList[29], new Option("Agarrou a bola", "") },
            {SoccerEventList[30], new Option("N�o agarrou a bola", "") },
        };
    }
}
