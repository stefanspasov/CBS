namespace CBS.Logic.Unity
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using CBS.DAL.Repositories;
    using CBS.DAL.Repositories.Decorators;
    using CBS.DAL.Repositories.Decorators.Interfaces;
    using CBS.Logic.Factories;
    using CBS.Logic.Handlers;
    using CBS.Logic.Handlers.Implementations;
    using CBS.Logic.Loggers;
    using CBS.Logic.Loggers.Implementations;
    using CBS.Logic.Mappers;
    using CBS.Logic.Mappers.Implementations;

    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.Configuration;

    public static class UnityContainerFactory
    {
        public static IUnityContainer Create()
        {
            var unityContainer = new UnityContainer();

            unityContainer.RegisterType<ILogger, DummyLogger>(new HierarchicalLifetimeManager());
            unityContainer.RegisterType<IReservableFactory, ReservableFactory>(new HierarchicalLifetimeManager());
            unityContainer.RegisterType<IReservationMapper, ReservationMapper>(new HierarchicalLifetimeManager());
            unityContainer.RegisterType<IReservationHandler, ReservationHandler>(new HierarchicalLifetimeManager());

            LoadAssembliesFromUnitySectionAndRegisterTypes(unityContainer);

            Func<IUnityContainer, object> reservationSettingChain = container =>
            new SettingRepositoryCacheDecorator(container.Resolve<IExternalSettingRepository>());
            unityContainer.RegisterType<ISettingRepository>(new InjectionFactory(reservationSettingChain));

            Func<IUnityContainer, object> reservationRepositoryChain = container =>
              new ReservationRepositoryLoggingDecorator(container.Resolve<ILogger>(), container.Resolve<IExternalReservationRepository>());
            unityContainer.RegisterType<IReservationRepository>(new InjectionFactory(reservationRepositoryChain));

            return unityContainer;
        }

        private static void LoadAssembliesFromUnitySectionAndRegisterTypes(IUnityContainer uc)
        {
            var section = ConfigurationManager.GetSection(UnityConfigurationSection.SectionName) as UnityConfigurationSection;
            if (section == null)
            {
                throw new ConfigurationErrorsException("Unable to locate Unity configuration section.");
            }

            foreach (var assemblyName in section.Assemblies.Select(element => element.Name))
            {
                try
                {
                    // TODO Extension point: Add a separate folder for external assemblies 
                    var solutionPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\"));
                    var assembly = Assembly.LoadFrom($"{solutionPath}{assemblyName}\\bin\\Debug\\{assemblyName}.dll");
                    RegisterTypesFromExternalAssembly(uc, assembly);
                }
                catch (Exception ex)
                {
                    throw new ConfigurationErrorsException("Unable to load required assembly specified in Unity configuration section.  Assembly: " + assemblyName, ex);
                }
            }
        }

        private static void RegisterTypesFromExternalAssembly(IUnityContainer uc, Assembly assembly)
        {
            try
            {
                var reservationTypes = from t in assembly.GetExportedTypes()
                            where typeof(IExternalReservationRepository).IsAssignableFrom(t)
                            select t;
                foreach (var t in reservationTypes)
                    uc.RegisterType(typeof(IExternalReservationRepository), t);

                var settingTypes = from t in assembly.GetExportedTypes()
                             where typeof(IExternalSettingRepository).IsAssignableFrom(t)
                             select t;
                foreach (var t in settingTypes)
                    uc.RegisterType(typeof(IExternalSettingRepository), t);
            }
            catch (Exception)
            {
                throw new ConfigurationErrorsException("Failed to register types from external assembly" + assembly?.FullName);
            }
        }
    }
}