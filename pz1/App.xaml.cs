using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using pz1.ViewModels;
using Unity;

namespace pz1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IUnityContainer container = new UnityContainer();

            var customXmlRW = new DataBase(@"C:\Users\stefan\Desktop\db.xml");

            // Register ViewModels
            var loginViewModel = new LoginViewModel(customXmlRW);
            container.RegisterInstance<LoginViewModel>(loginViewModel);
            var showImagesViewModel = new ImagesViewModel();
            container.RegisterType<ImagesViewModel>();
            var addImageViewModel = new AddImageViewModel();
            container.RegisterType<AddImageViewModel>();
            var accountDetailsViewModel = new AccountDetailsViewModel();
            container.RegisterType<AccountDetailsViewModel>();
            var contentViewModel = new ContentViewModel(container, customXmlRW);
            container.RegisterInstance<ContentViewModel>(contentViewModel);
            var mainWindowViewModel = new MainWindowViewModel(container);
            container.RegisterInstance<MainWindowViewModel>(mainWindowViewModel);

            var mainWindow = container.Resolve<MainWindow>();
            mainWindow.DataContext = container.Resolve<MainWindowViewModel>();
            Application.Current.MainWindow = mainWindow;
            Application.Current.MainWindow.Show();
        }
    }
}
