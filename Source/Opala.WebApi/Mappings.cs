using AutoMapper;
using Opala.Core.Domain.Models;
using Opala.WebApi.Dtos;

namespace Opala.WebApi
{
    public static class Mappings
    {
        public static void Initialize()
        {
            Mapper.Initialize(c =>
            {
                //Get
                c.CreateMap<Client, GetClientResult>();
                c.CreateMap<Address, AddClientPost.AddressPost>();

                //Post
                c.CreateMap<AddClientPost, Client>();
                c.CreateMap<AddClientPost.AddressPost, Address>();
                c.CreateMap<AddClientSubscriptionPost, Subscription>();
                c.CreateMap<AddPayPalPaymentPost, PayPalPayment>();
                c.CreateMap<AddCreditCardPaymentPost, CreditCardPayment>();

                //Put
                c.CreateMap<UpdateClientPut, Client>();
                c.CreateMap<UpdateClientPut.AddressPut, Address>();
            });
        }
    }
}
