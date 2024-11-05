using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
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

    // Derived class: Lock
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

    // Derived class: Camera
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

    // Derived class: Sensor
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

    // Derived class: Alarm
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

    // Derived class: Tracker
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
        public static string connectionString = "Data Source=sample.db;Version=3;";

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

            if (!data.ContainsKey("UserId"))
                throw new ArgumentException("User 'UserId' not specified in JSON data.");

            int userId = Convert.ToInt32(data["UserId"]);
            string deviceName = data["Name"].ToString();

            if (!ValidateUser(userId))
            {
                Console.WriteLine("User not authenticated, cannot update device.");
                return null;
            }

            if (predefinedDevices.TryGetValue(deviceName, out var device))
            {
                device.Initialize(data);
                InsertOrUpdateDeviceInDatabase(device);
                Console.WriteLine($"{deviceName} updated with new JSON data.");
                return device;
            }

            throw new NotSupportedException($"Device '{deviceName}' is not supported.");
        }

        private static bool ValidateUser(int userId)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string selectQuery = "SELECT COUNT(1) FROM Users WHERE Id = @Id";
                var cmd = new SQLiteCommand(selectQuery, conn);
                cmd.Parameters.AddWithValue("@Id", userId);

                int userExists = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
                return userExists > 0;
            }
        }

        private static void InsertOrUpdateDeviceInDatabase(Device device)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    INSERT OR REPLACE INTO Devices (DeviceId, Name, Status, LastUpdate, IsLocked, IsRecording, CurrentReading, IsTriggered, IsActive)
                    VALUES (@DeviceId, @Name, @Status, @LastUpdate, @IsLocked, @IsRecording, @CurrentReading, @IsTriggered, @IsActive);";
                var cmd = new SQLiteCommand(query, conn);
                cmd.Parameters.AddWithValue("@DeviceId", device.DeviceId);
                cmd.Parameters.AddWithValue("@Name", device.Name);
                cmd.Parameters.AddWithValue("@Status", device.Status);
                cmd.Parameters.AddWithValue("@LastUpdate", device.LastUpdate);
                cmd.Parameters.AddWithValue("@IsLocked", device is Lock l ? (object)l.IsLocked : DBNull.Value);
                cmd.Parameters.AddWithValue("@IsRecording", device is Camera c ? (object)c.IsRecording : DBNull.Value);
                cmd.Parameters.AddWithValue("@CurrentReading", device is Sensor s ? (object)s.CurrentReading : DBNull.Value);
                cmd.Parameters.AddWithValue("@IsTriggered", device is Alarm a ? (object)a.IsTriggered : DBNull.Value);
                cmd.Parameters.AddWithValue("@IsActive", device is Tracker t ? (object)t.IsActive : DBNull.Value);
                cmd.ExecuteNonQuery();

                conn.Close();
                Console.WriteLine($"Device '{device.Name}' saved to database.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            CreateDatabase();

            InsertUser(1, "1", "fds", "fsdfds@gmail.com", "fwwefwef");
            InsertUser(2, "1", "dqw", "ewfrewrwr@gmail.com", "f34tykdwdas");

            string jsonCamera = @"{
                'UserId': 2,
                'Name': 'Backyard Camera',
                'DeviceId': 2,
                'Status': 'Recording',
                'IsRecording': true
            }";

            string jsonLock = @"{
                'UserId': 1,
                'Name': 'Front Door Smart Lock',
                'DeviceId': 1,
                'Status': 'Locked',
                'IsLocked': true
            }";

            DeviceFactory.InitializeOrUpdateDeviceFromJson(jsonCamera);
            DeviceFactory.InitializeOrUpdateDeviceFromJson(jsonLock);
        }

        static void CreateDatabase()
        {
            using (SQLiteConnection conn = new SQLiteConnection(DeviceFactory.connectionString))
            {
                conn.Open();

                string dropDevicesTableQuery = "DROP TABLE IF EXISTS Devices;";
                new SQLiteCommand(dropDevicesTableQuery, conn).ExecuteNonQuery();

                string createUsersTableQuery = @"
            CREATE TABLE IF NOT EXISTS Users (
                Id INTEGER PRIMARY KEY,
                FirstName TEXT,
                LastName TEXT,
                Email TEXT,
                Password TEXT
            );";
                new SQLiteCommand(createUsersTableQuery, conn).ExecuteNonQuery();

                string createDevicesTableQuery = @"
            CREATE TABLE IF NOT EXISTS Devices (
                DeviceId INTEGER PRIMARY KEY,
                Name TEXT,
                Status TEXT,
                LastUpdate DATETIME,
                IsLocked BOOLEAN,
                IsRecording BOOLEAN,
                CurrentReading FLOAT,
                IsTriggered BOOLEAN,
                IsActive BOOLEAN
            );";
                new SQLiteCommand(createDevicesTableQuery, conn).ExecuteNonQuery();

                conn.Close();
                Console.WriteLine("Database created and tables initialized.");
            }
        }


        static void InsertUser(int id, string firstName, string lastName, string email, string password)
        {
            using (SQLiteConnection conn = new SQLiteConnection(DeviceFactory.connectionString))
            {
                conn.Open();
                string checkQuery = "SELECT COUNT(1) FROM Users WHERE Id = @Id";
                var checkCmd = new SQLiteCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@Id", id);
                int userExists = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (userExists > 0)
                {
                    Console.WriteLine($"User with Id {id} already exists. Skipping insertion.");
                }
                else
                {
                    string insertQuery = "INSERT INTO Users (Id, FirstName, LastName, Email, Password) VALUES (@Id, @FirstName, @LastName, @Email, @Password)";
                    var cmd = new SQLiteCommand(insertQuery, conn);
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@FirstName", firstName);
                    cmd.Parameters.AddWithValue("@LastName", lastName);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine($"Inserted user {firstName} {lastName}.");
                }

                conn.Close();
            }
        }

    }
}
