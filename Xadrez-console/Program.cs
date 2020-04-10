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
                    Console.Clear();
                    Tela.ImprimirTabuleiro(partidaDeXadrez.Tabuleiro);
                    Console.WriteLine("\n");
                    Console.Write("Origem: ");
                    Posicao origem = Tela.LerPosicao().ToPosicao();
                    bool[,] posicoesPossiveis = partidaDeXadrez.Tabuleiro.Peca(origem).MovimentosPossiveis();                    
                    Console.Clear();
                    Tela.ImprimirTabuleiro(partidaDeXadrez.Tabuleiro,posicoesPossiveis);
                    Console.WriteLine("\n");
                    Console.Write("Destino: ");
                    Posicao destino = Tela.LerPosicao().ToPosicao();
                    partidaDeXadrez.ExecutarMovimento(origem, destino);
                }
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }            
        }
    }
}
