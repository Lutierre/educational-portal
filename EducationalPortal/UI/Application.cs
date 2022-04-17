using UI.Abstractions.Interfaces;

namespace EducationalPortal
{
    public class Application : IApplication
    {
        private readonly IAuthorizationView _authorizationView;

        public Application(IAuthorizationView authorizationView)
        {
            _authorizationView = authorizationView;
        }

        public void Start()
        {
            _authorizationView.Start();
        }
    }
}
