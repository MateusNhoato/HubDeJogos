﻿using HubDeJogos.Hub.Models.Interfaces;
using HubDeJogos.Xadrez.Models.Pecas;

namespace HubDeJogos.Xadrez.Models.Tabuleiro
{
    public class TabuleiroDeXadrez : HubDeJogos.Models.Tabuleiro, IAlteraTabuleiro
    {
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        private Peca[,] Pecas;

        public TabuleiroDeXadrez(int tamanho)
        {
            Tamanho = tamanho;
            Linhas = tamanho;
            Colunas = tamanho;
            Pecas = new Peca[Linhas, Colunas];           
        }

        public Peca peca(int linha, int coluna)
        {
            return Pecas[linha, coluna];
        }

        public Peca peca(Posicao pos)
        {
            return Pecas[pos.Linha, pos.Coluna];
        }

        public bool existePeca(Posicao pos)
        {
            validarPosicao(pos);
            return peca(pos) != null;
        }
        public void colocarPeca(Peca p, Posicao pos)
        {
            if (existePeca(pos))
                throw new TabuleiroException("Já existe uma peça nessa posição!");

            Pecas[pos.Linha, pos.Coluna] = p;
            p.Posicao = pos;
        }

        public Peca retirarPeca(Posicao pos)
        {
            if (peca(pos) == null)
                return null;
            Peca aux = peca(pos);
            aux.Posicao = null;
            Pecas[pos.Linha, pos.Coluna] = null;
            return aux;

        }

        public bool posicaoValida(Posicao pos)
        {
            if (pos.Linha < 0 || pos.Linha >= Linhas) return false;
            if (pos.Coluna < 0 || pos.Coluna >= Colunas) return false;
            return true;
        }
        public void validarPosicao(Posicao pos)
        {
            if (!posicaoValida(pos))
                throw new TabuleiroException("Posição inválida!");
        }

        public void AlterarTabuleiroMatrizParaRegistro()
        {
            TabuleiroMatriz = new string[Tamanho,Tamanho];

            for (int i=0; i<Tamanho; i++)
            {
                for(int j=0; j<Tamanho; i++)
                {
                    TabuleiroMatriz[i,j] = Pecas[i,j].ToString();
                }
            }           
        }
    }

}