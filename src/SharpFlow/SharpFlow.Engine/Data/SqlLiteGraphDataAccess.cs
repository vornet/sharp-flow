using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VorNet.SharpFlow.Engine.Data.Models;

namespace VorNet.SharpFlow.Engine.Data
{
    public class SqlLiteGraphDataAccess : IGraphDataAccess
    {
        private SqliteConnection _connection;

        public SqlLiteGraphDataAccess(SqliteConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));

            _connection.Open();

            var sql = @"
                CREATE TABLE IF NOT EXISTS graph (
                    id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    name TEXT NOT NULL,
                    data BLOB NOT NULL
                );";

            connection.Execute(sql);
        }

        public async Task<Graph> GetGraphByIdAsync(long id)
        {
            var graphRow = await _connection.QueryFirstAsync<GraphRow>(@"SELECT name, data FROM graph WHERE id = @id", new { id = id });
            return JsonSerializer.Deserialize<Graph>(graphRow.Data);
        }

        public async Task<long> SaveGraphAsync(Graph graph)
        {
            string serializedGraph = JsonSerializer.Serialize(graph);
            await _connection.ExecuteAsync("INSERT into graph (name, data) values (@name, @data)", new { name = graph.Name, data = serializedGraph });
            return (long)_connection.ExecuteScalar("select last_insert_rowid()");
        }
    }
}
