using System.Diagnostics;
using AvaApp1.Services;
using AvaApp1.ViewModels;
using AvaApp1.Views;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Navigation.Regions;

namespace AvaApp1
{
    public partial class App : PrismApplication
    {

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);

            // Required when overriding Initialize
            base.Initialize();
        }
        protected override AvaloniaObject CreateShell()
        {
            Debug.WriteLine("CreateShell()");
            var it = Container.Resolve<MainWindow>();
            var top = TopLevel.GetTopLevel(it);
            IContainerRegistry Registry = (IContainerRegistry)this.Container;
            Registry.RegisterInstance<TopLevel?>(top);

            //IContainerProvider pyd = this.Container;
            //IContainerRegistry reg = (IContainerRegistry)pyd;
            //DryIoc.IContainer con1 = (DryIoc.IContainer)pyd;// 失败
            //DryIoc.IContainer con2 = (DryIoc.IContainer)reg;// 失败
            //DryIoc.IContainer con3 = pyd.GetContainer();
            //DryIoc.IContainer con4 = reg.GetContainer();

            return it;
        }

        public override void OnFrameworkInitializationCompleted()
        {

            base.OnFrameworkInitializationCompleted();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            Debug.WriteLine("RegisterTypes()");

            // Note:
            // SidebarView isn't listed, note we're using `AutoWireViewModel` in the View's AXAML.
            // See the line, `prism:ViewModelLocator.AutoWireViewModel="True"`

            // Services
            containerRegistry.RegisterSingleton<INotificationService, NotificationService>();



            // Views - Region Navigation
            containerRegistry.RegisterForNavigation<DashboardView, DashboardViewModel>();
            containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();
            containerRegistry.RegisterForNavigation<SettingsSubView, SettingsSubViewModel>();
            containerRegistry.RegisterForNavigation<LightBeltViewer, LightBeltViewerViewModel>();

            containerRegistry.RegisterForNavigation<TitleBarRightContent, TitleBarRightContentViewModel>();

            // Dialogs, etc.
        }

        /// <summary>Register optional modules in the catalog.</summary>
        /// <param name="moduleCatalog">Module Catalog.</param>
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            Debug.WriteLine("ConfigureModuleCatalog()");
            base.ConfigureModuleCatalog(moduleCatalog);
        }

        /// <summary>Called after Initialize.</summary>
        protected override void OnInitialized()
        {
            Debug.WriteLine("OnInitialized()");

            // Register Views to the Region it will appear in. Don't register them in the ViewModel.
            var regionManager = Container.Resolve<IRegionManager>();

            // WARNING: Prism v11.0.0-prev4
            // - DataTemplates MUST define a DataType or else an XAML error will be thrown
            // - Error: DataTemplate inside of DataTemplates must have a DataType set
            regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(DashboardView));
            regionManager.RegisterViewWithRegion(RegionNames.SidebarRegion, typeof(SidebarView));

            ////regionManager.RegisterViewWithRegion(RegionNames.DynamicSettingsListRegion, typeof(Setting1View));
            ////regionManager.RegisterViewWithRegion(RegionNames.DynamicSettingsListRegion, typeof(Setting2View));
        }

        /// <summary>Custom region adapter mappings.</summary>
        /// <param name="regionAdapterMappings">Region Adapters.</param>
        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            Debug.WriteLine("ConfigureRegionAdapterMappings()");
            regionAdapterMappings.RegisterMapping<ContentControl, ContentControlRegionAdapter>();
        }
    }
}
