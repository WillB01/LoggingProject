using System;
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
        public int NextPage { get; set; } = 0;
        public bool IsNext { get; set; } = false;
        public bool IsPrevious { get; set; } = false;
        public int AddMorePages { get; set; }
        public int Ten { get; set; }




        //public bool IsMore { get; set; } => CurrentPage => 10 == true;

        public int TotalPages =>
            (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);

        public int GetLastPage => (TotalPages - TotalPages) + 14;
        public int GetFirstPage => (TotalPages - TotalPages) + 1;


        public int Test => IsMore() ;
            
        public int IsMore()
        {
           
            if(CurrentPage % 10 == 0 && CurrentPage >= 10 )
            {
                return 20;
            }
            return 0;
        }

        
      

        
    }
}
