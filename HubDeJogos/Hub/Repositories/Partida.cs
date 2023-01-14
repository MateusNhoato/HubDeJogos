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
        public Tabuleiro Tabuleiro { get; set; }
       
        public Partida(Jogo jogo, string jogador, string oponente)
        {
            DateTime = DateTime.Now;
            Jogo = jogo;
            Jogador = jogador;
            Oponente = oponente;
        }

        [JsonConstructor]
        public Partida(DateTime dateTime, Jogo jogo, string jogador , string oponente, Resultado resultado, Tabuleiro tabuleiro) : this(jogo, jogador, oponente)
        {
            DateTime = dateTime;
            Resultado = resultado;
            Tabuleiro = tabuleiro;
        }


        public override string ToString()
        {
            DateTime dateTime = new DateTime(DateTime.Year, DateTime.Month, DateTime.Day, DateTime.Hour, DateTime.Minute, 0, DateTime.Kind);
            string[] tabuleiroArray = Tabuleiro.TabuleiroParaImpressao().Split(';');
            int countAux = 0;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{Utilidades.Utilidades.Linha}\n");
            sb.AppendLine($"Partida de {Jogo} | {dateTime}\n");
            sb.AppendLine($"{Jogador} contra {Oponente}\n");
            

            for(int i = 0; i< Tabuleiro.Tamanho; i++)
            {
                for(int j=0; j<Tabuleiro.Tamanho; j++)
                {
                    sb.Append(tabuleiroArray[countAux++]);
                }
                sb.AppendLine();
            }
            sb.AppendLine();
            if(Resultado == Resultado.Empate)
                sb.AppendLine($"Resultado: {Resultado}\n");
            else
                sb.AppendLine($"Resultado: {Resultado} de {Jogador}\n");


            return sb.ToString();
        }
    }
}
