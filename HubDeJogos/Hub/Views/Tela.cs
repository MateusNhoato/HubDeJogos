﻿using HubDeJogos.Repositories;

namespace HubDeJogos.Views
{
    public class Tela
    {
        private readonly static string _hub = @"
 __   __  __   __  _______    ______   _______        ___  _______  _______  _______  _______ 
|  | |  ||  | |  ||  _    |  |      | |       |      |   ||       ||       ||       ||       |
|  |_|  ||  | |  || |_|   |  |  _    ||    ___|      |   ||   _   ||    ___||   _   ||  _____|
|       ||  |_|  ||       |  | | |   ||   |___       |   ||  | |  ||   | __ |  | |  || |_____ 
|       ||       ||  _   |   | |_|   ||    ___|   ___|   ||  |_|  ||   ||  ||  |_|  ||_____  |
|   _   ||       || |_|   |  |       ||   |___   |       ||       ||   |_| ||       | _____| |
|__| |__||_______||_______|  |______| |_______|  |_______||_______||_______||_______||_______|";

        private readonly static string _jogos = @"
     ___  _______  _______  _______  _______ 
    |   ||       ||       ||       ||       |
    |   ||   _   ||    ___||   _   ||  _____|
    |   ||  | |  ||   | __ |  | |  || |_____ 
 ___|   ||  |_|  ||   ||  ||  |_|  ||_____  |
|       ||       ||   |_| ||       | _____| |
|_______||_______||_______||_______||_______|";

        private readonly static string _registrar = @"
 ______    _______  _______  ___   _______  _______  ______    _______  ______   
|    _ |  |       ||       ||   | |       ||       ||    _ |  |   _   ||    _ |  
|   | ||  |    ___||    ___||   | |  _____||_     _||   | ||  |  |_|  ||   | ||  
|   |_||_ |   |___ |   | __ |   | | |_____   |   |  |   |_||_ |       ||   |_||_ 
|    __  ||    ___||   ||  ||   | |_____  |  |   |  |    __  ||       ||    __  |
|   |  | ||   |___ |   |_| ||   |  _____| |  |   |  |   |  | ||   _   ||   |  | |
|___|  |_||_______||_______||___| |_______|  |___|  |___|  |_||__| |__||___|  |_|";

        private readonly static string _entrar = @"
 _______  __    _  _______  ______    _______  ______   
|       ||  |  | ||       ||    _ |  |   _   ||    _ |  
|    ___||   |_| ||_     _||   | ||  |  |_|  ||   | ||  
|   |___ |       |  |   |  |   |_||_ |       ||   |_||_ 
|    ___||  _    |  |   |  |    __  ||       ||    __  |
|   |___ | | |   |  |   |  |   |  | ||   _   ||   |  | |
|_______||_|  |__|  |___|  |___|  |_||__| |__||___|  |_|";

        private readonly static string _jogadores = @"
     ___  _______  _______  _______  ______   _______  ______    _______  _______ 
    |   ||       ||       ||   _   ||      | |       ||    _ |  |       ||       |
    |   ||   _   ||    ___||  |_|  ||  _    ||   _   ||   | ||  |    ___||  _____|
    |   ||  | |  ||   | __ |       || | |   ||  | |  ||   |_||_ |   |___ | |_____ 
 ___|   ||  |_|  ||   ||  ||       || |_|   ||  |_|  ||    __  ||    ___||_____  |
|       ||       ||   |_| ||   _   ||       ||       ||   |  | ||   |___  _____| |
|_______||_______||_______||__| |__||______| |_______||___|  |_||_______||_______|";
        private readonly static string _ranking = @"
 ______    _______  __    _  ___   _  ___   __    _  _______ 
|    _ |  |   _   ||  |  | ||   | | ||   | |  |  | ||       |
|   | ||  |  |_|  ||   |_| ||   |_| ||   | |   |_| ||    ___|
|   |_||_ |       ||       ||      _||   | |       ||   | __ 
|    __  ||       ||  _    ||     |_ |   | |  _    ||   ||  |
|   |  | ||   _   || | |   ||    _  ||   | | | |   ||   |_| |
|___|  |_||__| |__||_|  |__||___| |_||___| |_|  |__||_______|";

