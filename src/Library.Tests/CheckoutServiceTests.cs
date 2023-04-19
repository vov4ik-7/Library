using LibraryData;
using LibraryData.Models;
using LibraryServices;
using Microsoft.EntityFrameworkCore;

namespace Library.Tests
{
    public class CheckoutServiceTests
    {
        [Test]
        public void Add_Checkout_Success()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(databaseName: "LibraryDb")
                .Options;
            using (var context = new LibraryContext(options))
            {
                var checkoutService = new CheckoutService(context);
                var newCheckout = new Checkout { Id = 1, LibraryCard = new LibraryCard { Id = 1 }, LibraryAsset = new Book { Id = 1 } };

                // Act
                checkoutService.Add(newCheckout);

                // Assert
                var result = context.Checkouts.Find(1);
                Assert.NotNull(result);
                Assert.AreEqual(1, result.Id);
                Assert.AreEqual(1, result.LibraryAsset.Id);
                Assert.AreEqual(1, result.LibraryCard.Id);
            }
        }

        [Test]
        public void Get_CheckoutById_Success()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(databaseName: "LibraryDb")
                .Options;
            using (var context = new LibraryContext(options))
            {
                var checkoutService = new CheckoutService(context);
                var newCheckout = new Checkout { Id = 1, LibraryCard = new LibraryCard { Id = 1 }, LibraryAsset = new Book { Id = 1 } };
                context.Checkouts.Add(newCheckout);
                context.SaveChanges();

                // Act
                var result = checkoutService.Get(1);

                // Assert
                Assert.NotNull(result);
                Assert.AreEqual(1, result.Id);
                Assert.AreEqual(1, result.LibraryAsset.Id);
                Assert.AreEqual(1, result.LibraryCard.Id);
            }
        }

        [Test]
        public void GetAll_Checkouts_Success()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(databaseName: "LibraryDb")
                .Options;
            using (var context = new LibraryContext(options))
            {
                var checkoutService = new CheckoutService(context);
                var newCheckout1 = new Checkout { Id = 1, LibraryCard = new LibraryCard { Id = 1 }, LibraryAsset = new Book { Id = 1 } };
                var newCheckout2 = new Checkout { Id = 2, LibraryCard = new LibraryCard { Id = 2 }, LibraryAsset = new Book { Id = 2 } };
                context.Checkouts.AddRange(newCheckout1, newCheckout2);
                context.SaveChanges();

                // Act
                var result = checkoutService.GetAll();

                // Assert
                Assert.NotNull(result);
                Assert.AreEqual(2, result.Count());
                Assert.AreEqual(1, result.First().Id);
                Assert.AreEqual(2, result.Last().Id);
            }
        }

        [Test]
        public void CheckoutItem_ItemAvailable_Success()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(databaseName: "LibraryDb")
                .Options;
            using (var context = new LibraryContext(options))
            {
                var checkoutService = new CheckoutService(context);
                var libraryAsset = new Book { Id = 1, Title = "Book 1", Status = new Status { Id =  2 } };
                var status = new Status { Id = 1, Name = "Available" };
                var libraryCard = new LibraryCard { Id = 1 };
                context.LibraryAssets.Add(libraryAsset);
                context.Statuses.Add(status);
                context.LibraryCards.Add(libraryCard);
                context.SaveChanges();

                // Act
                checkoutService.CheckoutItem(1, 1);

                // Assert
                var resultAsset = context.LibraryAssets.Find(1);
                Assert.AreEqual(1, resultAsset.Id);
            }
        }
    }
}