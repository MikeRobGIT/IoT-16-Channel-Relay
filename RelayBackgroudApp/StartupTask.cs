using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Devices.Gpio;

//using GHIElectronics.UWP.GadgeteerCore;
//using GSI = GHIElectronics.UWP.GadgeteerCore.SocketInterfaces;


// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace RelayBackgroudApp {
    public sealed class StartupTask : IBackgroundTask {
        internal readonly Relay Relay = new Relay {
            Channels = new List<Channel> {
                    new Channel { Pin = 4 , Position = 1  },
                    new Channel { Pin = 17, Position = 2  },
                    new Channel { Pin = 27, Position = 3  },
                    new Channel { Pin = 18, Position = 4  },
                    new Channel { Pin = 23, Position = 5  },
                    new Channel { Pin = 22, Position = 6  },
                    new Channel { Pin = 25, Position = 7  },
                    new Channel { Pin = 24, Position = 8  },
                    new Channel { Pin = 6 , Position = 9  },
                    new Channel { Pin = 5 , Position = 10 },
                    new Channel { Pin = 12, Position = 11 },
                    new Channel { Pin = 13, Position = 12 },
                    new Channel { Pin = 19, Position = 13 },
                    new Channel { Pin = 16, Position = 14 },
                    new Channel { Pin = 26, Position = 15 },
                    new Channel { Pin = 20, Position = 16 }

                }
        };


        public void Run(IBackgroundTaskInstance taskInstance) {
            RelayTest();

            // TODO: Insert code to perform background work
            //
            // If you start any asynchronous methods here, prevent the task
            // from closing prematurely by using BackgroundTaskDeferral as
            // described in http://aka.ms/backgroundtaskdeferral
        }

        public void RelayTest() {
            // Get the default GPIO controller on the system
            var gpio = GpioController.GetDefault();
            if (gpio == null) return; // GPIO not available on this system

            using (var openPin = gpio.OpenPin(Relay.Channels[8].Pin)) {
                // Latch HIGH value first. This ensures a default value when the pin is set as output
                openPin.Write(GpioPinValue.High);
                // Set the IO direction as output
                openPin.SetDriveMode(GpioPinDriveMode.Output);
                Task.Delay(120000).Wait();
            } // Close pin - will revert to its power-on state



            //for (var i = 0; i < 3; i++) {
            //    foreach (var channel in Relay.Channels) {
            //        using (var openPin = gpio.OpenPin(channel.Pin)) {
            //            // Latch HIGH value first. This ensures a default value when the pin is set as output
            //            openPin.Write(GpioPinValue.High);
            //            // Set the IO direction as output
            //            openPin.SetDriveMode(GpioPinDriveMode.Output);
            //            if (channel.Pin == 9) Task.Delay(30000).Wait();
            //            else Task.Delay(500).Wait();
            //        } // Close pin - will revert to its power-on state
            //    }
            //}
        }
    }

    internal class Relay {
        public List<Channel> Channels { get; set; }
        public Relay() {
            Channels = new List<Channel>();
        }

    }
    internal class Channel {
        public int Position { get; set; }
        public int Pin { get; set; }
    }

}
//new List<int> { 4, 17, 27, 18, 22, 23, 25, 5, 6, 21, 12, 13, 16, 19, 20, 26 };