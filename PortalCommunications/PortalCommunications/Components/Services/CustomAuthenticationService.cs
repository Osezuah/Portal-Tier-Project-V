using System.Security.Claims;

namespace PortalCommunications.Components.Services
{
    public class CustomAuthenticationService
    {
        public event Action<ClaimsPrincipal>? UserChanged;
        private ClaimsPrincipal? currentUser;



        public ClaimsPrincipal CurrentUser
        {
            get { return currentUser ?? new(); }
            set
            {
                currentUser = value;
                Console.WriteLine("UserChanged event triggered"); // Debug message
                UserChanged?.Invoke(currentUser);
            }
        }
    }
}
