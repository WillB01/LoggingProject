using System.Collections.Generic;

namespace NectimaLogging.Controllers
{
    public interface IDateAndExcetionsRepository
    {
        int AmountOfExceptions { get; set; }
       
        string Date { get; set; }

    }

}