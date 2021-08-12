using NUnit.Framework;
using Paybook.ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paybook.ServiceLayer.Tests.Services
{
    [TestFixture]
    public class ActivityBuilderTests
    {
        [Test]
        public void AddHeader_WhenCalledWithData_ShouldReturnActivityBuilderObject()
        {
            // Arrange
            ActivityBuilder activity = new ActivityBuilder();
            // Act
            var result = activity.AddHeader("Test", "2021/08/12", "");
            // Assert
            Assert.That(result, Is.EqualTo(activity));
        }

        [Test]
        public void AddHeader_WhenCalledWithData_ShouldReturnString()
        {
            // Arrange
            ActivityBuilder activity = new ActivityBuilder();
            // Act
            var result = activity.AddHeader("Test", "2021/08/12", "");
            // Assert
            Assert.That(result.ToString(), Is.EqualTo("Test (2021/08/12)"));
        }

        [Test]
        public void AddHeader_WhenCalledWithDataWhenTitleColorCssIsBlank_ShouldReturnStringHtml()
        {
            // Arrange
            ActivityBuilder activity = new ActivityBuilder();
            // Act
            var result = activity.AddHeader("Title1", "2021/08/12");
            // Assert
            Assert.That(result.ToStringHtml(), Is.EqualTo("<div class=\" fwt-large\"><i class='fa fa-info-circle'></i>&nbsp;Title1 <span class=\"small text-grey\">(2021/08/12)</span></div>"));
        }

        [Test]
        public void AddHeader_WhenCalledWithData_ShouldReturnStringHtml()
        {
            // Arrange
            ActivityBuilder activity = new ActivityBuilder();
            // Act
            var result = activity.AddHeader("Title1", "2021/08/12", "colorcss");
            // Assert
            Assert.That(result.ToStringHtml(), Is.EqualTo("<div class=\"colorcss fwt-large\"><i class='fa fa-info-circle'></i>&nbsp;Title1 <span class=\"small text-grey\">(2021/08/12)</span></div>"));
        }

        [Test]
        public void AddBody_WhenCalledWithData_ShouldReturnActivityBuilderObject()
        {
            // Arrange
            ActivityBuilder activity = new ActivityBuilder();
            // Act
            var result = activity.AddBody("Heading1", "1", "Name1", "State1");
            // Assert
            Assert.That(result, Is.EqualTo(activity));
        }

        [Test]
        public void AddBody_WhenCalledWithDataAmountIsBlank_ShouldReturnString()
        {
            // Arrange
            ActivityBuilder activity = new ActivityBuilder();
            // Act
            var result = activity.AddBody("Heading1", "1", "Name1", "State1");
            // Assert
            Assert.That(result.ToString(), Is.EqualTo("Heading1 (1) to Name1 is State1"));
        }

        [Test]
        public void AddBody_WhenCalledWithData_ShouldReturnString()
        {
            // Arrange
            ActivityBuilder activity = new ActivityBuilder();
            // Act
            var result = activity.AddBody("Heading1", "1", "Name1", "State1", "Amount1");
            // Assert
            Assert.That(result.ToString(), Is.EqualTo("Heading1 (1) to Name1 of Amount1 is State1"));
        }

        [Test]
        public void AddBody_WhenCalledWithDataAmountIsBlank_ShouldReturnStringHtml()
        {
            // Arrange
            ActivityBuilder activity = new ActivityBuilder();
            // Act
            var result = activity.AddBody("Heading1", "1", "Name1", "State1");
            // Assert
            Assert.That(result.ToStringHtml(), Is.EqualTo("<div class=\"small\">Heading1 <span class=\"text-blue\">1</span> to <span class=\"text-blue\">Name1</span> is State1.</div>"));
        }
    }
}
