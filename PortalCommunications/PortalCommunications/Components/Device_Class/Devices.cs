namespace PortalCommunications.Components.Device_Class
{
    // device log class
    //this class will have an objewct in the device class . and from that e
    public class deviceLogs
    {
        public int deviceLogId { get; set; } // PK
        public int deviceId { get; set; } // FK

        //this is the main data which have the csv value of the device details and we have to extract it from here and perform the validaitron for each of teh resp. class
        public string loggedMessage { get; set; }

        // time of when log was created
        public DateTime loggedTime { get; set; }


    }

    //base class 
    public abstract class Device
    {
        //id is the device id 
        public int Id { get; set; }

        // name is the device name 
        public string Name { get; set; }

        //state in this the Active attribute in the device table whicih would represent teh active/not active so it would be a true = active or false = inactive ( boolean )
        public bool State { get; set; }



        public DateTime LastUpdated { get; set; }

        protected deviceLogs DeviceActivity { get; set; }
        //each class will use deviecacitivity.loggedmessage in thier respective class adn extract the data CSV from this to validate for thier part.

        //parametrized constructor for device
        protected Device(int id, string name, bool state, DateTime lastupdated, int devicelogid, string message)
        {
            Id = id;
            Name = name;
            State = state;
            LastUpdated = lastupdated;
            DeviceActivity = new deviceLogs
            {
                deviceLogId = devicelogid,
                deviceId = id,
                loggedMessage = message,
                loggedTime = lastupdated
            };
        }


        //function is defined here and will be used by all other classes
        public abstract bool Validate();
        public void SaveToDatabase()
        {
            //call the database fucntion and pass teh apreameters
            //this will be database fuyncitonality impolemented by thier team.
        }

    }


    public class Lock : Device
    {
        public Lock(int id, string name, bool state, DateTime lastUpdated, int deviceLogId, string message) : base(id, name, state, lastUpdated, deviceLogId, message)
        {

        }

        // List of valid lock names which are present . so this will be used in the validation if there is anything 
        private static readonly List<string> ValidNames = new List<string>
        { "Front door smart lock",
          "Garage door smart lock",
          "Back door smart lock",
          "Window locks",
          "Interior door locks (mud room)",
          "Gate locks (fence gate)"
        };

        public override bool Validate()
        {
            if (DeviceActivity.loggedMessage != "true" || DeviceActivity.loggedMessage != "false")
            {
                //if it is false then we have to send it to the portal UI or the Home if in case this is not validated 
                //WE HAVE TO TALKN TO COMMUNICATIONS TEAM FOR THIS 
                return false;
            }
            if (!ValidNames.Contains(Name))
            {
                return false;
            }
            return true;
        }
    }

    //class for the SmartFrdige whihc is without the hub meaning nooo list is reuqired here.....NICE
    public class SmartFridge : Device
    {

        //this naming is the same as I got from teh devices without hub ppt for design sprint 2...
        public int fridgeTemperature { get; set; }
        public int freezerTemperature { get; set; }
        public SmartFridge(int id, string name, bool state, DateTime lastUpdated, int deviceLogId, string message) : base(id, name, state, lastUpdated, deviceLogId, message)
        {

        }
        public override bool Validate()
        {
            //extracting the values now 
            //since it is comma seperated right so spliitng the logged message ...
            var values = DeviceActivity.loggedMessage.Split(',');
            foreach (var value in values)
            {
                var keyValue = value.Split('=');
                if (keyValue.Length == 2)
                {
                    if (keyValue[0].Trim() == "fridgeTemperature")
                    {
                        if (!int.TryParse(keyValue[1], out int fridgeTemp))
                        {
                            return false;
                        }
                        fridgeTemperature = fridgeTemp;
                    }
                    else if (keyValue[0].Trim() == "freezerTemperature")
                    {
                        if (!int.TryParse(keyValue[1], out int freezerTemp))
                        {
                            return false;
                        }
                        freezerTemperature = freezerTemp;
                    }
                }
            }
            if (fridgeTemperature < -10 || fridgeTemperature > 10)
            {
                return false;
            }
            if (freezerTemperature < -20 || freezerTemperature > 0)
            {
                return false;
            }

            return true;
        }

    }

    public class Sensors: Device
    {

        public float currentReading;

        private static readonly List<string> ValidNames = new List<string>
        { "Motion sensor",
          "Glass break sensors",
          "Door and window sensors",
          "Garage door sensors",
          "Water leak sensors"
        };

        public Sensors(int id, string name, bool state, DateTime lastupdated, int devicelogid, string message):base(id, name,state, lastupdated, devicelogid, message ) {

        }


        public override bool Validate() {


            var values = DeviceActivity.loggedMessage.Split(',');
            foreach (var value in values)
            {
                var keyValue = value.Split('=');
                if (keyValue.Length == 2)
                {
                    if (keyValue[0].Trim() == "currentReading")
                    {
                        if (!float.TryParse(keyValue[1], out float currentRead))
                        {
                            return false;
                        }
                        currentReading = currentRead;
                    }
                }
                else
                {
                    return false;
                }
            }
            if (currentReading < 0 || currentReading > 100)
            {
                return false;
            }
            if (!ValidNames.Contains(Name))
            {
                return false;
            }

            return true;
        }

    }

    public class Alarm : Device
    {
        public bool IsActivated { get; set; }

        private static readonly List<string> ValidNames = new List<string>
    {
        "Fire/smoke detectors",
        "Carbon monoxide (CO) detectors",
        "Garage alarm",
        "Main house alarm system",
        "Panic buttons",
        "Flood/water alarm"
    };
        public Alarm(int id, string name, bool state, DateTime lastUpdated, int deviceLogId, string message) : base(id, name, state, lastUpdated, deviceLogId, message)
        {

        }

        public override bool Validate()
        {
            if (DeviceActivity.loggedMessage != "true" && DeviceActivity.loggedMessage != "false")
            {
                return false;
            }

            IsActivated = DeviceActivity.loggedMessage == "true";

            if (!ValidNames.Contains(Name))
            {
                return false;
            }

            return true;
        }
    }


    public class Tracker : Device
    {
        public bool IsTriggered { get; set; }

        private static readonly List<string> ValidNames = new List<string>
    {
        "Car GPS tracker",
        "Bike GPS tracker",
        "Pet GPS tracker"
    };

        public Tracker(int id, string name, bool state, DateTime lastUpdated, int deviceLogId, string message) : base(id, name, state, lastUpdated, deviceLogId, message)
        {

        }
        public override bool Validate()
        {
            if (DeviceActivity.loggedMessage != "true" && DeviceActivity.loggedMessage != "false")
            {
                return false;
            }

            IsTriggered = DeviceActivity.loggedMessage == "true";

            if (!ValidNames.Contains(Name))
            {
                return false;
            }

            return true;
        }
    }

    public class Thermostat : Device
    {
        public int ThermostatTemperature { get; set; }

        public Thermostat(int id, string name, bool state, DateTime lastUpdated, int deviceLogId, string message)
            : base(id, name, state, lastUpdated, deviceLogId, message) { }

        public override bool Validate()
        {
            var values = DeviceActivity.loggedMessage.Split(',');
            foreach (var value in values)
            {
                var keyValue = value.Split('=');
                if (keyValue.Length == 2)
                {
                    if (keyValue[0].Trim() == "ThermostatTemp")
                    {
                        if (!int.TryParse(keyValue[1], out int thermostatTemp))
                        {
                            return false;
                        }
                        ThermostatTemperature = thermostatTemp;
                    }
                }
                else
                {
                    return false;
                }
            }
            return ThermostatTemperature >= 0 && ThermostatTemperature <= 50;
        }
    }



    public class User
    {
        public int id { get; set; } // Primary key
        public string username { get; set; }
        public string password { get; set; }
       // removed email

        //removed the constructor
    }



}
