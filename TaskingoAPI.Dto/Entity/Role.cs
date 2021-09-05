﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TaskingoAPI.Dto.Entity;

namespace TaskingoAPI.Database.Entity
{
    public class Role
    {

        [Key]
        public int Id { get; set; }
        public string RoleName { get; set; }
        public List<User> Users { get; set; }
    }
}