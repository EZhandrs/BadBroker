using System.Text.Json;

using BadBroker.Domain.Constants;
using D = BadBroker.Domain.Entities;

namespace Domain.UnitTests.Entities.BestRate
{
    public class SerializationTests
    {
        [Fact]
        public void ToJson()
        {
            //Arrange 
            var rate = new D.Rate
            {
                Date = new DateTime(2014, 12, 15),
                RUB = 60.1735876388m,
                EUR = 0.8047642041m,
                GBP = 0.6386608724m,
                JPY = 118.8395300177m
            };

            var bestRate = new D.BestRate
            {
                Rates = new List<D.Rate>() { rate },
                BuyDate = new DateTime(2014, 12, 16),
                SellDate = new DateTime(2014, 12, 22),
                Tool = Currency.RUB,
                Revenue = 27.258783297622983m
            };

            var expectedJson = "{\"rates\":[{\"date\":\"2014-12-15T00:00:00\",\"rub\":60.1735876388,\"eur\":0.8047642041,\"gbp\":0.6386608724,\"jpy\":118.8395300177}],\"buyDate\":\"2014-12-16T00:00:00\",\"sellDate\":\"2014-12-22T00:00:00\",\"tool\":\"RUB\",\"revenue\":27.258783297622983}";

            //Act
            var actualJson = JsonSerializer.Serialize(bestRate);

            //Assert
            Assert.Equal(expectedJson, actualJson);
        }
    }
}