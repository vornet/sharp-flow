using Dapper;
using Microsoft.Data.Sqlite;
using System.Text.Json;
using VorNet.SharpFlow.Engine.Models;

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
                    name TEXT NOT NULL PRIMARY KEY,
                    data BLOB NOT NULL
                );";

            connection.Execute(sql);
        }

        public async Task<Graph> GetGraphByNameAsync(string name)
        {
            var graphRow = await _connection.QueryFirstAsync<GraphRow>(@"SELECT name, data FROM graph WHERE name = @name", new { name });
            return JsonSerializer.Deserialize<Graph>(graphRow.Data);
        }

        public async Task SaveGraphAsync(Graph graph)
        {
            string serializedGraph = JsonSerializer.Serialize(graph);
            await _connection.ExecuteAsync("INSERT into graph (name, data) values (@name, @data) ON CONFLICT(name) DO UPDATE SET data=@data", new { name = graph.Name, data = serializedGraph });
        }
    }
}
