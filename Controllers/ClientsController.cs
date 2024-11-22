using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SolarSync_API.DTO;
using SolarSync_API.Models;
using SolarSync_API.Services;

namespace SolarSync_API.Controllers
{
    /// <summary>
    /// Controlador para gerenciar cadastro de Clientes no sistema.
    /// Este controlador fornece endpoints para criar, ler, atualizar e deletar cadastros de clientes.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="ClientsController"/>.
        /// </summary>
        /// <param name="clientService">O serviço responsável pelas operações de cliente.</param>
        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        /// <summary>
        /// Obtém uma lista de todos os Clientes.
        /// </summary>
        /// <returns>Uma lista de clientes.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            var clients = await _clientService.GetClientsAsync();
            return Ok(clients);
        }

        /// <summary>
        /// Obtém um cliente específico pelo seu ID.
        /// </summary>
        /// <param name="id">O ID do cliente a ser obtido.</param>
        /// <returns>O cliente correspondente ao ID fornecido, ou um status 404 se não for encontrado.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Client?>> GetClientById(string id)
        {
            var client = await _clientService.GetClientByIdAsync(id);
            return client is not null ? Ok(client) : NotFound();
        }

        /// <summary>
        /// Obtém um cliente específico pelo seu CPF.
        /// </summary>
        /// <param name="cpf">O CPF do cliente a ser obtido.</param>
        /// <returns>O cliente correspondente ao CPF fornecido, ou um status 404 se não for encontrado.</returns>
        [HttpGet("cpf/{cpf}")]
        public async Task<ActionResult<Client?>> GetClientByCPF(string cpf)
        {
            var client = await _clientService.GetClientByCPFAsync(cpf);
            return client is not null ? Ok(client) : NotFound();
        }

        /// <summary>
        /// Atualiza um cliente existente.
        /// </summary>
        /// <param name="id">O ID do cliente a ser atualizado.</param>
        /// <param name="clientDto">O objeto contendo os dados atualizados do cliente.</param>
        /// <returns>Um status 200 se a atualização for bem-sucedida, um status 404 se o cliente não for encontrado, ou um status 400/409 em caso de erro.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(string id, UpdateClientDTO clientDto)
        {
            try
            {
                var updatedClient = await _clientService.UpdateClientAsync(id, clientDto);
                return updatedClient != null ? Ok(updatedClient) : NotFound();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Cria um novo cliente.
        /// </summary>
        /// <param name="clientDto">Os dados do novo cliente a ser criado.</param>
        /// <returns>Um status 201 se o cliente for criado com sucesso, ou um status 400/409 em caso de erro.</returns>
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(BaseClient clientDto)
        {
            try
            {
                var client = await _clientService.CreateClientAsync(clientDto);
                return CreatedAtAction(nameof(GetClientById), new { id = client.Id }, client);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Deleta um cliente específico pelo seu ID.
        /// </summary>
        /// <param name="id">O ID do cliente a ser deletado.</param>
        /// <returns>Um status 200 se o cliente for deletado com sucesso, ou um status 404 se o cliente não for encontrado.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(string id)
        {
            var deleted = await _clientService.DeleteClientAsync(id);
            return deleted ? Ok() : NotFound();
        }
    }
}
