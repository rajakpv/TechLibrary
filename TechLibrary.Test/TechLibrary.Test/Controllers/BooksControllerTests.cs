using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechLibrary.Services;
using Xunit;

namespace TechLibrary.Controllers.Tests
{
    [TestFixture()]
    [Category("ControllerTests")]
    public class BooksControllerTests
    {

        private  Mock<ILogger<BooksController>> _mockLogger;
        private  Mock<IBookService> _mockBookService;
        private  Mock<IMapper> _mockMapper;
        private NullReferenceException _expectedException;

        [OneTimeSetUp]
        public void TestSetup()
        {
            _expectedException = new NullReferenceException("Test Failed...");
            _mockLogger = new Mock<ILogger<BooksController>>();
            _mockBookService = new Mock<IBookService>();
            _mockMapper = new Mock<IMapper>();
        }

        [Test()]
        public async Task GetAllTest()
        {
            //  Arrange
            _mockBookService.Setup(b => b.GetBooksAsync()).Returns(Task.FromResult(It.IsAny<List<Domain.Book>>()));
            var sut = new BooksController(_mockLogger.Object, _mockBookService.Object, _mockMapper.Object);

            //  Act
            var result = await sut.GetBooks(new BookParameters
            {
                PageNumber = 1,
                PageSize = 10
            });            

            //  Assert
            _mockBookService.Verify(s => s.GetBooksAsync(), Times.Once, "Expected GetBooksAsync to have been called once");
        }

        [Test()]
        public async Task SearchBookTest()
        {
            //  Arrange
            _mockBookService.Setup(b => b.SearchBook("Android")).Returns(Task.FromResult(It.IsAny<IEnumerable<Domain.Book>>()));
            var sut = new BooksController(_mockLogger.Object, _mockBookService.Object, _mockMapper.Object);

            //  Act
            var result = await sut.SearchBook(new BookParameters
            {
                SearchString = "Android",
                PageNumber = 1,
                PageSize = 10
            });

            //  Assert
            _mockBookService.Verify(s => s.SearchBook("Android"), Times.Once, "Expected SearchBook to have been called once");
        }

        //#region get by id
        ////[Fact]
        ////public async void Task_GetPostById_Return_OkResult()
        ////{
        ////    //Arrange  
        ////    var controller = new BooksController(_mockLogger.Object, _mockBookService.Object, _mockMapper.Object);
        ////    int bookId = 2;

        ////    //Act  
        ////    var data = await controller.GetById(bookId);
            
        ////    //Assert  
        ////    Assert.IsType<OkObjectResult>(data);
        ////}

        ////[Fact]
        ////public async void Task_GetPostById_Return_NotFoundResult()
        ////{
        ////    //Arrange  
        ////    var controller = new BooksController(_mockLogger.Object, _mockBookService.Object, _mockMapper.Object);
        ////    var postId = 3;

        ////    //Act  
        ////    var data = await controller.GetPost(postId);

        ////    //Assert  
        ////    Assert.IsType<NotFoundResult>(data);
        ////}

        ////[Fact]
        ////public async void Task_GetPostById_Return_BadRequestResult()
        ////{
        ////    //Arrange  
        ////    var controller = new BooksController(_mockLogger.Object, _mockBookService.Object, _mockMapper.Object);
        ////    int? postId = null;

        ////    //Act  
        ////    var data = await controller.GetPost(postId);

        ////    //Assert  
        ////    Assert.IsType<BadRequestResult>(data);
        ////}

        ////[Fact]
        ////public async void Task_GetPostById_MatchResult()
        ////{
        ////    //Arrange  
        ////    var controller = new BooksController(_mockLogger.Object, _mockBookService.Object, _mockMapper.Object);
        ////    int? postId = 1;

        ////    //Act  
        ////    var data = await controller.GetPost(postId);

        ////    //Assert  
        ////    Assert.IsType<OkObjectResult>(data);

        ////    var okResult = data.Should().BeOfType<OkObjectResult>().Subject;
        ////    var post = okResult.Value.Should().BeAssignableTo<PostViewModel>().Subject;

        ////    Assert.Equal("Test Title 1", post.Title);
        ////    Assert.Equal("Test Description 1", post.Description);
        ////}
        //#endregion
    }
}