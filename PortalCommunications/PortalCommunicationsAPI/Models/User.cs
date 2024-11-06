namespace PortalCommunicationsAPI.Models
{
    //business layer user model  (interpretation)
    public class User
    {
        public int Id { get; set; } //primary key
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        //need to add some sort of list of all the users devices related to them here somehow (device group id or something)
    }
}
