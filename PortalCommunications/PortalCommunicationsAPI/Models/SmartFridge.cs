namespace PortalCommunicationsAPI.Models
{
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

            public void ErrorNotification(string message)
            {
                notification = message;
            }
        }
    }
}
