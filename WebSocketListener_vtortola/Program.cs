using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets;
using Nefarius.ViGEm.Client.Targets.Xbox360;



namespace WsChatServer
{

    
    class Program
    {
        static void Main(string[] args)
        {


            IDictionary<Guid, String> test = new Dictionary<Guid, String>();

            


            Guid g = new();

            test.Add(g, "TEST");



        /*            ViGEmClient client = new ViGEmClient();

                    IXbox360Controller controller = client.CreateXbox360Controller();

                    //Gamepad controller = new Gamepad();

                    controller.Connect();


                    Console.Write("Constroller User Index: ");
                    Console.WriteLine(controller.UserIndex);

                    controller.SetButtonState(Xbox360Button.X, true);
                    controller.SetAxisValue(Xbox360Axis.LeftThumbX, -32768);*/




        var t = Task.Run(() =>
            {
                _ = Connection.ServerStart();
            });


            while (true)
            {
                Console.WriteLine(".");
                Thread.Sleep(2000);
            }

            //controller.Disconnect();
        }
    }

}