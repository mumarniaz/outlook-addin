using CommonServiceLocator;
using EmailSaveAddin.Services;
using GalaSoft.MvvmLight.Ioc;

namespace EmailSaveAddin.ViewModel
{
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            //TODO: Register dummy service. Replace it with acutal service
            SimpleIoc.Default.Register<IApiService, DummyApiService>();

            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<SaveEmailViewModel>();
            SimpleIoc.Default.Register<MainViewModel>();
            
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public LoginViewModel Login
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LoginViewModel>();
            }
        }
        
        public SaveEmailViewModel SaveEmail
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SaveEmailViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}