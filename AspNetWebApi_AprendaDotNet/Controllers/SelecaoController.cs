using AspNetWebApi_AprendaDotNet.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetWebApi_AprendaDotNet.Controllers 
{
    [Produces("application/json")]
    [Route("api/Selecao")]
    public class SelecaoController : Controller
    {
        private readonly ISelecaoDal _selecaoDal;

        public SelecaoController(ISelecaoDal selecaoDal)
        {
            _selecaoDal = selecaoDal;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _selecaoDal.ObterSelecoes();
            return Ok(result);
        }

        [HttpPost, Route("criarselecao")]
        public IActionResult Create([FromBody] Selecao selecao)
        {
            if (selecao == null)
                return BadRequest();

            var status = _selecaoDal.IncluirSelecao(selecao);

            if (status != 1)
                return StatusCode(500, "Erro ao incluir a selecao");

            return Ok();
        }

        [HttpGet("{id}/obterselecaoporid")]
        public IActionResult Get(int id)
        {
            var selecao = _selecaoDal.ObterSelecaoPorId(id);

            if (selecao == null)
                return NotFound();

            return Ok(selecao);
        }

        [HttpPut, Route("atualizarselecao")]
        public IActionResult Put([FromBody] Selecao selecao)
        {
            if (selecao == null)
                return BadRequest();

            var status = _selecaoDal.AtualizarSelecao(selecao);

            if (status != 1)
                return StatusCode(500, "Erro ao atualizar a selecao");


            return NoContent();
        }

        [HttpDelete("{id}/excluirselecao")]
        public IActionResult Delete(int id)
        {
            var status = _selecaoDal.ExcluirSelecao(id);

            if (status != 1)
                return StatusCode(500, "Erro ao excluir a selecao");

            return NoContent();
        }
    }
}