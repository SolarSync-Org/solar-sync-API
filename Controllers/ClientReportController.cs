using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using SolarSync_API.DTO;
using SolarSync_API.DTO.ClientReports;
using SolarSync_API.Interface;
using SolarSync_API.Models;

namespace SolarSync_API.Controllers
{
    /// <summary>
    /// Controlador responsável pelo gerenciamento de relatórios de clientes.
    /// Oferece endpoints para criar, atualizar, buscar e deletar relatórios de clientes.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ClientReportController : ControllerBase
    {
        private readonly IClientReportService _clientReportService;

        /// <summary>
        /// Inicializa uma nova instância de <see cref="ClientReportController"/>.
        /// </summary>
        /// <param name="clientReportService">Serviço de gerenciamento de relatórios dos clientes.</param>
        public ClientReportController(IClientReportService clientReportService)
        {
            _clientReportService = clientReportService;
        }

        /// <summary>
        /// Cria novos relatórios de clientes em massa.
        /// </summary>
        /// <param name="clientReportDtos">Lista de dados dos relatórios de clientes a serem criados.</param>
        /// <returns>Confirmação da criação dos relatórios de clientes.</returns>
        /// <response code="201">Relatórios de clientes criados com sucesso.</response>
        /// <response code="400">Dados inválidos para criação dos relatórios.</response>
        [HttpPost]
        [ProducesResponseType(typeof(List<ClientReport>), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Create([FromBody] List<ClientReportDTO> clientReportDtos)
        {
            var createdReports = new List<ClientReport>();
            foreach (var clientReportDto in clientReportDtos)
            {
                var createdReport = await _clientReportService.CreateAsync(clientReportDto);
                createdReports.Add(createdReport);
            }
            return CreatedAtAction(nameof(GetAll), createdReports);
        }

        /// <summary>
        /// Busca todos os relatórios de clientes.
        /// </summary>
        /// <returns>Lista de relatórios de clientes existentes.</returns>
        /// <response code="200">Retorna a lista de relatórios de clientes.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ClientReport>), 200)]
        public async Task<ActionResult<IEnumerable<ClientReport>>> GetAll()
        {
            var reports = await _clientReportService.GetAllAsync();
            return Ok(reports);
        }

        /// <summary>
        /// Busca um relatório de cliente específico pelo ID.
        /// </summary>
        /// <param name="id">ID do relatório de cliente.</param>
        /// <returns>O relatório de cliente correspondente ao ID.</returns>
        /// <response code="200">Retorna o relatório de cliente com o ID especificado.</response>
        /// <response code="404">Relatório de cliente não encontrado para o ID especificado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ClientReport), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ClientReport>> GetById(string id)
        {
            var report = await _clientReportService.GetByIdAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            return Ok(report);
        }

        /// <summary>
        /// Atualiza um relatório de cliente existente.
        /// </summary>
        /// <param name="id">ID do relatório de cliente a ser atualizado.</param>
        /// <param name="clientReportDto">Dados atualizados do relatório de cliente.</param>
        /// <returns>Status da operação de atualização.</returns>
        /// <response code="204">Relatório de cliente atualizado com sucesso.</response>
        /// <response code="404">Relatório de cliente não encontrado para o ID especificado.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Update(string id, [FromBody] UpdateClientReportDTO clientReportDto)
        {
            var updated = await _clientReportService.UpdateAsync(id, clientReportDto);
            if (updated == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        /// <summary>
        /// Exclui um relatório de cliente pelo ID.
        /// </summary>
        /// <param name="id">ID do relatório de cliente a ser excluído.</param>
        /// <returns>Status da operação de exclusão.</returns>
        /// <response code="204">Relatório de cliente excluído com sucesso.</response>
        /// <response code="404">Relatório de cliente não encontrado para o ID especificado.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(string id)
        {
            var deleted = await _clientReportService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
        
        [HttpPost("train-and-update-labels")]
        public async Task<ActionResult> TrainAndUpdateLabels()
        {
            await _clientReportService.UpdateLabelsAsync();
            return Ok("Labels atualizadas com sucesso!");
        }


    }
}
