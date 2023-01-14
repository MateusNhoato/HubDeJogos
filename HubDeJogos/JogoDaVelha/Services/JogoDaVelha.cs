
using HubDeJogos.JogoDaVelha.Models.Enums;
using HubDeJogos.Models;
using HubDeJogos.Repositories;
using HubDeJogos.JogoDaVelha.Models;
using HubDeJogos.JogoDaVelha.Views;


namespace HubDeJogos.JogoDaVelha.Services
{
    public class JogoDaVelha
    {
        private Tela _tela = new Tela();

        private readonly Jogador _jogador1;
        private readonly Jogador _jogador2;
        private Partida _partida;
        public JogoDaVelha(Jogador jogador1, Jogador jogador2, Partida partida)
        {
            _jogador1 = jogador1;
            _jogador2 = jogador2;
            _partida = partida;
            Jogar();
        }
        
        // função de jogada de cada jogador
        private void Jogada(string posicaoDaJogada, string simbolo, TabuleiroJogoDaVelha tabuleiro)
        {

            for (int i = 0; i < tabuleiro.TamanhoDoTabuleiro; i++)
            {
                for (int j = 0; j < tabuleiro.TamanhoDoTabuleiro; j++)
                {
                    if (tabuleiro.MatrizTabuleiro[i, j].Trim().Equals(posicaoDaJogada))
                    {
                        tabuleiro.MatrizTabuleiro[i, j] = simbolo;
                    }
                }
            }
        }


        // função principal de jogar
        private void Jogar()
        {
            int tamanho;
            string? vencedor;
            do
            {
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
            Jogador jogador = _jogador1;
            string jogada = $" {Simbolo.X} ";
            while (true)
            {
                // enquanto o jogador tentar colocar uma posição inválida, o jogo não continua
                string posicao;
                do
                {
                    _tela.ImprimirTabuleiro(tabuleiro);
                    Console.WriteLine($"\n  Vez de {jogador.NomeDeUsuario}\n");
                    Console.Write($"  Digite a posição da jogada ({jogada.Trim()}): ");
                    posicao = Console.ReadLine();
                } while (!tabuleiro.JogadasPossiveis.Contains(posicao));

                // removendo a jogada das jogadas possíveis e chamando a função Jogada que altera o tabuleiro
                tabuleiro.JogadasPossiveis.Remove(posicao);
                Jogada(posicao, jogada, tabuleiro);

                // chamando a função de checkar vitória após cada jogada válida
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
                    _tela.ImprimirTabuleiro(tabuleiro);

                    // CheckarVitoriaOuVelha retornou velha 
                    // alterando o resultado para empate e adicionando a partida nos históricos
                    if (vencedor == "Velha")
                    {
                        Console.WriteLine("\n  Deu velha. Empate.");
                        
                        _partida.Resultado = HubDeJogos.Models.Enums.Resultado.Empate;
                        _jogador1.HistoricoDePartidas.Add(_partida);

                        _partida.Jogador = _jogador2;
                        _partida.Oponente = _jogador1;
                        _jogador2.HistoricoDePartidas.Add(_partida);

                    }
                    // retornau algo que não é null e nem velha, logo teve um vencedor (x ou o)
                    else
                    {
                        Console.WriteLine($"\n  Vencedor: {jogador.NomeDeUsuario} ({vencedor}).");

                        // alterando os resultados da partida para adicionar nos históricos respectivos
                        if (jogador.Equals(_jogador1))
                        {

                            jogador.HistoricoDePartidas.Add(_partida);

                            _partida.Jogador = _jogador2;
                            _partida.Oponente = jogador;
                            _partida.Resultado = HubDeJogos.Models.Enums.Resultado.Derrota;
                            _jogador2.HistoricoDePartidas.Add(_partida);
                        }
                        else
                        {
                            _partida.Jogador = _jogador2;
                            _partida.Oponente = _jogador1;
                            jogador.HistoricoDePartidas.Add(_partida);

                            _partida.Jogador = _jogador1;
                            _partida.Oponente = _jogador2;
                            _partida.Resultado = HubDeJogos.Models.Enums.Resultado.Derrota;
                            _jogador1.HistoricoDePartidas.Add(_partida);
                        }   
                    }
                    Utilidades.Utilidades.AperteEnterParaContinuar();
                    break;
                }
            }
        }


        // função para checkar se alguém ganhou ou deu velha
        private string? CheckarVitoriaOuVelha(TabuleiroJogoDaVelha tabuleiro)
        {

            int tamanhoAuxiliar = (tabuleiro.TamanhoDoTabuleiro + 1) / 2;

            string[] valoresNaDiagonalPrincipal = new string[tamanhoAuxiliar];
            string[] valoresNaDiagonalSecundaria = new string[tamanhoAuxiliar];

            int diagonalSecundariaAuxiliar = tamanhoAuxiliar + 1;
            if (tamanhoAuxiliar % 2 == 0)
                diagonalSecundariaAuxiliar = tamanhoAuxiliar + 2;

            // lista das colunas
            List<string[]> colunas = new List<string[]>();
            for (int i = 0; i < tabuleiro.TamanhoDoTabuleiro + 1; i += 2)
            {
                colunas.Add(new string[tamanhoAuxiliar]);
            }

            // loop de checkagem
            for (int i = 0; i < tabuleiro.TamanhoDoTabuleiro; i += 2)
            {

                string[] valoresNaLinha = new string[tamanhoAuxiliar];

                for (int j = 0; j < tabuleiro.TamanhoDoTabuleiro; j += 2)
                {
                    // utilizando o .Trim() pois usei espaços no X e na O para ficar com espaçamento
                    string valor = tabuleiro.MatrizTabuleiro[i, j].Trim();

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
                    colunas[posicaoAuxiliarParaVetoresI][posicaoAuxiliarParaVetoresJ] = tabuleiro.MatrizTabuleiro[j, i].Trim();
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
