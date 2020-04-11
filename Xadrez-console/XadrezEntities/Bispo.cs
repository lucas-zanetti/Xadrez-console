using TabuleiroEntities;

namespace XadrezEntities
{
    class Bispo:Peca
    {
        public Bispo(Tabuleiro tabuleiro, Cor cor) : base(cor, tabuleiro)
        {
        }
        public override string ToString()
        {
            return "B";
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

            //Nordeste
            p.DefinirValores(Posicao.Linha - 1, Posicao.Coluna+1);
            while (Tabuleiro.PosicaoValida(p) && PodeMover(p))
            {
                movimentosPossiveis[p.Linha, p.Coluna] = true;
                if (Tabuleiro.Peca(p) != null && Tabuleiro.Peca(p).Cor != Cor)
                    break;
                p.Linha -= 1;
                p.Coluna += 1;
            }

            //Sudeste
            p.DefinirValores(Posicao.Linha+1, Posicao.Coluna + 1);
            while (Tabuleiro.PosicaoValida(p) && PodeMover(p))
            {
                movimentosPossiveis[p.Linha, p.Coluna] = true;
                if (Tabuleiro.Peca(p) != null && Tabuleiro.Peca(p).Cor != Cor)
                    break;
                p.Linha += 1;
                p.Coluna += 1;
            }

            //Sudoeste
            p.DefinirValores(Posicao.Linha + 1, Posicao.Coluna-1);
            while (Tabuleiro.PosicaoValida(p) && PodeMover(p))
            {
                movimentosPossiveis[p.Linha, p.Coluna] = true;
                if (Tabuleiro.Peca(p) != null && Tabuleiro.Peca(p).Cor != Cor)
                    break;
                p.Linha += 1;
                p.Coluna -= 1;
            }

            //Noroeste
            p.DefinirValores(Posicao.Linha-1, Posicao.Coluna - 1);
            while (Tabuleiro.PosicaoValida(p) && PodeMover(p))
            {
                movimentosPossiveis[p.Linha, p.Coluna] = true;
                if (Tabuleiro.Peca(p) != null && Tabuleiro.Peca(p).Cor != Cor)
                    break;
                p.Linha -= 1;
                p.Coluna -= 1;
            }

            return movimentosPossiveis;
        }
    }
}
