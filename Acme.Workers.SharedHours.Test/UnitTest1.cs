using Acme.Workers.SharedHours.Core.Interfaces;
using System;
using Xunit;
using Moq;
using Acme.Workers.SharedHours.Core.Controllers;
using System.Linq;
using Acme.Workers.SharedHours.Core.Models;
using System.Collections.Generic;
using System.Globalization;

namespace Acme.Workers.SharedHours.Test
{
    public class UnitTest1
    {
        //Test that returns an empty employee since the input data was incorrect
        [Fact]
        public void Test_CreateWorkerFromInput_IncorrectFormat()
        {
            Mock<ISharedHoursView> _view = new Mock<ISharedHoursView>();
            SharedHoursController _controller = new SharedHoursController(_view.Object);
            var employee = _controller.CreateWorkerFromInput("");
            Assert.Null(employee.Name);
        }

        //Test that returns an empty list since it has no working days on input
        [Fact]
        public void Test_CreateWorkerFromInput_EmptyDays()
        {
            Mock<ISharedHoursView> _view = new Mock<ISharedHoursView>();
            SharedHoursController _controller = new SharedHoursController(_view.Object);
            var employee = _controller.CreateWorkerFromInput("TestName=");
            Assert.Equal("TestName", employee.Name);
        }

        //Test that returns an employee with at least one working day
        [Fact]
        public void Test_CreateWorkerFromInput_TestTrue()
        {
            Mock<ISharedHoursView> _view = new Mock<ISharedHoursView>();
            SharedHoursController _controller = new SharedHoursController(_view.Object);
            var employee = _controller.CreateWorkerFromInput("TestName=Mo13:00-14:00");
            Assert.Equal(14, employee.TimeWorked.FirstOrDefault().FinalHour.Hour);
        }

        //Test that returns an empty list of shared hours
        [Fact]
        public void Test_CheckSharedHours_EmptyList()
        {
            Mock<ISharedHoursView> _view = new Mock<ISharedHoursView>();
            SharedHoursController _controller = new SharedHoursController(_view.Object);
            var sharedHours = _controller.CheckSharedHours(new List<Employee>());
            Assert.Empty(sharedHours);
        }

        //Test that returns a list of shared hours between two workers
        [Fact]
        public void Test_CheckSharedHours_ReturnSharedHours()
        {
            var employees = new List<Employee>()
            {
                new Employee()
                {
                    Name = "Test1",
                    TimeWorked = new List<OfficeHours>()
                    {
                        new OfficeHours()
                        {
                            Day = "MO",
                            InitalHour = System.DateTime.ParseExact("12:00", "HH:mm", CultureInfo.InvariantCulture),
                            FinalHour = System.DateTime.ParseExact("13:00", "HH:mm", CultureInfo.InvariantCulture)
                        }
                    }
                },
                new Employee()
                {
                    Name = "Test2",
                    TimeWorked = new List<OfficeHours>()
                    {
                        new OfficeHours()
                        {
                            Day = "MO",
                            InitalHour = System.DateTime.ParseExact("12:00", "HH:mm", CultureInfo.InvariantCulture),
                            FinalHour = System.DateTime.ParseExact("14:00", "HH:mm", CultureInfo.InvariantCulture)
                        }
                    }
                }
            };
            Mock<ISharedHoursView> _view = new Mock<ISharedHoursView>();
            SharedHoursController _controller = new SharedHoursController(_view.Object);
            var sharedHours = _controller.CheckSharedHours(employees);
            Assert.Equal("1", sharedHours.FirstOrDefault().Value);
        }
    }
}
