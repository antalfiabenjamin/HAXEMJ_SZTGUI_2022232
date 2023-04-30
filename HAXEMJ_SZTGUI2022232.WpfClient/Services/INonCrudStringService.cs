using HAXEMJ_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAXEMJ_SZTGUI2022232.WpfClient.Services
{
    public interface INonCrudStringService
    {
        List<string> GetAlliPhones(IEnumerable<Phone> phones);
        List<string> GetPreferredColorPhones(IEnumerable<Manufacturer> manus);
        List<string> GetScreenTimeBd(IEnumerable<User> users);
        List<string> GetPhoneCountByUser(IEnumerable<User> users);
        List<string> GetPhonesBySpecificLocation(IEnumerable<Phone> phones);
    }
}
