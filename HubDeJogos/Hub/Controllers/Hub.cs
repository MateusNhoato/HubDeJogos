using HubDeJogos.Models;
using HubDeJogos.Views;
using HubDeJogos.Models.Enums;
using HubDeJogos.Hub.Repositories;
using HubDeJogos.Repositories;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;

namespace HubDeJogos.Controllers
{
    public class Hub
    {
        private readonly Tela _tela = new();
        private readonly List<Jogador> _jogadores;
        private readonly Regex _padraoNomeDoUsuario = new(@"^[a-zA-Z0-9]{2,30}$");
        private readonly Regex _padraoSenhaDoUsuario = new(@"^[a-zA-Z0-9]{6,10}$");


        public Hub()
        {
            Jogadores jogadores = new Jogadores();
            jogadores.CarregarListaDeJogadores();

            Partidas.CarregarPartidas();


            if (jogadores.ListaDeJogadores != null)
                _jogadores = jogadores.ListaDeJogadores;
            else
                _jogadores = new List<Jogador>();
        }

        public void Menu()
        {

            string? opcao;
            do
            {
                _tela.ImprimirMenuDoHub();
                opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "0":
                        break;
                    case "1":
                        AcessarMenuDeJogos();
                        break;
                    case "2":
                        RegistrarJogador();
                        break;
                    case "3":
                        ListarJogadores();
                        break;
                    case "4":
                        RankingDosJogadores();
                        break;
                    case "5":
                        HistoricoDePartidas();
                        break;
                    case "6":
                        ManipularContaJogador();
                        break;
                    default:
                        Console.WriteLine("  Opção não encontrada.");
                        break;
                }
            } while (opcao != "0");
        }

        private void AcessarMenuDeJogos()
        {
            _tela.ImprimirLogIn(false);
            if (_jogadores.Count < 2)
            {
                Console.WriteLine("  Parece que ainda não temos dois jogadores para escolher essa opção!\n" +
                    "  Redirecionando para Registar Novo Jogador.");

                for (int i = 0; i < 3; i++)
                {
                    Thread.Sleep(500);
                    Console.Write("  . ");
                }
                Utilidades.Utilidades.AperteEnterParaContinuar();
                RegistrarJogador();
                AcessarMenuDeJogos();
                return;
            }

            _tela.ImprimirLogIn(false);
            Jogador? jogador2;
            Jogador? jogador1;

            //auxiliar para contar quantas vezes tentaram fazer LogIn
            int auxCont = 0;

            do
            {
                if (++auxCont > 3)
                {
                    TresTentativasDeLoginErrado();
                    return;
                }
                jogador1 = Login(1);
            } while (jogador1 == null);

            auxCont = 0;
            do
            {
                if (++auxCont > 3)
                {
                    TresTentativasDeLoginErrado();
                    return;
                }
                jogador2 = Login(2);
            } while (jogador2 == null);


            if (jogador1 != jogador2)
            {
                MenuDeJogos menudeJogos = new(jogador1, jogador2, this);
                menudeJogos.Menu();
            }
            else
            {
                Console.WriteLine("  Não é possível jogar contra si mesmo.");
                Utilidades.Utilidades.AperteEnterParaContinuar();
            }
        }

        private void RegistrarJogador()
        {

            _tela.ImprimirRegistrar();
            Console.Write("  Nome do Usuário: ");
            string? nomeDoUsuario = Console.ReadLine();
            Console.Write("  Senha: ");
            string senha = PedirSenha();

            if (_padraoNomeDoUsuario.IsMatch(nomeDoUsuario) && _padraoSenhaDoUsuario.IsMatch(senha))
            {
                foreach (Jogador jogador in _jogadores)
                {
                    if (nomeDoUsuario.Equals(jogador.NomeDeUsuario))
                    {
                        Console.WriteLine("\n  Jogador já cadastrado.");
                        Utilidades.Utilidades.AperteEnterParaContinuar();
                        return;
                    }
                }

                Jogador novoJogador = new Jogador(nomeDoUsuario, senha);
                _jogadores.Add(novoJogador);
                PassarListaDeJogadoresParaRepositorio();
                Console.WriteLine("\n\n\n  Novo jogador cadastrado com sucesso!!");
            }
            else
                Console.WriteLine("\n\n\n  Nome de usuário e/ou senha inválido(s).");

            Utilidades.Utilidades.AperteEnterParaContinuar();
        }

