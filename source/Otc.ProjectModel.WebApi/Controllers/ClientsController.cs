using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Otc.ProjectModel.Core.Domain.Exceptions;
using Otc.ProjectModel.Core.Domain.Services;
using Otc.ProjectModel.WebApi.Dtos;

namespace Otc.ProjectModel.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService clientService;

        public ClientsController(IClientService clientService)
        {
            this.clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
        }

        [HttpGet("{clientId}")]
        [ProducesResponseType(typeof(ClientCoreException), 400)]
        [ProducesResponseType(typeof(ClientResponse), 200)]
        public async Task<IActionResult> GetClientAsync(Guid clientId)
        {
            var client = Mapper.Map<ClientResponse>(await clientService.GetClientAsync(clientId));

            return Ok(client);
        }
    }
}