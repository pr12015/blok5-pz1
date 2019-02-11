using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pz1.Commands;
using pz1.EventArgs;
using pz1.Models;

namespace pz1.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        private DataBase _dataBase;

        private string _username;
        private string _password;

        public string Username
        {
            get => _username;
            set => SetField<string>(ref _username, value);
        }

        public string Password
        {
            get => _password;
            set => SetField<string>(ref _password, value);
        }

        public List<User> Users { get; set; }

        public Command LoginCommand { get; private set; }

        public delegate void LoginEventHandler(object source, LoginEventArgs args);

        public event LoginEventHandler Login;

        public Command RegisterCommand { get; private set; }

        public delegate void RegisterEventHandler(object source, RegistrationEventArgs args);

        public event RegisterEventHandler Registration;

        public LoginViewModel() { }

        public LoginViewModel(DataBase db)
        {
            _dataBase = db;

            var users = _dataBase.Read();
            if (users == null)
            {
                Users = new List<User>();
                db.Write(Users);
            }

            LoginCommand = new Command(OnLogin);
            RegisterCommand = new Command(OnRegistration);
        }

        protected virtual void OnLogin()
        {
            var loginUser = new User(Username, Password);
            if (CheckIfExists(loginUser))
            {
                Login?.Invoke(this, new LoginEventArgs(Username, Password));
            }
        }

        protected virtual void OnRegistration()
        {
            var user = new User(Username, Password);
            if (CheckIfExists(user)) return;

            Users.Add(user);
            _dataBase.Write(Users);
            Registration?.Invoke(this, new RegistrationEventArgs(Username, Password));
        }

        private bool CheckIfExists(User user)
        {
            if (string.IsNullOrWhiteSpace(user.UserName)) return false;
            if (Users.Count == 0) return false;

            foreach (var u in Users)
            {
                if (u.UserName == user.UserName)
                    return true;
            }
            
            return false;
        }
    }
}
