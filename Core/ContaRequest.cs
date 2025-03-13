public class ContaRequest
{
    public string? Name { get; set; }
    public decimal Saldo { get; set; }
    public bool Ativo { get; set; }

    public Conta CriarConta(int id)
    {
        return new Conta()
        {
            Id = id,
            Name = Name,
            Saldo = Saldo,
            Ativo = Ativo
        };
    }

    public Conta AlterarConta(Conta conta)
    {
        conta.Name = Name;
        conta.Saldo = Saldo;
        conta.Ativo = Ativo;

        return conta;
    }
}