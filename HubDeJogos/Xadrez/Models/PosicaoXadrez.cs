using HubDeJogos.Xadrez.Models.Enums;
namespace HubDeJogos.Xadrez.Models;

public class PosicaoXadrez
{
    public char Coluna { get; set; }
    public int Linha { get; set;}
    

    public PosicaoXadrez(char coluna, int linha)
    {
        Coluna = coluna;
        Linha = linha;
    }

    public override string ToString()
    {
        return "" + Coluna + Linha;
    }

    public Posicao toPosicao()
    {
        return new Posicao(8 - Linha, Coluna - 'a');
    }
}
