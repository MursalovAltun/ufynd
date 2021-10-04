using System;
using System.Linq;
using Autofac;
using AutoMapper;

namespace Application.UnitTests.Fixtures
{
    public class TestFixture
    {
        public IMapper Mapper { get; }

        public TestFixture(params Type[] profileTypes)
        {
            if (profileTypes.Any(t => t.BaseType != typeof(Profile)))
            {
                throw new ArgumentException("Argument parameter must inherit AutoMapper.Profile");
            }

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                var profiles = profileTypes
                    .Select(type => Activator.CreateInstance(type) as Profile)
                    .ToList();

                cfg.AddProfiles(profiles);
            });

            Mapper = configurationProvider.CreateMapper();
        }

        public Action<ContainerBuilder> BeforeBuild => cfg => { cfg.RegisterInstance(Mapper).As<IMapper>(); };
    }
}