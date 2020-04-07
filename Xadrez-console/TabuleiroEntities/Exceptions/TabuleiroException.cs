using System;

namespace TabuleiroEntities.Exceptions
{
    class TabuleiroException:Exception
    {
        public TabuleiroException(string mensagem) : base(mensagem)
        {
        }
    }
}
