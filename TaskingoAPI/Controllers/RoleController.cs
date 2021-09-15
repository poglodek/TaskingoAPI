using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskingoAPI.Dto.Role;
using TaskingoAPI.Dto.User;
using TaskingoAPI.Services.IRepositories;

namespace TaskingoAPI.Controllers
{
    [ApiController]
    [Route("/Role")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleServices _roleServices;

        public RoleController(IRoleServices roleServices)
        {
            _roleServices = roleServices;
        }
        [HttpGet("GetAll")]
        public ActionResult<List<RoleDto>> GetAllRoles()
        {
            var roles = _roleServices.GetAllRoles();
            return Ok(roles);
        }
        [HttpPost]
        public ActionResult AddNewRole([FromQuery]string roleName)
        {
            var id = _roleServices.AddNewRole(roleName);
            return Created("GetAll",null);
        }
    }
}
