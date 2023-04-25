using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using HAXEMJ_HFT_2022231.Models;

namespace HAXEMJ_HFT_2022231.Logic.Interfaces
{
    public interface IPhoneLogic
    {
        void Create(Phone item);
        void Delete(int id);
        Phone Read(int id);
        IQueryable<Phone> ReadAll();
        void Update(Phone item);
        IEnumerable<Phone> GetAlliPhones();
        IEnumerable<Manufacturer> GetPreferredColorPhones(string color);
        IEnumerable<Phone> PhonesBySpecificLocation(string company);
        IEnumerable<User> GetScreenTimeBd();
        IEnumerable<User> GetPhoneCountByUser();
    }
}

