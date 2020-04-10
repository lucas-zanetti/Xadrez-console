using System;
using TabuleiroEntities;

namespace XadrezEntities
{
    class PartidaDeXadrez
    {
        public Tabuleiro Tabuleiro { get; private set; }
        private int _turno;
        private Cor _jogadorAtual;
        public bool Terminada { get; private set; }

        public PartidaDeXadrez()
        {
            Tabuleiro = new Tabuleiro(8, 8);
            _turno = 1;
            _jogadorAtual = Cor.Branca;
            Terminada = false;
            ColocarPecas();
        }

        public void ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = Tabuleiro.RetirarPeca(origem);
            peca.IncrementarQuantidadeMovimentos();
            Peca pecaCapturada = Tabuleiro.RetirarPeca(destino);
            Tabuleiro.ColocarPeca(peca, destino);
        }

        private void ColocarPecas()
        {
            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Branca), new PosicaoTabuleiro('c', 1).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Branca), new PosicaoTabuleiro('c', 2).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Branca), new PosicaoTabuleiro('d', 2).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Branca), new PosicaoTabuleiro('e', 1).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Branca), new PosicaoTabuleiro('e', 2).ToPosicao());
            Tabuleiro.ColocarPeca(new Rei(Tabuleiro, Cor.Branca), new PosicaoTabuleiro('d', 1).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Preta), new PosicaoTabuleiro('c', 7).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Preta), new PosicaoTabuleiro('c', 8).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Preta), new PosicaoTabuleiro('d', 7).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Preta), new PosicaoTabuleiro('e', 7).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Preta), new PosicaoTabuleiro('e', 8).ToPosicao());
            Tabuleiro.ColocarPeca(new Rei(Tabuleiro, Cor.Preta), new PosicaoTabuleiro('d', 8).ToPosicao());
        }
    }
}
