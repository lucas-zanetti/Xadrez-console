using TabuleiroEntities;

namespace XadrezEntities
{
    class Cavalo : Peca
    {
        public Cavalo(Tabuleiro tabuleiro, Cor cor) : base(cor, tabuleiro)
        {
        }
        public override string ToString()
        {
            return "C";
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
            p.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 2);
            if (Tabuleiro.PosicaoValida(p) && PodeMover(p))
                movimentosPossiveis[p.Linha, p.Coluna] = true;

            p.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 2);
            if (Tabuleiro.PosicaoValida(p) && PodeMover(p))
                movimentosPossiveis[p.Linha, p.Coluna] = true;

            p.DefinirValores(Posicao.Linha - 2, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(p) && PodeMover(p))
                movimentosPossiveis[p.Linha, p.Coluna] = true;

            p.DefinirValores(Posicao.Linha - 2, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(p) && PodeMover(p))
                movimentosPossiveis[p.Linha, p.Coluna] = true;

            //Leste
            p.DefinirValores(Posicao.Linha - 2, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(p) && PodeMover(p))
                movimentosPossiveis[p.Linha, p.Coluna] = true;

            p.DefinirValores(Posicao.Linha + 2, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(p) && PodeMover(p))
                movimentosPossiveis[p.Linha, p.Coluna] = true;

            p.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 2);
            if (Tabuleiro.PosicaoValida(p) && PodeMover(p))
                movimentosPossiveis[p.Linha, p.Coluna] = true;

            p.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 2);
            if (Tabuleiro.PosicaoValida(p) && PodeMover(p))
                movimentosPossiveis[p.Linha, p.Coluna] = true;

            //Sul
            p.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 2);
            if (Tabuleiro.PosicaoValida(p) && PodeMover(p))
                movimentosPossiveis[p.Linha, p.Coluna] = true;

            p.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 2);
            if (Tabuleiro.PosicaoValida(p) && PodeMover(p))
                movimentosPossiveis[p.Linha, p.Coluna] = true;

            p.DefinirValores(Posicao.Linha + 2, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(p) && PodeMover(p))
                movimentosPossiveis[p.Linha, p.Coluna] = true;

            p.DefinirValores(Posicao.Linha + 2, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(p) && PodeMover(p))
                movimentosPossiveis[p.Linha, p.Coluna] = true;

            //Oeste
            p.DefinirValores(Posicao.Linha - 2, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(p) && PodeMover(p))
                movimentosPossiveis[p.Linha, p.Coluna] = true;

            p.DefinirValores(Posicao.Linha + 2, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(p) && PodeMover(p))
                movimentosPossiveis[p.Linha, p.Coluna] = true;

            p.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 2);
            if (Tabuleiro.PosicaoValida(p) && PodeMover(p))
                movimentosPossiveis[p.Linha, p.Coluna] = true;

            p.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 2);
            if (Tabuleiro.PosicaoValida(p) && PodeMover(p))
                movimentosPossiveis[p.Linha, p.Coluna] = true;

            return movimentosPossiveis;
        }
    }


}
