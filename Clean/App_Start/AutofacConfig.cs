using Autofac;
using Autofac.Integration.Mvc;
using Clean.Services;
using Common.Database;
using Common.Repository;
using System.Web.Mvc;

namespace Clean.App_Start
{
    internal static class AutofacConfig
    {
        internal static void Configure()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<LibraryDbContext>()
                   .AsSelf()
                   .InstancePerRequest();

            builder.RegisterType<BookRepository>()
                   .As<IBookRepository>()
                   .InstancePerRequest();

            builder.RegisterType<BookService>()
                   .As<IBookService>()
                   .InstancePerRequest();

            builder.RegisterType<InMemoryAuthenticationService>()
                   .As<IAuthenticationService>()
                   .SingleInstance();

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