        private readonly static string _historico = @"
 __   __  ___   _______  _______  _______  ______    ___   _______  _______ 
|  | |  ||   | |       ||       ||       ||    _ |  |   | |       ||       |
|  |_|  ||   | |  _____||_     _||   _   ||   | ||  |   | |       ||   _   |
|       ||   | | |_____   |   |  |  | |  ||   |_||_ |   | |       ||  | |  |
|       ||   | |_____  |  |   |  |  |_|  ||    __  ||   | |      _||  |_|  |
|   _   ||   |  _____| |  |   |  |       ||   |  | ||   | |     |_ |       |
|__| |__||___| |_______|  |___|  |_______||___|  |_||___| |_______||_______|";

        #region Hub
        public void ImprimirMenuDoHub()
        {
            Console.Clear();
            Console.WriteLine(_hub + "\n\n");
            Console.WriteLine("1- Acessar Menu de Jogos");
            Console.WriteLine("2- Registrar Novo Jogador");
            Console.WriteLine("3- Listar Jogadores");
            Console.WriteLine("4- Ranking dos Jogadores");
            Console.WriteLine("5- Historico de Partidas");
            Console.WriteLine("0- Sair");
            Console.Write("\nDigite a opção desejada: ");
        }
        public void ImprimirLogIn()
        {
            Console.Clear();
            Console.WriteLine(_entrar + "\n");
            Console.WriteLine("Para começar a jogar, ambos os jogadores  precisam estar logados.\n\n");
        }
        public void ImprimirRegistrar()
        {
            Console.Clear();
            Console.WriteLine(_registrar + "\n");
            Console.WriteLine("Preencha as informações para cadastrar um novo jogador.\n\n");
        }

        public void ImprimirListaDeJogadores()
        {
            Console.Clear();
            Console.WriteLine(_jogadores + "\n");
            Console.WriteLine("Todos os jogadores.\n\n");
        }

        public void ImprimirRanking()
        {
            Console.Clear();
            Console.WriteLine(_ranking + "\n");
            Console.WriteLine("Os melhores jogadores em ordem de pontuação!!\n\n");
        }
        #endregion
        #region MenuDeJogos
        public void ImprimirMenuDeJogos()
        {
            Console.Clear();
            Console.WriteLine(_jogos + "\n\n");
            Console.WriteLine("1- Jogar");
            Console.WriteLine("2- Histórico Dos Jogadores");
            Console.WriteLine("0- Voltar");
            Console.Write("\nDigite a opção desejada: ");
        }

        public void ImprimirMenuDeEscolhaDeJogos()
        {
            Console.Clear();
            Console.WriteLine(_jogos + "\n\n");
            Console.WriteLine("1- Jogo da Velha");
            Console.WriteLine("2- Xadrez");
            Console.WriteLine("0- Voltar");
            Console.Write("\nDigite a opção desejada: ");
        }

        public void ImprimirHistoricoMenu(string? nomeDoJogador)
        {
            Console.Clear();
            Console.WriteLine(_historico + "\n");
            if(nomeDoJogador!= null)
                Console.WriteLine($"Histórico de {nomeDoJogador}:\n");
        }
        public void ImprimirHistoricoMenu(string nomeDoJogador1, string nomeDoJogador2)
        {
            Console.Clear();
            Console.WriteLine(_historico + "\n\n");
            Console.WriteLine($"1- Histórico De {nomeDoJogador1}");
            Console.WriteLine($"2- Histórico De {nomeDoJogador2}");
            Console.WriteLine("0- Voltar");
            Console.Write("\nDigite a opção desejada: ");
        }
        #endregion

        #region Jogador
        public void ImprimirHistoricoDoJogador(List<Partida> partidas)
        {
            foreach(Partida partida in partidas) 
            {
                Console.WriteLine(partida);
            }
            Utilidades.Utilidades.AperteEnterParaContinuar();
        }
        #endregion
    }
}
