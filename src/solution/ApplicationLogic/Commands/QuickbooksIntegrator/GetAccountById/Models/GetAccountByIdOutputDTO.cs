﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic.Commands.QuickbooksIntegrator.GetAccountById.Models
{
    public class GetAccountByIdOutputDTO
    {
        public string ListID { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public bool? IsActive { get; set; }
    }
}
