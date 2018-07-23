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
                c.CreateMap<Address, AddressDto>();

                //Post
                c.CreateMap<ClientRequest, Client>();//.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ClientId));
                c.CreateMap<AddressDto, Address>();
                c.CreateMap<SubscriptionRequest, Subscription>();
            });
        }
    }
}
