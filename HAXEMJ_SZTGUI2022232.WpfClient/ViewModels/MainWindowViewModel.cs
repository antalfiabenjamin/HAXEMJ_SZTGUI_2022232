﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HAXEMJ_HFT_2022231.Models;
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
    public class MainWindowViewModel : ObservableRecipient
    {
        public RestCollection<Phone> Items { get; set; }
        private Phone selectedItem;

        public Phone SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (value != null)
                {
                    selectedItem = new Phone()
                    {
                        Name = value.Name,
                        Id = value.Id,
                    };
                    OnPropertyChanged();
                    (DeleteCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        private string selectedTable;

        public string SelectedTable
        {
            get { return selectedTable; }
            set { SetProperty(ref selectedTable, value); }
        }

        public ICommand CreateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand UpdateCommand { get; set; }

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
                Items = new RestCollection<Phone>("http://localhost:29971/", "phone", "hub");
                CreateCommand = new RelayCommand(
                    () => Items.Add(new Phone() { Name = SelectedItem?.Name })
                );

                DeleteCommand = new RelayCommand(
                    () =>
                    {
                        Items.Delete(SelectedItem.Id.ToString());
                    },
                    () => SelectedItem != null
                );

                UpdateCommand = new RelayCommand(
                    () => Items.Update(SelectedItem)
                );
                SelectedItem = new Phone();
            }
        }
    }
}
