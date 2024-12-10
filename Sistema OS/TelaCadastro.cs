using Desktp;
using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace Sistema_OS
{
    public partial class TelaCadastro : Form
    {
        private DatabaseConnection dbConnection;
        private AutenticacaoService autenticacaoService;

        public TelaCadastro()
        {
            InitializeComponent();
            dbConnection = new DatabaseConnection(); // Instancia a classe DatabaseConnection
            autenticacaoService = new AutenticacaoService();
        }

        private void btnCadastro_Click(object sender, EventArgs e)
        {
            // Captura os dados do formulário
            string nome = txtNome.Text;
            string fone = txtFone.Text;
            string email = txtEmail.Text;
            string usuario = txtLogin.Text;
            string senha = txtSenha.Text;
            string perfil = cmbPerfil.Text;
            string departamento = cmbDepartamento.Text;

            // Validação básica de campos vazios
            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(senha) || string.IsNullOrEmpty(perfil) || string.IsNullOrEmpty(departamento))
            {
                MessageBox.Show("Por favor, preencha todos os campos.");
                return;
            }

            try
            {
                // REMOVER: Gera o hash da senha utilizando o BCrypt
                // string senhaHash = BCrypt.Net.BCrypt.HashPassword(senha);

                // Agora, usa a senha diretamente sem hash
                string senhaHash = senha; // Senha sem criptografia

                // Obtém a conexão com o banco de dados a partir da classe DatabaseConnection
                using (MySqlConnection conexao = dbConnection.GetConnection()) // Aqui está a mudança
                {
                    string query = "INSERT INTO usuarios (nome, email, usuario, senha, perfil, departamento) " +
                                   "VALUES (@nome, @email, @usuario, @senha, @perfil, @departamento)";

                    MySqlCommand cmd = new MySqlCommand(query, conexao);
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@senha", senhaHash); // Envia a senha sem hash
                    cmd.Parameters.AddWithValue("@perfil", perfil);
                    cmd.Parameters.AddWithValue("@departamento", departamento);

                    conexao.Open();
                    cmd.ExecuteNonQuery();
                    conexao.Close();

                    MessageBox.Show("Usuário cadastrado com sucesso!");
                    LimparCampos(); // Limpar os campos após cadastro
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao cadastrar usuário: {ex.Message}");
            }
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            string usuario = txtLogin.Text; // Pegando o login para deletar

            if (string.IsNullOrEmpty(usuario))
            {
                MessageBox.Show("Por favor, insira o login do usuário para deletar.");
                return;
            }

            // Pergunta ao usuário se ele realmente deseja excluir
            var confirmResult = MessageBox.Show(
                "Tem certeza de que deseja excluir este usuário?",
                "Confirmar Exclusão",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult == DialogResult.No)
            {
                return; // Se o usuário escolher "Não", cancela a exclusão
            }

            try
            {
                using (MySqlConnection conexao = dbConnection.GetConnection())
                {
                    string query = "DELETE FROM usuarios WHERE usuario = @usuario";
                    MySqlCommand cmd = new MySqlCommand(query, conexao);
                    cmd.Parameters.AddWithValue("@usuario", usuario);

                    conexao.Open();
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Usuário deletado com sucesso!");
                    LimparCampos(); // Limpar os campos após exclusão
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao deletar usuário: {ex.Message}");
            }
        }


        // Método para limpar os campos após as operações
        private void LimparCampos()
        {
            txtNome.Clear();
            txtEmail.Clear();
            txtLogin.Clear();
            txtSenha.Clear();
            txtFone.Clear();
            cmbPerfil.Items.Clear();
            cmbDepartamento.Items.Clear();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            string usuario = txtLogin.Text; // Pegando o login para pesquisa

            if (string.IsNullOrEmpty(usuario))
            {
                MessageBox.Show("Por favor, insira um login para pesquisa.");
                return;
            }

            try
            {
                // Conexão com o banco de dados
                using (MySqlConnection conexao = dbConnection.GetConnection())
                {
                    string query = "SELECT nome, email, fone, departamento, perfil FROM usuarios WHERE usuario = @usuario";
                    MySqlCommand cmd = new MySqlCommand(query, conexao);
                    cmd.Parameters.AddWithValue("@usuario", usuario);

                    conexao.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Preenche os campos do formulário com os dados encontrados
                        txtNome.Text = reader.GetString("nome");
                        txtEmail.Text = reader.GetString("email");
                        txtFone.Text = reader.GetString("fone");
                        cmbDepartamento.SelectedItem = reader.GetString("departamento");
                        cmbPerfil.SelectedItem = reader.GetString("perfil");
                    }
                    else
                    {
                        MessageBox.Show("Usuário não encontrado.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao pesquisar usuário: {ex.Message}");
            }
        }

        // Método para editar um usuário
        private void btnEditar_Click(object sender, EventArgs e)
        {
            string nome = txtNome.Text;
            string fone = txtFone.Text;
            string email = txtEmail.Text;
            string usuario = txtLogin.Text;
            string perfil = cmbPerfil.Text;
            string departamento = cmbDepartamento.Text;

            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(usuario))
            {
                MessageBox.Show("Nome e Login são obrigatórios.");
                return;
            }

            try
            {
                using (MySqlConnection conexao = dbConnection.GetConnection())
                {
                    string query = "UPDATE usuarios SET nome = @nome, email = @email, fone = @fone, departamento = @departamento, perfil = @perfil WHERE usuario = @usuario";
                    MySqlCommand cmd = new MySqlCommand(query, conexao);

                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@fone", fone);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@perfil", perfil);
                    cmd.Parameters.AddWithValue("@departamento", departamento);

                    conexao.Open();
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Usuário atualizado com sucesso!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao editar usuário: {ex.Message}");
            }
        }

        private void TelaCadastro_Load(object sender, EventArgs e)
        {

        }
    }
}
