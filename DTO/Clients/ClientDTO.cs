using SolarSync_API.Models;

namespace SolarSync_API.DTO;

public class ClientDTO : BaseClient
{
    /// <summary>
    /// Propriedades opcionais para atualização.
    /// </summary>
    public new string? Name { get; set; }
    public new string? Description { get; set; }
    public new string? CNPJ { get; set; }
    public new string? Email { get; set; }
    public new string? Status { get; set; }
}