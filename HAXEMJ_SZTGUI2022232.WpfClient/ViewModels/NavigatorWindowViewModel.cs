using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using HAXEMJ_SZTGUI2022232.WpfClient.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HAXEMJ_SZTGUI2022232.WpfClient.ViewModels
{
    public class NavigatorWindowViewModel : ObservableRecipient
    {
        ITableManagerService? service;
        public ICommand OpenPhoneTable { get; set; }
        public ICommand OpenManufacturerTable { get; set; }
        public ICommand OpenUserTable { get; set; }
        public ICommand OpenNONCrudMenu { get; set; }

        public NavigatorWindowViewModel() : this(Ioc.Default.GetService<ITableManagerService>()) { }

        public NavigatorWindowViewModel(ITableManagerService? service) 
        { 
            this.service = service;

            OpenPhoneTable = new RelayCommand(
                () => service.Open(0)
            );

            OpenManufacturerTable = new RelayCommand(
                () => service.Open(1)
            );

            OpenUserTable = new RelayCommand(
                () => service.Open(2)
            );

            OpenNONCrudMenu = new RelayCommand(() => service.Open(3));
        }
    }
}
