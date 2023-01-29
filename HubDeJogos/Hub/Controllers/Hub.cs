using HubDeJogos.Hub.Repositories;
using HubDeJogos.Models;
using HubDeJogos.Models.Enums;
using HubDeJogos.Repositories;
using HubDeJogos.Views;
using System.Text.RegularExpressions;
using Utilidades;


namespace HubDeJogos.Controllers
{
    public class Hub
    {
        private readonly Tela _tela = new();
        private readonly List<Jogador> _jogadores;
        private readonly Regex _padraoNomeDoUsuario = new(@"^[a-zA-Z0-9]{2,30}$");
        private readonly Regex _padraoSenhaDoUsuario = new(@"^[a-zA-Z0-9]{6,10}$");
        private readonly Jogadores _repositorioJogadores = new Jogadores();


        public Hub()
        {
            _repositorioJogadores.CarregarListaDeJogadores();
            Partidas.CarregarPartidas();

            if (_repositorioJogadores.ListaDeJogadores != null)
                _jogadores = _repositorioJogadores.ListaDeJogadores;
            else
                _jogadores = new List<Jogador>();
        }

        public void Menu()
        {
            string? opcao;

            do
            {
                Thread.Sleep(300);
                Som.Musica(Musica.hub);

                _tela.ImprimirMenuDoHub();
                opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "0":
                        Comunicacao.ComunicacaoComUsuario(Efeito.obrigado, null);
                        Thread.Sleep(2200);
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
                        Comunicacao.ComunicacaoComUsuario(Efeito.falha, null);
                        break;
                }
            } while (opcao != "0");
        }


        private void AcessarMenuDeJogos()
        {
            _tela.ImprimirLogIn(false);
            if (_jogadores.Count < 2)
            {
                Comunicacao.ComunicacaoComUsuario(Efeito.novatela, 
                    "Parece que ainda não temos dois jogadores para escolher essa opção!\n" +
                    "  Redirecionando para Registar Novo Jogador.");

                Comunicacao.Carregando();
                Comunicacao.AperteEnterParaContinuar();
                RegistrarJogador();
                AcessarMenuDeJogos();
                return;
            }

            Comunicacao.ComunicacaoComUsuario(Efeito.novatela, null);
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

                if (jogador1 == jogador2)
                {
                    Comunicacao.ComunicacaoComUsuario(Efeito.falha, "Não é possível jogar contra si mesmo.");
                    Comunicacao.AperteEnterParaContinuar();
                    return;
                }
            } while (jogador2 == null);


            MenuDeJogos menuDeJogos = new(jogador1, jogador2, this);
            Comunicacao.ComunicacaoComUsuario(Efeito.novatela, null);

