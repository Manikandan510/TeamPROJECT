using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ProductMicroService.Controllers;
using ProductMicroService.Models;
using ProductMicroService.Provider;
using ProductMicroService.Repository;
using System.Collections.Generic;
using System.Linq;

namespace ProductMicroServiceTest
{
    [TestFixture]
    public class Tests
    {
        private ProductController _controller;
        private Mock<IProvider> _config;
        private ProductProvider provider;
        private Mock<IProductRepository> _repo;
        private List<ProductDto> dto;

        [SetUp]
        public void Setup()
        {
            _config = new Mock<IProvider>();
            _controller = new ProductController(_config.Object);
            _repo = new Mock<IProductRepository>();
            provider = new ProductProvider(_repo.Object);
            dto = new List<ProductDto>()
            {
                new ProductDto{Id=1,Price=25000,Name="iWatch",Description="The new Apple Watch Series 6 and the new Apple Watch SE.",Image_name="iWatch.png",Rating=4},
                new ProductDto(){Id=2,Price=4000,Name="NeckBand",Description="Boult Audio Probass Curve Bluetooth Wireless Neckband With Mic (Black).",Image_name="Neckband.png",Rating=4},
                new ProductDto(){Id=3,Price=3000,Name="PowerBank",Description="Power up! Power down! Use the power to charge all of your fun.",Image_name="PowerBank.png",Rating=5},
                new ProductDto(){Id=4,Price=2000,Name="Fitnessband",Description="The perfect workout companion, Mi Smart Band 6 provides 14 days of battery life4 and a handy magnetic port for quick clip-on and clip-off",Image_name="FitBand.png",Rating=5},
                new ProductDto(){Id=5,Price=5000,Name="HomeTheatre",Description="Break out the popcorn and get an immersive experience with our all-in-one 5.1 home theatre.",Image_name="HomeTheatre.png",Rating=3},
            };
        }

        [Test]
        [TestCase(1)]
        [TestCase(5)]
        //[TestCase(6)]
        public void GetProductDetailsById_Success(int productid)
        {
            _config.Setup(p => p.SearchProductById(productid)).Returns(dto.FirstOrDefault(x=>x.Id==productid));
            var result = _controller.SearchProductById(productid);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }
        [Test]
        [TestCase("iWatch")]
        [TestCase("PowerBank")]
        //[TestCase("PS5")]
        public void GetProductDetailsByName_Success(string name)
        {
            _config.Setup(p => p.SearchProductByName(name)).Returns(dto.FirstOrDefault(x => x.Name == name));
            var result = _controller.SearchProductByName(name);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }
       
        [Test]
        [TestCase(1)]
        [TestCase(5)]
        public void GetProductByIdRepo_Sucess(int id)
        {
            _repo.Setup(x => x.GetAllProducts()).Returns(dto);
            var response = provider.SearchProductById(id);
            Assert.IsNotNull(response);
        }
        [Test]
        [TestCase(11)]
        [TestCase(51)]
        public void GetProductByIdRepo_Fail(int id)
        {
            _repo.Setup(x => x.GetAllProducts()).Returns(dto);
            var response = provider.SearchProductById(id);
            Assert.IsNull(response);
        }
        [Test]
        [TestCase("iWatch")]
        [TestCase("PowerBank")]
        public void GetProductByNameRepo_Sucess(string name)
        {
            _repo.Setup(x => x.GetAllProducts()).Returns(dto);
            var response = provider.SearchProductByName(name);
            Assert.IsNotNull(response);
        }
        [Test]
        [TestCase("Charger")]
        [TestCase("PS5")]
        public void GetProductByNameRepo_Fail(string name)
        {
            _repo.Setup(x => x.GetAllProducts()).Returns(dto);
            var response = provider.SearchProductByName(name);
            Assert.IsNull(response);
        }
        [Test]
        [TestCase(1,3)]
        public void AddRatingToProduct_Success(int prodid,int rat)
        {
            ProductRating rating = new ProductRating { Id = prodid, Rating = rat};
            _config.Setup(p => p.AddProductRating(rating)).Returns(new RatingStatus { Message = "Rating added Successfully to the Product" });
            var result = _controller.AddProductRating(rating);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }
    }
}