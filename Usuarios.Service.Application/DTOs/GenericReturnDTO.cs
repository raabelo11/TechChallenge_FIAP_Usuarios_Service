namespace Usuarios.Service.Application.DTOs
{
    public class GenericReturnDTO
    {
        public bool Success { get; set; }
        public object? Data { get; set; }
        public string? Error { get; set; }
    }
}
