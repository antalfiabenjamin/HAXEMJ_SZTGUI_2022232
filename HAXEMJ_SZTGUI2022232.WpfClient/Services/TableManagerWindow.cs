using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAXEMJ_SZTGUI2022232.WpfClient.Services
{
    public class TableManagerWindow : ITableManagerService
    {
        public void Open(int table)
        {
            switch (table)
            {
                case 0:
                    new MainWindow().ShowDialog();
                    break;
                case 1:
                    new ManufacturerTableWindow().ShowDialog();
                    break;
                case 2:
                    new UserTableWindow().ShowDialog();
                    break;
                case 3:
                    new MainWindow().ShowDialog();
                    break;
            }
        }
    }
}