        private void ListarJogadores()
        {
            _tela.ImprimirListaDeJogadores();
            if (_jogadores.Count < 1)
                Console.WriteLine("\n  Nenhum jogador cadastrado.");

            else
                foreach (Jogador jogador in _jogadores)
                    Console.WriteLine($"  {jogador}\n");

            Utilidades.Utilidades.AperteEnterParaContinuar();
        }

        private void RankingDosJogadores()
        {
            //Ordenando o ranking por ordem de pontuação, como critério de desempate
            //temos quem ganhou mais vezes e depois quem perdeu menos
            var jogadores = _jogadores
                .OrderByDescending(j => j.GetPontuacao(Jogo.JogoDaVelha) +
                j.GetPontuacao(Jogo.Xadrez))
                .ThenBy(j => j.HistoricoDePartidas.Count(p =>
                p.Resultado.Equals(Resultado.Decisivo) &&
                p.NomeJogadorGanhou.Equals(j.NomeDeUsuario)))
                .ThenByDescending(j => j.HistoricoDePartidas.Count(p =>
                p.Resultado.Equals(Resultado.Decisivo) &&
                p.NomeJogadorPerdeu.Equals(j.NomeDeUsuario)))
                .ToList();

            _tela.ImprimirRanking();

            if (jogadores.Count < 1)
                Console.WriteLine("\n  Nenhum jogador cadastrado.");
            else
            {
                for (int i = 0; i < jogadores.Count; i++)
                {
                    if (i > 10)
                        break;
                    if (jogadores[i].GetPontuacao(Jogo.JogoDaVelha)
                        + jogadores[i].GetPontuacao(Jogo.Xadrez) <= 0)
                        continue;
                    Console.WriteLine($"  Top {i + 1}: {jogadores[i]}\n");
                }
            }
            Utilidades.Utilidades.AperteEnterParaContinuar();
        }



        // função auxiliar para achar o jogador, caso o login esteja correto
        private Jogador? Login(int? n)
        {
            
            Jogador? jogador = null;
            if (n != null)
            {
                _tela.ImprimirLogIn(false);
                Console.WriteLine($"  Jogador {n}:\n");
            }
            else
                _tela.ImprimirLogIn(true);
            Console.Write("  Nome do Usuário: ");
            string? nomeDeUsuario = Console.ReadLine();

            if (!(string.IsNullOrEmpty(nomeDeUsuario)))
            {
                Console.Write("  Senha: ");
                string senha = PedirSenha();

                if (!(string.IsNullOrEmpty(senha)))
                {
                    jogador = _jogadores.Find(j =>
                    j.NomeDeUsuario.ToLower()
                    .Equals(nomeDeUsuario.ToLower()) &&
                    j.Senha.ToUpper().Equals(senha.ToUpper()));
                }

            }
            if (jogador != null)
                Console.WriteLine("\n\n  Jogador logado com sucesso!");
            else
                Console.WriteLine("\n\n  Jogador não encontrado");
            Utilidades.Utilidades.AperteEnterParaContinuar();
            return jogador;
        }

        // função auxiliar para pedir senha
        private string PedirSenha()
        {
            string senha = string.Empty;

            var tecla = Console.ReadKey().Key;
            while (tecla != ConsoleKey.Enter)
            {
                if (tecla != ConsoleKey.Backspace
                    && tecla != ConsoleKey.Escape
                    && tecla != ConsoleKey.Spacebar)
                {
                    Console.Write("\b");
                    Console.Write("*");
                    senha += ((char)tecla);
                }
                else
                {
                    if (senha.Length > 0)
                    {
                        senha = senha.Remove(senha.Length - 1);
                        Console.Write(" ");
                        Console.Write("\b \b");
                    }
                    else
                        Console.Write(" ");
                }

                tecla = Console.ReadKey().Key;
            }
            return senha;
        }

