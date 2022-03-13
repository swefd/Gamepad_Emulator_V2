using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets;
using Nefarius.ViGEm.Client.Targets.Xbox360;

using System.Text.Json;

namespace WebSocketListener_vtortola.GamePad
{

    public class JsonHelper
    {
        //public Guid Guid;

        // Buttons
        public bool ButtonA { get; set; }
        public bool ButtonB { get; set; }
        public bool ButtonX { get; set; }
        public bool ButtonY { get; set; }

        // Cros
        public bool ButtonUp { get; set; }
        public bool ButtonDown { get; set; }
        public bool ButtonRight { get; set; }
        public bool ButtonLeft { get; set; }

    }
    public class GamepadHandeler
    {

        private static Dictionary<Guid, IXbox360Controller> controllers = new Dictionary<Guid, IXbox360Controller>();
        private static ViGEmClient client = new ViGEmClient();

        public GamepadHandeler(Guid guid)
        {
            IXbox360Controller xbox360Controller = client.CreateXbox360Controller();
            xbox360Controller.Connect();
            controllers.Add(guid, xbox360Controller);
        }


        public static IXbox360Controller GetController(Guid guid)
        {
            return controllers.GetValueOrDefault(guid);
        }


        public static void Remove(Guid guid)
        {
            GetController(guid).Disconnect();
            controllers.Remove(guid);
        }
        
        public static void Disconnect(Guid guid)
        {
            GetController(guid).Disconnect();
        }

        public static void Action(Guid guid, string json)
        {
            
            JsonHelper? jsonHelper = JsonSerializer.Deserialize<JsonHelper>(json);
            Console.WriteLine(jsonHelper?.ButtonA);

        }


    }
}
