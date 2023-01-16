﻿
using HubDeJogos.JogoDaVelha.Models.Enums;
using HubDeJogos.Models.Enums;
using HubDeJogos.Models;
using HubDeJogos.Repositories;
using HubDeJogos.JogoDaVelha.Models;
using HubDeJogos.JogoDaVelha.Views;
using HubDeJogos.Hub.Repositories;
using HubDeJogos.Hub.Views;

namespace HubDeJogos.JogoDaVelha.Services
{
    public class JogoDaVelha
    {
        private readonly Tela _tela = new();
        private readonly Jogador _jogador1;
        private readonly Jogador _jogador2;


        public JogoDaVelha() 
        {
            _jogador1 = new Jogador("Fulano", "");
            _jogador2 = new Jogador("Beltrano", "");

            Tutorial();
        }
        public JogoDaVelha(Jogador jogador1, Jogador jogador2)
        {
            _jogador1 = jogador1;
            _jogador2 = jogador2;

            Jogar();
        }

        // função de jogada de cada jogador
        private void Jogada(string posicaoDaJogada, string simbolo, TabuleiroJogoDaVelha tabuleiro)
        {

            for (int i = 0; i < tabuleiro.Tamanho; i++)
            {
                for (int j = 0; j < tabuleiro.Tamanho; j++)
                {
                     
                    if (tabuleiro.TabuleiroMatriz[i, j].Trim().Equals(posicaoDaJogada))
                    {
                        tabuleiro.TabuleiroMatriz[i, j] = simbolo;
                    }
                }
            }
        }
        // função tutorial
        private void Tutorial() 
        {
            TabuleiroJogoDaVelha tabuleiro = new(3);
            ConsoleColor aux = Console.ForegroundColor;

            for (int i=0; i<4; i++)
            {
                Console.Clear();
                Console.WriteLine(Tutoriais.ExplicacoesJogoDaVelha[i]);
                Utilidades.Utilidades.AperteEnterParaContinuar();

                if(i==0)
                {
                    Console.Write("\n  Digite o tamanho do jogo (3 a 10): 3");
                    Console.WriteLine("\n\n  Como mencionado vamos usar o tabuleiro 3x3 neste tutorial!");
                    Utilidades.Utilidades.AperteEnterParaContinuar();
                    
                    Console.Clear();
                    Console.WriteLine();
                    _tela.ImprimirTabuleiro(tabuleiro);
                    Console.WriteLine("\n  Ta-dãan! Um tabuleiro 3x3 novíssimo foi criado!");
                }
                else if(i ==1)
                {
                    Console.WriteLine();
                    _tela.ImprimirTabuleiro(tabuleiro);
                    Console.Write($"\n  Vez de {_jogador1.NomeDeUsuario}(");                   
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("X");
                    Console.ForegroundColor = aux;
                    Console.WriteLine(")");
                    Console.WriteLine("\n  Posição da jogada: 1");

                    Jogada("1", $" {Simbolo.X} ", tabuleiro);
                    Utilidades.Utilidades.AperteEnterParaContinuar();
                    
                    Console.Clear();
                    Console.WriteLine();
                    _tela.ImprimirTabuleiro(tabuleiro);
                    Console.WriteLine("\n\n  O primeiro jogador escolheu a posição 1,\n" +
                                          "  logo o 'X' aparece na posição.\n" +
                                          "  Agora é vez do segundo jogador.");                   
                    Utilidades.Utilidades.AperteEnterParaContinuar();

                    Console.Clear();
                    Console.WriteLine();
                    _tela.ImprimirTabuleiro(tabuleiro);
                    Console.Write($"\n  Vez de {_jogador2.NomeDeUsuario}(");
                   
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("O");
                    Console.ForegroundColor = aux;
                    Console.WriteLine(")");
                    Console.WriteLine("\n  Posição da jogada: 5");
                    Jogada("5", $" {Simbolo.O} ", tabuleiro);
                    Utilidades.Utilidades.AperteEnterParaContinuar();


                    Console.Clear();
                    Console.WriteLine();
                    _tela.ImprimirTabuleiro(tabuleiro);
                    Console.WriteLine("\n\n  O segundo jogador escolheu a posição 5,\n" +
                                           "  a lógica é a mesma: 'O' aparece na posição.");

                }
                else if(i==2)
                {
                    Jogada("4", $" {Simbolo.X} ", tabuleiro);
                    Console.Clear();
                    Console.WriteLine();
                    _tela.ImprimirTabuleiro(tabuleiro);
                    Thread.Sleep(2000);
                    Jogada("3", $" {Simbolo.O} ", tabuleiro);
                    Console.Clear();
                    Console.WriteLine();
                    _tela.ImprimirTabuleiro(tabuleiro);                  
                    Jogada("7", $" {Simbolo.X} ", tabuleiro);
                    Thread.Sleep(2000);
                    Console.Clear();
                    Console.WriteLine();
                    _tela.ImprimirTabuleiro(tabuleiro);
                    Console.WriteLine($"\n  Vencedor: {_jogador1.NomeDeUsuario}(X)");
                }
                else
                {
                    for(int j=0; j<7; j++) 
                    {
                        Console.Clear();
                        Console.WriteLine(Tutoriais.JogoDaVelhaAnimação[j]);
                        Thread.Sleep(1700);
                    }                   
                }
                Utilidades.Utilidades.AperteEnterParaContinuar();
            }
        }

