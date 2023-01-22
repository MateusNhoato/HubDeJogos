using HubDeJogos.Models;

namespace HubDeJogos.JogoDaVelha.Views
{
    public class Tela
    {
        // função para imprimir o tabuleiro
        public void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            for (int i = 0; i < tabuleiro.Tamanho; i++)
            {
                Console.Write("   ");
                for (int j = 0; j < tabuleiro.Tamanho; j++)
                {
                    ConsoleColor aux = Console.ForegroundColor;

                    if (tabuleiro.TabuleiroMatriz[i, j].Trim() == "X")
                        Console.ForegroundColor = ConsoleColor.Black;
                    else if (tabuleiro.TabuleiroMatriz[i, j].Trim() == "O")
                        Console.ForegroundColor = ConsoleColor.DarkRed;

                    Console.Write(tabuleiro.TabuleiroMatriz[i, j]);
                    Console.ForegroundColor = aux;
                }
                Console.WriteLine();
            }
        }
    }
}
