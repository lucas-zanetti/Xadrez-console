using TabuleiroEntities;

namespace XadrezEntities
{
    class Peao : Peca
    {
        private PartidaDeXadrez _partida;
        public Peao(Tabuleiro tabuleiro, Cor cor, PartidaDeXadrez partida) : base(cor, tabuleiro)
        {
            _partida = partida;
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

                //Jogada Especial En Passant
                if (Posicao.Linha == 3)
                {
                    Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    if (Tabuleiro.PosicaoValida(esquerda) && ExisteAdversario(esquerda) && Tabuleiro.Peca(esquerda) == _partida.VulneravelEnPassant)
                        movimentosPossiveis[esquerda.Linha - 1, esquerda.Coluna] = true;
                    if (Tabuleiro.PosicaoValida(direita) && ExisteAdversario(direita) && Tabuleiro.Peca(direita) == _partida.VulneravelEnPassant)
                        movimentosPossiveis[direita.Linha - 1, direita.Coluna] = true;
                }

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

                //Jogada Especial En Passant
                if (Posicao.Linha == 4)
                {
                    Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    if (Tabuleiro.PosicaoValida(esquerda) && ExisteAdversario(esquerda) && Tabuleiro.Peca(esquerda) == _partida.VulneravelEnPassant)
                        movimentosPossiveis[esquerda.Linha + 1, esquerda.Coluna] = true;
                    if (Tabuleiro.PosicaoValida(direita) && ExisteAdversario(direita) && Tabuleiro.Peca(direita) == _partida.VulneravelEnPassant)
                        movimentosPossiveis[direita.Linha + 1, direita.Coluna] = true;
                }
            }


            return movimentosPossiveis;
        }
    }
}
