﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDesigner.Domain.Entities
{
    public class ItemCard
    {
        [Key]
        public int ID { get; set; }
    }
}