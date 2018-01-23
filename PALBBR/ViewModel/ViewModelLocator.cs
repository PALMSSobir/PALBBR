using Autofac;
using Autofac.Extras.CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using PALBBR.ViewModel;
using System.ComponentModel;

namespace PALBBR
{

    //public class ViewModelLocator
    //{
    //    /// <summary>
    //    /// Initializes a new instance of the ViewModelLocator class.
    //    /// </summary>
    //    public ViewModelLocator()
    //    {
    //        ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

    //        SimpleIoc.Default.Register<MainViewModel>();
    //    }

    //    public MainViewModel Main
    //    {
    //        get
    //        {
    //            return ServiceLocator.Current.GetInstance<MainViewModel>();
    //        }
    //    }
        
    //    public static void Cleanup()
    //    {
    //        // TODO Clear the ViewModels
    //    }
    //}

    public class ViewModelLocator
    {
        private static Autofac.IContainer Container { get; set; }

        public MainViewModel Main => Container.Resolve<MainViewModel>();

        public ViewModelLocator()
        {
            var builder = new ContainerBuilder();

            RegisterServices(builder);
            RegisterViewModels(builder);

            Container = builder.Build();

            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(Container));
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }

        private void RegisterServices(ContainerBuilder builder)
        {
        }

        private static void RegisterViewModels(ContainerBuilder builder)
        {
            builder.RegisterType<MainViewModel>().SingleInstance();
        }
    }
}