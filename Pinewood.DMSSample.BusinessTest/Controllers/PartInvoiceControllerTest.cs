using Moq;
using NUnit.Framework;
using Pinewood.DMSSample.Business;
using Pinewood.DMSSample.Business.Clients;
using Pinewood.DMSSample.Data;
using Pinewood.DMSSample.Data.Models;
using System;
using System.Threading.Tasks;

namespace Pinewood.DMSSample.BusinessTest.Controllers
{
    internal class PartInvoiceControllerTest
    {
        private MockRepository _mockRepository;
        private Mock<IPartAvailabilityClient> _partAvailabilityClient;
        private Mock<IPartInvoiceRepositoryDB> _partInvoiceRepositoryDB;
        private Mock<ICustomerRepositoryDB> _customerRepositoryDB;
        private PartInvoiceController _partInvoiceController;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _partAvailabilityClient = _mockRepository.Create<IPartAvailabilityClient>();
            _partInvoiceRepositoryDB = _mockRepository.Create<IPartInvoiceRepositoryDB>();
            _customerRepositoryDB = _mockRepository.Create<ICustomerRepositoryDB>();
            _partInvoiceController = new PartInvoiceController(
                _customerRepositoryDB.Object,
                _partAvailabilityClient.Object,
                _partInvoiceRepositoryDB.Object);
        }

        [TestCase("ABC", 10, "John Doe")]
        [TestCase("FGH", 2, "Smith Reed")]
        public async Task CreatPartInvoiceAsync_ValidRequest(string stockCode, int quantity, string customerName)
        {
            _customerRepositoryDB.Setup(o => o.GetByName(It.IsAny<string>()))
                .ReturnsAsync(new Customer(1, customerName, "location"));

            _partAvailabilityClient.Setup(o => o.GetAvailability(It.IsAny<string>()))
                .ReturnsAsync(10);

            _partInvoiceRepositoryDB.Setup(o => o.AddInvoice(It.IsAny<PartInvoice>()))
                .Returns(Task.CompletedTask);

            var result = await _partInvoiceController
                .CreatePartInvoiceAsync(stockCode, quantity, customerName);

            _partInvoiceRepositoryDB.Verify();
            _customerRepositoryDB.Verify();
            _partAvailabilityClient.Verify();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);



        }

        [TestCase("ABC", 10, "Tony Stark")]
        [TestCase("FGH", 2, "Steve Rogers")]
        public async Task CreatPartInvoiceAsync_InvalidCustomerRequest(string stockCode, int quantity, string customerName)
        {
            _customerRepositoryDB.Setup(o => o.GetByName(It.IsAny<string>()))
                .ReturnsAsync(new Customer(0, customerName, "location"));

            _partAvailabilityClient.Setup(o => o.GetAvailability(It.IsAny<string>()))
                .ReturnsAsync(10);

            _partInvoiceRepositoryDB.Setup(o => o.AddInvoice(It.IsAny<PartInvoice>()))
                .Returns(Task.CompletedTask);

            var result = await _partInvoiceController
                .CreatePartInvoiceAsync(stockCode, quantity, customerName);

            _partInvoiceRepositoryDB.Verify();
            _customerRepositoryDB.Verify();
            _partAvailabilityClient.Verify();

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
        }
        [TestCase("ZZZ", 10, "Tony Stark")]
        [TestCase("YYY", 2, "Steve Rogers")]
        public async Task CreatPartInvoiceAsync_InvalidStockCode(string stockCode, int quantity, string customerName)
        {
            _customerRepositoryDB.Setup(o => o.GetByName(It.IsAny<string>()))
                .ReturnsAsync(new Customer(1, customerName, "location"));

            _partAvailabilityClient.Setup(o => o.GetAvailability(It.IsAny<string>()))
                .ReturnsAsync(0);

            var result = await _partInvoiceController
                .CreatePartInvoiceAsync(stockCode, quantity, customerName);

            _customerRepositoryDB.Verify();
            _partAvailabilityClient.Verify();

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
        }
        [TestCase("ZZZ", 10, "Tony Stark")]
        [TestCase("YYY", 2, "Steve Rogers")]
        public async Task CreatPartInvoiceAsync_SavingFailed(string stockCode, int quantity, string customerName)
        {
            _customerRepositoryDB.Setup(o => o.GetByName(It.IsAny<string>()))
                .ReturnsAsync(new Customer(1, customerName, "location"));

            _partAvailabilityClient.Setup(o => o.GetAvailability(It.IsAny<string>()))
                .ReturnsAsync(10);

            _partInvoiceRepositoryDB.Setup(o => o.AddInvoice(It.IsAny<PartInvoice>()))
                .Throws<Exception>(() => new Exception("failed to connect"));

            var result = await _partInvoiceController
                .CreatePartInvoiceAsync(stockCode, quantity, customerName);

            _partInvoiceRepositoryDB.Verify();
            _customerRepositoryDB.Verify();
            _partAvailabilityClient.Verify();

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
        }
    }
}
