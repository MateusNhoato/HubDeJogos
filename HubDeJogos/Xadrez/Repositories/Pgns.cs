using System.Text;

namespace HubDeJogos.Xadrez.Repositories
{
    public static class Pgns
    {
        private static string _pathParcial = @"..\..\..\Xadrez\Repositories\Arquivos_pgn\";

        public static void CriarArquivoPgn(Pgn pgn)
        {
            string pathCompleto = _pathParcial + $"{pgn.Id}.pgn";
            try
            {
                File.Create(pathCompleto).Close();
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(_pathParcial);
                File.Create(pathCompleto).Close();
            }

            File.WriteAllText(pathCompleto, PgnParaString(pgn, true));
        }

        public static string PgnParaString(Pgn pgn, bool arquivo)
        {
            // espacamento é utilizado para imprimir o pgn no console depois da partida.
            string espacamento = string.Empty;
            if (!arquivo)
                espacamento = "  ";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{espacamento}[Event \"{pgn.Evento}\"]\n");
            sb.AppendLine($"{espacamento}[Site \"{pgn.Local}\"]\n");

            string data = $"{pgn.Data.Year}.{pgn.Data.Month}.{pgn.Data.Day}";
            sb.AppendLine($"{espacamento}[Date \"{data}\"]\n");
            sb.AppendLine($"{espacamento}[Round \"{pgn.Round}\"]\n");

            sb.AppendLine($"{espacamento}[White \"{pgn.JogadorBrancas}\"]\n");
            sb.AppendLine($"{espacamento}[Black \"{pgn.JogadorPretas}\"]\n");
            sb.AppendLine($"{espacamento}[Result \"{pgn.Resultado}\"]\n");

            sb.Append($"{espacamento}");
            foreach (string jogada in pgn.Jogadas)
            {
                sb.Append(jogada + " ");
            }
            sb.Append($"{pgn.Resultado}");

            return sb.ToString();
        }

    }
}
