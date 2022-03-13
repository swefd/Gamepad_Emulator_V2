using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nefarius.ViGEm.Client;

namespace Gamepad
{
    class VirtualClient
    {
        private static ViGEmClient client;


        public static ViGEmClient GetInstance()
        {
            if (client == null)
            {
                client = new ViGEmClient();
            }
            return client;
        }
    }
}
