using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SmartHomeSystem
{
    // Base Device class
    public class Device
    {
        public int DeviceId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public DateTime LastUpdate { get; set; }

        protected Device() { }

        public virtual void UpdateStatus()
        {
            LastUpdate = DateTime.Now;
            Console.WriteLine($"{Name} status updated to {Status}.");
        }

        public virtual void Initialize(Dictionary<string, object> data)
        {
            if (data.ContainsKey("DeviceId")) DeviceId = Convert.ToInt32(data["DeviceId"]);
            if (data.ContainsKey("Name")) Name = data["Name"].ToString();
            if (data.ContainsKey("Status")) Status = data["Status"].ToString();
            LastUpdate = DateTime.Now;
        }
    }
    //derived class lock
    public class Lock : Device
    {
        public bool IsLocked { get; private set; }

        public void LockDevice() => ChangeLockState(true);
        public void UnlockDevice() => ChangeLockState(false);

        private void ChangeLockState(bool state)
        {
            IsLocked = state;
            Status = state ? "Locked" : "Unlocked";
            UpdateStatus();
            Console.WriteLine($"{Name} is now {Status.ToLower()}.");
        }

        public override void Initialize(Dictionary<string, object> data)
        {
            base.Initialize(data);
            if (data.ContainsKey("IsLocked")) IsLocked = Convert.ToBoolean(data["IsLocked"]);
            Console.WriteLine($"{Name} initialized as Lock.");
        }
    }


    //derived class camera
    public class Camera : Device
    {
        public bool IsRecording { get; private set; }
        public override void Initialize(Dictionary<string, object> data)
        {
            base.Initialize(data);
            if (data.ContainsKey("IsRecording")) IsRecording = Convert.ToBoolean(data["IsRecording"]);
            Console.WriteLine($"{Name} initialized as Camera.");
        }
    }

    //dervied class Sensor
    public class Sensor : Device
    {
        public float CurrentReading { get; private set; }
        public override void Initialize(Dictionary<string, object> data)
        {
            base.Initialize(data);
            if (data.ContainsKey("CurrentReading")) CurrentReading = Convert.ToSingle(data["CurrentReading"]);
            Console.WriteLine($"{Name} initialized as Sensor.");
        }
    }

    //derived class Alarm
    public class Alarm : Device
    {
        public bool IsTriggered { get; private set; }
        public override void Initialize(Dictionary<string, object> data)
        {
            base.Initialize(data);
            if (data.ContainsKey("IsTriggered")) IsTriggered = Convert.ToBoolean(data["IsTriggered"]);
            Console.WriteLine($"{Name} initialized as Alarm.");
        }
    }

    //derived class tracker
    public class Tracker : Device
    {
        public bool IsActive { get; private set; }
        public override void Initialize(Dictionary<string, object> data)
        {
            base.Initialize(data);
            if (data.ContainsKey("IsActive")) IsActive = Convert.ToBoolean(data["IsActive"]);
            Console.WriteLine($"{Name} initialized as Tracker.");
        }
    }

    public static class DeviceFactory
    {
        private static readonly Dictionary<string, Device> predefinedDevices = new()
        {
            // Predefined Locks
            { "Front Door Smart Lock", new Lock() },
            { "Garage Door Smart Lock", new Lock() },
            { "Back Door Smart Lock", new Lock() },
            { "Window Lock", new Lock() },
            { "Interior Door Lock (Mud Room)", new Lock() },
            { "Gate Lock (Fence Gate)", new Lock() },

            // Predefined Sensors
            { "Motion Sensor (Front Outdoor)", new Sensor() },
            { "Motion Sensor (Back Outdoor)", new Sensor() },
            { "Glass Break Sensor", new Sensor() },
            { "Door and Window Sensor", new Sensor() },
            { "Garage Door Sensor", new Sensor() },
            { "Water Leak Sensor", new Sensor() },

            // Predefined Cameras
            { "Front Doorbell Camera", new Camera() },
            { "Backyard Camera", new Camera() },
            { "Side Gate Camera", new Camera() },
            { "Garage Camera", new Camera() },
            { "Driveway Camera", new Camera() },

            // Predefined Alarms
            { "Fire/Smoke Detector", new Alarm() },
            { "Carbon Monoxide (CO) Detector", new Alarm() },
            { "Garage Alarm", new Alarm() },
            { "Main House Alarm System", new Alarm() },
            { "Panic Button", new Alarm() },
            { "Flood/Water Alarm", new Alarm() },

            // Predefined Trackers
            { "Car GPS Tracker", new Tracker() },
            { "Bike GPS Tracker", new Tracker() },
            { "Pet GPS Tracker", new Tracker() }
        };

        public static Device InitializeOrUpdateDeviceFromJson(string jsonData)
        {
            var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonData);

            if (!data.ContainsKey("Name"))
                throw new ArgumentException("Device 'Name' not specified in JSON data.");

            string deviceName = data["Name"].ToString();

            if (predefinedDevices.TryGetValue(deviceName, out var device))
            {
                device.Initialize(data);
                Console.WriteLine($"{deviceName} updated with new JSON data.");
                return device;
            }

            throw new NotSupportedException($"Device '{deviceName}' is not supported.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string jsonLock = @"{
                'Name': 'Front Door Smart Lock',
                'DeviceId': 1,
                'Status': 'Locked',
                'IsLocked': true
            }";

            string jsonCamera = @"{
                'Name': 'Backyard Camera',
                'DeviceId': 2,
                'Status': 'Recording',
                'IsRecording': true
            }";
            string jsonSensor = @"{
                'Name': 'Motion Sensor (Front Outdoor)',
                'DeviceId': 3,
                'Status': 'Active',
                'CurrentReading': 50.5
             }";
            DeviceFactory.InitializeOrUpdateDeviceFromJson(jsonLock);
            DeviceFactory.InitializeOrUpdateDeviceFromJson(jsonCamera);
            DeviceFactory.InitializeOrUpdateDeviceFromJson(jsonSensor);
        }
    }
}
