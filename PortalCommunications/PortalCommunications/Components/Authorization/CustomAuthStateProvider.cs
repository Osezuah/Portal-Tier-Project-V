using Microsoft.AspNetCore.Components.Authorization;
using PortalCommunications.Components.Services;
using System.Security.Claims;

namespace PortalCommunications.Components.Authorization
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private AuthenticationState authenticationState;

        public CustomAuthStateProvider(CustomAuthenticationService service)
        {
            authenticationState = new AuthenticationState(service.CurrentUser);


            service.UserChanged += (newUser) =>
            {
                authenticationState = new AuthenticationState(newUser);
                NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));
            };
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync() =>
            Task.FromResult(authenticationState);
    }
}
