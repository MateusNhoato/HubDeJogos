using HubDeJogos.Models;
using HubDeJogos.Models.Enums;
using Newtonsoft.Json;

namespace HubDeJogos.Repositories
{
    public class Partida
    {
        public DateTime DateTime { get; private set; }
        public Jogo Jogo { get; private set; }

        public Resultado Resultado { get; set; }
        public string? JogadorGanhou { get; set; }
        public string Jogador1 { get; set; }
        public string Jogador2 { get; set; }
        public Tabuleiro Tabuleiro { get; set; }

        public Partida(Jogo jogo, string jogador1, string jogador2, string jogadorGanhou, Resultado resultado, Tabuleiro tabuleiro)
        {
            DateTime = DateTime.Now;
            JogadorGanhou = jogadorGanhou;
            Jogo = jogo;
            Jogador1 = jogador1;
            Jogador2 = jogador2;
            Resultado = resultado;
            Tabuleiro = tabuleiro;
        }

        [JsonConstructor]
        public Partida(DateTime dateTime, Jogo jogo, string jogador1, string jogador2, string jogadorGanhou, Resultado resultado, Tabuleiro tabuleiro) : this(jogo, jogador1, jogador2, jogadorGanhou, resultado, tabuleiro)
        {
            DateTime = dateTime;
        }

    }
}
