using AutoMapper;
using Otc.ProjectModel.Core.Domain.Models;
using Otc.ProjectModel.WebApi.Dtos;

namespace Otc.ProjectModel.WebApi
{
    public static class Mappings
    {
        public static void Initialize()
        {
            Mapper.Initialize(c =>
            {
                c.CreateMap<Client, ClientResponse>();
            });
        }
    }
}
