using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeyStore.Business.Abstract;
using KeyStore.Business.Concreate;
using KeyStore.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KeyStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorityController : ControllerBase
    {
        private IAuthorityServices authority_services;

        public AuthorityController()
        {
            authority_services = new AuthorityManager();
        }

        [HttpGet]
        public List<Authority> Get()
        {
            return authority_services.GetAllAuthority();
        }

        [HttpGet("{id}")]
        public Authority Get(int id)
        {
            return authority_services.GetAuthorityById(id);
        }
    }
}