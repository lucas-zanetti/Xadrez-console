using System;
using TabuleiroEntities;
using TabuleiroEntities.Exceptions;
using XadrezEntities;

namespace Xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PartidaDeXadrez partidaDeXadrez = new PartidaDeXadrez();

                Tela.ImprimirTabuleiro(partidaDeXadrez.Tabuleiro);
                while (partidaDeXadrez.Terminada == false)
                {
                    try
                    {
                        Console.Clear();
                        Tela.ImprimirPartida(partidaDeXadrez);
                        Console.WriteLine();
                        Console.Write("Origem: ");
                        Posicao origem = Tela.LerPosicao().ToPosicao();
                        partidaDeXadrez.ValidarPosicaoOrigem(origem);
                        bool[,] posicoesPossiveis = partidaDeXadrez.Tabuleiro.Peca(origem).MovimentosPossiveis();
                        Console.Clear();
                        Tela.ImprimirTabuleiro(partidaDeXadrez.Tabuleiro, posicoesPossiveis);
                        Console.WriteLine("\n");
                        Console.Write("Destino: ");
                        Posicao destino = Tela.LerPosicao().ToPosicao();
                        partidaDeXadrez.ValidarPosicaoDestino(origem, destino);
                        partidaDeXadrez.RealizaJogada(origem, destino);
                    }
                    catch(TabuleiroException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
                Console.Clear();
                Tela.ImprimirPartida(partidaDeXadrez);
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
