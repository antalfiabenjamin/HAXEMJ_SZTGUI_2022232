using System;
using System.Linq;
using System.Numerics;
using HAXEMJ_HFT_2022231.Models;

namespace HAXEMJ_HFT_2022231.Logic.Interfaces
{
    public interface IUserLogic
    {
        void Create(User item);
        void Delete(int id);
        User Read(int id);
        IQueryable<User> ReadAll();
        void Update(User item);
    }
}

