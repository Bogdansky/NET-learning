namespace AlteringBehavior.Cookers
{
    public class UkranianCooker : BaseCooker
    {
        public UkranianCooker() : base() { }

        public override string CookRice()
        {
            return string.Format(RiceTemplate, 500, "strongly", "strongly", "low");
        }

        public override string CookChicken()
        {
            return string.Format(ChickenTemplate, 300, "medium", "medium", "low");
        }
        public override string CookTea()
        {
            return string.Format(TeaTemplate, 10, "black", 10);
        }
    }
}