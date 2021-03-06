﻿using AutoMapper;
using System;
using System.Linq;
using System.Reflection;

namespace Common
{
    public class ServiceMapper : IServiceMapper
    {
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _mapperConfiguration;
        private readonly IMapper _mergeMapper;

        public ServiceMapper(Action<IMapperConfigurationExpression> configExpression)
        {
            _mapperConfiguration = new MapperConfiguration(configExpression);
            _mapperConfiguration.AssertConfigurationIsValid();
            _mapper = _mapperConfiguration.CreateMapper();
            _mergeMapper = CreateMergeMapper(configExpression);
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

        public TDestination Merge<TSource, TDestination>(TSource src, TDestination dest)
        {
            return _mergeMapper.Map(src, dest);
        }

        internal IMapper CreateMergeMapper(Action<IMapperConfigurationExpression> configExpression)
        {
            var ignoreDefaultValueConfiguration = new MapperConfiguration((cfg) =>
            {
                configExpression(cfg);
                cfg.ForAllPropertyMaps(pm => true, (pm, opt) =>
                {
                    //if value is null
                    if (pm.SourceMember == null)
                    {
                        opt.Ignore();
                    }
                    else
                    {
                        opt.MapFrom(new IgnoreNullResolver(), pm.SourceMember.Name);
                    }
                });
            });
            return ignoreDefaultValueConfiguration.CreateMapper();
        }
    }
}
