using HubDeJogos.Repositories;

namespace HubDeJogos.Models
{
    public class Jogador
    {      
        public string Login { get; private set; }
        public string Senha { get; private set; }

        public List<Partida> HistoricoDePartidas { get; private set; }

        public Jogador(string login, string senha)
        {
            Login = login;
            Senha = senha;
            HistoricoDePartidas= new List<Partida>();
        }

        public Jogador(string login, string senha, List<Partida> historicoDePartidas) : this(login, senha)
        {
            HistoricoDePartidas = historicoDePartidas;
        }
    }
}
