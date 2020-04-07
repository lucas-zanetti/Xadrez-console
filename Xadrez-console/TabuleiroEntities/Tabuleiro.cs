using TabuleiroEntities.Exceptions;

namespace TabuleiroEntities
{
    class Tabuleiro
    {
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        private Peca[,] _matrizPecas;

        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            _matrizPecas = new Peca[Linhas,Colunas];
        }

        public Peca Peca(int linha, int coluna)
        {
            return _matrizPecas[linha, coluna];
        }

        public Peca Peca(Posicao posicao)
        {
            return _matrizPecas[posicao.Linha, posicao.Coluna];
        }

        public bool ExistePeca(Posicao posicao)
        {
            ValidarPosicao(posicao);
            return Peca(posicao) != null;
        }
        public void ColocarPeca(Peca peca, Posicao posicao)
        {
            if (ExistePeca(posicao))
                throw new TabuleiroException("Já existe uma peça nessa posição!");
            _matrizPecas[posicao.Linha, posicao.Coluna] = peca;
            peca.Posicao = posicao;
        }

        public bool PosicaoValida(Posicao posicao)
        {
            if (posicao.Linha >= Linhas || posicao.Coluna >= Colunas || posicao.Linha < 0 || posicao.Coluna < 0)
                return false;
            return true;

        }

        public void ValidarPosicao(Posicao posicao)
        {
            if (PosicaoValida(posicao) == false)
                throw new TabuleiroException("Posição inválida!");
        }
    }
}
