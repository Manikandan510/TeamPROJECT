using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using VendorMicroService.Controllers;
using VendorMicroService.Models;
using VendorMicroService.Provider;
using VendorMicroService.Repository;

namespace VendorMicroServiceTest
{
    [TestFixture]
    public class Tests
    {
        List<VendorDto> vendors ;
        List<VendorStockDto> stock;
        private Mock<IVendorRepository> _repo;
        private VendorProvider provider;
        private VendorController _controller;
        private Mock<IVendorProvider> _config;

        [SetUp]
        public void Setup()
        {
            _repo = new Mock<IVendorRepository>();
            provider = new VendorProvider(_repo.Object);

            _config = new Mock<IVendorProvider>();
            _controller = new VendorController(_config.Object);
            vendors = new List<VendorDto> {new VendorDto()
                {
                VendorId = 1,
                VendorName = "GoPro",
                DeliveryCharge= 150,
                VendorRating = 4
                } };
            stock = new List<VendorStockDto> {new VendorStockDto
            {
                ProductId=1,
                VendorId = 1,
                StockInHand=1,
                ExpectedStockReplenishmentDate=new DateTime(2020,7,6) }
            };

        }

        [Test]
        [TestCase(1)]
        public void GetVendorDetails_Success(int id)
        {
            _config.Setup(p => p.GetDetailsOfVendor(1)).Returns(new List<VendorDto> {new VendorDto()
                {
                VendorId = 1,
                VendorName = "GoPro",
                DeliveryCharge= 150,
                VendorRating = 4
                } });
            var result = _controller.GetVendorDetails(id);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }


        [Test]
        [TestCase(0)]
        public void GetVendorDetails_Fail(int id)
        {
            _config.Setup(p => p.GetDetailsOfVendor(1)).Returns(new List<VendorDto> {new VendorDto()
                {
                VendorId = 1,
                VendorName = "GoPro",
                DeliveryCharge= 150,
                VendorRating = 4
                } });
            var result = _controller.GetVendorDetails(id);
            Assert.IsNull(result);
        }
        [Test]
        public void GetVendorListSuccess()
        {
            var result = _controller.VendorList();
            Assert.IsNotNull(result);
        }
    }
}