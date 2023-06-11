using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlteringBehavior.Cookers;
using AlteringBehavior.Enums;
using NUnit.Framework;

namespace AlteringBehavior.Tests
{
    internal class TemplateTesting
    {
        [TestCase("200 grams of rice, which should be strongly fried.\nRice should be strongly salted and strongly peppered.")]
        public void IndianRiceTest(string expectedRecipe)
        {
            var cooker = new IndianCooker();
            var recipe = cooker.CookRice();

            Assert.AreEqual(expectedRecipe, recipe);
        }

        [TestCase("500 grams of rice, which should be strongly fried.\nRice should be strongly salted and low peppered.")]
        public void UkranianRiceTest(string expectedRecipe)
        {
            var cooker = new UkranianCooker();
            var recipe = cooker.CookRice();

            Assert.AreEqual(expectedRecipe, recipe);
        }
    }
}
