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
                //Get
                c.CreateMap<Client, ClientResponse>();
                c.CreateMap<Core.Domain.Models.Address, Dtos.Address>();

                //Post
                c.CreateMap<ClientRequest,Client>();
                c.CreateMap<Dtos.Address, Core.Domain.Models.Address>();
                c.CreateMap<Dtos.SubscriptionRequest, Core.Domain.Models.Subscription>();
            });
        }
    }
}
