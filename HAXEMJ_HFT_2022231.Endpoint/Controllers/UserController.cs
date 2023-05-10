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
    public class UserController : Controller
    {
        IUserLogic pl;
        IHubContext<SignalRHub> hub;

        public UserController(IUserLogic pl, IHubContext<SignalRHub> hub)
        {
            this.pl = pl;
            this.hub = hub;
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
            this.hub.Clients.All.SendAsync("UserCreated", value);
        }

        [HttpPut]
        public void Update([FromBody] User value)
        {
            this.pl.Update(value);
            this.hub.Clients.All.SendAsync("UserUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var userToDelete = this.pl.Read(id);
            this.pl.Delete(id);
            this.hub.Clients.All.SendAsync("UserDeleted", userToDelete);
        }
    }
}
