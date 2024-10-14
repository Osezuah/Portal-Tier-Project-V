using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Databases
{
    public class Device
    {
        public int Id { get; set; } // PK
        public string Name { get; set; }
        public string State { get; set; }

        public ICollection<DeviceLog> DeviceLogs { get; set; } // Logging data for the device
        public DateTime LastUpdated { get; set; } // Last time the status was updated

        public class Thermostat : Device
        {
            public double currentTemp { get; set; }
            public double targetTemp { get; set; }

            public void SetTemp(double temp)
            {
                targetTemp = temp;
                State = "Heating/Cooling";
            }

            public void ToggleSystem(string mode)
            {
                State = mode;
            }
        }

        public class SmartFridge : Device
        {
            public double fridgeTemp { get; set; }
            public double freezerTemp { get; set; }

            public double humidityLevel { get; set; }

            public void SetFridgeTemp(double temp)
            {
                fridgeTemp = temp;
                State = "Fridge Temperature Set";
            }

            public void SetFreezerTemp(double temp)
            {
                freezerTemp = temp;
                State = "Freezer Temperature Set";
            }

            public class SmartVacuum : Device
            {
                public double batteryLife { get; set; }
                public string location { get; set; }

                public string notification { get; set; }

                public void TurnOn()
                {
                    State = "On";
                }

                public void TurnOff()
                {
                    State = "Off";
                }

                public void ErrorNotification (string message)
                {
                    notification = message;
                }
            }
        }

        public class Dehumidifier : Device
        {
            public double humidityLevel { get; set; }
            public double waterLevel { get; set; }

            public void TurnOn()
            {
                State = "On";
            }

            public void TurnOff()
            {
                State = "Off";
            }

            public void TankEmpty()
            {
               if (waterLevel <= 0 )
                {
                    State = "The water tank empty";
                }
            }



        }

        public class SmartOven : Device
        {
            public double currentTemp { get; set; }
            public double targetTemp { get; set; }

            public void SetTemp(double temp)
            {
                targetTemp = temp;

                State = "Temperature has been set";
            }

            public void TurnOn()
            {
                State = "On";
            }

            public void TurnOff()
            {
                State = "Off";
            }

            public void SendNotification (string message)
            {
                State = message;
            }
        }


    }
}
