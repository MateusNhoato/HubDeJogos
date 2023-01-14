using HubDeJogos.Models;
using HubDeJogos.Models.Enums;
namespace HubDeJogos.Repositories
{
    public class Partida
    {
        public Guid Id { get; private set; }
        public Jogo Jogo { get; private set; }
       
        public Resultado Resultado { get; set; }
        public Jogador Jogador { get; set; }
        public Jogador Oponente { get; set; }

        public Partida(Jogo jogo)
        {
            Id = new Guid();
            Jogo = jogo;
        }
        public Partida(Guid id, Jogo jogo,Jogador jogador ,Jogador oponente, Resultado resultado) : this(jogo)
        {
            Id = id;
            Jogador = jogador;
            Oponente = oponente;
            Resultado = resultado;
        }


        public override string ToString()
        {
            return $"Partida de {Jogo} | Id: {Id}:\n" +
                   $"{Jogador.NomeDeUsuario} contra {Oponente.NomeDeUsuario}\n" +                 
                   $"Resultado: {Resultado}\n";
        }
    }
}
