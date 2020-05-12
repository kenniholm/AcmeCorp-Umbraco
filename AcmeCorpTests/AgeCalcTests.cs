using ageCalc;
using System;
using Xunit;

namespace AcmeCorpTests
{
    public class AgeCalcTests
    {
        private AgeCalculator calculator = new AgeCalculator();

        [Fact]
        public void IsAbove18()
        {
            DateTime dobAbove18 = new DateTime(1996, 02, 25);

            bool ageCheck = calculator.Is18(dobAbove18);

            Assert.True(ageCheck);
        }

        [Fact]
        public void IsUnder18()
        {
            DateTime dobUnder18 = new DateTime(2004, 02, 25);

            bool ageCheck = calculator.Is18(dobUnder18);

            Assert.False(ageCheck);
        }
    }
}
