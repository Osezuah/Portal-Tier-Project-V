namespace PortalCommunicationsAPI.Models
{
    public class SmartVacuum : Device
    {
        public double batteryLife { get; set; } // Battery life percentage
        public string location { get; set; } // Current location of vacuum in the house
        public string notification { get; set; } // Status (e.g., "Clogged", "Change Filter")

        // Command functions
        public void TurnOn()
        {
            State = "On";
        }

        public void TurnOff()
        {
            State = "Off";
        }

        public void IssueNotification(string message)
        {
            notification = message;
        }
    }
}
