﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NectimaLogging.ViewModels
{
    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public bool IsNext { get; set; } = false;
        public bool IsPrevious { get; set; } = false;
        public int AddMorePages { get; set; }
        

        public int TotalPages =>
            (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);  
        
    }
}
