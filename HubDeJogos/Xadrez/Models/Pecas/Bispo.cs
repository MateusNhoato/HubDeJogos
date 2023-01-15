using HubDeJogos.Xadrez.Models.Enums;
using HubDeJogos.Xadrez.Models.Tabuleiro;

namespace HubDeJogos.Xadrez.Models.Pecas;

public class Bispo : Peca
{
    public Bispo(TabuleiroDeXadrez tab, Cor cor) : base(cor,tab)
    { }

    public override string ToString()
    {
        return "B";
    }

    private bool podeMover(Posicao pos)
    {
        Peca p = Tab.peca(pos);
        return p == null || p.Cor != Cor;
    }

    public override bool[,] movimentosPossiveis()
    {
        bool[,] matriz = new bool[Tab.Linhas, Tab.Colunas];
        Posicao pos = new Posicao(0, 0);

        //NO
        pos.definirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
        while (Tab.posicaoValida(pos) && podeMover(pos))
        {
            matriz[pos.Linha, pos.Coluna] = true;

            if (Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor)
                break;
            pos.definirValores(pos.Linha -1, pos.Coluna- 1);
        }

        // NE
        pos.definirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
        while (Tab.posicaoValida(pos) && podeMover(pos))
        {
            matriz[pos.Linha, pos.Coluna] = true;

            if (Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor)
                break;
            pos.definirValores(pos.Linha - 1, pos.Coluna + 1);
        }

        // SE
        pos.definirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
        while (Tab.posicaoValida(pos) && podeMover(pos))
        {
            matriz[pos.Linha, pos.Coluna] = true;

            if (Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor)
                break;
            pos.definirValores(pos.Linha + 1, pos.Coluna + 1);
        }

        //SO
        pos.definirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
        while (Tab.posicaoValida(pos) && podeMover(pos))
        {
            matriz[pos.Linha, pos.Coluna] = true;

            if (Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor)
                break;
            pos.definirValores(pos.Linha + 1, pos.Coluna - 1);
        }

        return matriz;
    }


}
