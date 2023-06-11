using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlteringBehavior.Interfaces;
using AlteringBehavior.Models;

namespace AlteringBehavior
{
    public class TemporaryTripRepository : ITripRepository
    {
        private Dictionary<string, TripDetails> _tripDetails;

        public TemporaryTripRepository()
        {
            _tripDetails = new Dictionary<string, TripDetails>
            {
                {"Nick", new TripDetails() { TouristName = "Nick", AccomodationCost = 123, ExcursionCost = 32, FlyCost = 155} },
                {"Mike", new TripDetails() { TouristName = "Mike", AccomodationCost = 23, ExcursionCost = 123, FlyCost = 105} }
            };
        }

        public TripDetails LoadTrip(string touristName)
        {
            return _tripDetails[touristName];
        }
    }
}
