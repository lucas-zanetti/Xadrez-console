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

            //Jogada Especial Roque Pequeno
            if(peca is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca torre = Tabuleiro.RetirarPeca(origemTorre);
                torre.IncrementarQuantidadeMovimentos();
                Tabuleiro.ColocarPeca(torre, destinoTorre);
            }

            //Jogada Especial Roque Grande
            if (peca is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna -4);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna -1);
                Peca torre = Tabuleiro.RetirarPeca(origemTorre);
                torre.IncrementarQuantidadeMovimentos();
                Tabuleiro.ColocarPeca(torre, destinoTorre);
            }

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

            //Jogada Especial Roque Pequeno
            if (pecaRetirada is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca torre = Tabuleiro.RetirarPeca(destinoTorre);
                torre.DecrementarQuantidadeMovimentos();
                Tabuleiro.ColocarPeca(torre, origemTorre);
            }

            //Jogada Especial Roque Grande
            if (pecaRetirada is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca torre = Tabuleiro.RetirarPeca(destinoTorre);
                torre.DecrementarQuantidadeMovimentos();
                Tabuleiro.ColocarPeca(torre, origemTorre);
            }
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
            if (EstaEmMate(Adversaria(JogadorAtual)))
                Terminada = true;
            else
            {
                Turno++;
                MudaJogador();
            }
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
            if (Tabuleiro.Peca(origem).MovimentoPossivel(destino) == false)
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
                throw new TabuleiroException("Não há rei da cor " + cor + " no Tabuleiro!");
            foreach (Peca pecaEmJogoAdversaria in PecasEmJogoDaCor(Adversaria(cor)))
            {
                bool[,] matrizMovimentosPossiveisAdversario = pecaEmJogoAdversaria.MovimentosPossiveis();
                if (matrizMovimentosPossiveisAdversario[reiTestado.Posicao.Linha, reiTestado.Posicao.Coluna])
                    return true;
            }
            return false;
        }
        public bool EstaEmMate(Cor cor)
        {
            if (EstaEmXeque(cor) == false)
                return false;
            foreach (Peca pecaJogadorAtual in PecasEmJogoDaCor(cor))
            {
                bool[,] matrizMovimentosPossiveis = pecaJogadorAtual.MovimentosPossiveis();
                for (int i = 0; i < Tabuleiro.Linhas; i++)
                    for (int j = 0; j < Tabuleiro.Colunas; j++)
                        if (matrizMovimentosPossiveis[i, j])
                        {
                            Posicao origem = pecaJogadorAtual.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecutarMovimento(origem, destino);
                            bool testeXeque = EstaEmXeque(cor);
                            DesfazMovimento(origem, destino, pecaCapturada);
                            if (testeXeque == false)
                                return false;
                        }

            }
            return true;
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
            //Peças Pretas
            ColocarNovaPeca(new Peao(Tabuleiro, Cor.Preta), 'a', 7);
            ColocarNovaPeca(new Peao(Tabuleiro, Cor.Preta), 'b', 7);
            ColocarNovaPeca(new Peao(Tabuleiro, Cor.Preta), 'c', 7);
            ColocarNovaPeca(new Peao(Tabuleiro, Cor.Preta), 'd', 7);
            ColocarNovaPeca(new Peao(Tabuleiro, Cor.Preta), 'e', 7);
            ColocarNovaPeca(new Peao(Tabuleiro, Cor.Preta), 'f', 7);
            ColocarNovaPeca(new Peao(Tabuleiro, Cor.Preta), 'g', 7);
            ColocarNovaPeca(new Peao(Tabuleiro, Cor.Preta), 'h', 7);
            ColocarNovaPeca(new Torre(Tabuleiro, Cor.Preta), 'a', 8);
            ColocarNovaPeca(new Cavalo(Tabuleiro, Cor.Preta), 'b', 8);
            ColocarNovaPeca(new Bispo(Tabuleiro, Cor.Preta), 'c', 8);
            ColocarNovaPeca(new Rei(Tabuleiro, Cor.Preta, this), 'e', 8);
            ColocarNovaPeca(new Dama(Tabuleiro, Cor.Preta), 'd', 8);
            ColocarNovaPeca(new Bispo(Tabuleiro, Cor.Preta), 'f', 8);
            ColocarNovaPeca(new Cavalo(Tabuleiro, Cor.Preta), 'g', 8);
            ColocarNovaPeca(new Torre(Tabuleiro, Cor.Preta), 'h', 8);

            //Peças Brancas
            ColocarNovaPeca(new Peao(Tabuleiro, Cor.Branca), 'a', 2);
            ColocarNovaPeca(new Peao(Tabuleiro, Cor.Branca), 'b', 2);
            ColocarNovaPeca(new Peao(Tabuleiro, Cor.Branca), 'c', 2);
            ColocarNovaPeca(new Peao(Tabuleiro, Cor.Branca), 'd', 2);
            ColocarNovaPeca(new Peao(Tabuleiro, Cor.Branca), 'e', 2);
            ColocarNovaPeca(new Peao(Tabuleiro, Cor.Branca), 'f', 2);
            ColocarNovaPeca(new Peao(Tabuleiro, Cor.Branca), 'g', 2);
            ColocarNovaPeca(new Peao(Tabuleiro, Cor.Branca), 'h', 2);
            ColocarNovaPeca(new Torre(Tabuleiro, Cor.Branca), 'a', 1);
            ColocarNovaPeca(new Cavalo(Tabuleiro, Cor.Branca), 'b', 1);
            ColocarNovaPeca(new Bispo(Tabuleiro, Cor.Branca), 'c', 1);
            ColocarNovaPeca(new Dama(Tabuleiro, Cor.Branca), 'd', 1);
            ColocarNovaPeca(new Rei(Tabuleiro, Cor.Branca, this), 'e', 1);
            ColocarNovaPeca(new Bispo(Tabuleiro, Cor.Branca), 'f', 1);
            ColocarNovaPeca(new Cavalo(Tabuleiro, Cor.Branca), 'g', 1);
            ColocarNovaPeca(new Torre(Tabuleiro, Cor.Branca), 'h', 1);
        }
    }
}
