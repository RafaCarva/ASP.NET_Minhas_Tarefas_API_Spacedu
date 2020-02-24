using MinhasTarefasAPI.DataBase;
using MinhasTarefasAPI.Models;
using MinhasTarefasAPI.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhasTarefasAPI.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly MinhasTarefasContext _banco;

        public TarefaRepository(MinhasTarefasContext banco)
        {
            _banco = banco;
        }

        public List<Tarefa> Restauracao(ApplicationUser usuario, DateTime dataUltimaSincronizacao)
        {
            var query = _banco.Tarefas.Where(a=>a.UsuarioId == usuario.Id).AsQueryable();

            if(dataUltimaSincronizacao != null)
            {
                query.Where(a => a.Criado >= dataUltimaSincronizacao || a.Atualizado >= dataUltimaSincronizacao);
            }
            return query.ToList<Tarefa>();
        }

        public List<Tarefa> Sincronizacao(List<Tarefa> tarefas)
        {
            // Cadastrar novos registros
            var tarefasNovas = tarefas.Where(a => a.IdTarefaApi == 0);     
            if (tarefasNovas.Count() > 0)
            {
                foreach (var tarefa in tarefasNovas)
                {
                    _banco.Tarefas.Add(tarefa);
                }
            }

            // Atualização de registro (Excluido)
            var tarefasExcluidasAtualizadas = tarefas.Where(a => a.IdTarefaApi != 0);
            if (tarefasExcluidasAtualizadas.Count() > 0)
            {
                foreach (var tarefa in tarefasExcluidasAtualizadas)
                {
                    _banco.Tarefas.Add(tarefa);
                }
            }
            _banco.SaveChanges();
            return tarefasNovas.ToList();
        }
    }
}
