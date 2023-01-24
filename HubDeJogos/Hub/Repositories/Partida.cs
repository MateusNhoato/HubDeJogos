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
        public Tabuleiro? Tabuleiro2 { get; set; }

        public Partida(Jogo jogo, string jogador1, string jogador2, string jogadorGanhou, Resultado resultado, Tabuleiro tabuleiro, Tabuleiro? tabuleiro2)
        {
            DateTime = DateTime.Now;
            JogadorGanhou = jogadorGanhou;
            Jogo = jogo;
            Jogador1 = jogador1;
            Jogador2 = jogador2;
            Resultado = resultado;
            Tabuleiro = tabuleiro;
            Tabuleiro2 = tabuleiro2;
        }

        [JsonConstructor]
        public Partida(DateTime dateTime, Jogo jogo, string jogador1, string jogador2, string jogadorGanhou, Resultado resultado, Tabuleiro tabuleiro, Tabuleiro tabuleiro2) : this(jogo, jogador1, jogador2, jogadorGanhou, resultado, tabuleiro, tabuleiro2)
        {
            DateTime = dateTime;
        }

    }
}
