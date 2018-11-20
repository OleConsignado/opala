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
            CreateMap<Cliente, RetornaClienteGet>();
            CreateMap<Endereco, IncluiClientePost.EnderecoPost>();

            //Post
            CreateMap<IncluiClientePost, Cliente>();
            CreateMap<IncluiClientePost.EnderecoPost, Endereco>();
            CreateMap<IncluiAssinaturaClientePost, Assinatura>();
            CreateMap<IncluiPagamentoPayPalPost, PayPal>();
            CreateMap<IncluiPagamentoCartaoCreditoPost, CartaoCredito>();

            //Put
            CreateMap<AtualizaClientePut, Cliente>();
            CreateMap<AtualizaClientePut.EnderecoPut, Endereco>();
        }
    }
}
