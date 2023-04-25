using System;
using System.Linq;
using System.Reflection;
using HAXEMJ_HFT_2022231.Models;
using HAXEMJ_HFT_2022231.Repository.Interfaces;

namespace HAXEMJ_HFT_2022231.Repository.ModelRepos
{
    public class UserRepository : Repository<User>, IRepository<User>
    {
        public UserRepository(PhoneDbContext ctx) : base(ctx)
        {

        }

        public override User Read(int id)
        {
            return ctx.User.FirstOrDefault(t => t.Id == id);
        }

        public override User Read(string id)
        {
            throw new NotImplementedException();
        }

        public override void Update(User item)
        {
            var old = Read(item.Id);
            PropertyInfo[] propInf = old.GetType().GetProperties();

            foreach (var prop in propInf)
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                    prop.SetValue(old, prop.GetValue(item));
            }
            ctx.SaveChanges();
        }
    }
}

