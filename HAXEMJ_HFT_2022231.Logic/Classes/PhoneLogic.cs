using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using HAXEMJ_HFT_2022231.Logic.Interfaces;
using HAXEMJ_HFT_2022231.Models;
using HAXEMJ_HFT_2022231.Repository.Interfaces;

namespace HAXEMJ_HFT_2022231.Logic.Classes
{
    public class PhoneLogic : IPhoneLogic
    {
        IRepository<Phone> repo;
        IRepository<Manufacturer> manuRepo;
        IRepository<User> userRepo;

        public PhoneLogic(IRepository<Phone> repo,
            IRepository<Manufacturer> manuRepo, IRepository<User> userRepo)
        {
            this.repo = repo;
            this.manuRepo = manuRepo;
            this.userRepo = userRepo;
        }

        public void Create(Phone item)
        {
            if (item.Name == null)
            {
                throw new ArgumentException("Item's name can not be null...");
            }
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Phone Read(int id)
        {
            return this.repo.Read(id);
        }

        public IQueryable<Phone> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Phone item)
        {
            this.repo.Update(item);
        }

        // NON-CRUD
        // Get a list of all the iPhones in the database

        // FIXME

        public IEnumerable<Phone> GetAlliPhones()
        {
            //return from x in this.repo.ReadAll()
            //       join y in manuRepo.ReadAll() on x.ManufacturerID equals y.ManufacturerId
            //       where y.Name == "Apple"
            //       select new Phone
            //       {
            //           PhoneId = x.PhoneId,
            //           Name = x.Name
            //       };

            return from x in this.repo.ReadAll()
                   where x.Manufacturer.Name == "Apple"
                   select new Phone
                   {
                       Id = x.Id,
                       Name = x.Name
                   };
        }

        // Returns a count of the specific colored phones made by the manufacturers 

        public IEnumerable<Manufacturer> GetPreferredColorPhones(string color)
        {
            //return from x in this.repo.ReadAll()
            //       join y in manuRepo.ReadAll() on x.ManufacturerID equals y.ManufacturerId
            //       where x.Color == color
            //       group y by y.Name into g
            //       select new Manufacturer
            //       {
            //           Name = g.Key,
            //           ColorPhoneCount = g.Count()
            //       };

            return from x in this.repo.ReadAll()
                   where x.Color == color
                   group x by x.Manufacturer.Name into g
                   select new Manufacturer
                   {
                       Name = g.Key,
                       ColorPhoneCount = g.Count()
                   };
        }

        // Get the count of phones by users

        public IEnumerable<User> GetPhoneCountByUser()
        {
            //return from x in this.repo.ReadAll()
            //       join y in userRepo.ReadAll() on x.UserID equals y.UserID
            //       group y by y.Name into g
            //       select new User
            //       {
            //           Name = g.Key,
            //           PhoneCount = g.Count()
            //       };

            return from x in this.repo.ReadAll()
                   group x by x.User.Name into g
                   select new User
                   {
                       Name = g.Key,
                       PhoneCount = g.Count()
                   };
        }

        // List all of the phones that were made by companies based on given location

        public IEnumerable<Phone> PhonesBySpecificLocation(string company)
        {
            //return from x in this.repo.ReadAll()
            //       join y in manuRepo.ReadAll() on x.ManufacturerID equals y.ManufacturerId
            //       where y.Location == company
            //       select new Phone
            //       {
            //           PhoneId = x.PhoneId,
            //           Name = x.Name
            //       };

            return from x in this.repo.ReadAll()
                   where x.Manufacturer.Location == company
                   select new Phone
                   {
                       Id = x.Id,
                       Name = x.Name
                   };
        }

        // A breakdown of average screentime by the phones of the users

        public IEnumerable<User> GetScreenTimeBd()
        {
            //return from x in this.repo.ReadAll()
            //       join y in userRepo.ReadAll() on x.UserID equals y.UserID
            //       group x by y.Name into g
            //       select new User
            //       {
            //           Name = g.Key,
            //           TotalAvgScrTime = g.Average(z => z.AvgScreenTime)
            //       };

            return from x in this.repo.ReadAll()
                   group x by x.User.Name into g
                   select new User
                   {
                       Name = g.Key,
                       TotalAvgScrTime = g.Average(z => z.AvgScreenTime)
                   };
        }

    }
}

