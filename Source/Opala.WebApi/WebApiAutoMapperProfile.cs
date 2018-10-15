using AutoMapper;
using Opala.Core.Domain.Models;
using Opala.WebApi.Dtos;

namespace Opala.WebApi
{
    public class WebApiAutoMapperProfile : Profile
    {
        public WebApiAutoMapperProfile()
        {
            //Get
            CreateMap<Client, GetClientResult>();
            CreateMap<Address, AddClientPost.AddressPost>();

            //Post
            CreateMap<AddClientPost, Client>();
            CreateMap<AddClientPost.AddressPost, Address>();
            CreateMap<AddClientSubscriptionPost, Subscription>();
            CreateMap<AddPayPalPaymentPost, PayPalPayment>();
            CreateMap<AddCreditCardPaymentPost, CreditCardPayment>();

            //Put
            CreateMap<UpdateClientPut, Client>();
            CreateMap<UpdateClientPut.AddressPut, Address>();
        }
    }
}
