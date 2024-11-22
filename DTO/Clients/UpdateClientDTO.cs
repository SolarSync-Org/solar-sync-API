using SolarSync_API.Models;

namespace SolarSync_API.DTO;

public class UpdateClientDTO
{
    /// <summary>
    /// Propriedades opcionais para atualização.
    /// </summary>
    public new string? Name { get; set; }
    public new Address? Address { get; set; }
    public new string? CPF { get; set; }
    public new string? Rent { get; set; }
    public new string? Email { get; set; }
    public new string? Phone { get; set; }
}