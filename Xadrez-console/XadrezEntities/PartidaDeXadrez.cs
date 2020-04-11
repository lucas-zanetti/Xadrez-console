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
        public Peca VulneravelEnPassant { get; private set; }
        private HashSet<Peca> _pecasEmJogo;
        private HashSet<Peca> _pecasCapturadas;


        public PartidaDeXadrez()
        {
            Tabuleiro = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            Xeque = false;
            VulneravelEnPassant = null;
            _pecasEmJogo = new HashSet<Peca>();
            _pecasCapturadas = new HashSet<Peca>();
            ColocarPecas();
        }

        public Peca ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = Tabuleiro.RetirarPeca(origem);
            peca.IncrementarQuantidadeMovimentos();
            Peca pecaCapturada = Tabuleiro.RetirarPeca(destino);
            Tabuleiro.ColocarPeca(peca, destino);
            if (pecaCapturada != null)
                _pecasCapturadas.Add(pecaCapturada);

            //Jogada Especial Roque Pequeno
            if (peca is Rei && destino.Coluna == origem.Coluna + 2)
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
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca torre = Tabuleiro.RetirarPeca(origemTorre);
                torre.IncrementarQuantidadeMovimentos();
                Tabuleiro.ColocarPeca(torre, destinoTorre);
            }

            //Jogada Especial En Passant
            if (peca is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == null)
                {
                    Posicao posicaoPeao;
                    if (peca.Cor == Cor.Branca)
                        posicaoPeao = new Posicao(destino.Linha + 1, destino.Coluna);
                    else
                        posicaoPeao = new Posicao(destino.Linha - 1, destino.Coluna);
                    pecaCapturada = Tabuleiro.RetirarPeca(posicaoPeao);
                    _pecasCapturadas.Add(pecaCapturada);
                }
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
                _pecasCapturadas.Remove(pecaCapturada);
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

            //Jogada Especial En Passant
            if (pecaRetirada is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == VulneravelEnPassant)
                {
                    Peca peaoDevolvido = Tabuleiro.RetirarPeca(destino);
                    Posicao posicaoPeaoDevolvido;
                    if (pecaRetirada.Cor == Cor.Branca)
                        posicaoPeaoDevolvido = new Posicao(3, destino.Coluna);
                    else
                        posicaoPeaoDevolvido = new Posicao(4, destino.Coluna);
                    Tabuleiro.ColocarPeca(peaoDevolvido, posicaoPeaoDevolvido);

                }
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
            Peca pecaMovida = Tabuleiro.Peca(destino);

            //Jogada especial Promocao
            if(pecaMovida is Peao)
                if((pecaMovida.Cor==Cor.Branca&&destino.Linha==0)||(pecaMovida.Cor == Cor.Branca && destino.Linha == 0))
                {
                    pecaMovida = Tabuleiro.RetirarPeca(destino);
                    _pecasEmJogo.Remove(pecaMovida);
                    Peca dama = new Dama(Tabuleiro, pecaMovida.Cor);
                    Tabuleiro.ColocarPeca(dama, destino);
                    _pecasEmJogo.Add(dama);
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

            //Jogada Especial En Passant
            if (pecaMovida is Peao && (destino.Linha == origem.Linha + 2 || destino.Linha == origem.Linha - 2))
                VulneravelEnPassant = pecaMovida;
            else
                VulneravelEnPassant = null;
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
            foreach (Peca peca in _pecasCapturadas)
                if (peca.Cor == cor)
                    aux.Add(peca);
            return aux;
        }

        public HashSet<Peca> PecasEmJogoDaCor(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca peca in _pecasEmJogo)
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
            _pecasEmJogo.Add(peca);
        }

        private void ColocarPecas()
        {
            //Peças Pretas
            ColocarNovaPeca(new Peao(Tabuleiro, Cor.Preta, this), 'a', 7);
            ColocarNovaPeca(new Peao(Tabuleiro, Cor.Preta, this), 'b', 7);
            ColocarNovaPeca(new Peao(Tabuleiro, Cor.Preta, this), 'c', 7);
            ColocarNovaPeca(new Peao(Tabuleiro, Cor.Preta, this), 'd', 7);
            ColocarNovaPeca(new Peao(Tabuleiro, Cor.Preta, this), 'e', 7);
            ColocarNovaPeca(new Peao(Tabuleiro, Cor.Preta, this), 'f', 7);
            ColocarNovaPeca(new Peao(Tabuleiro, Cor.Preta, this), 'g', 7);
            ColocarNovaPeca(new Peao(Tabuleiro, Cor.Preta, this), 'h', 7);
            ColocarNovaPeca(new Torre(Tabuleiro, Cor.Preta), 'a', 8);
            ColocarNovaPeca(new Cavalo(Tabuleiro, Cor.Preta), 'b', 8);
            ColocarNovaPeca(new Bispo(Tabuleiro, Cor.Preta), 'c', 8);
            ColocarNovaPeca(new Rei(Tabuleiro, Cor.Preta, this), 'e', 8);
            ColocarNovaPeca(new Dama(Tabuleiro, Cor.Preta), 'd', 8);
            ColocarNovaPeca(new Bispo(Tabuleiro, Cor.Preta), 'f', 8);
            ColocarNovaPeca(new Cavalo(Tabuleiro, Cor.Preta), 'g', 8);
            ColocarNovaPeca(new Torre(Tabuleiro, Cor.Preta), 'h', 8);

            //Peças Brancas
            ColocarNovaPeca(new Peao(Tabuleiro, Cor.Branca, this), 'a', 2);
            ColocarNovaPeca(new Peao(Tabuleiro, Cor.Branca, this), 'b', 2);
            ColocarNovaPeca(new Peao(Tabuleiro, Cor.Branca, this), 'c', 2);
            ColocarNovaPeca(new Peao(Tabuleiro, Cor.Branca, this), 'd', 2);
            ColocarNovaPeca(new Peao(Tabuleiro, Cor.Branca, this), 'e', 2);
            ColocarNovaPeca(new Peao(Tabuleiro, Cor.Branca, this), 'f', 2);
            ColocarNovaPeca(new Peao(Tabuleiro, Cor.Branca, this), 'g', 2);
            ColocarNovaPeca(new Peao(Tabuleiro, Cor.Branca, this), 'h', 2);
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
