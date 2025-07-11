﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricalLearning.Repositories.Abstraction
{
    public  abstract class Entity<T>
    {
        public T Id { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
