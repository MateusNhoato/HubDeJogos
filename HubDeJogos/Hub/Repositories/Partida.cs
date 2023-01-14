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
        public string Jogador { get; set; }
        public string Oponente { get; set; }

       
        public Partida(Jogo jogo, string jogador, string oponente)
        {
            DateTime = DateTime.Now;
            Jogo = jogo;
            Jogador = jogador;
            Oponente = oponente;
        }

        [JsonConstructor]
        public Partida(DateTime dateTime, Jogo jogo, string jogador , string oponente, Resultado resultado) : this(jogo, jogador, oponente)
        {
            DateTime = dateTime;
            Resultado = resultado;
        }


        public override string ToString()
        {
            DateTime dateTime = new DateTime(DateTime.Year, DateTime.Month, DateTime.Day, DateTime.Hour, DateTime.Minute, 0, DateTime.Kind);
            StringBuilder sb = new StringBuilder();

            return $"{Utilidades.Utilidades.Linha}" +
                $"Partida de {Jogo} | {dateTime}\n" +
                $"{Jogador} contra {Oponente}\n" +
                $"Resultado: {Resultado}\n";
        }
    }
}
