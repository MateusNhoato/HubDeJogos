using HubDeJogos.Models;
using HubDeJogos.Xadrez.Models;
using HubDeJogos.Xadrez.Models.Enums;
using HubDeJogos.Xadrez.Models.Pecas;
using HubDeJogos.Xadrez.Models.Tabuleiro;
using HubDeJogos.Xadrez.Repositories;
using Utilidades;

namespace HubDeJogos.Xadrez.Views
{
    public class Tela
    {
        public void ImprimirPartida(Services.Xadrez partida, bool tutorial)
        {
            ImprimirTabuleiro(partida.Tabuleiro);

            ConsoleColor aux = Console.ForegroundColor;
            ConsoleColor cor = (partida.CorAtual == Cor.Brancas) ? ConsoleColor.White : ConsoleColor.Black;
            ImprimirPecasCapturadas(partida);

            string nomeJogador = (partida.CorAtual == Cor.Brancas) ? partida.Jogador1.NomeDeUsuario : partida.Jogador2.NomeDeUsuario;


            Console.ForegroundColor = cor;
            Console.WriteLine("\n  Turno: " + partida.Turno);
            //efeito sonoro
            if (!tutorial)
                Som.XequeOuXequemate(partida.Xeque, partida.Terminada, partida.Empate);

            if (!partida.Terminada)
            {
                Console.WriteLine($"  Aguardando jogada: {nomeJogador}({partida.CorAtual})");
                if (partida.Xeque)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("  Xeque!");
                    Console.ForegroundColor = cor;
                }
            }
            else
            {
                if (partida.Empate)
                    Console.WriteLine("  Empate!");
                else
                {
                    if (!partida.Render)
                        Console.WriteLine("  XEQUEMATE!");
                    Console.WriteLine($"  Vencedor: {nomeJogador}({partida.CorAtual})");
                }
            }
            Console.ForegroundColor = aux;
        }

        public void ImprimirPecasCapturadas(Services.Xadrez partida)
        {
            ConsoleColor aux = Console.ForegroundColor;
            Console.WriteLine("\n  Peças capturadas:");

            Console.ForegroundColor = ConsoleColor.White;
            ImprimirConjunto(partida.PecasCapturadas(Cor.Brancas));

            Console.ForegroundColor = ConsoleColor.Black;
            ImprimirConjunto(partida.PecasCapturadas(Cor.Pretas));

            Console.ForegroundColor = aux;
        }

        public void ImprimirConjunto(HashSet<Peca> conjunto)
        {
            int aux = 0;
            Console.Write("  [");
            foreach (Peca p in conjunto)
            {
                aux++;
                if (aux % 8 == 0)
                    Console.Write("\n   ");

                Console.Write(p + " ");
            }


            Console.WriteLine("]");
        }


        public void ImprimirTabuleiro(TabuleiroDeXadrez tabuleiro)
        {

            ConsoleColor aux = Console.BackgroundColor;
            ConsoleColor fgAux = Console.ForegroundColor;


            Console.WriteLine("\n      a  b  c  d  e  f  g  h");
            Console.Write("    ");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("┌────────────────────────┐");
            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                Console.ForegroundColor = fgAux;
                Console.BackgroundColor = aux;

                Console.Write($"  {(8 - i)} ");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("│");
                Console.ForegroundColor = fgAux;


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
                Console.BackgroundColor = aux;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("│");
                Console.ForegroundColor = fgAux;
                if (i < tabuleiro.Linhas - 1)
                    Console.WriteLine();
            }
            Console.Write("\n    ");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("└────────────────────────┘");
            Console.ForegroundColor = fgAux;

        }

        public void ImprimirTabuleiro(TabuleiroDeXadrez tabuleiro, bool[,] posicoesPossiveis)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.Gray;
            ConsoleColor fgAux = Console.ForegroundColor;

            Console.WriteLine("\n      a  b  c  d  e  f  g  h");
            Console.Write("    ");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("┌────────────────────────┐");
            Console.ForegroundColor = fgAux;

            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                Console.Write($"  {(8 - i)} ");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("│");
                Console.ForegroundColor = fgAux;

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

                    if (posicoesPossiveis[i, j])
                        Console.BackgroundColor = fundoAlterado;


                    ImprimirPeca(tabuleiro.Peca(i, j));
                    Console.BackgroundColor = fundoOriginal;
                }
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("│");
                Console.ForegroundColor = fgAux;
                if (i < tabuleiro.Linhas - 1)
                    Console.WriteLine();
            }
            Console.BackgroundColor = fundoOriginal;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\n    └────────────────────────┘");
            Console.ForegroundColor = fgAux;
        }

        public void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            ConsoleColor bgAux = Console.BackgroundColor;
            ConsoleColor fgAux = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(" ┌────────────────────────┐");
            Console.ForegroundColor = fgAux;
            for (int i = 0; i < tabuleiro.Tamanho; i++)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(" │");
                Console.ForegroundColor = fgAux;
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

                    Console.Write(tabuleiro.TabuleiroMatriz[i, j].Replace('x', ' '));
                    Console.ForegroundColor = fgAux;
                    Console.BackgroundColor = bgAux;
                }
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("│");
                Console.ForegroundColor = fgAux;
                if (i < tabuleiro.Tamanho - 1)
                    Console.WriteLine();

            }
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\n └────────────────────────┘");
            Console.ForegroundColor = fgAux;
        }

        public void ImprimirPgn(Pgn pgn)
        {
            Console.Clear();
            Console.WriteLine("\n  Este é o registro 'pgn' da partida!\n" +
                                "  Ele fica salvo na pasta 'Xadrez>Repositories>Arquivos_pgn'.\n" +
                               $"  Nome do arquivo desta partida: {pgn.Id}.pgn\n\n");

            Console.WriteLine(Pgns.PgnParaString(pgn, false));

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
                if (peca.Cor == Cor.Brancas)
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
