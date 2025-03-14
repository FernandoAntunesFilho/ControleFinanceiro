public class ContaRequest
{
    public string? Name { get; set; }
    public decimal SaldoInicial { get; set; }
    public bool Ativo { get; set; }

    public Conta CriarConta(int id)
    {
        return new Conta()
        {
            Id = id,
            Name = Name,
            SaldoInicial = SaldoInicial,
            Ativo = Ativo
        };
    }

    public Conta AlterarConta(Conta conta)
    {
        conta.Name = Name;
        conta.SaldoInicial = SaldoInicial;
        conta.Ativo = Ativo;

        return conta;
    }
}