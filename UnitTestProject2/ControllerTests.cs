using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.CSharp.RuntimeBinder;
using System.Web.Mvc;
using project;
using project.Controllers;
using project.Models;
using System.Collections.Generic;
using project.Models.Repositories;
using System.Linq;


namespace UnitTestProject2
{
    [TestClass]
    public class ControllerTests
    {
      

        [TestMethod]
        public void Index()
        {
  
            // Arrange
            BreedController controller = new BreedController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Details()
        {
            // Arrange
            BreedController controller = new BreedController();
            // Act
            var result = controller.Details(2) as ViewResult;
            //Assert
            Assert.AreEqual("Details", result.ViewName);
          
        }

        [TestMethod]
        public void Create()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void Edit()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }


    }
}
