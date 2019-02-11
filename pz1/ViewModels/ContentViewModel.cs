using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pz1.Commands;
using pz1.EventArgs;
using Unity;

namespace pz1.ViewModels
{
    public class ContentViewModel : BindableBase
    {
        public string CurrentUsername { get; set; }
        public string CurrentPassword { get; set; }

        private IUnityContainer _container;
        private DataBase _dataBase;

        private ImagesViewModel _showImagesViewModel;
        private AddImageViewModel _addImageViewModel;
        private AccountDetailsViewModel _accountDetailsViewModel;
        private BindableBase _currentContentViewModel;

        public Command<string> NavCommand { get; private set; }
        public Command LogoutCommand { get; private set; }

        public delegate void LogoutEventHandler(object sender, System.EventArgs args);
        public event LogoutEventHandler LoggedOut;

        public ContentViewModel() { }

        public ContentViewModel(IUnityContainer container, DataBase db)
        {
            _container = container;

            _dataBase = db;

            _showImagesViewModel = _container.Resolve<ImagesViewModel>();
            _addImageViewModel = _container.Resolve<AddImageViewModel>();
            _accountDetailsViewModel = _container.Resolve<AccountDetailsViewModel>();

            CurrentContentViewModel = _showImagesViewModel;

            NavCommand = new Command<string>(OnNav);
            LogoutCommand = new Command(OnLoggingOut);
        }

        public BindableBase CurrentContentViewModel
        {
            get => _currentContentViewModel;
            set => SetField(ref _currentContentViewModel, value);
        }

        private void OnNav(string destination)
        {
            switch (destination)
            {
                case "showImages":
                    CurrentContentViewModel = _showImagesViewModel;
                    break;
                case "addImage":
                    CurrentContentViewModel = _addImageViewModel;
                    break;
                case "accountDetails":
                    CurrentContentViewModel = _accountDetailsViewModel;
                    break;
            }
        }

        private void OnLoggingOut()
        {
            LoggedOut?.Invoke(this, System.EventArgs.Empty);
        }

        // Subscription to Registered event on MainWindowViewModel
        public void OnRegistered(object source, RegistrationEventArgs e)
        {
            CurrentContentViewModel = _addImageViewModel;
            CurrentUsername = e.Username;
            CurrentPassword = e.Password;
        }
    }
}
