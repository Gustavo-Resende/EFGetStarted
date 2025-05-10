using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using var db = new ShedullingContext(); // Conecta ao contexto de agendamento

// Exibe o caminho do banco de dados
Console.WriteLine($"Database path: {db.DbPath}.");

// CREATE: Inserindo um novo cliente
Console.WriteLine("Inserting a new client");
db.Add(new Cliente { Nome = "João Silva", Telefone = "123456789" });
await db.SaveChangesAsync();

// READ: Consultando o primeiro cliente
Console.WriteLine("Querying for a client");
var cliente = await db.Cliente
    .OrderBy(c => c.Id)
    .FirstAsync();
Console.WriteLine($"Cliente encontrado: {cliente.Nome}, Telefone: {cliente.Telefone}");

// UPDATE: Atualizando o cliente e adicionando um agendamento
Console.WriteLine("Updating the client and adding a scheduling");
cliente.Nome = "João da Silva";
var servico = new Servico { Tipo = "Corte de Cabelo", Tempo = "30 minutos", Preco = "50,00" };
var barbeiro = new Barbeiro { Nome = "Carlos Barbeiro", Telefone = "987654321" };

db.Servico.Add(servico);
db.Barbeiro.Add(barbeiro);
await db.SaveChangesAsync();

cliente.Agendamentos.Add(new Agendamento
{
    DataHora = DateTime.Now.AddDays(1),
    ServicoId = servico.Id,
    BarbeiroId = barbeiro.Id
});
await db.SaveChangesAsync();

// DELETE: Removendo o cliente
Console.WriteLine("Deleting the client");
db.Remove(cliente);
await db.SaveChangesAsync();