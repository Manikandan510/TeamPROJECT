using AuthorizationMicroService.Controllers;
using AuthorizationMicroService.Models;
using AuthorizationMicroService.ProviderLayer;
using AuthorizationMicroService.RepoLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace AuthorizationTests
{
    public class Tests
    {
        private Mock<IConfiguration> _config;
        private Mock<IUserRepo> _repository;
        List<UserDetails> users;
        [SetUp]
        public void Setup()
        {
            users = new List<UserDetails>() {
            new UserDetails{ Userid = 1, Username = "user1",Password = "user1@123"},
            new UserDetails{ Userid = 2, Username = "user2",Password = "user2@123"},
            new UserDetails{ Userid = 1, Username = "user3",Password = "user3@123"} };
            _config = new Mock<IConfiguration>();
            _config.Setup(c => c["Jwt:Key"]).Returns("ThisismySecretKey");
            _repository = new Mock<IUserRepo>();
        }
        [Test]
        [TestCase("user1","user1@123")]
        public void ValidUser(string uname,string pass)
        {
            UserDetails user = new UserDetails()
            {
                Userid = 1,
                Username=uname,
                Password=pass
            };
            _repository.Setup(p => p.GetUserDetails(It.IsAny<UserDetails>())).Returns(users.FirstOrDefault(x => x.Username == user.Username && x.Password == user.Password));
            UserProvider cp = new UserProvider(_repository.Object,_config.Object);
            var result = cp.LoginProvider(user);
            Assert.IsNotNull(result);
        }
        [Test]
        [TestCase("user1", "user12@123")]
        public void InValidUser(string uname, string pass)
        {
            UserDetails user = new UserDetails()
            {
                Userid = 1,
                Username = uname,
                Password = pass
            };
            _repository.Setup(p => p.GetUserDetails(It.IsAny<UserDetails>())).Returns(users.FirstOrDefault(x => x.Username == user.Username && x.Password == user.Password));
            UserProvider cp = new UserProvider(_repository.Object, _config.Object);
            var result = cp.LoginProvider(user);
            Assert.IsNull(result);
        }
    }
}