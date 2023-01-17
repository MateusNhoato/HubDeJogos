using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubDeJogos.Xadrez.Repositories
{
    public static class Pgns
    {
        private static string _pathParcial = @"..\..\..\Xadrez\Repositories\Data\";


        public static void CriarArquivoPgn(Pgn pgn)
        {
            string pathCompleto = _pathParcial + $"{pgn.Id}.pgn";
            File.Create(pathCompleto).Close();
            
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"[Event \"{pgn.Evento}\"]\n");
            sb.AppendLine($"[Site \"{pgn.Local}\"]\n");

            string data = $"{pgn.Data.Year}.{pgn.Data.Month}.{pgn.Data.Day}";
            sb.AppendLine($"[Date \"{data}\"]\n");
            sb.AppendLine($"[Round \"{pgn.Round}\"]\n");

            sb.AppendLine($"[White \"{pgn.JogadorBrancas}\"]\n");
            sb.AppendLine($"[Black \"{pgn.JogadorPretas}\"]\n");
            sb.AppendLine($"[Result \"{pgn.Resultado}\"]\n");

            foreach(string jogada in pgn.Jogadas)
            {
                sb.Append(jogada+ " ");
            }
            sb.Append($"{pgn.Resultado}");
            
            File.WriteAllText(pathCompleto, sb.ToString());
        }


    }
}
