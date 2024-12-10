using Desktp;
using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sistema_OS
{
    public partial class TelaLogin : Form
    {
        private DatabaseConnection dbConnection;

        public TelaLogin()
        {
            InitializeComponent();

            // Instancia o DatabaseConnection para verificar a conexão
            dbConnection = new DatabaseConnection();

            // Verifica o status da conexão e exibe o ícone correspondente
            if (dbConnection.IsConnected())
            {
                picStatus.Image = Image.FromFile("imagens/dbon.png"); // Certifique-se de que o caminho está correto
            }
            else
            {
                picStatus.Image = Image.FromFile("imagens/dberror.png");
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text; // Nome digitado no campo do formulário
            string senha = txtSenha.Text; // Senha digitada no campo do formulário

            // Verifica se os campos não estão vazios
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(senha))
            {
                MessageBox.Show("Por favor, preencha ambos os campos.", "Erro de Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Consulta no banco de dados para verificar se o usuário existe e obter a senha armazenada
            try
            {
                using (MySqlConnection conexao = dbConnection.GetConnection())
                {
                    string query = "SELECT senha FROM usuarios WHERE usuario = @usuario";
                    MySqlCommand cmd = new MySqlCommand(query, conexao);
                    cmd.Parameters.AddWithValue("@usuario", usuario);

                    conexao.Open();
                    var senhaBanco = cmd.ExecuteScalar(); // Retorna a senha armazenada no banco

                    if (senhaBanco != null)
                    {
                        // Verifica se a senha fornecida corresponde à senha armazenada
                        if (senha.Equals(senhaBanco.ToString()))
                        {
                            MessageBox.Show("Login realizado com sucesso!");

                            // Abre a tela principal (por exemplo, TelaDesktop)
                            TelaDesktop telaDesktop = new TelaDesktop();
                            telaDesktop.Show();

                            // Fecha o formulário de login
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Usuário ou senha inválidos!", "Erro de Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Usuário não encontrado!", "Erro de Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao realizar login: {ex.Message}", "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TelaLogin_Load(object sender, EventArgs e)
        {
            // Qualquer inicialização adicional pode ser feita aqui
        }

        private void picStatus_Click(object sender, EventArgs e)
        {
            // Método para lidar com o clique no ícone (se necessário)
        }

        private void txtSenha_TextChanged(object sender, EventArgs e)
        {
            // Código para lidar com alterações no campo da senha (se necessário)
        }

        private void TelaLogin_Load_1(object sender, EventArgs e)
        {
            // Inicialização adicional (se necessário)
        }
    }
}
