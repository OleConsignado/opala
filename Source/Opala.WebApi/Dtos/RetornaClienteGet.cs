using System;

namespace Opala.WebApi.Dtos
{
    public class RetornaClienteGet
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Telefone { get; set; }

        public bool Ativo { get; set; }

       public EnderecoGet Endereco { get; set; }

        /// <summary>
        /// Classe aninhada tratada somente como objeto de valor
        /// </summary>
        public class EnderecoGet
        {
            public string Rua { get; set; }

            public string Numero { get; set; }

            public string Bairro { get; set; }

            public string Cidade { get; set; }

            public string Estado { get; set; }

            public string Pais { get; set; }

            public string Cep { get; set; }
        }
    }
}