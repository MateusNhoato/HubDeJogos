using HubDeJogos.Models.Enums;
using HubDeJogos.Repositories;

namespace HubDeJogos.Models
{
    public class Jogador
    {      
        public string NomeDeUsuario { get; private set; }   
        public string Senha { get; private set; }

        public List<Partida> HistoricoDePartidas { get; private set; }

        public Jogador(string nomeDeUsuario, string senha)
        {
            NomeDeUsuario = nomeDeUsuario;
            Senha = senha;
            HistoricoDePartidas= new List<Partida>();
        }

        public Jogador(string login, string senha, List<Partida> historicoDePartidas) : this(login, senha)
        {
            HistoricoDePartidas = historicoDePartidas;
        }

        public int GetPontuacao(Jogo jogo)
        {
            int pontuacao = 0;

            foreach(Partida partida in HistoricoDePartidas) 
            {
                if(partida.Jogo == jogo)
                {
                    if (partida.Resultado < 0)
                        pontuacao--;
                    else if (partida.Resultado == 0)
                        pontuacao++;
                    else
                        pontuacao += 3;
                }
            }
            return pontuacao;
        }


        public override string ToString()
        {
            return $"{NomeDeUsuario} | Pontuação no JogoDaVelha da Velha [{GetPontuacao(Jogo.JogoDaVelha)}] | Pontuação no Xadrez [{GetPontuacao(Jogo.Xadrez)}] | Total [{GetPontuacao(Jogo.JogoDaVelha) + GetPontuacao(Jogo.Xadrez)}]";
        }
    }
}
