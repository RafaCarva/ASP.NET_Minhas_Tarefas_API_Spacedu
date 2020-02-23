using Microsoft.EntityFrameworkCore;
using MinhasTarefasAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhasTarefasAPI.DataBase
{
    public class MinhasTarefasContext : DbContext
    {
        public MinhasTarefasContext(DbContextOptions<MinhasTarefasContext> options) : base(options)
        {

        }

        // Propriedades que vão virar tabelas
        public DbSet<Tarefa> Tarefas { get; set; }
    }
}
