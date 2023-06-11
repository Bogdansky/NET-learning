using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlteringBehavior.Cookers
{
    public class IndianCooker : BaseCooker
    {
        public IndianCooker() : base() { }

        public override string CookRice()
        {
            return string.Format(RiceTemplate, 200, "strongly", "strongly", "strongly");
        }

        public override string CookChicken()
        {
            return string.Format(ChickenTemplate, 100, "strongly", "strongly", "strongly");
        }
        public override string CookTea()
        {
            return string.Format(TeaTemplate, 15, "green", 12);
        }
    }
}
