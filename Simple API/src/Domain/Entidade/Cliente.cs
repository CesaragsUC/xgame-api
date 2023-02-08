namespace Domain.Entidade
{
    public class Cliente : Entity
    {
        public string Nome { get;  set; }
        public string Email { get;  set; }
        public string Cpf { get;  set; }
        public bool Ativo { get;  set; }
        public DateTime DataCadastro { get;  set; }

        public void Inativar()
        {
            Ativo= false;    
        }
        public void Ativar()
        {
            Ativo = true;
        }
    }
}
