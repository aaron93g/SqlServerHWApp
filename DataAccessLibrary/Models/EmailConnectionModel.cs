﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class EmailConnectionModel
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int EmailAddressId { get; set; }
    }
}
