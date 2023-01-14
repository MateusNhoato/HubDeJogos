using HubDeJogos.Models.Enums;
namespace HubDeJogos.Repositories
{
    public class Partida
    {
        public Guid Id { get; private set; }
        public Jogo Jogo { get; private set; }
       
        public Resultado Resultado { get; private set; }

        public Partida(Jogo jogo)
        {
            Id = new Guid();
            Jogo = jogo;
        }
        public Partida(Guid id, Jogo jogo, Resultado resultado) : this(jogo)
        {
            Id = id;
            Resultado = resultado;
        }


        public void ResultadoVitoria() => Resultado = Resultado.Vitoria;
        public void ResultadoDerrota() => Resultado = Resultado.Derrota;
        public void ResultadoEmpate() => Resultado = Resultado.Empate;
    }
}
