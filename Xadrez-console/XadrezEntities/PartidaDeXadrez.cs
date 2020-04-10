using System;
using TabuleiroEntities;
using TabuleiroEntities.Exceptions;

namespace XadrezEntities
{
    class PartidaDeXadrez
    {
        public Tabuleiro Tabuleiro { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }

        public PartidaDeXadrez()
        {
            Tabuleiro = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
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

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            ExecutarMovimento(origem, destino);
            Turno++;
            MudaJogador();
        }

        public void ValidarPosicaoOrigem(Posicao posicaoOrigem)
        {
            if (Tabuleiro.Peca(posicaoOrigem) == null)
                throw new TabuleiroException("Posição escolhida não possui peça!");
            if (Tabuleiro.Peca(posicaoOrigem).Cor != JogadorAtual)
                throw new TabuleiroException("A peça escolhida não pertence ao jogador atual!");
            if (Tabuleiro.Peca(posicaoOrigem).ExisteMovimentosPossiveis() == false)
                throw new TabuleiroException("Não existe movimento possível para a peça selecionada!");
        }

        public void ValidarPosicaoDestino(Posicao origem, Posicao destino)
        {
            if (Tabuleiro.Peca(origem).PodeMoverPara(destino) == false)
            {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }

        public void MudaJogador()
        {
            if (JogadorAtual == Cor.Branca)
                JogadorAtual = Cor.Preta;
            else
                JogadorAtual = Cor.Branca;
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
