using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlteringBehavior.Models;

namespace AlteringBehavior.Interfaces
{
    public interface ITripRepository
    {
        TripDetails LoadTrip(string touristName);
    }
}
