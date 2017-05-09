using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Whale.Api.Providers;
using Whale.Api.Models;

namespace Whale.Api.Controllers
{
    [Route("api/[controller]")]
    public class SloganController : Controller
    {
        private ICacheProvider _cache;

        // GET api/values
        [HttpGet]
        public IDictionary<string, ISlogan> Get()
        {
            return _cache.GetAll<ISlogan>();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ISlogan Get(string id)
        {
            return _cache.Get<ISlogan>(id);
        }

        // POST api/values
        [HttpPost]
        public int Post([FromBody] Slogan value)
        {
            var id = Get().Count + 1;
            _cache.Set<ISlogan>(id.ToString(), value);
            return id;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public string Put(string id, [FromBody] Slogan value)
        {
            _cache.Set<ISlogan>(id, value);
            return id;
        }

        // DELETE api/values/5
        [HttpDelete]
        public void Delete()
        {
            _cache.Clear<ISlogan>();
        }

        public SloganController()
        {
            _cache = new CacheProvider();
        }
    }
}
