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
            for (int i = 0; i < tabuleiro.Tamanho; i++)
            {
                Console.Write("   ");
                for (int j = 0; j < tabuleiro.Tamanho; j++)
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    string stringAux = tabuleiro.TabuleiroMatriz[i, j] as string;

                    if (stringAux.Trim() == "X")
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                    else if (stringAux.Trim() == "O")
                        Console.ForegroundColor = ConsoleColor.DarkRed;

                    Console.Write(tabuleiro.TabuleiroMatriz[i, j]);
                    Console.ForegroundColor = aux;
                }
                Console.WriteLine();
            }
        }



    }
}