            if (jogador1.HistoricoDePartidas.Count < 1 || jogador2.HistoricoDePartidas.Count < 1)
                menuDeJogos.Menu(true);
            else
                menuDeJogos.Menu(false);

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
                        Comunicacao.ComunicacaoComUsuario(Efeito.falha, "Jogador já cadastrado.");
                        Comunicacao.AperteEnterParaContinuar();
                        return;
                    }
                }

                Jogador novoJogador = new Jogador(nomeDoUsuario, senha);
                _jogadores.Add(novoJogador);
                PassarListaDeJogadoresParaRepositorio();
                Comunicacao.ComunicacaoComUsuario(Efeito.confirma, "Novo jogador cadastrado com sucesso!!");
            }
            else
                Comunicacao.ComunicacaoComUsuario(Efeito.falha, "Nome de usuário e/ou senha inválido(s).");
                        
            Comunicacao.AperteEnterParaContinuar();
        }

        private void ListarJogadores()
        {
            ConsoleColor aux = Console.ForegroundColor;
            _tela.ImprimirListaDeJogadores();
            if (_jogadores.Count < 1)
                Console.WriteLine("\n  Nenhum jogador cadastrado.");

            else
            {
                foreach (Jogador jogador in _jogadores)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine($"\n  # {jogador}");
                    Console.ForegroundColor = aux;
                    Console.WriteLine($"{jogador.Pontuacoes()}");
                    Console.WriteLine(Comunicacao.MeiaLinha);
                }

            }



            Comunicacao.AperteEnterParaContinuar();
        }

        private void RankingDosJogadores()
        {

            //Ordenando o ranking por ordem de pontuação, como critério de desempate
            //temos quem jogos menos, depois ganhou mais vezes e depois quem perdeu menos
            var jogadores = _jogadores
                .OrderByDescending(j => j.GetPontuacao())
                .ThenBy(j => j.HistoricoDePartidas.Count)
                .ThenBy(j => j.HistoricoDePartidas.Count(p =>
                p.Resultado.Equals(Resultado.Decisivo) &&
                p.JogadorGanhou.Equals(j.NomeDeUsuario)))
                .ThenByDescending(j => j.HistoricoDePartidas.Count(p =>
                p.Resultado.Equals(Resultado.Decisivo) &&
                p.JogadorGanhou != j.NomeDeUsuario))
                .ToList();

            _tela.ImprimirRanking();

            if (jogadores.Count < 1)
                Console.WriteLine("\n  Nenhum jogador cadastrado.");
            else
            {
                Comunicacao.Carregando();
                Som.Musica(Musica.ranking);
                _tela.ImprimirRanking();
                for (int i = 0; i < jogadores.Count; i++)
                {
                    if (i > 10)
                        break;
                    if (jogadores[i].GetPontuacao() <= 0)
                        continue;
                    Console.WriteLine($"  Top {i + 1}: {jogadores[i]} | Pontuação Total: {jogadores[i].GetPontuacao()}\n");
                }
            }
            Comunicacao.AperteEnterParaContinuar();
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
                Comunicacao.ComunicacaoComUsuario(Efeito.confirma, "Jogador logado com sucesso!");
            else
                Comunicacao.ComunicacaoComUsuario(Efeito.falha, "Jogador não encontrado");

            Comunicacao.AperteEnterParaContinuar();
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
            Console.Clear();
            Console.WriteLine("\n");
            Comunicacao.Carregando();
            Som.Musica(Musica.historico);
            _tela.ImprimirHistoricoMenu(null);
            if (Partidas.HistoricoDePartidas.Count > 0)
                foreach (Partida partida in Partidas.HistoricoDePartidas)
                    _tela.ImprimirPartida(partida);

            else
                Console.WriteLine("\n  Nenhuma partida foi registrada até o momento.");


            Comunicacao.AperteEnterParaContinuar();
        }

        private void TresTentativasDeLoginErrado()
        {
            _tela.ImprimirLogIn(false);
            Comunicacao.ComunicacaoComUsuario(Efeito.falha, "Três tentativas de LogIn foram feitas sem sucesso.\n" +      
                "  Para a segurança da sua conta o procedimento será encerrado.");
            Comunicacao.AperteEnterParaContinuar();
        }

        private void ManipularContaJogador()
        {
            Comunicacao.ComunicacaoComUsuario(Efeito.novatela, null);
            _tela.ImprimirLogIn(true);
            Jogador jogador = Login(null);
            if (jogador != null)
            {
                string opcao;
                do
                {
                    Som.Musica(Musica.conta);
                    _tela.ImprimirOpcoesDaConta();
                    opcao = Console.ReadLine();
                    switch (opcao)
                    {
                        case "0":
                            Comunicacao.ComunicacaoComUsuario(Efeito.voltar, null);
                            break;
                        case "1":
                            AlterarNomeDoUsuario(jogador);
                            break;
                        case "2":
                            AlterarSenhaDoUsuario(jogador);
                            break;
                        case "3":
                            if (DeletarConta(jogador))
                                opcao = "0";
                            break;
                        default:
                            Comunicacao.ComunicacaoComUsuario(Efeito.falha, null);
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
                        Comunicacao.ComunicacaoComUsuario(Efeito.falha, "Jogador já cadastrado.");
                        Comunicacao.AperteEnterParaContinuar();
                        return;
                    }
                }
                // mudando o nome do jogador no histórico antes de mudar para o novo nome
                for (int i = 0; i < jogador.HistoricoDePartidas.Count; i++)
                {
                    Partida partida = jogador.HistoricoDePartidas[i];

                    if (partida.Jogador1 == jogador.NomeDeUsuario)
                        partida.Jogador1 = novoNome;
                    else
                        partida.Jogador2 = novoNome;

                    if (partida.JogadorGanhou == jogador.NomeDeUsuario)
                    {
                        partida.JogadorGanhou = novoNome;
                    }

                }

                jogador.AlterarNomeDeUsuario(novoNome);
                Comunicacao.ComunicacaoComUsuario(Efeito.confirma, $"Deu certo, {novoNome}!\n" +
                    $"  Nome de Usuário alterado com sucesso!");
                PassarListaDeJogadoresParaRepositorio();
            }
            else
                Comunicacao.ComunicacaoComUsuario(Efeito.falha, "Nome inválido.");
       
            Comunicacao.AperteEnterParaContinuar();
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
                Comunicacao.ComunicacaoComUsuario(Efeito.confirma, "Senha alterada com sucesso!");
                PassarListaDeJogadoresParaRepositorio();
            }
            else
                Comunicacao.ComunicacaoComUsuario(Efeito.falha, "Senha inválida.");
           
            Comunicacao.AperteEnterParaContinuar();
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
                    Comunicacao.ComunicacaoComUsuario(Efeito.confirma, "Conta excluída com sucesso!");
                    Comunicacao.AperteEnterParaContinuar();
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
            _repositorioJogadores.SalvarJogadores(_jogadores);
        }
    }
}
