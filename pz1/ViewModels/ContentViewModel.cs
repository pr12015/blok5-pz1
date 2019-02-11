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

        private ImagesViewModel _ImagesViewModel;
        private AddImageViewModel _addImageViewModel;
        private AccountDetailsViewModel _accountDetailsViewModel;
        private BindableBase _currentContentViewModel;

        public Command<string> NavCommand { get; private set; }

        public ContentViewModel() { }

        public ContentViewModel(IUnityContainer container, DataBase db)
        {
            _container = container;

            _dataBase = db;

            _ImagesViewModel = _container.Resolve<ImagesViewModel>();
            _addImageViewModel = _container.Resolve<AddImageViewModel>();
            _accountDetailsViewModel = _container.Resolve<AccountDetailsViewModel>();

            CurrentContentViewModel = _ImagesViewModel;

            NavCommand = new Command<string>(OnNav);
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
                    CurrentContentViewModel = _ImagesViewModel;
                    break;
                case "addImage":
                    CurrentContentViewModel = _addImageViewModel;
                    break;
                case "accountDetails":
                    CurrentContentViewModel = _accountDetailsViewModel;
                    break;
            }
        }


        // Subscription to Registered event on MainWindowViewModel
        public void OnRegistration(object source, RegistrationEventArgs e)
        {
            CurrentContentViewModel = _addImageViewModel;
            CurrentUsername = e.Username;
            CurrentPassword = e.Password;
        }
    }
}
