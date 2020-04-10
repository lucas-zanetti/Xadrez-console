﻿using TabuleiroEntities;

namespace XadrezEntities
{
    class Dama : Peca
    {
        public Dama(Tabuleiro tabuleiro, Cor cor) : base(cor, tabuleiro)
        {
        }
        public override string ToString()
        {
            return "D";
        }
    }
}