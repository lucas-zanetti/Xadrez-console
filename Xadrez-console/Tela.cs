﻿using System;
using System.Collections.Generic;
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
                    ImprimirPeca(tabuleiro.Peca(i, j));
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

        public static void ImprimirTabuleiro(Tabuleiro tabuleiro, bool[,] posicoesPossiveis)
        {
            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                Console.Write((8 - i) + " ");
                for (int j = 0; j < tabuleiro.Colunas; j++)
                {
                    if (posicoesPossiveis[i, j])
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                    ImprimirPeca(tabuleiro.Peca(i, j));
                    Console.BackgroundColor = ConsoleColor.Black;
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
            if (peca == null)
                Console.Write("_ ");
            else
            {
                if (peca.Cor == Cor.Branca)
                    Console.Write(peca + " ");
                else
                {
                    ConsoleColor consoleColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write(peca + " ");
                    Console.ForegroundColor = consoleColor;
                }
            }
        }

        public static void ImprimirPartida(PartidaDeXadrez partidaDeXadrez)
        {
            ImprimirTabuleiro(partidaDeXadrez.Tabuleiro);
            Console.WriteLine("\n");
            ImprimirPecasCapturadas(partidaDeXadrez);
            Console.WriteLine("Turno: " + partidaDeXadrez.Turno);
            if (partidaDeXadrez.Terminada == false)
            {
                Console.WriteLine("Aguardando jogada: " + partidaDeXadrez.JogadorAtual);
                if (partidaDeXadrez.Xeque)
                    Console.WriteLine("XEQUE!");
            }
            else
            {
                Console.WriteLine("XEQUEMATE!");
                Console.WriteLine("Vencedor: " + partidaDeXadrez.JogadorAtual);
            }
        }

        public static void ImprimirPecasCapturadas(PartidaDeXadrez partidaDeXadrez)
        {
            Console.WriteLine("Peças capturadas:");
            Console.Write("Brancas: ");
            ImprimirConjunto(partidaDeXadrez.PecasCapturadasDaCor(Cor.Branca));
            Console.Write("Pretas: ");
            ImprimirConjunto(partidaDeXadrez.PecasCapturadasDaCor(Cor.Preta));
            Console.WriteLine();
        }

        public static void ImprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[");
            foreach (Peca peca in conjunto)
                if (peca.Cor == Cor.Preta)
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write(peca);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(peca);
                }
            Console.WriteLine("]");
        }

        public static PosicaoTabuleiro LerPosicao()
        {
            string posicaoDigitada = Console.ReadLine();
            char colunaDigitada = posicaoDigitada[0];
            int linhaDigitada = int.Parse(posicaoDigitada[1] + "");
            return new PosicaoTabuleiro(colunaDigitada, linhaDigitada);
        }

    }
}
