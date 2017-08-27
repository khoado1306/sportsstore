using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Ninject;
using System.Configuration;
using Domain.Abstract;
using Domain.Concrete;

namespace WebUI.Infrastructure
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
            kernel.Bind<EFDbContext>().To<EFDbContext>().InSingletonScope();
            kernel.Bind<ICategoryRepository>().To<EFCategoryRepository>().InSingletonScope();
            kernel.Bind<IProductRepository>().To<EFProductReposity>().InSingletonScope();
            kernel.Bind<ICartRepository>().To<EFCartRepository>().InSingletonScope();
            kernel.Bind<ICartLineRepository>().To<EFCartLineRepository>().InSingletonScope();

            

           
        }
    }
}