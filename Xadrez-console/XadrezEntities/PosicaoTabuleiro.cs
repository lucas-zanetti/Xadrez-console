using TabuleiroEntities;

namespace XadrezEntities
{
    class PosicaoTabuleiro
    {
        public char Coluna { get; set; }
        public int Linha { get; set; }

        public PosicaoTabuleiro(char coluna, int linha)
        {
            Coluna = coluna;
            Linha = linha;
        }

        public Posicao ToPosicao()
        {
            return new Posicao(8 - Linha, Coluna - 'a');
        }

        public override string ToString()
        {
            return ""+Coluna+Linha;
        }
    }
}
