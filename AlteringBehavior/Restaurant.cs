using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlteringBehavior.Cookers;
using AlteringBehavior.Enums;
using AlteringBehavior.Interfaces;

namespace AlteringBehavior
{
    public class Restaurant
    {
        public void CookMasala(Country country)
        {
            if (country == Country.Ukraine)
            {
                var cooker = new UkranianCooker();
                CookSpecifiedMasala(cooker);
            }
            else if (country == Country.India)
            {
                var cooker = new IndianCooker();
                CookSpecifiedMasala(cooker);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private void CookSpecifiedMasala(ICooker cooker)
        {
            var stringBuilder = new StringBuilder("Cook rice: \n");
            stringBuilder.AppendLine(cooker.CookRice());
            stringBuilder.AppendLine("Cook chicken:");
            stringBuilder.AppendLine(cooker.CookChicken());
            stringBuilder.AppendLine("Cook tea:");
            stringBuilder.AppendLine(cooker.CookTea());

            Console.WriteLine(stringBuilder.ToString());
        }
    }
}
