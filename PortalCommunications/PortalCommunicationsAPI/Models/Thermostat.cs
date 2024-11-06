namespace PortalCommunicationsAPI.Models
{
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
}
