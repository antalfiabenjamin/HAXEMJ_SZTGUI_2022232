using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using HAXEMJ_HFT_2022231.Models;
using HAXEMJ_SZTGUI2022232.WpfClient.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HAXEMJ_SZTGUI2022232.WpfClient.ViewModels
{
    public class NonCrudWindowViewModel : ObservableRecipient
    {
        static RestService rest;
        public ObservableCollection<string> Data { get; set; }

        public ICommand GetAlliPhones { get; set; }
        public ICommand GetPreferredColorPhones { get; set; }
        public ICommand GetPhoneCountByUser { get; set; }
        public ICommand PhonesBySpecificLocation { get; set; }
        public ICommand GetScreenTimeBd { get; set; }

        INonCrudStringService service;

        private string inputText;

        public string InputText
        {
            get { return inputText; }
            set { SetProperty(ref inputText, value); }
        }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public NonCrudWindowViewModel() : this(IsInDesignMode ? null : Ioc.Default.GetService<INonCrudStringService>()) { }

        public NonCrudWindowViewModel(INonCrudStringService service)
        {
            if (!IsInDesignMode)
            {
                this.service = service;

                rest = new RestService("http://localhost:29971/", "phone");
                Data = new ObservableCollection<string>();

                GetAlliPhones = new RelayCommand(
                    () =>
                    {
                        Data.Clear();
                        foreach (var item in service.GetAlliPhones(rest.Get<Phone>("NonCrud/GetAlliPhones")))
                        {
                            Data.Add(item);
                        }
                    }
                );

                GetPreferredColorPhones = new RelayCommand(
                    () =>
                    {
                        Data.Clear();
                        var restData = new List<Manufacturer>();
                        try
                        {
                            restData = rest.Get<Manufacturer>($"NonCrud/GetPreferredColorPhones/{InputText}");
                        } catch (ArgumentException ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                        foreach (var item in service.GetPreferredColorPhones(restData))
                        {
                            Data.Add(item);
                        }
                    }
                );

                GetPhoneCountByUser = new RelayCommand(
                    () =>
                    {
                        Data.Clear();
                        foreach (var item in service.GetPhoneCountByUser(rest.Get<User>("NonCrud/GetPhoneCountByUser")))
                        {
                            Data.Add(item);
                        }
                    }
                );

                GetScreenTimeBd = new RelayCommand(
                    () =>
                    {
                        Data.Clear();
                        foreach (var item in service.GetScreenTimeBd(rest.Get<User>("NonCrud/GetScreenTimeBd")))
                        {
                            Data.Add(item);
                        }
                    }
                );

                PhonesBySpecificLocation = new RelayCommand(
                    () =>
                    {
                        Data.Clear();
                        var restData = new List<Phone>();
                        try
                        {
                            restData = rest.Get<Phone>($"NonCrud/PhonesBySpecificLocation/{InputText}");
                        }
                        catch (ArgumentException ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }


                        foreach (var item in service.GetPhonesBySpecificLocation(restData))
                        {
                            Data.Add(item);
                        }
                    }
                );
            }
        }
    }
}
