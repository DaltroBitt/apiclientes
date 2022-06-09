using System.ComponentModel.DataAnnotations;

namespace ApiClientes.Services.Requests
{
    /// <summary>
    /// Modelo de dados para a requisição de atualização de cliente
    /// </summary>
    public class ClientePutRequest
    {
        [Required(ErrorMessage = "Campo obrigatório.")]
        public Guid IdCliente { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Nome { get; set; }

        
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Email { get; set; }

        [MinLength(11, ErrorMessage = "Mínimo de caracteres")]
        [MaxLength(11, ErrorMessage = "Máximo de caracteres")]
        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public DateTime DataNascimento { get; set; }
    }
}
