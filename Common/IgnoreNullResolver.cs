﻿using AutoMapper;

namespace Common
{
    public class IgnoreNullResolver : IMemberValueResolver<object, object, object, object>
    {
        public object Resolve(object source, object destination, object sourceMember, object destMember, ResolutionContext context)
        {
            var test = sourceMember ?? destMember;
            return test;
        }
    }
}
