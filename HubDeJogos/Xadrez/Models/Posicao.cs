namespace HubDeJogos.Xadrez.Models;

public class Posicao
{
    public int Linha { get; set; }
    public int Coluna { get; set; }

    public Posicao(int linha, int coluna)
    {
        Linha = linha;
        Coluna = coluna;
    }

    public void DefinirValores(int linha, int coluna)
    {
        Linha = linha;
        Coluna = coluna;
    }
    public override string ToString()
    {
        return Linha + ", " + Coluna;
    }

    public PosicaoXadrez ToPosicaoXadrez()
    {
        char coluna = 'a';
        switch (Coluna)
        {
            case 1:
                coluna = 'b';
                break;
            case 2:
                coluna = 'c';
                break;
            case 3:
                coluna = 'd';
                break;
            case 4:
                coluna = 'e';
                break;
            case 5:
                coluna = 'f';
                break;
            case 6:
                coluna = 'g';
                break;
            case 7:
                coluna = 'h';
                break;

        }
        return new PosicaoXadrez(coluna, 8 - Linha);
    }

}
