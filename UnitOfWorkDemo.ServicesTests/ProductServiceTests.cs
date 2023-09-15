using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitOfWorkDemo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkDemo.Core.Interfaces;
using Moq;
using UnitOfWorkDemo.Core.Models;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace UnitOfWorkDemo.Services.Tests
{
    [TestClass()]
    public class ProductServiceTests
    {
        private readonly ProductService _productService;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();
        private readonly Mock<IProductRepository> _productRepoMock = new Mock<IProductRepository>();

        public ProductServiceTests()
        {
            _productService = new ProductService(_unitOfWorkMock.Object);
        }
        [TestMethod()]
        public async Task GetProductById_ShouldReturnProduct_WhenProductExist()
        {
            //Arrange
            var productId = 5;
            var productDetail = new ProductDetails
            {
                Id = productId,
                ProductDescription = "desc",
                ProductName = "name",
                ProductPrice = 10,
                ProductStock = 12
            };

            //**** Methode 1 ****
            //_unitOfWorkMock.Setup(m => m.Products.GetById(productId)).ReturnsAsync(productDetail);
            //**** Methode 2 ****
            _productRepoMock.Setup(x => x.GetById(productId)).ReturnsAsync(productDetail);
            _unitOfWorkMock.Setup(x=>x.Products).Returns(_productRepoMock.Object);
            //Act
            var expectedProduct = await _productService.GetProductById(productId);            
            //Assert
            Assert.IsNotNull(expectedProduct);
            Assert.AreEqual(expectedProduct.Id, productId);
        }

        //[TestMethod()]
        //public async Task GetProductById_ShouldReturnNull_WhenProductdoesNotExist()
        //{
        //    //IUnitOfWork _unitOfWorkMock2 = Substitute.For<IUnitOfWork>();
        //    IProductRepository _productRepoMock2 = Substitute.For<IProductRepository>();
        //    //Arrange
        //    //var productId = Arg.Any<int>();
        //    var productId = 1;
        //    //**** Methode 1 ****
        //    //_unitOfWorkMock.Setup(m => m.Products.GetById(productId)).ReturnsAsync(productDetail);
        //    //**** Methode 2 ****
        //    _productRepoMock2.GetById(productId).ReturnsNull();
        //    _unitOfWorkMock.Setup(x => x.Products).Returns(_productRepoMock2);
        //    //Act
        //    var expectedProduct = await _productService.GetProductById(productId);
        //    //Assert
        //    Assert.IsNull(expectedProduct);
        //}

        [TestMethod()]
        public async Task GetAllProducts_ShouldReturnAllProducts_WhenProductExist()
        { 
            //Arrange
            //var productId = Arg.Any<int>();
            var productList = new List<ProductDetails>()
            {
                new ProductDetails(){ Id = Arg.Any<int>(),
                ProductDescription = "desc",
                ProductName = "name",
                ProductPrice = 10,
                ProductStock = 12 },
                new ProductDetails(){Id = Arg.Any<int>(),
                ProductDescription = "desc",
                ProductName = "name",
                ProductPrice = 10,
                ProductStock = 12  },
            };
             
            //**** Methode 1 ****
            //_unitOfWorkMock.Setup(m => m.Products.GetById(productId)).ReturnsAsync(productDetail);
            //**** Methode 2 ****
            _productRepoMock.Setup(x => x.GetAll()).ReturnsAsync(productList);
            _unitOfWorkMock.Setup(x => x.Products).Returns(_productRepoMock.Object);
            //Act
            var expectedProduct = await _productService.GetAllProducts();
            //Assert
            Assert.AreNotEqual(expectedProduct, null);
            Assert.AreNotEqual(expectedProduct.Count(), 0);
        }



    }
}