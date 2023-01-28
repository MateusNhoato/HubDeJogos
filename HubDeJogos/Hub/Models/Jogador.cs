using HubDeJogos.Models.Enums;
using HubDeJogos.Repositories;
using Newtonsoft.Json;
using System.Text;

namespace HubDeJogos.Models
{
    [JsonObject]
    public class Jogador
    {
        public string NomeDeUsuario { get; private set; }
        public string Senha { get; private set; }

        public List<Partida>? HistoricoDePartidas { get; private set; } = new List<Partida>();

        private const int PONTOS_POR_VITORIA = 3;
        private const int PONTOS_POR_DERROTA = -1;
        private const int PONTOS_POR_EMPATE = 1;

        public Jogador(string nomeDeUsuario, string senha)
        {
            NomeDeUsuario = nomeDeUsuario;
            Senha = senha;
        }

        [JsonConstructor]
        public Jogador(string nomeDeUsuario, string senha, List<Partida> historicoDePartidas) : this(nomeDeUsuario, senha)
        {

            HistoricoDePartidas = historicoDePartidas;
        }

        public int GetPontuacao()
        {
            int pontuacao = 0;

            foreach (Partida partida in HistoricoDePartidas)
            {
                if (partida.Resultado != Resultado.Empate)
                {
                    // se não foi empate vejo o nome de quem ganhou para confirmar o vitorioso
                    if (partida.JogadorGanhou.Equals(NomeDeUsuario))
                        pontuacao += PONTOS_POR_VITORIA;
                    else
                        pontuacao += PONTOS_POR_DERROTA;
                }
                else
                    pontuacao += PONTOS_POR_EMPATE;
            }
            return pontuacao;
        }


        private int GetPontuacao(Jogo jogo)
        {
            int pontuacao = 0;

            foreach (Partida partida in HistoricoDePartidas)
            {
                if (partida.Jogo.Equals(jogo))
                {
                    if (partida.Resultado != Resultado.Empate)
                    {
                        // se não foi empate vejo o nome de quem ganhou para confirmar o vitorioso
                        if (partida.JogadorGanhou.Equals(NomeDeUsuario))
                            pontuacao += PONTOS_POR_VITORIA;
                        else
                            pontuacao += PONTOS_POR_DERROTA;
                    }
                    else
                        pontuacao += PONTOS_POR_EMPATE;
                }
            }
            return pontuacao;
        }

        public void AlterarNomeDeUsuario(string nomeDeUsuario) => NomeDeUsuario = nomeDeUsuario;
        public void AlterarSenha(string senha) => Senha = senha;




        public string Pontuacoes()
        {
            StringBuilder sb = new();
            sb.AppendLine("     Pontuações");

            var jogos = Enum.GetValues(typeof(Jogo));

            foreach(Jogo jogo in jogos)
            {
                sb.AppendLine($"  | {jogo} [{GetPontuacao(jogo)}] |");
            }

            sb.AppendLine($"  | Total [{GetPontuacao()}] |");

            return sb.ToString();
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
            return $"{NomeDeUsuario}";
        }

        public override int GetHashCode()
        {
            return NomeDeUsuario.GetHashCode() + Senha.GetHashCode();
        }
    }
}
