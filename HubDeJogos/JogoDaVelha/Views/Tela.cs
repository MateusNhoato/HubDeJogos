using HubDeJogos.JogoDaVelha.Models;

namespace HubDeJogos.JogoDaVelha.Views
{
    public class Tela
    {
        // função para imprimir o tabuleiro
        public void ImprimirTabuleiro(TabuleiroJogoDaVelha tabuleiro)
        {
            Console.Clear();
            Console.WriteLine("\n");
            for (int i = 0; i < tabuleiro.TamanhoDoTabuleiro; i++)
            {
                Console.Write("   ");
                for (int j = 0; j < tabuleiro.TamanhoDoTabuleiro; j++)
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    if (tabuleiro.MatrizTabuleiro[i, j].Trim() == "X")
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                    else if (tabuleiro.MatrizTabuleiro[i, j].Trim() == "O")
                        Console.ForegroundColor = ConsoleColor.DarkRed;

                    Console.Write(tabuleiro.MatrizTabuleiro[i, j]);
                    Console.ForegroundColor = aux;
                }
                Console.WriteLine();
            }
        }



    }
}
