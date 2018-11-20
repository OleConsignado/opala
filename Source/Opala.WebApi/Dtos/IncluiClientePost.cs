using System;

namespace Opala.WebApi.Dtos
{
    public class IncluiClientePost
    {
        public string Nome { get; set; }

        public string Email { get; set; }

        public string Telefone { get; set; }

        public EnderecoPost Endereco { get; set; }

        /// <summary>
        /// Classe aninhada tratada somente como objeto de valor
        /// </summary>
        public class EnderecoPost
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