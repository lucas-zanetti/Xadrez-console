using System.Collections.Generic;
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
        private HashSet<Peca> PecasEmJogo;
        private HashSet<Peca> PecasCapturadas;

        public PartidaDeXadrez()
        {
            Tabuleiro = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            PecasEmJogo = new HashSet<Peca>();
            PecasCapturadas = new HashSet<Peca>();
            ColocarPecas();
        }

        public void ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = Tabuleiro.RetirarPeca(origem);
            peca.IncrementarQuantidadeMovimentos();
            Peca pecaCapturada = Tabuleiro.RetirarPeca(destino);
            Tabuleiro.ColocarPeca(peca, destino);
            if (pecaCapturada != null)
                PecasCapturadas.Add(pecaCapturada);
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

        public HashSet<Peca> PecasCapturadasDaCor(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca peca in PecasCapturadas)
                if (peca.Cor == cor)
                    aux.Add(peca);
            return aux;
        }

        public HashSet<Peca> PecasEmJogoDaCor(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca peca in PecasEmJogo)
                if (peca.Cor == cor)
                    aux.Add(peca);
            aux.ExceptWith(PecasCapturadasDaCor(cor));
            return aux;
        }

        public void MudaJogador()
        {
            if (JogadorAtual == Cor.Branca)
                JogadorAtual = Cor.Preta;
            else
                JogadorAtual = Cor.Branca;
        }

        public void ColocarNovaPeca(Peca peca, char coluna, int linha)
        {
            Tabuleiro.ColocarPeca(peca, new PosicaoTabuleiro(coluna, linha).ToPosicao());
            PecasEmJogo.Add(peca);
        }

        private void ColocarPecas()
        {
            ColocarNovaPeca(new Torre(Tabuleiro, Cor.Branca), 'c', 1);
            ColocarNovaPeca(new Torre(Tabuleiro, Cor.Branca), 'c', 2);
            ColocarNovaPeca(new Torre(Tabuleiro, Cor.Branca), 'd', 2);
            ColocarNovaPeca(new Torre(Tabuleiro, Cor.Branca), 'e', 1);
            ColocarNovaPeca(new Torre(Tabuleiro, Cor.Branca), 'e', 2);
            ColocarNovaPeca(new Rei(Tabuleiro, Cor.Branca), 'd', 1);
            ColocarNovaPeca(new Torre(Tabuleiro, Cor.Preta), 'c', 7);
            ColocarNovaPeca(new Torre(Tabuleiro, Cor.Preta), 'c', 8);
            ColocarNovaPeca(new Torre(Tabuleiro, Cor.Preta), 'd', 7);
            ColocarNovaPeca(new Torre(Tabuleiro, Cor.Preta), 'e', 7);
            ColocarNovaPeca(new Torre(Tabuleiro, Cor.Preta), 'e', 8);
            ColocarNovaPeca(new Rei(Tabuleiro, Cor.Preta), 'd', 8);
        }
    }
}
