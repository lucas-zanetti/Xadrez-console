using System;
using TabuleiroEntities;

namespace Xadrez_console
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            for(int i = 0; i < tabuleiro.Linhas; i++)
            {
                for (int j = 0; j < tabuleiro.Linhas; j++)
                {
                    if(tabuleiro.Peca(i,j)==null)
                        Console.Write("_ ");
                    else
                        Console.Write(tabuleiro.Peca(i, j) + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
