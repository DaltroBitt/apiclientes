using ApiClientes.Infra.Data.Contexts;
using ApiClientes.Infra.Data.Entities;
using ApiClientes.Infra.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiClientes.Infra.Data.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        //atributo
        private readonly SqlServerContext _sqlServerContext;

        //método construtor para injeção de dependência (inicialização)
        public ClienteRepository(SqlServerContext sqlServerContext)
        {
            _sqlServerContext = sqlServerContext;
        }

        public void Create(Cliente entity)
        {
            _sqlServerContext.Cliente.Add(entity);
            _sqlServerContext.SaveChanges(); 
        }

        public void Update(Cliente entity)
        {
            _sqlServerContext.Entry(entity).State = EntityState.Modified;
            _sqlServerContext.SaveChanges();
        }

        public void Delete(Cliente entity)
        {
            _sqlServerContext.Cliente.Remove(entity);
            _sqlServerContext.SaveChanges();
        }

        public List<Cliente> GetAll()
        {
            return _sqlServerContext.Cliente
                .OrderBy(c => c.Nome)
                .ToList();
        }

        public Cliente GetById(Guid id)
        {
            return _sqlServerContext.Cliente.Find(id);
        }


        public Cliente GetByCpf(string cpf)
        {
            return _sqlServerContext.Cliente
                .FirstOrDefault(c => c.Cpf.Equals(cpf));
        }

        public Cliente GetByEmail(string email)
        {
            return _sqlServerContext.Cliente
                .FirstOrDefault(c => c.Email.Equals(email));
        }

        
        
    }
}
