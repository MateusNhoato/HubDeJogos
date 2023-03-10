using HubDeJogos.BatalhaNaval.Models;
using HubDeJogos.Models;

namespace HubDeJogos.BatalhaNaval.Views
{
    public class Tela
    {
        private readonly string _player = @"
   _______  ___      _______  __   __  _______  ______     
  |       ||   |    |   _   ||  | |  ||       ||    _ |    
  |    _  ||   |    |  |_|  ||  |_|  ||    ___||   | ||    
  |   |_| ||   |    |       ||       ||   |___ |   |_||_   
  |    ___||   |___ |       ||_     _||    ___||    __  |  
  |   |    |       ||   _   |  |   |  |   |___ |   |  | |  
  |___|    |_______||__| |__|  |___|  |_______||___|  |_|";
        private readonly string _1 = @"
                           ____  
                          |    | 
                           |   | 
                           |   | 
                           |   | 
                           |   | 
                           |___|";
        private readonly string _2 = @"
                           _______ 
                          |       |
                          |____   |
                           ____|  |
                          | ______|
                          | |_____ 
                          |_______|";

        public void ImprimirTabuleiro(TabuleiroBatalhaNaval tabuleiro, bool[,] tirosPossiveis)
        {
            Console.Clear();
            ConsoleColor bgAux = Console.BackgroundColor;
            ConsoleColor fgAux = Console.ForegroundColor;
            Random random = new Random();

            Console.WriteLine("     a  b  c  d  e  f  g  h  i  j");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("   ┌──────────────────────────────┐");
            Console.ForegroundColor = fgAux;
            for (int i = 0; i < tabuleiro.Tamanho; i++)
            {
                if (i <= 8)
                    Console.Write($"  {i + 1}");
                else
                    Console.Write($" {i + 1}");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("│");
                Console.ForegroundColor = fgAux;
                for (int j = 0; j < tabuleiro.Tamanho; j++)
                {
                    int ondinha = random.Next(1, 11);
                    string aux = "   ";
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    if (tabuleiro.MatrizNavios[i, j] != null)
                    {
                        if (tabuleiro.MatrizNavios[i, j].Destruida)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkRed;
                            ondinha = 5;
                        }

                    }
                    if (tirosPossiveis[i, j])
                    {
                        ondinha = 5;
                        aux = " X ";
                    }
                    Console.ForegroundColor = ConsoleColor.Black;
                    if (ondinha <= 2)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.Write(" ~ ");
                    }

                    else
                        Console.Write(aux);

                    Console.ForegroundColor = fgAux;
                }
                Console.BackgroundColor = bgAux;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("│");
                Console.ForegroundColor = fgAux;

                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("   └──────────────────────────────┘");
            Console.ForegroundColor = fgAux;
            if (tabuleiro.NumeroDeNavios > 0)
                Console.WriteLine($"\n  {tabuleiro.NumeroDeNavios} Navios Restantes");
        }
        public void ImprimirTabuleiro(TabuleiroBatalhaNaval tabuleiro, Posicao pos)
        {
            Console.Clear();
            ConsoleColor bgAux = Console.BackgroundColor;
            ConsoleColor fgAux = Console.ForegroundColor;

            int linha = pos.Linha;
            int coluna = pos.Coluna;


            Console.WriteLine("     a  b  c  d  e  f  g  h  i  j");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("   ┌──────────────────────────────┐");
            Console.ForegroundColor = fgAux;
            for (int i = 0; i < tabuleiro.Tamanho; i++)
            {
                if (i <= 8)
                    Console.Write($"  {i + 1}");
                else
                    Console.Write($" {i + 1}");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("│");
                Console.ForegroundColor = fgAux;

                for (int j = 0; j < tabuleiro.Tamanho; j++)
                {
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    int ondinha = new Random().Next(1, 11);
                    if (tabuleiro.MatrizNavios[i, j] != null)
                    {
                        if (tabuleiro.MatrizNavios[i, j].Destruida)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkRed;
                            ondinha = 5;
                        }
                    }
                    Console.ForegroundColor = ConsoleColor.Black;
                    if (i == linha && j == coluna)
                        Console.Write("-+-");
                    else if (i == linha)
                        Console.Write("---");
                    else if (j == coluna)
                        Console.Write(" | ");
                    else
                    {
                        if (ondinha <= 2)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.Write(" ~ ");
                            Console.ForegroundColor = fgAux;
                        }
                        else
                            Console.Write("   ");
                    }
                }
                Console.BackgroundColor = bgAux;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("│");
                Console.ForegroundColor = fgAux;

                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("   └──────────────────────────────┘");
            Console.ForegroundColor = fgAux;
            if (tabuleiro.NumeroDeNavios > 0)
                Console.WriteLine($"\n  {tabuleiro.NumeroDeNavios} Navios Restantes");


        }
        public void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            ConsoleColor bgAux = Console.BackgroundColor;
            ConsoleColor fgAux = Console.ForegroundColor;
            ConsoleColor azulMar = ConsoleColor.Cyan;

            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("  ┌────────────────────┐");
            Console.ForegroundColor = fgAux;
            for (int i = 0; i < tabuleiro.Tamanho; i++)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("  │");
                Console.ForegroundColor = fgAux;
                for (int j = 0; j < tabuleiro.Tamanho; j++)
                {
                    Console.BackgroundColor = azulMar;

                    string aux = tabuleiro.TabuleiroMatriz[i, j];
                    if (aux.Trim() == "x")
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                    else if (aux.Trim() == "i")
                        Console.BackgroundColor = ConsoleColor.Black;

                    Console.Write("  ");
                    Console.BackgroundColor = bgAux;
                }
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("│");
                Console.ForegroundColor = fgAux;
            }
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("  └────────────────────┘");
            Console.ForegroundColor = fgAux;
        }

        public void ImprimirVezDoJogador(int jogador)
        {
            Console.Clear();
            Console.WriteLine(Utilidades.Comunicacao.Linha);
            Console.WriteLine(_player);
            if (jogador == 1)
                Console.WriteLine(_1);
            else
                Console.WriteLine(_2);
            Console.WriteLine(Utilidades.Comunicacao.Linha);
            Thread.Sleep(1000);
        }
    }
}
