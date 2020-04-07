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
    }
}
