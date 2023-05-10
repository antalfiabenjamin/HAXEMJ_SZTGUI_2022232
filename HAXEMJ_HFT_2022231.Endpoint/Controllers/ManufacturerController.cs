using HAXEMJ_HFT_2022231.Endpoint.Services;
using HAXEMJ_HFT_2022231.Logic.Interfaces;
using HAXEMJ_HFT_2022231.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
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
        IHubContext<SignalRHub> hub;

        public ManufacturerController(IManufacturerLogic pl, IHubContext<SignalRHub> hub)
        {
            this.pl = pl;
            this.hub = hub;
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
            this.hub.Clients.All.SendAsync("ManufacturerCreated", value);
        }

        [HttpPut]
        public void Update([FromBody] Manufacturer value)
        {
            this.pl.Update(value);
            this.hub.Clients.All.SendAsync("ManufacturerUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            var manuToDelete = this.pl.Read(id);
            this.pl.Delete(id);
            this.hub.Clients.All.SendAsync("ManufacturerDeleted", manuToDelete);
        }
    }
}
