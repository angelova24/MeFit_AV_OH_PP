﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeFit.DAL.Models.DTOs.Program
{
    public class ProgramCreateDTO
    {
        [Required, MaxLength(70)]
        public string Name { get; set; }
        [Required, MaxLength(70)]
        public string Category { get; set; }
    }
}
