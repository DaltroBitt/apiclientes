using System.ComponentModel.DataAnnotations;

namespace ApiClientes.Services.Requests
{
    /// <summary>
    /// Modelo de dados para a requisição de cadastro de cliente
    /// </summary>
    public class ClientePostRequest
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; set; }

        [EmailAddress(ErrorMessage ="Formato de email inválido")]
        [Required(ErrorMessage ="Campo obrigatório")]
        public string Email { get; set; }

        [MinLength(11, ErrorMessage ="Mínimo de caracteres")]
        [MaxLength(11, ErrorMessage = "Máximo de caracteres")]
        [Required(ErrorMessage ="Campo obrigatório")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public DateTime DataNascimento { get; set; }
    }
}
