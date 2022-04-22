using Hiberus.Model.Models.HiberusEntity;
using Hiberus.Services.Interfaces;
using Hiberus.Services.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Hiberus.Test
{ 

    public class ServicesTest
    {
        UnityContainer container;
        Mock<IRateService> mockRate;

        [SetUp]
        public void Setup()
        {
            container = new UnityContainer();
            mockRate = new Mock<IRateService>();
            container.RegisterInstance(mockRate.Object);
        }

        [Test]
        public void ReturnCount()
        {
            var rateService = container.Resolve<RateService>();

            var response = rateService.GetRates();

            Assert.IsTrue(response.Count > 0); 
        }


    }
}
