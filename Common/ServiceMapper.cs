using AutoMapper;
using System;
using System.Linq;
using System.Reflection;

namespace Common
{
    public class ServiceMapper : IServiceMapper
    {
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _mapperConfiguration;

        public ServiceMapper(Action<IMapperConfigurationExpression> configExpression)
        {
            _mapperConfiguration = new MapperConfiguration(configExpression);
            _mapperConfiguration.AssertConfigurationIsValid();
            _mapper = _mapperConfiguration.CreateMapper();
        }

        public static Action<IMapperConfigurationExpression> GetMapperConfiguration(string[] containerAssemblyNames)
        {
            var assemblies = containerAssemblyNames.Select(assemblyName => Assembly.Load(assemblyName)).ToArray();
            return GetDefaultMapperConfiguration(assemblies);
        }

        public static Action<IMapperConfigurationExpression> GetDefaultMapperConfiguration(Assembly[] containerAssemblyNames)
        {
            return cfg => cfg.AddMaps(containerAssemblyNames);
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return _mapper.Map<TSource, TDestination>(source);
        }
    }
}
