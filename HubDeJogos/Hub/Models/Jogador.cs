using HubDeJogos.Models.Enums;
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
                    // se não foi empate vejo o nome de quem ganhou para confiar o vitorioso
                    if (partida.JogadorGanhou.Equals(NomeDeUsuario))
                        pontuacao += 3;
                    else
                        pontuacao--;
                }
                else
                    pontuacao++;
            }
            return pontuacao;
        }


        public int GetPontuacao(Jogo jogo)
        {
            int pontuacao = 0;

            foreach (Partida partida in HistoricoDePartidas)
            {
                if (partida.Jogo.Equals(jogo))
                {
                    if (partida.Resultado != Resultado.Empate)
                    {
                        // se não foi empate vejo o nome de quem ganhou para confiar o vitorioso
                        if (partida.JogadorGanhou.Equals(NomeDeUsuario))
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

        public void AlterarNomeDeUsuario(string nomeDeUsuario) => NomeDeUsuario = nomeDeUsuario;
        public void AlterarSenha(string senha) => Senha = senha;




        public string Pontuacoes()
        {
            return 
                $"       Pontuações\n" +
                $"  | Jogo da Velha [{GetPontuacao(Jogo.JogoDaVelha)}] |\n" +
                $"  | Xadrez        [{GetPontuacao(Jogo.Xadrez)}] |\n" +
                $"  | Batalha Naval [{GetPontuacao(Jogo.BatalhaNaval)}] |\n" +
                $"  |-------------------|\n" +
                $"  | Total         [{GetPontuacao()}] |";
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
