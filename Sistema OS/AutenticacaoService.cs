using MySql.Data.MySqlClient;
using BCrypt.Net;
using System;
using System.Windows.Forms;

public class AutenticacaoService
{
    private string connectionString = "datasource=localhost;port=3306;username=root;password=;database=dbsistemaos";

    public bool ValidarSenha(string nome, string senha)
    {
        try
        {
            string sql = "SELECT senha FROM usuarios WHERE nome = @nome";

            using (MySqlConnection cnn = new MySqlConnection(connectionString))
            {
                MySqlCommand comando = new MySqlCommand(sql, cnn);
                comando.Parameters.AddWithValue("@nome", nome);

                cnn.Open();

                string hashSenha = comando.ExecuteScalar()?.ToString();
                cnn.Close();

                return hashSenha != null && BCrypt.Net.BCrypt.Verify(senha, hashSenha);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao validar a senha: {ex.Message}");
            return false;
        }
    }
}
