using TabuleiroEntities;

namespace XadrezEntities
{
    class Torre:Peca
    {
        public Torre(Tabuleiro tabuleiro, Cor cor): base(cor,tabuleiro)
        {
        }
        public override string ToString()
        {
            return "T";
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
            while (Tabuleiro.PosicaoValida(p) && PodeMover(p))
            {
                movimentosPossiveis[p.Linha, p.Coluna] = true;
                if (Tabuleiro.Peca(p) != null && Tabuleiro.Peca(p).Cor != Cor)
                    break;
                p.Linha -= 1;
            }

            //Leste
            p.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            while (Tabuleiro.PosicaoValida(p) && PodeMover(p))
            {
                movimentosPossiveis[p.Linha, p.Coluna] = true;
                if (Tabuleiro.Peca(p) != null && Tabuleiro.Peca(p).Cor != Cor)
                    break;
                p.Coluna += 1;
            }

            //Sul
            p.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            while (Tabuleiro.PosicaoValida(p) && PodeMover(p))
            {
                movimentosPossiveis[p.Linha, p.Coluna] = true;
                if (Tabuleiro.Peca(p) != null && Tabuleiro.Peca(p).Cor != Cor)
                    break;
                p.Linha += 1;
            }

            //Oeste
            p.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            while (Tabuleiro.PosicaoValida(p) && PodeMover(p))
            {
                movimentosPossiveis[p.Linha, p.Coluna] = true;
                if (Tabuleiro.Peca(p) != null && Tabuleiro.Peca(p).Cor != Cor)
                    break;
                p.Coluna -= 1;
            }

            return movimentosPossiveis;
        }
    }
}
