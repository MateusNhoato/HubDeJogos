﻿using HubDeJogos.Xadrez.Models.Enums;
using HubDeJogos.Xadrez.Models.Tabuleiro;

namespace HubDeJogos.Xadrez.Models.Pecas
{
    public class Dama : Peca
    {
        public Dama(TabuleiroDeXadrez tab, Cor cor) : base(cor, tab) { }


        public override string ToString()
        {
            return "D";
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
                pos.definirValores(pos.Linha - 1, pos.Coluna - 1);
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

            // acima
            pos.definirValores(Posicao.Linha - 1, Posicao.Coluna);
            while (Tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
                if (Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor)
                    break;
                pos.Linha = pos.Linha - 1;
            }

            // abaixo
            pos.definirValores(Posicao.Linha + 1, Posicao.Coluna);
            while (Tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
                if (Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor)
                    break;
                pos.Linha = pos.Linha + 1;
            }

            // direita
            pos.definirValores(Posicao.Linha, Posicao.Coluna + 1);
            while (Tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
                if (Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor)
                    break;
                pos.Coluna = pos.Coluna + 1;
            }

            // esquerda
            pos.definirValores(Posicao.Linha, Posicao.Coluna - 1);
            while (Tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
                if (Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor)
                    break;
                pos.Coluna = pos.Coluna - 1;
            }
            return matriz;

        }
    }
}