using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

class Program
{
    static void Main(string[] args)
    {
        using (var db = new ShedullingContext())
        {
            // CREATE: Adicionando clientes
            db.Cliente.Add(new Cliente { Nome = "João" });
            db.Cliente.Add(new Cliente { Nome = "Maria" });
            db.SaveChanges();

            // READ: Listando clientes
            ReadClientes(db);

            // UPDATE: Atualizando cliente
            UpdateCliente(1, "João Silva", db);

            // DELETE: Removendo todos os clientes
            DeleteAllClientes(db);
        }
    }

    // READ: Listar todos os clientes
    static void ReadClientes(ShedullingContext db)
    {
        foreach (var cliente in db.Cliente)
        {
            Console.WriteLine($"Cliente: {cliente.Nome}");
        }
    }

    // UPDATE: Atualizar um cliente pelo ID
    static void UpdateCliente(int id, string nome, ShedullingContext db)
    {
        var cliente = db.Cliente.Find(id);
        if (cliente != null)
        {
            cliente.Nome = nome;
            db.SaveChanges();
            Console.WriteLine($"Cliente atualizado: {cliente.Nome}");
        }
        else
        {
            Console.WriteLine($"Cliente com ID {id} não encontrado.");
        }
    }

    // DELETE: Remover todos os clientes
    static void DeleteAllClientes(ShedullingContext db)
    {
        foreach (var cliente in db.Cliente.ToList())
        {
            db.Cliente.Remove(cliente);
        }
        db.SaveChanges();
        Console.WriteLine("Todos os clientes foram removidos.");
    }
}
