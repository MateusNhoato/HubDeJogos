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


        public override string ToString()
        {
            DateTime dateTime = new DateTime(DateTime.Year, DateTime.Month, DateTime.Day, DateTime.Hour, DateTime.Minute, 0, DateTime.Kind);
            string[] tabuleiroArray = Tabuleiro.TabuleiroParaImpressao().Split(';');
            int countAux = 0;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{Utilidades.Utilidades.Linha}\n");
            sb.AppendLine($"Partida de {Jogo} | {dateTime}\n");
            sb.AppendLine($"{NomeJogadorGanhou} VS {NomeJogadorPerdeu}\n");
            
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
                sb.AppendLine($"Vencedor: {NomeJogadorGanhou}\n");


            return sb.ToString();
        }
    }
}