        private void HistoricoDePartidas()
        {
            _tela.ImprimirHistoricoMenu(null);
            if (Partidas.HistoricoDePartidas.Count > 0)
                foreach (Partida partida in Partidas.HistoricoDePartidas)
                    _tela.ImprimirPartida(partida);

            else
                Console.WriteLine("  Nenhuma partida foi registrada até o momento.");


            Utilidades.Utilidades.AperteEnterParaContinuar();
        }

        private void TresTentativasDeLoginErrado()
        {
            _tela.ImprimirLogIn(false);
            Console.WriteLine("  Três tentativas de LogIn foram feitas sem sucesso.\n" +
                              "  Para a segurança da sua conta o procedimento será encerrado.");
            Utilidades.Utilidades.AperteEnterParaContinuar();
        }

        private void ManipularContaJogador()
        {
            _tela.ImprimirLogIn(true);
            Jogador jogador = Login(null);
            if (jogador != null)
            {
                string opcao;
                do
                {
                    _tela.ImprimirOpcoesDaConta();
                    opcao = Console.ReadLine();
                    switch (opcao)
                    {
                        case "0":
                            break;
                        case "1":
                            AlterarNomeDoUsuario(jogador);
                            break;
                        case "2":
                            AlterarSenhaDoUsuario(jogador);
                            break;
                        case "3":
                           if(DeletarConta(jogador))
                                opcao = "0";
                            break;
                        default:
                            Console.WriteLine("  Opção não encontrada.");
                            break;
                    }
                } while (opcao != "0");
            }

        }

        private void AlterarNomeDoUsuario(Jogador jogador)
        {
            _tela.ImprimirConta();
            Console.Write("  Digite o Novo Nome de Usuário: ");
            string novoNome = Console.ReadLine();
            if (_padraoNomeDoUsuario.IsMatch(novoNome))
            {
                foreach (Jogador jog in _jogadores)
                {
                    if (jog.NomeDeUsuario.ToLower().Equals(novoNome.ToLower()))
                    {
                        Console.WriteLine("  Nome já cadastrado.");
                        Utilidades.Utilidades.AperteEnterParaContinuar();
                        return;
                    }
                }
                jogador.AlterarNomeDeUsuario(novoNome);
                Console.WriteLine($"  Deu certo, {novoNome}!");
                Console.WriteLine("  Nome de Usuário alterado com sucesso!");
                PassarListaDeJogadoresParaRepositorio();
            }
            else
                Console.WriteLine("  Nome inválido.");
            Utilidades.Utilidades.AperteEnterParaContinuar();
        }

        private void AlterarSenhaDoUsuario(Jogador jogador)
        {
            _tela.ImprimirConta();
            Console.Write("  Digite a Nova Senha: ");
            string novaSenha = PedirSenha();
            if (_padraoSenhaDoUsuario.IsMatch(novaSenha))
            {
                jogador.AlterarSenha(novaSenha);
                _tela.ImprimirConta();
                Console.WriteLine("  Senha alterada com sucesso!");
                PassarListaDeJogadoresParaRepositorio();
            }
            else
                Console.WriteLine("  Senha inválida.");
            Utilidades.Utilidades.AperteEnterParaContinuar();
        }

        private bool DeletarConta(Jogador jogador)
        {
            if (!_jogadores.Contains(jogador))
                return true;

            _tela.ImprimirConta();
            Console.WriteLine("\n  Tem certeza que gostaria de deletar sua conta?");
            Console.WriteLine("  1- Sim");
            Console.WriteLine("  2- Não");
            Console.Write("  Digite a opção desejada: ");

            string opcao;
            
            opcao = Console.ReadLine();
            switch (opcao)
            {
                case "1":
                    _jogadores.Remove(jogador);
                    Console.WriteLine("\n  Conta excluída com sucesso.");
                    PassarListaDeJogadoresParaRepositorio();
                    return true;
                case "2":
                    return false;
                default:
                    DeletarConta(jogador);
                    break;
            }
            return false;
        }

        public void PassarListaDeJogadoresParaRepositorio()
        {
            Jogadores jogadores = new Jogadores();
            jogadores.SalvarJogadores(_jogadores);
        }
    }
}
