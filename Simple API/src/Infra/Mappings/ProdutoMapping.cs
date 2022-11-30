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
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.Id);


            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(c => c.Imagem)
                .HasColumnType("varchar(max)");

            //1:1
            builder.HasOne(c => c.Categoria);

            builder.ToTable("Produtos");
        }
    }
}
