using HAXEMJ_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HAXEMJ_SZTGUI2022232.WpfClient.Services
{
    public class NonCrudMethodHandler : INonCrudStringService
    {
        public List<string> GetAlliPhones(IEnumerable<Phone> phones)
        {
            var final = new List<string>();
            foreach (Phone phone in phones) 
            {
                final.Add($"{phone.Id}: {phone.Name}");
            }

            return final;
        }

        public List<string> GetPhoneCountByUser(IEnumerable<User> users)
        {
            var final = new List<string>();
            foreach (User u in users)
            {
                final.Add($"{u.Name}: {u.PhoneCount}");
            }

            return final;
        }

        public List<string> GetPhonesBySpecificLocation(IEnumerable<Phone> phones)
        {
            var final = new List<string>();
            foreach (Phone p in phones)
            {
                final.Add($"{p.Id}: {p.Name}");
            }

            return final;
        }

        public List<string> GetPreferredColorPhones(IEnumerable<Manufacturer> manus)
        {
            var final = new List<string>();
            foreach (Manufacturer m in manus)
            {
                final.Add($"{m.Name}: {m.ColorPhoneCount}");
            }

            return final;
        }

        public List<string> GetScreenTimeBd(IEnumerable<User> users)
        {
            var final = new List<string>();
            foreach (User u in users)
            {
                final.Add($"{u.Name}: {u.TotalAvgScrTime}");
            }

            return final;
        }
    }
}
