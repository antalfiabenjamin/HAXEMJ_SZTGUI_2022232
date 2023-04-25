using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using HAXEMJ_HFT_2022231.Logic.Interfaces;
using HAXEMJ_HFT_2022231.Models;
using HAXEMJ_HFT_2022231.Repository.Interfaces;

namespace HAXEMJ_HFT_2022231.Logic.Classes
{
    public class ManufacturerLogic : IManufacturerLogic
    {
        IRepository<Manufacturer> repo;

        public ManufacturerLogic(IRepository<Manufacturer> repo) 
        {
            this.repo = repo;
        }

        public void Create(Manufacturer item)
        {
            this.repo.Create(item);
        }

        public void Delete(string id)
        {
            this.repo.Delete(id);
        }

        public Manufacturer Read(string id)
        {
            return this.repo.Read(id);
        }

        public IQueryable<Manufacturer> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Manufacturer item)
        {
            this.repo.Update(item);
        }

    }
}

