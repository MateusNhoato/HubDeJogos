using HubDeJogos.Xadrez.Models.Enums;
using HubDeJogos.Xadrez.Models.Tabuleiro;

namespace HubDeJogos.Xadrez.Models.Pecas
{
    public class Cavalo : Peca
    {
        public Cavalo(TabuleiroDeXadrez tab, Cor cor ): base(cor, tab) { }

        public override string ToString()
        {
            return "C";
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

            pos.definirValores(Posicao.Linha - 1, Posicao.Coluna - 2);
            if(Tab.posicaoValida(pos) && podeMover(pos))
                matriz[pos.Linha, pos.Coluna] = true;

            pos.definirValores(Posicao.Linha - 2, Posicao.Coluna - 1);
            if (Tab.posicaoValida(pos) && podeMover(pos))
                matriz[pos.Linha, pos.Coluna] = true;

            pos.definirValores(Posicao.Linha - 2, Posicao.Coluna + 1);
            if (Tab.posicaoValida(pos) && podeMover(pos))
                matriz[pos.Linha, pos.Coluna] = true;

            pos.definirValores(Posicao.Linha - 1, Posicao.Coluna + 2);
            if (Tab.posicaoValida(pos) && podeMover(pos))
                matriz[pos.Linha, pos.Coluna] = true;

            pos.definirValores(Posicao.Linha + 1, Posicao.Coluna + 2);
            if (Tab.posicaoValida(pos) && podeMover(pos))
                matriz[pos.Linha, pos.Coluna] = true;

            pos.definirValores(Posicao.Linha + 2, Posicao.Coluna + 1);
            if (Tab.posicaoValida(pos) && podeMover(pos))
                matriz[pos.Linha, pos.Coluna] = true;

            pos.definirValores(Posicao.Linha + 2, Posicao.Coluna - 1);
            if (Tab.posicaoValida(pos) && podeMover(pos))
                matriz[pos.Linha, pos.Coluna] = true;

            pos.definirValores(Posicao.Linha + 1, Posicao.Coluna - 2);
            if (Tab.posicaoValida(pos) && podeMover(pos))
                matriz[pos.Linha, pos.Coluna] = true;

            return matriz;
        }
    }
}
