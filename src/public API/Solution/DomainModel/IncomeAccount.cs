﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel
{
    public class IncomeAccount : AbstractBaseEntity, ILogicalDeleteEntity
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? DeletedAt { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
