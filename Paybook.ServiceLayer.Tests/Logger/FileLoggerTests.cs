using System;
using Moq;
using NUnit.Framework;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Services;

namespace Paybook.ServiceLayer.Tests.Logger
{
    [TestFixture]
    public class FileLoggerTests
    {
        [Test]
        public void GetMethodName_ReturnsExpectedMethodName()
        {
            // Arrange
            string expectedMethodName = $"{"FileLogger"}.{"GetMethodName"}";
            var dateTimeProviderFake = new Mock<IDateTimeProvider>();

            dateTimeProviderFake.Setup(x => x.Now).Returns(new System.DateTime(2020, 01, 01));

            var myLogger = new FileLogger(dateTimeProviderFake.Object);

            // Act
            var methodName = myLogger.GetMethodName();

            // Assert
            Assert.AreEqual(expectedMethodName, methodName); // Replace with the actual expected method name
        }

        //[Test]
        //public void Error_LogsErrorMessage()
        //{
        //    // Arrange
        //    var dateTimeProvider = new Mock<IDateTimeProvider>();
        //    var dateTime = dateTimeProvider.Setup(x => x.Now).Returns(new DateTime(2020, 01, 01));
        //    var myLogger = new FileLogger(dateTimeProvider.Object);
        //    var methodName = "SomeMethod";
        //    var exception = new Exception("Test exception");

        //    // Act
        //    myLogger.Error(methodName, exception);

        //    // Assert
        //    Assert.That(x => x(exception, $"Error in method {methodName}"), Times.Once);
        //}
    }
}
