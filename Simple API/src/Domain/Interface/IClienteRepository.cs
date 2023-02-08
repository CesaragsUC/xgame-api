using Domain.Entidade;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{


    public interface IClienteRepository :  IRepository<Cliente>
    {
        Task<Cliente> ObterClientePorId(Guid id);
        Task<IEnumerable<Cliente>> ObterClientes();

        Task<ValidationResult> Add(Cliente cliente);
        Task<ValidationResult> Update(Cliente cliente);
    }
}
