using System;
using TabuleiroEntities;
using TabuleiroEntities.Exceptions;
using XadrezEntities;

namespace Xadrez_console
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                Console.Write((8 - i) + " ");
                for (int j = 0; j < tabuleiro.Colunas; j++)
                {
                    if (tabuleiro.Peca(i, j) == null)
                        Console.Write("_ ");
                    else
                        ImprimirPeca(tabuleiro.Peca(i, j));
                }
                Console.WriteLine();
            }
            Console.Write("  ");
            for (int i = 65; i < (tabuleiro.Colunas + 65); i++)
            {
                if (i <= 90)
                    Console.Write(Convert.ToChar(i) + " ");
                else
                    throw new TabuleiroException("Número de colunas excederam o limite!");
            }
        }

        public static void ImprimirPeca(Peca peca)
        {
            if (peca.Cor == Cor.Branca)
                Console.Write(peca+" ");
            else
            {
                ConsoleColor consoleColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write(peca+" ");
                Console.ForegroundColor = consoleColor;
            }
        }

        public static PosicaoTabuleiro LerPosicao()
        {
            string posicaoDigitada = Console.ReadLine();
            char colunaDigitada = posicaoDigitada[0];
            int linhaDigitada = int.Parse(posicaoDigitada[1]+"");
            return new PosicaoTabuleiro(colunaDigitada, linhaDigitada);
        }
      
    }
}
