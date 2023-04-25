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
    public class ManufacturerController : Controller
    {
        IManufacturerLogic pl;

        public ManufacturerController(IManufacturerLogic pl)
        {
            this.pl = pl;
        }

        [HttpGet]
        public IEnumerable<Manufacturer> Get()
        {
            return this.pl.ReadAll();
        }

        [HttpGet("{id}")]
        public Manufacturer Read(string id)
        {
            return this.pl.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Manufacturer value)
        {
            this.pl.Create(value);
        }

        [HttpPut]
        public void Update([FromBody] Manufacturer value)
        {
            this.pl.Update(value);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            this.pl.Delete(id);
        }
    }
}
