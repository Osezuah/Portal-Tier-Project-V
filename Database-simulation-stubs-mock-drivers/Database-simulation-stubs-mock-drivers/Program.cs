using System;
using System.Data.SQLite;
using System.IO;

class Program
{
    static string connectionString = "Data Source=sample.db;Version=3;";

    static void Main(string[] args)
    {
        string dbFile = "sample.db";
        //this was done to fix the error
        if (File.Exists(dbFile))
        {
            File.Delete(dbFile);
        }

        CreateDatabase();


        InsertUser(1, "aef", "qwer", "aef.aef@fakeemail.com", "password1234");
        InsertUser(2, "adsf", "ewqe", "adsf.adsf@fakeemail.com", "password1234");
        InsertUser(3, "wqe", "aaadef", "wqe.wqe@fakeemail.com", "password1234");

        Camera camera1 = new Camera(101, "FrontDoor Camera", "Active", DateTime.Now, true);
        Camera camera2 = new Camera(102, "Bell Camera", "Inactive", DateTime.Now, false);

        InsertDeviceData(camera1);
        InsertDeviceData(camera2);

        AuthenticateAndUpdate(2, 101, "Inactive", true); //simulating a change request coming to api


        Console.ReadKey();
    }
    static void AuthenticateAndUpdate(int userId, int deviceId, string status, bool isRecording)
    {
        bool found = false;

        using (SQLiteConnection conn = new SQLiteConnection(connectionString))
        {
            conn.Open();


            string selectQuery = "SELECT * FROM Users WHERE Id = @UserId";
            SQLiteCommand cmd = new SQLiteCommand(selectQuery, conn);
            cmd.Parameters.AddWithValue("@UserId", userId);

            SQLiteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                found = true;
            }
            reader.Close();

            if (found)
            {
                UpdateDeviceData(conn, deviceId, status, isRecording);
            }
            else
            {
                Console.WriteLine("User not found adn therefore cannot update device.");
            }

            conn.Close();
        }
    }
    static void UpdateDeviceData(SQLiteConnection conn, int deviceId, string status, bool isRecording)
    {
        string updateQuery = "UPDATE Camera SET Status = @Status, LastUpdate = @LastUpdate, IsRecording = @IsRecording WHERE DeviceId = @DeviceId";
        SQLiteCommand cmd = new SQLiteCommand(updateQuery, conn);

        cmd.Parameters.AddWithValue("@Status", status);
        cmd.Parameters.AddWithValue("@LastUpdate", DateTime.Now);
        cmd.Parameters.AddWithValue("@IsRecording", isRecording);
        cmd.Parameters.AddWithValue("@DeviceId", deviceId);

        int ifrowsAffected = cmd.ExecuteNonQuery();

        if (ifrowsAffected > 0)
        {
            Console.WriteLine($"Device {deviceId} updated successfully.");
        }
        else
        {
            Console.WriteLine($"Device {deviceId} not found.");
        }
    }
    static void CreateDatabase()
    {
        using (SQLiteConnection conn = new SQLiteConnection(connectionString))
        {
            conn.Open();

            string createUsersTableQuery = @"
                CREATE TABLE IF NOT EXISTS Users (
                    Id INTEGER PRIMARY KEY,
                    FirstName TEXT NOT NULL,
                    LastName TEXT NOT NULL,
                    Email TEXT NOT NULL,
                    Password TEXT NOT NULL
                )";
            SQLiteCommand cmd = new SQLiteCommand(createUsersTableQuery, conn);
            cmd.ExecuteNonQuery();

            string createDevicesTableQuery = @"
                CREATE TABLE IF NOT EXISTS Camera (
                    DeviceId INTEGER PRIMARY KEY,
                    Name TEXT NOT NULL,
                    Status TEXT NOT NULL,
                    LastUpdate DATETIME NOT NULL,
                    IsRecording BOOLEAN
                )";
            cmd = new SQLiteCommand(createDevicesTableQuery, conn);
            cmd.ExecuteNonQuery();

            conn.Close();
        }
        Console.WriteLine("Database and tables created (if not already exist).");
    }

    static void InsertUser(int id, string firstName, string lastName, string email, string password)
    {
        using (SQLiteConnection conn = new SQLiteConnection(connectionString))
        {
            conn.Open();
            string insertQuery = "INSERT INTO Users (Id, FirstName, LastName, Email, Password) VALUES (@Id, @FirstName, @LastName, @Email, @Password)";
            SQLiteCommand cmd = new SQLiteCommand(insertQuery, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@FirstName", firstName);
            cmd.Parameters.AddWithValue("@LastName", lastName);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Password", password);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        Console.WriteLine($"Inserted user {firstName} {lastName} into the Users table.");
    }

    static void InsertDeviceData(Device device)
    {
        using (SQLiteConnection conn = new SQLiteConnection(connectionString))
        {
            conn.Open();
            string insertQuery = "INSERT INTO Camera (DeviceId, Name, Status, LastUpdate, IsRecording) VALUES (@DeviceId, @Name, @Status, @LastUpdate, @IsRecording)";
            SQLiteCommand cmd = new SQLiteCommand(insertQuery, conn);
            cmd.Parameters.AddWithValue("@DeviceId", device.DeviceId);
            cmd.Parameters.AddWithValue("@Name", device.Name);
            cmd.Parameters.AddWithValue("@Status", device.Status);
            cmd.Parameters.AddWithValue("@LastUpdate", device.LastUpdate);
            cmd.Parameters.AddWithValue("@IsRecording", device is Camera camera ? camera.IsRecording : (object)DBNull.Value);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        Console.WriteLine($"Inserted the device {device.Name} into the Devices table.");
    }
}

public class Device
{
    public int DeviceId { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
    public DateTime LastUpdate { get; set; }

    public Device(int deviceId, string name, string status, DateTime lastUpdate)
    {
        DeviceId = deviceId;
        Name = name;
        Status = status;
        LastUpdate = lastUpdate;
    }
}
public class Camera : Device
{
    public bool IsRecording { get; set; }

    public Camera(int deviceId, string name, string status, DateTime lastUpdate, bool isRecording)
        : base(deviceId, name, status, lastUpdate)
    {
        IsRecording = isRecording;
    }
}
