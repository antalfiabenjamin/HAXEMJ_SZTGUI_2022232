using CommunityToolkit.Mvvm.DependencyInjection;
using HAXEMJ_SZTGUI2022232.WpfClient.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HAXEMJ_SZTGUI2022232.WpfClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Ioc.Default.ConfigureServices(
                new ServiceCollection()
                    .AddSingleton<ITableManagerService, TableManagerWindow>()
                    .BuildServiceProvider()
                );
        }
    }
}
