using MySql.Data.MySqlClient;
using System;

namespace Desktp
{
    public class DatabaseConnection
    {
        private string connectionString;

        public DatabaseConnection()
        {
            // Definindo a string de conexão
            connectionString = "Server=localhost;Database=dbsistemaos;Uid=root;Pwd=;";
        }

        // Método para obter uma conexão MySQL
        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        // Método para verificar se a conexão está funcionando
        public bool IsConnected()
        {
            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open(); // Tenta abrir a conexão
                    conn.Close(); // Fecha imediatamente após a verificação
                    return true; // Conexão está funcionando
                }
                catch
                {
                    return false; // Conexão falhou
                }
            }
        }

        // Método para executar comandos SQL que retornam um único valor
        public object ExecuteScalar(string query, params MySqlParameter[] parameters)
        {
            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    return cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Erro ao executar comando SQL: {ex.Message}");
                }
            }
        }

        // Método para executar comandos SQL que não retornam valores
        public int ExecuteNonQuery(string query, params MySqlParameter[] parameters)
        {
            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    return cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Erro ao executar comando SQL: {ex.Message}");
                }
            }
        }
    }
}
