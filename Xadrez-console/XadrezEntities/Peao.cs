using TabuleiroEntities;

namespace XadrezEntities
{
    class Peao : Peca
    {
        public Peao(Tabuleiro tabuleiro, Cor cor) : base(cor, tabuleiro)
        {
        }
        public override string ToString()
        {
            return "P";
        }
        private bool ExisteAdversario(Posicao posicaoDesejada)
        {
            Peca pecaPosicaoDesejada = Tabuleiro.Peca(posicaoDesejada);
            return pecaPosicaoDesejada != null && pecaPosicaoDesejada.Cor != Cor;
        }

        private bool PosicaoLivre(Posicao posicaoDesejada)
        {
            return Tabuleiro.Peca(posicaoDesejada) == null;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] movimentosPossiveis = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];
            Posicao p = new Posicao(0, 0);

            if (Cor == Cor.Branca)
            {
                p.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(p) && PosicaoLivre(p))
                    movimentosPossiveis[p.Linha, p.Coluna] = true;

                p.DefinirValores(Posicao.Linha - 2, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(p) && PosicaoLivre(p) && QuantidadeMovimentos == 0)
                    movimentosPossiveis[p.Linha, p.Coluna] = true;

                p.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
                if (Tabuleiro.PosicaoValida(p) && ExisteAdversario(p))
                    movimentosPossiveis[p.Linha, p.Coluna] = true;

                p.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
                if (Tabuleiro.PosicaoValida(p) && ExisteAdversario(p))
                    movimentosPossiveis[p.Linha, p.Coluna] = true;
            }
            else
            {
                p.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(p) && PosicaoLivre(p))
                    movimentosPossiveis[p.Linha, p.Coluna] = true;

                p.DefinirValores(Posicao.Linha + 2, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(p) && PosicaoLivre(p) && QuantidadeMovimentos == 0)
                    movimentosPossiveis[p.Linha, p.Coluna] = true;

                p.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
                if (Tabuleiro.PosicaoValida(p) && ExisteAdversario(p))
                    movimentosPossiveis[p.Linha, p.Coluna] = true;

                p.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
                if (Tabuleiro.PosicaoValida(p) && ExisteAdversario(p))
                    movimentosPossiveis[p.Linha, p.Coluna] = true;
            }


            return movimentosPossiveis;
        }
    }
}
