using Common;
using DataEntities;
using DataStorage;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Roles.Services
{
    public class RoleService
    {
        private readonly DatabaseContext _dbContext;
        private readonly IServiceMapper _serviceMapper;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleService(DatabaseContext dbContext, IServiceMapper serviceMapper, RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _serviceMapper = serviceMapper;
            _roleManager = roleManager;
        }


    }
}
