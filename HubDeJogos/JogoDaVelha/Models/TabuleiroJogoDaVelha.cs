﻿using HubDeJogos.Models;

namespace HubDeJogos.JogoDaVelha.Models
{
    public class TabuleiroJogoDaVelha : Tabuleiro
    {
        public override object[,] TabuleiroMatriz { get; protected set; }
        public List<string>? JogadasPossiveis { get; protected set; }
        public override int Tamanho { get; protected set; }
   


        // construtor para tabuleiro novo
        public TabuleiroJogoDaVelha(int tamanho)
        {
            // configurando o tabuleiro e gerando lista de jogadas possíveis
            Tamanho = tamanho * 2 - 1;
            TabuleiroMatriz = GerarTabuleiro();
            JogadasPossiveis = ListarJogadasPossiveis();

        }
        // construtor para tabuleiro de registro 
        public TabuleiroJogoDaVelha(object[,] matrizTabuleiro, int tamanho) : base(matrizTabuleiro, tamanho)
        {
            TabuleiroMatriz = matrizTabuleiro;
            Tamanho = (tamanho + 1) / 2;
        }

        

        // função para gerar o tabuleiro de 3 até 10 
        private string[,] GerarTabuleiro()
        {
            int cont = 1;
            string[,] matrizTabuleiro = new string[Tamanho, Tamanho];

            for (int i = 0; i < Tamanho; i++)
            {
                for (int j = 0; j < Tamanho; j++)
                {
                    if (i % 2 == 0)
                    {
                        if (j % 2 == 0)
                        {
                            if (cont > 9)
                                matrizTabuleiro[i, j] = $" {cont}";
                            else if (cont > 99)
                                matrizTabuleiro[i, j] = $"{cont}";
                            else
                                matrizTabuleiro[i, j] = $" {cont} ";
                            cont++;
                        }
                        else
                            matrizTabuleiro[i, j] = "|";
                    }
                    else
                    {
                        if (j % 2 != 0)
                            matrizTabuleiro[i, j] = "+";
                        else
                            matrizTabuleiro[i, j] = "---";
                    }

                }
            }
           return matrizTabuleiro;
        }

        // função para criar lista de jogadas possíveis
        private List<string> ListarJogadasPossiveis()
        {
            List<string> jogadasPossiveis = new List<string>();
            for (int i = 1; i <= Math.Pow((Tamanho + 1) / 2, 2); i++)
            {
                jogadasPossiveis.Add($"{i}");
            }
            return jogadasPossiveis;
        }

        public override string TabuleiroParaImpressao()
        {
            string tabuleiroParaImpressao = string.Empty;

            for (int i = 0; i < Tamanho; i++)
            {
                for (int j = 0; j < Tamanho; j++)
                {
                    string stringAux = TabuleiroMatriz[i, j] as string;
                    if (int.TryParse(stringAux, out int n))
                        tabuleiroParaImpressao += "   ;";
                    else
                        tabuleiroParaImpressao += $"{TabuleiroMatriz[i, j]};";
                }
            }
            return tabuleiroParaImpressao;
        }


    }
}
