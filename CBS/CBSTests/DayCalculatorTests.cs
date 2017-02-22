namespace CBSTests
{
    using System;
    using CBS.Logic.Handlers.Implementations;
    using NUnit.Framework;

    public class DayCalculatorTests
    {
        public class TestDate
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public int ExpectedResult { get; set; }
        }

        private static readonly TestDate[] TestDates = {
                       new TestDate { StartDate = new DateTime(2017, 1, 20, 19, 30, 0),
                                      EndDate = new DateTime(2017, 1, 20, 20, 30, 0),
                                      ExpectedResult = 1 },
                       new TestDate { StartDate = new DateTime(2017, 1, 25, 10, 30, 0),
                                      EndDate = new DateTime(2017, 1, 26, 20, 30, 0),
                                      ExpectedResult = 2 },
                       new TestDate { StartDate = new DateTime(2017, 1, 25, 21, 30, 0),
                                      EndDate = new DateTime(2017, 1, 26, 20, 30, 0),
                                      ExpectedResult = 1 },
                       new TestDate { StartDate = new DateTime(2017, 1, 25, 21, 30, 0),
                                      EndDate = new DateTime(2017, 1, 25, 21, 30, 0),
                                      ExpectedResult = 0 },
                       new TestDate { StartDate = new DateTime(2017, 1, 26, 21, 30, 0),
                                      EndDate = new DateTime(2017, 1, 25, 21, 30, 0),
                                      ExpectedResult = -1 }
        };

        [Test]
        public void Should_calculate_difference_between_two_in_days([ValueSource(nameof(TestDates))] TestDate testDate)
        {
            // Act
            var result = DayCalculator.CalculateDateDifferenceCountStartedDay(testDate.StartDate, testDate.EndDate);

            // Assert 
            Assert.AreEqual(testDate.ExpectedResult, result);
        }
    }
}
