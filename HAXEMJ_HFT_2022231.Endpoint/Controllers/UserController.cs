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
    public class UserController : Controller
    {
        IUserLogic pl;

        public UserController(IUserLogic pl)
        {
            this.pl = pl;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return this.pl.ReadAll();
        }

        [HttpGet("{id}")]
        public User Read(int id)
        {
            return this.pl.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] User value)
        {
            this.pl.Create(value);
        }

        [HttpPut]
        public void Update([FromBody] User value)
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
