using ApiClientes.Infra.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiClientes.Infra.Data.Mappings
{
    /// <summary>
    /// Classe de mapeamento para a entidade Cliente
    /// </summary>
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            //nome da tabela do banco de dados
            builder.ToTable("CLIENTE");

            //mapear o campo chave primária
            builder.HasKey(c => c.IdCliente);

            //mapear cada campo da tabela
            builder.Property(c => c.IdCliente)
                .HasColumnName("IDCLIENTE");

            builder.Property(c => c.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(c => c.Email)
                .HasColumnName("EMAIL")
                .HasMaxLength(250)
                .IsRequired();

            builder.HasIndex(c => c.Email)
                .IsUnique();

            builder.Property(c => c.Cpf)
                .HasColumnName("CPF")
                .HasMaxLength(11)
                .IsRequired();

            builder.HasIndex(c => c.Cpf)
                .IsUnique();

            builder.Property(c => c.DataNascimento)
                .HasColumnName("DATANASCIMENTO")
                .IsRequired();  
                
        }

    }
}
