using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlteringBehavior.Interfaces;

namespace AlteringBehavior.Cookers
{
    public class BaseCooker : ICooker
    {
        internal readonly string RiceTemplate;
        internal readonly string ChickenTemplate;
        internal readonly string TeaTemplate;

        public BaseCooker()
        {
            RiceTemplate = "{0} grams of rice, which should be {1} fried.\nRice should be {2} salted and {3} peppered.";
            ChickenTemplate = "{0} of chicken {1} fried.\nChicken should be {2} salted and {3} peppered.";
            TeaTemplate = "{0} grams of {1} tea with {2} grams of honey";
        }

        public virtual string CookRice() => throw new NotImplementedException();
        public virtual string CookChicken() => throw new NotImplementedException();
        public virtual string CookTea() => throw new NotImplementedException();
    }
}
