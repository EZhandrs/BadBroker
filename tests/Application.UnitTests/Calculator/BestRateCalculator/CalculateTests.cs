using BadBroker.Domain.Constants;
using BadBroker.Domain.Entities;
using A = BadBroker.Application.Calculator;

namespace Application.UnitTests.Calculator.BestRateCalculator
{
    public class CalculateTests
    {
        [Fact]
        public void Calculate()
        {
            //Arrange
            var rates = new List<Rate>
            {
                new Rate
                {
                    Date = new DateTime(2014,12,15),
                    RUB =  57.9957m,
                    EUR = 0.803818m,
                    GBP = 0.63935m,
                    JPY = 118.084399m
                },
                new Rate
                {
                    Date = new DateTime(2014,12,16),
                    RUB = 68.53245m,
                    EUR = 0.799252m,
                    GBP = 0.634963m,
                    JPY = 116.831m
                },
                new Rate
                {
                    Date = new DateTime(2014,12,17),
                    RUB = 68.30813m,
                    EUR = 0.810611m,
                    GBP = 0.642142m,
                    JPY = 118.468099m
                },
                new Rate
                {
                    Date = new DateTime(2014,12,18),
                    RUB = 61.891725m,
                    EUR = 0.813927m,
                    GBP = 0.638237m,
                    JPY = 118.569499m

                },
                new Rate
                {
                    Date = new DateTime(2014,12,19),
                    RUB = 58.9055m,
                    EUR = 0.817716m,
                    GBP = 0.639964m,
                    JPY = 119.3527m
                },
                new Rate
                {
                    Date = new DateTime(2014,12,20),
                    RUB = 58.9055m,
                    EUR = 0.817703m,
                    GBP = 0.63999m,
                    JPY = 119.4555m
                },
                new Rate
                {
                    Date = new DateTime(2014,12,21),
                    RUB = 58.67535m,
                    EUR = 0.818161m,
                    GBP = 0.63995m,
                    JPY = 119.5304m
                },
                new Rate
                {
                    Date = new DateTime(2014, 12, 22),
                    RUB = 55.61375m,
                    EUR = 0.817742m,
                    GBP = 0.64158m,
                    JPY = 119.9486m
                },
                new Rate
                {
                    Date = new DateTime(2014, 12, 23),
                    RUB = 54.89676m,
                    EUR = 0.82153m,
                    GBP = 0.644409m,
                    JPY = 120.5218m
                }
            };
            var calculator = new A.BestRateCalculator();

            var expectedBuyDate = new DateTime(2014, 12, 17);
            var expectedSellDate = new DateTime(2014, 12, 23);
            var expectedTool = Currency.RUB;
            var expectedRevenue = 18.43016673479454889505318711m;

            //Act
            var actualBestRate = calculator.Calculate(rates, 100);

            //Assert
            Assert.Equal(expectedBuyDate, actualBestRate.BuyDate);
            Assert.Equal(expectedSellDate, actualBestRate.SellDate);
            Assert.Equal(expectedTool, actualBestRate.Tool);
            Assert.Equal(expectedRevenue, actualBestRate.Revenue);
        }
    }
}