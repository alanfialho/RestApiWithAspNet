namespace DatabaseIntegration.ViewModels
{
    public class UserViewModel
    {
        
        public string Login { get; }
        public string Password { get; }

        public UserViewModel(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}
