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
    [Route("[controller]/[action]")]
    [ApiController]
    public class NonCrudController : Controller
    {
        IPhoneLogic pl;

        public NonCrudController(IPhoneLogic pl)
        {
            this.pl = pl;
        }

        [HttpGet]
        public IEnumerable<Phone> GetAlliPhones()
        {
            return this.pl.GetAlliPhones();
        }

        [HttpGet("{color}")]
        public IEnumerable<Manufacturer> GetPreferredColorPhones(string color)
        {
            return this.pl.GetPreferredColorPhones(color);
        }

        [HttpGet]
        public IEnumerable<User> GetPhoneCountByUser()
        {
            return this.pl.GetPhoneCountByUser();
        }

        [HttpGet]
        public IEnumerable<User> GetScreenTimeBd()
        {
            return this.pl.GetScreenTimeBd();
        }

        [HttpGet("{location}")]
        public IEnumerable<Phone> PhonesBySpecificLocation(string location)
        {
            return this.pl.PhonesBySpecificLocation(location);
        }

    }
}
