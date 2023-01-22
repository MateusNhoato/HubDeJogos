using HubDeJogos.Hub.Models.Interfaces;
using HubDeJogos.Models;

namespace HubDeJogos.JogoDaVelha.Models
{
    public class TabuleiroJogoDaVelha : Tabuleiro, IAlteraTabuleiro
    {
        public List<string>? JogadasPossiveis { get; protected set; }

        // construtor para tabuleiro novo
        public TabuleiroJogoDaVelha(int tamanho)
        {
            // configurando o tabuleiro e gerando lista de jogadas possíveis
            Tamanho = tamanho * 2 - 1;
            TabuleiroMatriz = GerarTabuleiro();
            JogadasPossiveis = ListarJogadasPossiveis();

        }
        // construtor para tabuleiro de registro 
        public TabuleiroJogoDaVelha(string[,] matrizTabuleiro, int tamanho) : base(matrizTabuleiro, tamanho)
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
                            matrizTabuleiro[i, j] = "║";
                    }
                    else
                    {
                        if (j % 2 != 0)
                            matrizTabuleiro[i, j] = "╬";
                        else
                            matrizTabuleiro[i, j] = "═══";
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

        public void AlterarTabuleiroMatrizParaRegistro()
        {
            for (int i = 0; i < Tamanho; i++)
            {
                for (int j = 0; j < Tamanho; j++)
                {
                    if (int.TryParse(TabuleiroMatriz[i, j], out _))
                        TabuleiroMatriz[i, j] = "   ";
                }
            }

        }


    }
}
