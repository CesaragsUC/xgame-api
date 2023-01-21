using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidade
{
    public abstract  class Entity
    {
        public Guid Id { get; set; }


        [NotMapped]
        public ValidationResult ValidationResult { get;  set; }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }
        public Entity()
        {
            Id = Guid.NewGuid();
        }
        
    }
}
