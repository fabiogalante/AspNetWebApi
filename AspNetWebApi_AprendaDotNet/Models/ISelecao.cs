using System.Collections.Generic;

namespace AspNetWebApi_AprendaDotNet.Models
{
    public interface ISelecaoDal
    {
        IEnumerable<Selecao> ObterSelecoes();
        int IncluirSelecao(Selecao selecao);
        int AtualizarSelecao(Selecao selecao);
        Selecao ObterSelecaoPorId(int id);
        int ExcluirSelecao(int id);
    }
}
