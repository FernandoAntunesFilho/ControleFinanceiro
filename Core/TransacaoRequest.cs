public class TransacaoRequest
{
    public DateTime Data { get; set; }
    public string? Descricao { get; set; }
    public string? Categoria { get; set; }
    public int ContaId { get; set; }
    public decimal Valor { get; set; }
    public bool Consolidado { get; set; }

    public Transacao CriarTransacao(int id, Conta conta)
    {
        return new Transacao()
        {
            Id = id,
            Data = Data,
            Descricao = Descricao,
            Categoria = Categoria,
            Conta = conta,
            Valor = Valor,
            Consolidado = false
        };
    }

    public Transacao AtualizarTransacao(int id, Conta conta, Transacao transacao)
    {
        transacao.Data = Data;
        transacao.Descricao = Descricao;
        transacao.Categoria = Categoria;
        transacao.Conta = conta;
        transacao.Valor = Valor;
        transacao.Consolidado = Consolidado;

        return transacao;
    }
}