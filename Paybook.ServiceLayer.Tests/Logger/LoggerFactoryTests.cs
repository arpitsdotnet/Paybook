using NUnit.Framework;
using Paybook.ServiceLayer.Logger;
using System;

namespace Paybook.ServiceLayer.Tests.Logger
{
    [TestFixture]
    public class LoggerFactoryTests
    {
        [Test]
        public void Instance_WhenCalledMoreThenOne_ShouldReturnSameInstance()
        {
            var db = LoggerFactory.Instance;
            var db2 = LoggerFactory.Instance;

            Assert.That(db, Is.SameAs(db2));
        }
    }
}
