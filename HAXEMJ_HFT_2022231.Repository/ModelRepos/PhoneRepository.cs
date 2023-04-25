using System;
using System.Linq;
using System.Reflection;
using HAXEMJ_HFT_2022231.Models;
using HAXEMJ_HFT_2022231.Repository.Interfaces;

namespace HAXEMJ_HFT_2022231.Repository.ModelRepos
{
    public class PhoneRepository : Repository<Phone>, IRepository<Phone>
    {
        public PhoneRepository(PhoneDbContext ctx) : base(ctx)
        {

        }

        public override Phone Read(int id)
        {
            return ctx.Phones.FirstOrDefault(t => t.PhoneId == id);
        }

        public override Phone Read(string id)
        {
            throw new NotImplementedException();
        }

        public override void Update(Phone item)
        {
            var old = Read(item.PhoneId);
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

