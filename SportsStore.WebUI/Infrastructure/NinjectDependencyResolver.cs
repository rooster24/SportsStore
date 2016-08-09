using Moq;
using Ninject;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Concrete;
using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using SportsStore.WebUI.Infrastructure.Abstract;
using SportsStore.WebUI.Infrastructure.Concrete;

namespace SportsStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            //Mock<IProductRepository> mock = new Mock<IProductRepository>();
            //mock.Setup(m => m.Products).Returns(new List<Product>
            //{
            //    new Product {Name = "Football", Description = "A long football Desc", Price = 25},
            //    new Product {Name = "Surf Board", Description = "A long surf board Desc", Price = 179},
            //    new Product {Name = "Running Shoes", Description = "A long shoe Desc", Price = 95 },
            //    new Product {Name = "2Football", Description = "2A long football Desc", Price = 225},
            //    new Product {Name = "2Surf Board", Description = "2A long surf board Desc", Price = 2179},
            //    new Product {Name = "2Running Shoes", Description = "2A long shoe Desc", Price = 295 }
            //});

            //kernel.Bind<IProductRepository>().ToConstant(mock.Object);

// --- uncomment below to use SQL DB instead of mock data above
             kernel.Bind<IProductRepository>().To<EFProductRepository>();

            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager
        .AppSettings["Email.WriteAsFile"] ?? "false")
            };

            kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>()
                .WithConstructorArgument("settings", emailSettings);

            kernel.Bind<IAuthProvider>().To<FormsAuthProvider>();

        }



    }
}