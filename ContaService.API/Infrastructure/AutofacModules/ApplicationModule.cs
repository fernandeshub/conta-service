using Autofac;
using ContaService.API.Application.Queries;
using ContaService.Domain.AggregatesModel.ContaCorrenteAggregate;
using ContaService.Infrastructure.Idempotency;
using ContaService.Infrastructure.Repositories;

namespace ContaService.API.Infrastructure.AutofacModules
{
    public class ApplicationModule :Autofac.Module
    {
        public string QueriesConnectionString { get; }

        public ApplicationModule(string qconstr)
        {
            QueriesConnectionString = qconstr;

        }

        protected override void Load(ContainerBuilder builder)
        {

            builder.Register(c => new ContaCorrenteQueries(QueriesConnectionString))
                .As<IContaCorrenteQueries>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ContaCorrenteRepository>()
                .As<IContaCorrenteRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<RequestManager>()
               .As<IRequestManager>()
               .InstancePerLifetimeScope(); 
        }
    }
}