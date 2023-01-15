using HubDeJogos.Models;
using HubDeJogos.Views;
using HubDeJogos.Models.Enums;
using HubDeJogos.Hub.Repositories;
using HubDeJogos.Repositories;

namespace HubDeJogos.Controllers
{
    public class Hub
    {
        private readonly Tela _tela = new();
        private readonly List<Jogador> _jogadores;
        

        public Hub()
        {
            Jogadores jogadores = new Jogadores();
            jogadores.CarregarListaDeJogadores();
            
            Partidas.CarregarPartidas();


            if(jogadores.ListaDeJogadores != null)
                _jogadores = jogadores.ListaDeJogadores;
            else
                _jogadores= new List<Jogador>();
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
                    default:
                        Console.WriteLine("Opção não encontrada.");
                        break;
                }
            } while (opcao != "0");
        }

        private void AcessarMenuDeJogos()
        {
            _tela.ImprimirLogIn();
            if (_jogadores.Count < 2)
            {
                Console.WriteLine("Parece que ainda não temos dois jogadores para escolher essa opção!\n" +
                    "Redirecionando para Registar Novo Jogador.");

                for (int i = 0; i < 3; i++)
                {
                    Thread.Sleep(500);
                    Console.Write(". ");
                }
                Utilidades.Utilidades.AperteEnterParaContinuar();
                RegistrarJogador();
                AcessarMenuDeJogos();
                return;
            }

            _tela.ImprimirLogIn();
            Jogador? jogador2;
            Jogador? jogador1;
            do
            {
                jogador1 = Login();
            } while (jogador1 == null);

            do
            {
                jogador2 = Login();
            } while (jogador2 == null);


            if (jogador1 != jogador2)
            {
                MenuDeJogos menudeJogos = new(jogador1, jogador2, this);
                menudeJogos.Menu();
            }
            else
            {
                Console.WriteLine("Não é possível jogar contra si mesmo.");
                Utilidades.Utilidades.AperteEnterParaContinuar();
            }
        }

        private void RegistrarJogador()
        {
            _tela.ImprimirRegistrar();
            Console.Write("Nome do Usuário: ");
            string? nomeDoUsuário = Console.ReadLine();

            Console.Write("Senha: ");

            string senha = PedirSenha();
            
            if (nomeDoUsuário.Length > 1)
            {
                foreach (Jogador jogador in _jogadores)
                {
                    if(nomeDoUsuário.Equals(jogador.NomeDeUsuario))
                    {
                        Console.WriteLine("\nJogador já cadastrado.");
                        Utilidades.Utilidades.AperteEnterParaContinuar();
                        return;
                    }
                }
                if (senha.Length > 5 && senha.Length < 11)
                {
                    Jogador jogador = new Jogador(nomeDoUsuário, senha);
                    _jogadores.Add(jogador);
                    PassarListaDeJogadoresParaRepositorio();
                    Console.WriteLine("\nNovo jogador cadastrado com sucesso!!");
                }
                else
                    Console.WriteLine("Senha precisa ter entre 6 a 10 caracteres (letras e números).");
            }
            else
                Console.WriteLine("Nome de usuário precisa ter pelo menos 2 caracteres.");

            Utilidades.Utilidades.AperteEnterParaContinuar();
        }

        private void ListarJogadores()
        {
            _tela.ImprimirListaDeJogadores();
            if (_jogadores.Count < 1)
                Console.WriteLine("\nNenhum jogador cadastrado.");

            else            
                foreach (Jogador jogador in _jogadores)
                    Console.WriteLine(jogador + "\n");
            
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
                Console.WriteLine("\nNenhum jogador cadastrado.");
            else
            {
                for (int i = 0; i < jogadores.Count; i++)
                {
                    if (i > 10)
                        break;
                    if (jogadores[i].GetPontuacao(Jogo.JogoDaVelha)
                        + jogadores[i].GetPontuacao(Jogo.Xadrez) <= 0)
                        continue;
                    Console.WriteLine($"Top {i + 1}: {jogadores[i]}\n");
                }
            }
            Utilidades.Utilidades.AperteEnterParaContinuar();
        }



        // função auxiliar para achar o jogador, caso o login esteja correto
        private Jogador? Login()
        {
            _tela.ImprimirLogIn();
            Jogador? jogador = null;

            Console.Write("Nome do Usuário: ");
            string? nomeDeUsuario = Console.ReadLine();

            if (!(string.IsNullOrEmpty(nomeDeUsuario)))
            {
                Console.Write("Senha: ");
                string senha = PedirSenha();

                if (!(string.IsNullOrEmpty(senha)))
                {
                    jogador = _jogadores.Find(j => 
                    j.NomeDeUsuario.ToLower()
                    .Equals(nomeDeUsuario.ToLower()) && 
                    j.Senha.ToUpper().Equals(senha.ToUpper()));                                      
                }

            }
            if(jogador != null)
                Console.WriteLine("\nJogador logado com sucesso!");
            else
                Console.WriteLine("\nJogador não encontrado");
            Utilidades.Utilidades.AperteEnterParaContinuar();
            return  jogador;
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
                Console.WriteLine("Nenhuma partida foi registrada até o momento.");
            
            
            Utilidades.Utilidades.AperteEnterParaContinuar();
        }

        public void PassarListaDeJogadoresParaRepositorio()
        {
            Jogadores jogadores = new Jogadores();
            jogadores.SalvarJogadores(_jogadores);
        }
    }
}
