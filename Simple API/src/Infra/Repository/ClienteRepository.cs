using Domain.Entidade;
using Domain.Interface;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repository
{
    public class ClienteRepository : Repository<Cliente> , IClienteRepository
    {
        private readonly XGamesContext _db;
        private ValidationResult ValidationResult;

        public ClienteRepository(XGamesContext db) : base(db)
        {
            _db = db;
            ValidationResult = new ValidationResult();
        }

        public async Task<ValidationResult> Add(Cliente cliente)
        {
            _db.Clientes.Add(cliente);
            var result =  _db.SaveChanges();
            if(result  == 0) AdicionarErro("Houve um erro ao persistir os dados");
            return ValidationResult;
        }

        public async Task<ValidationResult> Update(Cliente cliente)
        {
            _db.Clientes.Update(cliente);
            var result = _db.SaveChanges();
            if (result == 0) AdicionarErro("Houve um erro ao autlizar os dados");
            return ValidationResult;
        }
        public async Task<Cliente> ObterClientePorId(Guid id)
        {
            return await Db.Clientes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Cliente>> ObterClientes()
        {
            return await Db.Clientes
                .AsNoTracking().ToListAsync();
        }
        protected void AdicionarErro(string mensagem)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, mensagem));
        }

    }
}
