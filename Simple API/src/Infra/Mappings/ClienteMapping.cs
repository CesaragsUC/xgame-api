using Domain.Entidade;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Mappings
{
    public class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(p => p.Id);


            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(c => c.Nome)
                .HasColumnType("varchar(50)");

            builder.Property(c => c.Email)
                 .HasColumnType("varchar(50)");

            builder.Property(c => c.Cpf)
                .HasColumnType("varchar(50)");

            builder.ToTable("Clientes");
        }
    }
}
