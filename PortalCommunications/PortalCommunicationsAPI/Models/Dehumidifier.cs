namespace PortalCommunicationsAPI.Models
{
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
            if (waterLevel <= 0)
            {
                State = "The water tank empty";
            }
        }



    }
}
