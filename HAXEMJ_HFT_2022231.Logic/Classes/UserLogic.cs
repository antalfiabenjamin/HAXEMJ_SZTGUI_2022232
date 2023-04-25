using System;
using System.Linq;
using System.Collections.Generic;
using System.Numerics;
using HAXEMJ_HFT_2022231.Logic.Interfaces;
using HAXEMJ_HFT_2022231.Models;
using HAXEMJ_HFT_2022231.Repository.Interfaces;

namespace HAXEMJ_HFT_2022231.Logic.Classes
{
    public class UserLogic : IUserLogic
    {
        IRepository<User> repo;

        public UserLogic(IRepository<User> repo)
        {
            this.repo = repo;
        }

        public void Create(User item)
        {
            if (item.Name.Length < 4) throw new ArgumentException("Too short for a full name...");
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public User Read(int id)
        {
            return this.repo.Read(id);
        }

        public IQueryable<User> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(User item)
        {
            this.repo.Update(item);
        }
        
    }
}

