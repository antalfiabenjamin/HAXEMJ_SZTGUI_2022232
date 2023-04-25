using HAXEMJ_HFT_2022231.Logic.Interfaces;
using HAXEMJ_HFT_2022231.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HAXEMJ_HFT_2022231.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PhoneController : Controller
    {
        IPhoneLogic pl;

        public PhoneController(IPhoneLogic pl)
        {
            this.pl = pl;
        }

        [HttpGet]
        public IEnumerable<Phone> Get()
        {
            return this.pl.ReadAll();
        }

        [HttpGet("{id}")]
        public Phone Read(int id)
        {
            return this.pl.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Phone value)
        {
            this.pl.Create(value);
        }

        [HttpPut]
        public void Update([FromBody] Phone value)
        {
            this.pl.Update(value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.pl.Delete(id);
        }
    }
}
