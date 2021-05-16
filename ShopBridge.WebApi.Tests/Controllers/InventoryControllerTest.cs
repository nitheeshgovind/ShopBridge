using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ShopBridge.Application.Abstractions;
using ShopBridge.Application.Common.Exceptions;
using ShopBridge.Application.Data;
using ShopBridge.Domain.Models;
using ShopBridge.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace ShopBridge.WebApi.Tests.Controllers
{
    [TestClass]
    public class InventoryControllerTest
    {
        private readonly Mock<IInventoryRepository> repository;

        public InventoryControllerTest()
        {
            repository = new Mock<IInventoryRepository>();
        }

        [TestMethod]
        public async Task GetInventoriesAsync()
        {

            repository.SetupGet(x => x.GetInventoryAsync().Result).Returns(new List<InventoryModel>()
            {
                new InventoryModel() { Id = 1, Name = "Test Inventory Item1", Description = "Test Inventory Item" },
                new InventoryModel() { Id = 2, Name = "Test Inventory Item2", Description = "Test Inventory Item" },
                new InventoryModel() { Id = 3, Name = "Test Inventory Item2", Description = "Test Inventory Item" },
                new InventoryModel() { Id = 4, Name = "Test Inventory Item2", Description = "Test Inventory Item" },
                new InventoryModel() { Id = 5, Name = "Test Inventory Item2", Description = "Test Inventory Item" },
                new InventoryModel() { Id = 6, Name = "Test Inventory Item2", Description = "Test Inventory Item" }
            });

            InventoryController controller = new InventoryController(repository.Object);

            var result = await controller.GetInventoriesAsync() as OkNegotiatedContentResult<IEnumerable<InventoryModel>>;

            Assert.AreEqual(6, result.Content.Count());
        }

        [TestMethod]
        public async Task GetInventories_Async_ReturnEmpty()
        {
            repository.SetupGet(x => x.GetInventoryAsync().Result).Returns(new List<InventoryModel>()
            {
            });

            InventoryController controller = new InventoryController(repository.Object);

            var result = await controller.GetInventoriesAsync() as OkNegotiatedContentResult<IEnumerable<InventoryModel>>;

            Assert.AreEqual(false, result.Content.Any());
        }

        [TestMethod]
        public async Task GetInventoryAsync_ShouldReturnInventory_WhenExists()
        {
            var inventory = new InventoryModel() { Id = 2, Name = "Test Inventory" };
            repository.Setup(x => x.GetInventoryAsync(2).Result).Returns(inventory);

            InventoryController controller = new InventoryController(repository.Object);

            var response = await controller.GetInventoryAsync(2);

            var result = response as OkNegotiatedContentResult<InventoryModel>;

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Content.Id);
        }

        [TestMethod]
        public async Task GetInventoryAsync_ShoudReturnNotFound_WhenDoesNotExists()
        {
            repository.Setup(x => x.GetInventoryAsync(2).Result).Returns(() => throw new NotFoundException());
            InventoryController controller = new InventoryController(repository.Object);

            var result = await controller.GetInventoryAsync(2);

            Assert.IsNotNull(result);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task AddInventoryAsync_ShouldReturnBadRequest_WhenNameExceedsMaxLength()
        {
            InventoryController controller = new InventoryController(repository.Object);

            controller.ModelState.AddModelError("Name", "Name Exceeds 50 Characters");
            var response = await controller.AddInventoryAsync(new InventoryModel());

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(NegotiatedContentResult<string>));
            Assert.AreEqual(((NegotiatedContentResult<string>)response).StatusCode, System.Net.HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public async Task AddInventoryAsync_ShouldReturnBadRequest_WhenNameDescriptionIsEmpty()
        {
            InventoryController controller = new InventoryController(repository.Object);

            controller.ModelState.AddModelError("Description", "Description Is Empty");
            var response = await controller.AddInventoryAsync(new InventoryModel());

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(NegotiatedContentResult<string>));
            Assert.AreEqual(((NegotiatedContentResult<string>)response).StatusCode, System.Net.HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public async Task AddInventoryAsync_Success()
        {
            repository.Setup(x => x.AddInventoryAsync(It.IsAny<InventoryModel>()).Result).Returns(3);

            InventoryController controller = new InventoryController(repository.Object);

            var response = await controller.AddInventoryAsync(new InventoryModel());

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<int>));

            var result = (response as OkNegotiatedContentResult<int>).Content;

            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public async Task UpdateInventoryAsync_ShoudReturnNotFound_WhenDoesNotExists()
        {
            repository.Setup(x => x.UpdateInventoryAsync(It.IsAny<InventoryModel>()).Result).Returns(() => throw new NotFoundException());
            InventoryController controller = new InventoryController(repository.Object);

            var result = await controller.UpdateInventoryAsync(new InventoryModel());

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task UpdateInventoryAsync_ShoudReturnOK_WhenUpdated()
        {
            repository.Setup(x => x.UpdateInventoryAsync(It.IsAny<InventoryModel>()).Result).Returns(true);
            InventoryController controller = new InventoryController(repository.Object);

            var result = await controller.UpdateInventoryAsync(new InventoryModel());

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task DeleteInventoryAsync_ShouldReturnOk_WhenDeleted()
        {
            repository.Setup(x => x.DeleteInventoryAsync(It.IsAny<int>()).Result).Returns(true);
            InventoryController controller = new InventoryController(repository.Object);

            var result = await controller.DeleteInventoryAsync(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task DeleteInventoryAsync_ShouldReturnNotFound_WhenInventoryNotExists()
        {
            repository.Setup(x => x.DeleteInventoryAsync(It.IsAny<int>()).Result).Returns(() => throw new NotFoundException());
            InventoryController controller = new InventoryController(repository.Object);

            var result = await controller.DeleteInventoryAsync(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task DeleteInventoryAsync_ShouldReturnInternalServerError_OnUnhandeledException()
        {
            repository.Setup(x => x.DeleteInventoryAsync(It.IsAny<int>()).Result).Returns(() => throw new Exception());
            InventoryController controller = new InventoryController(repository.Object);

            var result = await controller.DeleteInventoryAsync(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(InternalServerErrorResult));
        }
    }
}
