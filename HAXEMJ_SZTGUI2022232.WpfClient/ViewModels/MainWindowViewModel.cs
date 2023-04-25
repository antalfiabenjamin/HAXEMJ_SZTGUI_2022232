using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HAXEMJ_HFT_2022231.Models;
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
    public class MainWindowViewModel : ObservableRecipient
    {
        public RestCollection<Phone> Phones { get; set; }
        private Phone selectedPhone;

        public Phone SelectedPhone
        {
            get { return selectedPhone; }
            set
            {
                if (value != null)
                {
                    selectedPhone = new Phone()
                    {
                        Name = value.Name,
                        PhoneId = value.PhoneId,
                    };
                    OnPropertyChanged();
                    (DeletePhoneCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        private string selectedTable;

        public string SelectedTable
        {
            get { return selectedTable; }
            set { SetProperty(ref selectedTable, value); }
        }


        public ICommand CreatePhoneCommand { get; set; }
        public ICommand DeletePhoneCommand { get; set; }
        public ICommand UpdatePhoneCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Phones = new RestCollection<Phone>("http://localhost:29971/", "phone");
                CreatePhoneCommand = new RelayCommand(
                    () => Phones.Add(new Phone() { Name = SelectedPhone?.Name })
                );

                DeletePhoneCommand = new RelayCommand(
                    () =>
                    {
                        Phones.Delete(SelectedPhone.PhoneId);
                    },
                    () => SelectedPhone != null
                );

                UpdatePhoneCommand = new RelayCommand(
                    () => Phones.Update(SelectedPhone)
                );
                SelectedPhone = new Phone();
            }
        }
    }
}
