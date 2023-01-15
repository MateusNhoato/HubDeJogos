﻿using HubDeJogos.Models.Enums;
using HubDeJogos.Repositories;
using Newtonsoft.Json;


namespace HubDeJogos.Models
{
    [JsonObject]
    public class Jogador
    {      
        public string NomeDeUsuario { get; private set; }
        public string Senha { get; private set; }
        
        public List<Partida>? HistoricoDePartidas { get; private set; } = new List<Partida>();

        public Jogador(string nomeDeUsuario, string senha)
        {
            NomeDeUsuario = nomeDeUsuario;
            Senha = senha;
        }
        [Newtonsoft.Json.JsonConstructor]
        public Jogador(string nomeDeUsuario, string senha, List<Partida> historicoDePartidas) : this(nomeDeUsuario, senha)
        {

            HistoricoDePartidas = historicoDePartidas;
        }

        public int GetPontuacao(Jogo jogo)
        {
            int pontuacao = 0;
            
            foreach(Partida partida in HistoricoDePartidas) 
            {
                if(partida.Jogo.Equals(jogo))
                {
                    if (partida.Resultado != Resultado.Empate)
                    {
                        // se não foi empate vejo o nome de quem ganhou para confiar o vitorioso
                        if (partida.NomeJogadorGanhou.Equals(NomeDeUsuario))
                            pontuacao += 3;
                        else
                            pontuacao--;                        
                    }
                    else
                        pontuacao++;
                }
            }
            return pontuacao;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Jogador)
                return false;
            Jogador other = obj as Jogador; 
            return NomeDeUsuario.Equals(other.NomeDeUsuario);
        }

        public override string ToString()
        {
            return $"{NomeDeUsuario} | Pontuação Jogo da Velha [{GetPontuacao(Jogo.JogoDaVelha)}] | Pontuação Xadrez [{GetPontuacao(Jogo.Xadrez)}] | Total [{GetPontuacao(Jogo.JogoDaVelha) + GetPontuacao(Jogo.Xadrez)}]";
        }

        public override int GetHashCode()
        {
            return NomeDeUsuario.GetHashCode() + Senha.GetHashCode();
        }
    }
}