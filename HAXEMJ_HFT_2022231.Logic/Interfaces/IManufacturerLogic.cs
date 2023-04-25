using System;
using System.Linq;
using System.Numerics;
using HAXEMJ_HFT_2022231.Models;

namespace HAXEMJ_HFT_2022231.Logic.Interfaces
{
    public interface IManufacturerLogic
    {
        void Create(Manufacturer item);
        void Delete(string id);
        Manufacturer Read(string id);
        IQueryable<Manufacturer> ReadAll();
        void Update(Manufacturer item);
    }
}

