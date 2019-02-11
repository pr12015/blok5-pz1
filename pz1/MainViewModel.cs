using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pz1.EventArgs;
using pz1.ViewModels;
using Unity;

namespace pz1
{
    public class MainWindowViewModel : BindableBase
    {
        private IUnityContainer _container;
        private LoginViewModel _loginViewModel;
        private ContentViewModel _contentViewModel;
        private BindableBase _currentViewModel;

        public delegate void RegisterEventHandler(object source, RegistrationEventArgs args);
        public event RegisterEventHandler Registered;

        public MainWindowViewModel() { }

        public MainWindowViewModel(IUnityContainer container)
        {
            _container = container;

            _loginViewModel = _container.Resolve<LoginViewModel>();
            _loginViewModel.Login += OnLogin;
            _loginViewModel.Registration += OnRegistering;

            _contentViewModel = _container.Resolve<ContentViewModel>();

            Registered += _contentViewModel.OnRegistration;

            CurrentViewModel = _loginViewModel;
        }

        public BindableBase CurrentViewModel
        {
            get => _currentViewModel;
            set => SetField(ref _currentViewModel, value);
        }

        // Subscription to Logging in event from LoginViewModel
        private void OnLogin(object source, LoginEventArgs e)
        {
            CurrentViewModel = _contentViewModel;
        }

        // Subscription to Registering event from LoginViewModel
        private void OnRegistering(object source, RegistrationEventArgs e)
        {
            CurrentViewModel = _contentViewModel;
            OnRegistration(e);
        }

        // Fire an event that ContentViewModel listens for
        // so it can change the CurrentContentViewModel to AddImageViewModel
        private void OnRegistration(RegistrationEventArgs e)
        {
            Registered?.Invoke(this, e);
        }
    }
}
