using HubDeJogos.Xadrez.Models;
using HubDeJogos.Xadrez.Models.Tabuleiro;
using HubDeJogos.Xadrez.Models.Pecas;
using HubDeJogos.Xadrez.Models.Enums;
using HubDeJogos.Models;

namespace HubDeJogos.Xadrez.Views
{
    public class Tela
    {
        public void ImprimirPartida(Services.Xadrez partida)
        {
            ImprimirTabuleiro(partida.Tabuleiro);
            ImprimirPecasCapturadas(partida);

            Console.WriteLine("\nTurno: " + partida.Turno);

            if (!partida.Terminada)
            {
                Console.WriteLine("Aguardando jogada: " + partida.CorAtual);
                if (partida.Xeque)
                    Console.WriteLine("Xeque!");
            }
            else if (partida.Empate)
            {
                Console.WriteLine("Empate!");
            }
            else
            {
                if (!partida.Render)
                    Console.WriteLine("XEQUEMATE!");
                Console.WriteLine("Vencedor: " + partida.CorAtual);
            }

        }

        public void ImprimirPecasCapturadas(Services.Xadrez partida)
        {
            Console.WriteLine("\nPeças capturadas: ");
            Console.Write("Brancas: ");
            ImprimirConjunto(partida.PecasCapturadas(Cor.Branca));

            Console.Write("Pretas: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Black;
            ImprimirConjunto(partida.PecasCapturadas(Cor.Preta));
            Console.ForegroundColor = aux;
        }

        public void ImprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[");
            foreach (Peca p in conjunto)
            {
                Console.Write(p + " ");
            }
            Console.WriteLine("]");
        }


        public void ImprimirTabuleiro(TabuleiroDeXadrez tabuleiro)
        {

            ConsoleColor aux = Console.BackgroundColor;

            Console.WriteLine("   a  b  c  d  e  f  g  h");
            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tabuleiro.Colunas; j++)
                {
                    if (i % 2 == 0)
                    {
                        //padrão 1
                        if (j % 2 == 0)
                            Console.BackgroundColor = ConsoleColor.Magenta;
                        else
                            Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    }
                    else
                    {
                        //padrão 2
                        if (j % 2 == 0)
                            Console.BackgroundColor = ConsoleColor.DarkMagenta;
                        else
                            Console.BackgroundColor = ConsoleColor.Magenta;
                    }
                    ImprimirPeca(tabuleiro.Peca(i, j));
                }
                Console.WriteLine();
                Console.BackgroundColor = aux;
            }
        }

        public void ImprimirTabuleiro(TabuleiroDeXadrez tab, bool[,] posicoesPossiveis)
        {
            Console.WriteLine("   a  b  c  d  e  f  g  h");
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.Gray;
            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.Colunas; j++)
                {
                    if (i % 2 == 0)
                    {
                        //padrão 1
                        if (j % 2 == 0)
                            Console.BackgroundColor = ConsoleColor.Magenta;
                        else
                            Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    }
                    else
                    {
                        //padrão 2
                        if (j % 2 == 0)
                            Console.BackgroundColor = ConsoleColor.DarkMagenta;
                        else
                            Console.BackgroundColor = ConsoleColor.Magenta;
                    }

                    if (posicoesPossiveis[i, j])
                        Console.BackgroundColor = fundoAlterado;


                    ImprimirPeca(tab.Peca(i, j));
                    Console.BackgroundColor = fundoOriginal;
                }
                Console.WriteLine();
            }
            Console.BackgroundColor = fundoOriginal;
        }

        public void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {

            ConsoleColor bgAux = Console.BackgroundColor;
            ConsoleColor fgAux = Console.ForegroundColor;
         
            for (int i = 0; i < tabuleiro.Tamanho; i++)
            {            
                for (int j = 0; j < tabuleiro.Tamanho; j++)
                {
                    if (i % 2 == 0)
                    {
                        //padrão 1
                        if (j % 2 == 0)
                            Console.BackgroundColor = ConsoleColor.Magenta;
                        else
                            Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    }
                    else
                    {
                        //padrão 2
                        if (j % 2 == 0)
                            Console.BackgroundColor = ConsoleColor.DarkMagenta;
                        else
                            Console.BackgroundColor = ConsoleColor.Magenta;
                    }
                    if (tabuleiro.TabuleiroMatriz[i, j].StartsWith('x'))
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                    else
                        Console.ForegroundColor = ConsoleColor.Black;

                    Console.Write(tabuleiro.TabuleiroMatriz[i,j].Replace('x', ' '));
                    Console.ForegroundColor = fgAux;
                    Console.BackgroundColor = bgAux;
                }
                Console.WriteLine();
                
            }
        }








        public PosicaoXadrez LerPosicaoXadrez(string s)
        {
            s = s.ToLower();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(coluna, linha);
        }


        public void ImprimirPeca(Peca peca)
        {
            ConsoleColor aux = Console.ForegroundColor;

            if (peca == null)
                Console.Write("   ");
            else
            {
                if (peca.Cor == Cor.Branca)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write($" {peca} ");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write($" {peca} ");
                }
            }
            Console.ForegroundColor = aux;
        }
    }
}
