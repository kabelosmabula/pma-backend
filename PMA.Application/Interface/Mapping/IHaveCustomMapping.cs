using AutoMapper;
using System;


namespace PMA.Application.Interface.Mapping
{
    public interface IHaveCustomMapping
    {
        void CreateMappings(Profile configuration);
    }
}
