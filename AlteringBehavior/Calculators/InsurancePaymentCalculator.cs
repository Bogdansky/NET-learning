using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlteringBehavior.Interfaces;

namespace AlteringBehavior.Calculators
{
    public class InsurancePaymentCalculator : ICalculator
    {
        private ICurrencyService currencyService;
        private ITripRepository tripRepository;

        public InsurancePaymentCalculator(ICurrencyService currencyService, ITripRepository tripRepository)
        {
            this.currencyService = currencyService;
            this.tripRepository = tripRepository;
        }

        public decimal CalculatePayment(string touristName)
        {
            var trip = tripRepository.LoadTrip(touristName);
            var rate = currencyService.LoadCurrencyRate();

            var result = Constants.A * rate * trip.FlyCost
                 + Constants.B * rate * trip.AccomodationCost
                 + Constants.C * rate * trip.ExcursionCost;

            return Math.Round(result, 2);
        }
    }
}
