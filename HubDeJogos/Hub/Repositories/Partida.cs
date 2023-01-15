using HubDeJogos.Hub.Models.Interfaces;
using HubDeJogos.Models;
using HubDeJogos.Models.Enums;
using Newtonsoft.Json;
using System.Text;

namespace HubDeJogos.Repositories
{
    public class Partida
    {
        public DateTime DateTime { get; private set; }
        public Jogo Jogo { get; private set; }
       
        public Resultado Resultado { get; set; }
        public string NomeJogadorGanhou { get; set; }
        public string NomeJogadorPerdeu { get; set; }
        public Tabuleiro Tabuleiro { get; set; }
       
        public Partida(Jogo jogo, string nomeJogadorGanhou, string nomeJogadorPerdeu, Resultado resultado, Tabuleiro tabuleiro)
        {
            DateTime = DateTime.Now;
            Jogo = jogo;
            NomeJogadorGanhou = nomeJogadorGanhou;
            NomeJogadorPerdeu = nomeJogadorPerdeu;
            Resultado = resultado;      
            Tabuleiro = tabuleiro;
        }

        [JsonConstructor]
        public Partida(DateTime dateTime, Jogo jogo, string nomeJogadorGanhou , string nomeJogadorPerdeu, Resultado resultado, Tabuleiro tabuleiro) : this(jogo, nomeJogadorGanhou, nomeJogadorPerdeu, resultado, tabuleiro)
        {
            DateTime = dateTime;            
        }

    }
}
