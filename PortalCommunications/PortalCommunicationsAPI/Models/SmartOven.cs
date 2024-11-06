namespace PortalCommunicationsAPI.Models
{
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

        public void SendNotification(string message)
        {
            State = message;
        }
    }
}
