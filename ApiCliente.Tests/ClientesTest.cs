using ApiClientes.Infra.Data.Entities;
using ApiClientes.Services.Requests;
using ApiClientes.Tests;
using ApiClientes.Tests.Config;
using Bogus;
using FluentAssertions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ApiCliente.Tests
{
    public class ClientesTest
    {
        private readonly string _endpoint;

        public ClientesTest()
        {
            _endpoint = ApiConfig.GetEndpoint() + "/clientes";
        }

        [Fact]
        public async Task<ClienteResult> Test_Post_Returns_Created()
        {

            var httpClient = new HttpClient();

            var response = await httpClient.PostAsync(_endpoint, CreateClienteData());

            response
                .StatusCode
                .Should()
                .Be(HttpStatusCode.Created);

            var result = JsonConvert.DeserializeObject<ClienteResult>(response.Content.ReadAsStringAsync().Result);

            return result;
        }

        [Fact]
        public async Task Test_Put_Returns_OK()
        {
            //Realizando o cadastro de um cliente
            var result = await Test_Post_Returns_Created();

            var faker = new Faker("pt_BR");

            //criando os dados para editar o cliente
            var request = new ClientePutRequest
            {
                IdCliente = result.cliente.IdCliente,
                Nome = faker.Person.FullName,
                Email = faker.Person.Email,
                Cpf   = CpfUtils.GerarCpf(),
                DataNascimento = faker.Person.DateOfBirth
            };

            var httpClient = new HttpClient();

            var content = new StringContent
                (JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync(_endpoint, content);

            response
               .StatusCode
               .Should()
               .Be(HttpStatusCode.OK);

        }

        [Fact]
        public async Task Test_Delete_Returns_Ok()
        {
            //Realizando o cadastro de um cliente
            var result = await Test_Post_Returns_Created();

            var httpClient = new HttpClient();

            var response = await httpClient.DeleteAsync(_endpoint + "/" + result.cliente.IdCliente);

            response
               .StatusCode
               .Should()
               .Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Test_GetAll_Returns_Ok()
        {
            //Realizando o cadastro de um cliente
            await Test_Post_Returns_Created();

            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync(_endpoint);

            var result = JsonConvert.DeserializeObject<List<Cliente>>(response.Content.ReadAsStringAsync().Result);

            result.
                Should()
                .NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Test_GetById_Returns_Ok()
        {
            //Realizando o cadastro de um cliente
            var result = await Test_Post_Returns_Created();

            var httpClient = new HttpClient();

            var path = _endpoint + "/" + result.cliente.IdCliente;

            var response = await httpClient.GetAsync(path);

            var resposta = JsonConvert.DeserializeObject<Cliente>(response.Content.ReadAsStringAsync().Result);

            resposta
                .Should()
                .NotBeNull();
        }

        
        private StringContent CreateClienteData()
        {
            var faker = new Faker("pt_BR");

            var request = new ClientePostRequest()
            {
                Nome = faker.Person.FullName,
                Email = faker.Person.Email,
                Cpf = CpfUtils.GerarCpf(),
                DataNascimento = faker.Person.DateOfBirth
            };

            return new StringContent
                (JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        }
    }
    /// <summary>
    /// Classe para capturar o retorno do resultado 
    /// dos testes POST, PUT ou DELETE
    /// </summary>
    public class ClienteResult
    {
        public string message { get; set; }
        public Cliente cliente { get; set; }
    }
}