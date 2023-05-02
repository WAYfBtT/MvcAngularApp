using BLL.Interfaces;
using BLL.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace TestProject.ServiceTests
{
    [TestFixture]
    internal class UrlShortenerServiceTest
    {
        [Test]
        public void Test()
        {
            // Arrange
            var input = "https://www.youtube.com/";
            var expected = "dbae2d0204aa";
            var mock = new Mock<IUrlShortenerService>();
            mock.Setup(x => x.ShortenUrl(It.Is<string>(x => x == input))).Returns(expected);
            var service = mock.Object;
            // Act
            var result = service.ShortenUrl(input);
            // Assert
            result.Should().Be(expected);
        }
    }
}
