using HubDeJogos.Xadrez.Models.Enums;

namespace HubDeJogos.Xadrez.Repositories
{
    public class Pgn
    {
        public Guid Id { get; private set; }
        public string Evento { get; private set; } = null!;
        public string Local { get; private set; } = null!;
        public DateOnly Data { get; private set; }
        public string Round { get; private set; }
        public string JogadorBrancas { get; private set; } = null!;
        public string JogadorPretas { get; private set; } = null!;
        public string Resultado { get; set; }
        public List<string>? Jogadas { get; private set; }

        public Pgn(string jogadorBrancas, string jogadorPretas)
        {
            Id = Guid.NewGuid();
            Data = DateOnly.FromDateTime(DateTime.Now);
            Evento = "Casual Game";
            Local = "?";
            Round = "-";
            Resultado = "*";
            JogadorBrancas = jogadorBrancas;
            JogadorPretas = jogadorPretas;
            Jogadas = new List<string>();
        }
    }
}
