﻿using System.Media;
using System.Runtime.InteropServices;

namespace Utilidades
{
    internal static class Som
    {

        public static void Musica(Musica musica)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                SoundPlayer sound = new SoundPlayer($@"..\..\..\Utilidades\Sons\musicas\{musica}.wav");

                sound.PlayLooping();
            }
        }

        public static void ReproduzirEfeito(Efeito efeitoDeSom)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                SoundPlayer sound = new SoundPlayer($@"..\..\..\Utilidades\Sons\{efeitoDeSom}.wav");
                sound.Play();
            }
        }

        public static void JogoDaVelha(string simbolo)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                SoundPlayer sound = new SoundPlayer($@"..\..\..\Utilidades\Sons\jogodavelha\{simbolo.Trim()}.wav");
                sound.Play();
            }
        }

        public static void JogoDaVelhaResultado(string resultado)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                SoundPlayer sound = new SoundPlayer(@"..\..\..\Utilidades\Sons\empate.wav");

                if (resultado != "Velha")
                    sound.SoundLocation = @"..\..\..\Utilidades\Sons\vitoria.wav";
                Thread.Sleep(200);
                sound.Play();
            }
        }

        public static void MovimentoPecaXadrez(string jogada)
        {

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                SoundPlayer sound = new SoundPlayer($@"..\..\..\Utilidades\Sons\xadrez\mov_{new Random().Next(1, 8)}.wav");
                sound.Play();


                if (jogada.Contains("O-O"))
                {
                    Thread.Sleep(200);
                    sound.Play();
                }
                else
                {
                    if (jogada.Contains('x'))
                    {
                        sound.SoundLocation = $@"..\..\..\Utilidades\Sons\xadrez\captura.wav";
                        Thread.Sleep(200);
                        sound.Play();
                    }

                    if (jogada.Contains('='))
                    {
                        sound.SoundLocation = $@"..\..\..\Utilidades\Sons\xadrez\promocao.wav";
                        Thread.Sleep(200);
                        sound.Play();
                    }
                }
            }
        }
        public static void XequeOuXequemate(bool xeque, bool xequemate, bool empate)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                SoundPlayer sound = new SoundPlayer(@"..\..\..\Utilidades\Sons\vitoria.wav");

                if (xequemate)
                {
                    if (empate)
                        sound.SoundLocation = @"..\..\..\Utilidades\Sons\empate.wav";
                }
                else
                {
                    if (xeque)
                        sound.SoundLocation = @"..\..\..\Utilidades\Sons\xadrez\xeque.wav";
                }
                if (xeque || empate)
                    sound.Play();
            }
        }
    }
}