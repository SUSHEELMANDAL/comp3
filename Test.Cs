using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tweet_App_API.Controllers;
using Tweet_App_API.Exceptions;
using Tweet_App_API.Model;
using Tweet_App_API.Services;

namespace TweetTestApi.controller
{
    [TestFixture]
    public class TweetTest
    {
        private Mock<IUserServices> userservice;
        private Mock<ITweetService> tweetservice;
        private Mock<ILogger<TweetsController>> logger;
        private TweetsController Controller;

        [SetUp]
        public void setup()
        {
            userservice = new Mock<IUserServices>();
            tweetservice = new Mock<ITweetService>();
            logger = new Mock<ILogger<TweetsController>>();

            Controller = new TweetsController(logger.Object, userservice.Object, tweetservice.Object);
        }

        public readonly User user = new User()
        {
            FirstName ="user",LastName="S",Email="as@gmail.com",ContactNumber="9000056789",LoginId = "as1234",Password="12345"
        };

        private readonly UserLoginModel userLoginModel = new UserLoginModel()
        {
            ContactNumber = "1234567890",
            UserName = "123@gmail.com",
            Password = "123@gmai"
        };

        private readonly UserViewModel userViewModel = new UserViewModel()
        {
            ContactNumber = "1234567890",
            Email = "123@gmail.com",
            FirstName = "user",
            LastName = "S"
        };

        private readonly List<Tweet> tweet = new List<Tweet>()
        {
            new Tweet()
            {
                Content="content",Tags = "#tags",CreateTime=DateTime.Now,TweetId="001",
                CreatorId="user",Likes= new List<string>{"liked"},
                Replys=new List<TweetReply>(){new TweetReply { Replied_userId="R001",ReplyMessage="looking good",ReplyTags=new List<string> { "tags"},Reply_Time=DateTime.Now } }
            }
        };

        private readonly Tweet tweeet = new Tweet()
        {
            Content = "content",
            Tags = "#tags",
            CreateTime = DateTime.Now,
            TweetId = "001",
            CreatorId = "user",
            Likes = new List<string> { "liked" },
            Replys = new List<TweetReply>() { new TweetReply { Replied_userId = "R001", ReplyMessage = "looking good", ReplyTags = new List<string> { "tags" }, Reply_Time = DateTime.Now } }

        };

        [Test]
        public async Task RegisterTest()
        {
            userservice.Setup(x => x.Register(It.IsAny<User>())).Returns(Task.FromResult<UserResponse>(new UserResponse() { Email = "aa123@gmail.com" }));
            var response = await Controller.Register(user) as OkObjectResult;

            Assert.NotNull(response);
            Assert.AreEqual(200, response.StatusCode);
        }