        // função principal de jogar
        private void Jogar()
        {
            int tamanho;
            do
            {
                Console.Clear();
                Console.Write("\n  Digite o tamanho do jogo (3 a 10): ");
                if (int.TryParse(Console.ReadLine(), out tamanho))
                {
                    if (tamanho >= 3 && tamanho <= 10)
                    {
                        Console.Clear();
                        break;
                    }
                }
            } while (true);

            // gerando o tabuleiro
            TabuleiroJogoDaVelha tabuleiro = new TabuleiroJogoDaVelha(tamanho);

            // começando pelo jogador1 como X
            int turno = 1;
            string? vencedor = null;
            Jogador jogador = _jogador1;
            string jogada = $" {Simbolo.X} ";
            while (true)
            {
                // enquanto o jogador tentar colocar uma posição inválida, o jogo não continua
                string posicao;
                do
                {
                    Console.Clear();
                    Console.WriteLine("\n");
                    _tela.ImprimirTabuleiro(tabuleiro);

                    ConsoleColor aux = Console.ForegroundColor;
                    

                    Console.Write($"\n  Vez de {jogador.NomeDeUsuario}(");
                    Console.ForegroundColor = (jogador.Equals(_jogador1)) ? ConsoleColor.Black : ConsoleColor.DarkRed;
                    Console.Write($"{jogada.Trim()}");
                    Console.ForegroundColor = aux;
                    Console.WriteLine(")\n");
                    Console.Write("  Posição da jogada: ");

                    posicao = Console.ReadLine();
                } while (!tabuleiro.JogadasPossiveis.Contains(posicao));
                turno++;

                // removendo a jogada das jogadas possíveis e chamando a função Jogada que altera o tabuleiro
                tabuleiro.JogadasPossiveis.Remove(posicao);
                Jogada(posicao, jogada, tabuleiro);

                // chamando a função de checkar vitória após cada jogada válida a partir do tamanho do tabuleiro
                // como o tamanho do tabuleiro é na verdade seu tamanho x2 -1, esse mesmo número é o número de
                // jogadas mínimas antes de ter um vencedor (em um tabuleiro 3x3 precisamos de pelo menos 5
                // jogadas para ter um vencedor 
                if(turno >= tabuleiro.Tamanho)
                    vencedor = CheckarVitoriaOuVelha(tabuleiro);

                // se vencedor == null (ninguém ganhou), a vez passa para o próximo jogador
                if (vencedor == null)
                {
                    // trocando o jogador, alternando as jogadas
                    if (jogador == _jogador1)
                    {
                        jogador = _jogador2;
                        jogada = $" {Simbolo.O} ";
                    }
                    else
                    {
                        jogador = _jogador1;
                        jogada = $" {Simbolo.X} ";
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\n");
                    _tela.ImprimirTabuleiro(tabuleiro);

                    //settando parâmetros da partida
                    Resultado resultado = Resultado.Empate;
                    string jogadorGanhou = _jogador1.NomeDeUsuario;
                    string jogadorPerdeu = _jogador2.NomeDeUsuario;
                    


                    // CheckarVitoriaOuVelha retornou velha 
                    if (vencedor == "Velha")
                        Console.WriteLine("\n  Deu velha");

                    // retornou algo que não é null e nem velha, logo teve um vencedor (x ou o)
                    else
                    {
                        resultado = Resultado.Decisivo;
                        Console.WriteLine($"\n  Vencedor: {jogador.NomeDeUsuario} ({vencedor})");

                        // alterando o vencedor/perdedor
                        if (jogador.Equals(_jogador2))
                        {
                            jogadorGanhou = _jogador2.NomeDeUsuario;
                            jogadorPerdeu = _jogador1.NomeDeUsuario;
                        }
                    }
                    // alterando o tabuleiro para registrá-lo
                    tabuleiro.AlterarTabuleiroMatrizParaRegistro();
                    Partida partida = new Partida(Jogo.JogoDaVelha, jogadorGanhou, jogadorPerdeu, resultado, tabuleiro);

                    //adicionando a partida no histórico de partidas
                    Partidas.HistoricoDePartidas.Add(partida);
                    Partidas.SalvarPartidas();

                    //adicionando a partida nos históricos dos jogadores
                    _jogador1.HistoricoDePartidas.Add(partida);
                    _jogador2.HistoricoDePartidas.Add(partida);
                    Utilidades.Utilidades.AperteEnterParaContinuar();
                    break;
                }
            }
        }


        // função para checkar se alguém ganhou ou deu velha
        private string? CheckarVitoriaOuVelha(TabuleiroJogoDaVelha tabuleiro)
        {

            int tamanhoAuxiliar = (tabuleiro.Tamanho + 1) / 2;

            string[] valoresNaDiagonalPrincipal = new string[tamanhoAuxiliar];
            string[] valoresNaDiagonalSecundaria = new string[tamanhoAuxiliar];

            int diagonalSecundariaAuxiliar = tamanhoAuxiliar + 1;
            if (tamanhoAuxiliar % 2 == 0)
                diagonalSecundariaAuxiliar = tamanhoAuxiliar + 2;

            // lista das colunas
            List<string[]> colunas = new List<string[]>();
            for (int i = 0; i < tabuleiro.Tamanho + 1; i += 2)
            {
                colunas.Add(new string[tamanhoAuxiliar]);
            }

            // loop de checkagem
            for (int i = 0; i < tabuleiro.Tamanho; i += 2)
            {

                string[] valoresNaLinha = new string[tamanhoAuxiliar];

                for (int j = 0; j < tabuleiro.Tamanho; j += 2)
                {
                    // utilizando o .Trim() pois usei espaços no X e na O para ficar com espaçamento correto
                    
                    string valor = tabuleiro.TabuleiroMatriz[i, j].Trim();

                    int posicaoAuxiliarParaVetoresJ = (int)Math.Floor(j / 2.0);
                    int posicaoAuxiliarParaVetoresI = (int)Math.Floor(i / 2.0);

                    valoresNaLinha[posicaoAuxiliarParaVetoresJ] = valor;

                    // adicionando valores na diagonal principal
                    if (i == j)
                        valoresNaDiagonalPrincipal[posicaoAuxiliarParaVetoresJ] = valor;

                    // adicionando valores na diagonal secundária
                    if (j == diagonalSecundariaAuxiliar)
                    {
                        valoresNaDiagonalSecundaria[posicaoAuxiliarParaVetoresJ] = valor;
                        diagonalSecundariaAuxiliar -= 2;
                    }

                    // adicionando os valores nas colunas                   
                    colunas[posicaoAuxiliarParaVetoresJ][posicaoAuxiliarParaVetoresI] = tabuleiro.TabuleiroMatriz[i, j].Trim();
                }
                // checkando os valores únicos da linha
                valoresNaLinha = valoresNaLinha.Distinct().ToArray();
                if (valoresNaLinha.Length == 1)
                    return valoresNaLinha[0];

            }
            // checkando a diagonal principal
            valoresNaDiagonalPrincipal = valoresNaDiagonalPrincipal.Distinct().ToArray();
            if (valoresNaDiagonalPrincipal.Length == 1)
                return valoresNaDiagonalPrincipal[0];

            // checkando a diagonal secundária
            valoresNaDiagonalSecundaria = valoresNaDiagonalSecundaria.Distinct().ToArray();
            if (valoresNaDiagonalSecundaria.Length == 1)
                return valoresNaDiagonalSecundaria[0];

            // checkando as colunas
            for (int i = 0; i < colunas.Count; i++)
            {
                string[] coluna = colunas[i];
                coluna = coluna.Distinct().ToArray();
                if (coluna.Length == 1)
                    return coluna[0];
            }

            // checkando velha
            if (tabuleiro.JogadasPossiveis.Count == 0)
                return "Velha";

            // caso ninguém ganhou e não deu velha
            return null;
        }

    }
}
