using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportskiKlub.Controllers;
using SportskiKlub.Data;
using SportskiKlub.Models;

namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {
        static ApplicationDbContext dbContext;

        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(1, 1);
            Assert.IsTrue(true);
        }


        [ClassInitialize]

        public static void ClassInitialize(TestContext testContext)
        {
            dbContext = new ApplicationDbContext(
                new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase
                ("TEST").Options);

            dbContext.Trening.Add(new Trening()
            {
                TreningID = 1,
                Datum = DateOnly.Parse("2024-10-03"),
                Vrijeme = "17:00",
                Grupa = "Senior",
                Vrsta = VrstaTreninga.Kondicioni
            });
            dbContext.SaveChanges();
        }


        [TestMethod]
        public void TestListaSvihTreninga()
        {

            var controller = new TreningController(dbContext);

            var result = controller.Index();

            var view = result.Result as ViewResult;

            var model = view.Model as List<Trening>;

            Assert.IsTrue(dbContext.Trening.Count() == 1);
            Assert.AreEqual(model.Count, dbContext.Trening.Count());
        }


        [TestMethod]
        public void TestDodavanjeNovogTreninga()
        {

            var controller = new TreningController(dbContext);

            var result = controller.Create(new Trening()
            {
                TreningID = 2,
                Datum = DateOnly.Parse("2024-11-05"),
                Vrijeme = "18:00",
                Grupa = "Kadet",
                Vrsta = VrstaTreninga.Igra
            });

            var action = (RedirectToActionResult)result.Result;

            Assert.IsTrue(action.ActionName == "Index");
            Assert.AreEqual(dbContext.Trening.Count(), 2);
            Assert.AreEqual(dbContext.Trening.ToList().ElementAt(1).TreningID, 2);
        }

    }
}