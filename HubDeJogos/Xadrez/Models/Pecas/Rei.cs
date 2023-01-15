using HubDeJogos.Xadrez.Models.Enums;
using HubDeJogos.Xadrez.Models.Tabuleiro;
using HubDeJogos.Xadrez.Services;



namespace HubDeJogos.Xadrez.Models.Pecas
{
    public class Rei : Peca
    {
        private Services.Xadrez partida;
        public Rei(TabuleiroDeXadrez tab, Cor cor, Services.Xadrez partida ) : base(cor, tab)
        {
            this.partida = partida;
        }

        public override string ToString()
        {
            return "R";
        }

        private bool podeMover(Posicao pos)
        {
            Peca p = Tab.peca(pos);
            return p == null || p.Cor != this.Cor;
        }

        private bool testeTorreParaRoque(Posicao pos)
        {
            Peca p = Tab.peca(pos);
            return p != null && p is Torre && p.Cor == Cor && p.QteMovimentos == 0;
        }
        public override bool[,] movimentosPossiveis()
        {
            bool[,] matriz = new bool[Tab.Linhas, Tab.Colunas];
            Posicao pos = new Posicao(0, 0);

            // acima
            pos.definirValores(Posicao.Linha - 1, Posicao.Coluna);

            if(Tab.posicaoValida(pos) && podeMover(pos))
                matriz[pos.Linha, pos.Coluna] = true;

            // ne
            pos.definirValores(Posicao.Linha - 1, Posicao.Coluna +1);
            if (Tab.posicaoValida(pos) && podeMover(pos))
                matriz[pos.Linha, pos.Coluna] = true;

            // direita
            pos.definirValores(Posicao.Linha, Posicao.Coluna + 1 );
            if (Tab.posicaoValida(pos) && podeMover(pos))
                matriz[pos.Linha, pos.Coluna] = true;

            // se
            pos.definirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            if (Tab.posicaoValida(pos) && podeMover(pos))
                matriz[pos.Linha, pos.Coluna] = true;

            // abaixo
            pos.definirValores(Posicao.Linha + 1, Posicao.Coluna);
            if (Tab.posicaoValida(pos) && podeMover(pos))
                matriz[pos.Linha, pos.Coluna] = true;

            // so
            pos.definirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            if (Tab.posicaoValida(pos) && podeMover(pos))
                matriz[pos.Linha, pos.Coluna] = true;

            // esquerda
            pos.definirValores(Posicao.Linha, Posicao.Coluna - 1);
            if (Tab.posicaoValida(pos) && podeMover(pos))
                matriz[pos.Linha, pos.Coluna] = true;

            // no
            pos.definirValores(Posicao.Linha - 1, Posicao.Coluna -1);
            if (Tab.posicaoValida(pos) && podeMover(pos))
                matriz[pos.Linha, pos.Coluna] = true;

            // #jogadaespecial roque
            if(QteMovimentos == 0 && !partida.Xeque)
            {
                // #jogadaespecial roque pequeno
                Posicao posT1 = new Posicao(Posicao.Linha, Posicao.Coluna + 3);
                if(testeTorreParaRoque(posT1))
                {
                    Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna + 2);
                    if (Tab.peca(p1) == null && Tab.peca(p2) == null)
                        matriz[Posicao.Linha, Posicao.Coluna +2] = true;
                }

                // #jogadaespecial roque grande
                Posicao posT2 = new Posicao(Posicao.Linha, Posicao.Coluna - 4);
                if (testeTorreParaRoque(posT2))
                {
                    Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna - 2);
                    Posicao p3 = new Posicao(Posicao.Linha, Posicao.Coluna - 3);
                    if (Tab.peca(p1) == null && Tab.peca(p2) == null && Tab.peca(p3) == null)
                        matriz[Posicao.Linha, Posicao.Coluna - 2] = true;
                }
            }

            return matriz;
        }
    }
}
