using AgendamentoDeTarefas.Context;
using AgendamentoDeTarefas.Enums;
using AgendamentoDeTarefas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgendamentoDeTarefas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly DataBaseContext _dataBaseContext;
        public TarefaController(DataBaseContext dataBaseContext) 
        {
            _dataBaseContext = dataBaseContext; 
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var tarefa = _dataBaseContext.Tarefas.Find(id);
            if(tarefa == null)
                return NotFound();
            return Ok(tarefa);
        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            var tarefas = _dataBaseContext.Tarefas.ToList();
            return Ok(tarefas);
        }

        [HttpGet("ObterPorTitulo")]
        public IActionResult ObterPorTitulo(string titulo)
        {
            var tarefas = _dataBaseContext.Tarefas.Where(x => x.Titulo == titulo);
            return Ok(tarefas);
        }

        [HttpGet("ObterPorData")]
        public IActionResult ObterPorData(DateTime data)
        {
            var tarefas = _dataBaseContext.Tarefas.Where(x => x.Data.Date == data.Date);
            return Ok(tarefas);
        }

        [HttpGet("ObterPorStatus")]
        public IActionResult ObterPorStatus(StatusTarefa status)
        {
            var tarefas = _dataBaseContext.Tarefas.Where(x => x.StatusTarefa == status);
            return Ok(tarefas);
        }

        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {
            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            _dataBaseContext.Add(tarefa);
            _dataBaseContext.SaveChanges(); 
            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Tarefa tarefa)
        {
            var tarefaBanco = _dataBaseContext.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            tarefaBanco.Titulo = tarefa.Titulo;
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Data = tarefa.Data;
            tarefaBanco.StatusTarefa = tarefa.StatusTarefa;
            _dataBaseContext.Tarefas.Update(tarefaBanco);
            _dataBaseContext.SaveChanges();
            return Ok(tarefaBanco);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var tarefaBanco = _dataBaseContext.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            _dataBaseContext.Tarefas.Remove(tarefaBanco);
            _dataBaseContext.SaveChanges();
            return NoContent();
        }
    }
}
