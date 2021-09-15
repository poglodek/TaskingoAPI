﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TaskingoAPI.Database;
using TaskingoAPI.Database.Entity;
using TaskingoAPI.Dto.Role;
using TaskingoAPI.Exceptions;
using TaskingoAPI.Services.IRepositories;

namespace TaskingoAPI.Services.Repositories
{
    public class RoleServices : IRoleServices
    {
        private readonly TaskingoDbContext _taskingoDbContext;
        private readonly IMapper _mapper;

        public RoleServices(TaskingoDbContext taskingoDbContext,
            IMapper mapper)
        {
            _taskingoDbContext = taskingoDbContext;
            _mapper = mapper;
        }

        public int AddNewRole(string roleName)
        {
            var ifRoleExist = _taskingoDbContext
                .Roles
                .Any(x => x.RoleName.ToUpper().Equals(roleName.ToUpper()));
            if (ifRoleExist) throw new ConflictExceptions("Role exist.");
            var newRole = new Role {RoleName = roleName};
            _taskingoDbContext
                .Roles
                .Add(newRole);
            _taskingoDbContext.SaveChanges();
            return newRole.Id;
        }
        public Role GetDefaultRole()
        {
            return _taskingoDbContext.Roles.First();
        }

        public Role GetRoleByName(string roleName)
        {
            var role = _taskingoDbContext
                .Roles
                .FirstOrDefault(x => x.RoleName.ToUpper().Equals(roleName.ToUpper()));
            if (role is null) throw new NotFound("Role not found."); 
            return role;
        }

        public List<RoleDto> GetAllRoles()
        {
            var roles = _taskingoDbContext
                .Roles
                .ToList();
            var rolesDto = _mapper.Map<List<RoleDto>>(roles);
            return rolesDto;
        }

    }
}
