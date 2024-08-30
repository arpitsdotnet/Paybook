using System.Collections.Generic;
using System.Data;
using NUnit.Framework;
using Paybook.ServiceLayer.Extensions;

namespace Paybook.ServiceLayer.Tests.Extensions
{
    [TestFixture]
    public class DataTableExtensionTests
    {
        public class DataModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        [Test]
        public void ToDataTable_WhenCalled_ShouldReturnAListToDataTable()
        {
            // Arrange
            var list = new List<DataModel>
            {
                new DataModel { Id = 1, Name = "Aarav" },
                new DataModel { Id = 2, Name = "Atharva" },
                new DataModel { Id = 3, Name = "Arpit" }
            };

            // Act
            DataTable dataTable = list.ToDataTable();
            // Assert

            Assert.That(dataTable.Rows.Count, Is.EqualTo(3));
            Assert.That(dataTable.Rows[0]["Name"].ToString(), Is.EqualTo("Aarav"));
        }
    }
}
