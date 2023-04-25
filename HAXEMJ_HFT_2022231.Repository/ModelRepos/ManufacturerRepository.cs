using System;
using System.Linq;
using System.Reflection;
using HAXEMJ_HFT_2022231.Models;
using HAXEMJ_HFT_2022231.Repository.Interfaces;

namespace HAXEMJ_HFT_2022231.Repository.ModelRepos
{
    public class ManufacturerRepository : Repository<Manufacturer>, IRepository<Manufacturer>
    {
        public ManufacturerRepository(PhoneDbContext ctx) : base(ctx)
        {

        }

        public override Manufacturer Read(string id)
        {
            return ctx.Manufacturer.FirstOrDefault(t => t.Id == id);
        }

        public override Manufacturer Read(int id)
        {
            throw new NotImplementedException();
        }

        public override void Update(Manufacturer item)
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

