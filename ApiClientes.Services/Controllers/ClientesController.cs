using ApiClientes.Infra.Data.Entities;
using ApiClientes.Infra.Data.Interfaces;
using ApiClientes.Services.Requests;
using ApiClientes.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiClientes.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        //atributo
        private readonly IClienteRepository _clienteRepository;

        //construtor para injeção de dependência
        public ClientesController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        [HttpPost]
        public IActionResult Post(ClientePostRequest request)
        {
            try
            {
                //verificar se o email informado já está cadastrado no banco de dados
                if (_clienteRepository.GetByEmail(request.Email) != null)
                    return StatusCode(422, new { message = "O email informado já está cadastrado, verifique." });

                //verificar se o email informado já está cadastrado no banco de dados
                if (_clienteRepository.GetByCpf(request.Cpf) != null)
                    return StatusCode(422, new { message = "O cpf informado já está cadastrado, verifique." });

                var idade = Utils.CalcularIdade(request.DataNascimento);
                if (idade < 18)
                    //HTTP 422 (UNPROCESSABLE ENTITY)
                    return StatusCode(422, new { message = "Cliente menor  de idade." });

                //cadastrando o cliente
                var cliente = new Cliente()
                {
                    IdCliente = Guid.NewGuid(),
                    Nome = request.Nome,
                    Email = request.Email,
                    Cpf = request.Cpf,
                    DataNascimento = request.DataNascimento
                };
                //gravando o cliente no banco de dados
                _clienteRepository.Create(cliente);
                return StatusCode(201, new { message = "Cliente cadastrado com sucesso.", cliente });
                
            }
            catch (Exception e)
            { 
                //HTTP 500 - INTERNAL SERVER ERROR
                return StatusCode(500, new { message = e.Message });
            }
        }

        [HttpPut]
        public IActionResult Put(ClientePutRequest request)
        {
            try
            {
                #region Buscando o Cliente no banco de dados através do ID

                var cliente = _clienteRepository.GetById(request.IdCliente);
                if (cliente == null)
                    //HTTP 422 (UNPROCESSABLE ENTITY)
                    return StatusCode(422, new { message = "Cliente não encontrado." });

                var idade = Utils.CalcularIdade(request.DataNascimento);
                if (idade < 18)
                    //HTTP 422 (UNPROCESSABLE ENTITY)
                    return StatusCode(422, new { message = "Cliente menor  de idade." });


                #endregion

                #region Atualizando o Cliente

                cliente.Nome = request.Nome;
                cliente.Email = request.Email;
                cliente.Cpf = request.Cpf;
                cliente.DataNascimento = request.DataNascimento;                

                _clienteRepository.Update(cliente);

                return StatusCode(200, new { message = "Cliente atualizado com sucesso", cliente });

                #endregion
            }
            catch (Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { message = e.Message });
            }
        }

        [HttpDelete("{idCliente}")]
        public IActionResult Delete(Guid idCliente)
        {
            try
            {
                #region Buscando o Cliente no banco de dados através do ID

                var cliente = _clienteRepository.GetById(idCliente);
                if (cliente == null)
                    //HTTP 422 (UNPROCESSABLE ENTITY)
                    return StatusCode(422, new { message = "Cliente não encontrado." });

                #endregion

                #region Excluindo o Cliente

                _clienteRepository.Delete(cliente);

                return StatusCode(200, new { message = "Clientee excluído com sucesso", cliente });

                #endregion
            }
            catch (Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { message = e.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var clientes = _clienteRepository.GetAll();

                //HTTP 200 (OK)
                return StatusCode(200, clientes);
            }
            catch (Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { message = e.Message });
            }
        }

        [HttpGet("{idCliente}")]
        public IActionResult GetById(Guid idCliente)
        {
            try
            {
                var cliente = _clienteRepository.GetById(idCliente);

                if (cliente != null)
                    //HTTP 200 (OK)
                    return StatusCode(200, cliente);
                else
                    //HTTP 204 (NO CONTENT)
                    return StatusCode(204);
            }
            catch (Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { message = e.Message });
            }
        }
        
        
        
        
    }
    
}
