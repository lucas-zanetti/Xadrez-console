using TabuleiroEntities;

namespace XadrezEntities
{
    class Rei : Peca
    {
        private PartidaDeXadrez _partida;
        public Rei(Tabuleiro tabuleiro, Cor cor, PartidaDeXadrez partida) : base(cor, tabuleiro)
        {
            _partida = partida;
        }
        public override string ToString()
        {
            return "R";
        }

        private bool PodeMover(Posicao posicaoDesejada)
        {
            Peca pecaMovida = Tabuleiro.Peca(posicaoDesejada);
            return pecaMovida == null || pecaMovida.Cor != Cor;
        }
        public override bool[,] MovimentosPossiveis()
        {
            bool[,] movimentosPossiveis = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];
            Posicao p = new Posicao(0, 0);

            //Norte
            p.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida(p) && PodeMover(p))
                movimentosPossiveis[p.Linha, p.Coluna] = true;

            //Nordeste
            p.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(p) && PodeMover(p))
                movimentosPossiveis[p.Linha, p.Coluna] = true;

            //Leste
            p.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(p) && PodeMover(p))
                movimentosPossiveis[p.Linha, p.Coluna] = true;

            //Sudeste
            p.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(p) && PodeMover(p))
                movimentosPossiveis[p.Linha, p.Coluna] = true;

            //Sul
            p.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida(p) && PodeMover(p))
                movimentosPossiveis[p.Linha, p.Coluna] = true;

            //Sudoeste
            p.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(p) && PodeMover(p))
                movimentosPossiveis[p.Linha, p.Coluna] = true;

            //Oeste
            p.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(p) && PodeMover(p))
                movimentosPossiveis[p.Linha, p.Coluna] = true;

            //Noroeste
            p.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(p) && PodeMover(p))
                movimentosPossiveis[p.Linha, p.Coluna] = true;

            //#Jogada Especial Roque
            if (QuantidadeMovimentos == 0 && _partida.Xeque == false)
            {
                //Pequeno
                Posicao posicaoTorreCurta = new Posicao(Posicao.Linha, Posicao.Coluna + 3);
                if (TesteTorreRoque(posicaoTorreCurta))
                {
                    Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna + 2);
                    if (Tabuleiro.Peca(p1) == null && Tabuleiro.Peca(p2) == null)
                    {
                        movimentosPossiveis[Posicao.Linha, Posicao.Coluna + 2] = true;
                    }
                }
                //Grande
                Posicao posicaoTorreLonga = new Posicao(Posicao.Linha, Posicao.Coluna - 4);
                if (TesteTorreRoque(posicaoTorreLonga))
                {
                    Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna - 2);
                    Posicao p3 = new Posicao(Posicao.Linha, Posicao.Coluna - 3);
                    if (Tabuleiro.Peca(p1) == null && Tabuleiro.Peca(p2) == null && Tabuleiro.Peca(p3) == null)
                    {
                        movimentosPossiveis[Posicao.Linha, Posicao.Coluna - 2] = true;
                    }
                }
            }                        
            return movimentosPossiveis;
        }
        private bool TesteTorreRoque(Posicao posicaoTorre)
        {
            Peca torre = Tabuleiro.Peca(posicaoTorre);
            return torre != null && torre is Torre && torre.Cor == Cor && torre.QuantidadeMovimentos == 0;
        }
    }
}
