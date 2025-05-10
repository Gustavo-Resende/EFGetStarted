using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class ShedullingContext : DbContext
{
    public DbSet<Cliente> Cliente { get; set; }
    public DbSet<Servico> Servico { get; set; }
    public DbSet<Barbeiro> Barbeiro { get; set; }
    public DbSet<Agendamento> Agendamento { get; set; }

    public string DbPath { get; }

    public ShedullingContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "shedulling.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}

public class Cliente
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public List<Agendamento> Agendamentos { get; set; } = new();
}

public class Servico
{
    public int Id { get; set; }
    public string Tipo { get; set; }
    public string Tempo { get; set; }
    public string Preco { get; set; }
    public List<BarbeiroServico> Barbeiros { get; set; } = new();
}

public class BarbeiroServico
{
    public int Id { get; set; }
    public int BarbeiroId { get; set; }
    public Barbeiro Barbeiro { get; set; }

    public int ServicoId { get; set; }
    public Servico Servico { get; set; }
}

public class Barbeiro
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public List<BarbeiroServico> Habilidades { get; set; } = new();
}

public class Agendamento
{ 
    public int Id { get; set; }
    public DateTime DataHora { get; set; }

    // Relacionamento: Um agendamento pertence a um cliente
    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; }

    // Relacionamento: Um agendamento é para um serviço
    public int ServicoId { get; set; }
    public Servico Servico { get; set; }

    // Relacionamento: Um agendamento é realizado por um barbeiro
    public int BarbeiroId { get; set; }
    public Barbeiro Barbeiro { get; set; }
}