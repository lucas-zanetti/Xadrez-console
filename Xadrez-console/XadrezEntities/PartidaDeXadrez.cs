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
        public bool Xeque { get; private set; }
        private HashSet<Peca> PecasEmJogo;
        private HashSet<Peca> PecasCapturadas;

        public PartidaDeXadrez()
        {
            Tabuleiro = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            Xeque = false;
            PecasEmJogo = new HashSet<Peca>();
            PecasCapturadas = new HashSet<Peca>();
            ColocarPecas();
        }

        public Peca ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = Tabuleiro.RetirarPeca(origem);
            peca.IncrementarQuantidadeMovimentos();
            Peca pecaCapturada = Tabuleiro.RetirarPeca(destino);
            Tabuleiro.ColocarPeca(peca, destino);
            if (pecaCapturada != null)
                PecasCapturadas.Add(pecaCapturada);
            return pecaCapturada;
        }

        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca pecaRetirada = Tabuleiro.RetirarPeca(destino);
            pecaRetirada.DecrementarQuantidadeMovimentos();
            if (pecaCapturada != null)
            {
                Tabuleiro.ColocarPeca(pecaCapturada, destino);
                PecasCapturadas.Remove(pecaCapturada);
            }
            Tabuleiro.ColocarPeca(pecaRetirada, origem);
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutarMovimento(origem, destino);
            if (EstaEmXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em Xeque!");
            }
            if (EstaEmXeque(Adversaria(JogadorAtual)))
                Xeque = true;
            else
                Xeque = false;
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

        private Peca Rei(Cor cor)
        {
            foreach (Peca pecaEmJogoDaCor in PecasEmJogoDaCor(cor))
                if (pecaEmJogoDaCor is Rei)
                    return pecaEmJogoDaCor;
            return null;
        }
        public bool EstaEmXeque(Cor cor)
        {
            Peca reiTestado = Rei(cor);
            if (reiTestado == null)
                throw new TabuleiroException("Não há rei da cor " + cor + "No Tabuleiro!");
            foreach (Peca pecaEmJogoAdversaria in PecasEmJogoDaCor(Adversaria(cor)))
            {
                bool[,] matrizMovimentosPossiveisAdversario = pecaEmJogoAdversaria.MovimentosPossiveis();
                if (matrizMovimentosPossiveisAdversario[reiTestado.Posicao.Linha, reiTestado.Posicao.Coluna])
                    return true;
            }
            return false;
        }

        private Cor Adversaria(Cor cor)
        {
            if (cor == Cor.Branca)
                return Cor.Preta;
            else
                return Cor.Branca;
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
