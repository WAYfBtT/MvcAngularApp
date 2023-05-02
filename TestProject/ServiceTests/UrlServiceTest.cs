using BLL.Interfaces;
using BLL.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using DAL.UnitOfWork;
using System.Linq.Expressions;
using DAL.Entities;
using BLL.Models;
using BLL.Exceptions;
using AutoMapper;
using BLL.Mapper;
using NUnit.Framework;

namespace TestProject.ServiceTests
{
    [TestFixture]
    internal class UrlServiceTest
    {
        private readonly IMapper _mapper = new MapperConfiguration(cfg => cfg.AddProfile(typeof(MappingProfile))).CreateMapper();

        [Test]
        public async Task UrlService_AddAsync_ThrowsUrlShortenerExceptionOnDuplicate()
        {
            //Arrange
            var uowMock = new Mock<IUnitOfWork>();
            var shortenerMock = new Mock<IUrlShortenerService>();
            uowMock.Setup(uow => uow.UrlRepository.
            AnyAsync(It.IsAny<Expression<Func<Url, bool>>>())).ReturnsAsync(true);
            var service = new UrlService(_mapper, uowMock.Object, shortenerMock.Object);

            //Act
            Func<Task> act = async () => await service.AddAsync(new UrlModel());

            //Assert
            await act.Should().ThrowAsync<UrlShortenerApplicationException>().WithMessage("Such url already exists.");
        }

        [Test]
        public async Task UrlService_AddAsync_AddsUrl()
        {
            //Arrange
            var input = new UrlModel { LongUrl = "https://www.youtube.com/", CreatedBy = 1};
            var uowMock = new Mock<IUnitOfWork>();
            var shortenerMock = new Mock<IUrlShortenerService>();

            uowMock.Setup(uow => uow.UrlRepository.
            AnyAsync(It.IsAny<Expression<Func<Url, bool>>>())).ReturnsAsync(false);

            shortenerMock.Setup(x => x.ShortenUrl(It.Is<string>(x => x == "https://www.youtube.com/"))).Returns("dbae2d0204aa");

            uowMock.Setup(uow => uow.UrlRepository.
            Add(It.Is<Url>(x =>
            x == new Url { LongUrl = "https://www.youtube.com/" , ShortUrl = "dbae2d0204aa", CreatedBy = 1 })));
            uowMock.Setup(x => x.SaveChangesAsync());

            var service = new UrlService(_mapper, uowMock.Object, shortenerMock.Object);

            Func<Task> act = async () => await service.AddAsync(input);
            //Act
            await act();

            //Assert
            uowMock.Verify(uow => uow.UrlRepository.AnyAsync(It.IsAny<Expression<Func<Url, bool>>>()), Times.Once);
            uowMock.Verify(x => x.UrlRepository.Add(It.IsAny<Url>()), Times.Once);
            uowMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task UrlService_DeleteAsync_ThrowsUrlShortenerExceptionOnUnknownId()
        {
            //Arrange
            var uowMock = new Mock<IUnitOfWork>();
            var shortenerMock = new Mock<IUrlShortenerService>();

            uowMock.Setup(uow => uow.UrlRepository.
            AnyAsync(It.IsAny<Expression<Func<Url, bool>>>())).ReturnsAsync(false);

            var service = new UrlService(_mapper, uowMock.Object, shortenerMock.Object);

            //Act
            Func<Task> act = async () => await service.DeleteByIdAsync(1);
            
            //Assert
            await act.Should().ThrowAsync<UrlShortenerApplicationException>().WithMessage("Url with such id does not exist.");
            uowMock.Verify(uow => uow.UrlRepository.AnyAsync(It.IsAny<Expression<Func<Url, bool>>>()), Times.Once);
        }

        [Test]
        public async Task UrlService_DeleteAsync_DeletesUrl()
        {
            //Arrange
            var uowMock = new Mock<IUnitOfWork>();
            var shortenerMock = new Mock<IUrlShortenerService>();

            uowMock.Setup(uow => uow.UrlRepository.
            AnyAsync(It.IsAny<Expression<Func<Url, bool>>>())).ReturnsAsync(true);
            uowMock.Setup(uow => uow.UrlRepository.DeleteByIdAsync(It.IsAny<int>()));
            uowMock.Setup(x => x.SaveChangesAsync());

            var service = new UrlService(_mapper, uowMock.Object, shortenerMock.Object);

            //Act
            await service.DeleteByIdAsync(1);

            //Assert
            uowMock.Verify(uow => uow.UrlRepository.AnyAsync(It.IsAny<Expression<Func<Url, bool>>>()), Times.Once);
            uowMock.Verify(x => x.UrlRepository.DeleteByIdAsync(It.Is<int>(x => x == 1)), Times.Once);
            uowMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}
