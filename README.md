
🛠️ Sistema de Ordem de Serviço

📋 Descrição

O Sistema de Ordem de Serviço é um projeto desenvolvido em C# para facilitar o gerenciamento de pedidos de serviço. Ele contém funcionalidades como:

🔑 Login de usuários.
🗂️ Cadastro de clientes, usuários e ordens de serviço (OS).
📊 Geração de relatórios.
ℹ️ Informações sobre o sistema.
O controle de acesso é baseado no tipo de usuário: Administrador ou Usuário Comum .

✨ Funcionalidades
  🔑 Entrar
      Autenticação de usuários por meio de credenciais.
      Diferença de permissões com base no tipo de usuário.
      
🏠 Menu Principal
Após o login, o usuário acessa o menu com as seguintes opções:
  📂 Cadastro
      Cadastro de clientes , ordens de serviço (OS) e usuários .
      👤 Usuário Comum: Pode cadastrar apenas clientes e ordens de serviço.
      👑 Administrador: Acesso total a todos os cadastros.

  📊 Relatórios
      Geração de relatórios detalhados de clientes, pedidos de serviço e usuários.
      Disponível apenas para administradores.

  ℹ️ Sobre
      Informações sobre o sistema e seus desenvolvedores.

  🚪 Sair
      Encerra a sessão do usuário e retorna à tela de login.

🔒 Controle de Acesso

  👑 Administrador:
      Acesso total ao sistema.
      Pode gerenciar usuários, visualizar relatórios e realizar
      qualquer operação.
      
  👤 Usuário Comum:
      Acesso limitado a cadastros de clientes e ordens de serviço.
      Não é possível acessar relatórios ou gerenciar usuários.


🚧 Status de Desenvolvimento
    ✅ Tela de Login
    ✅ Controle de acesso por tipo de usuário
    ✅ Cadastro de Clientes, SO e Usuários
    ✅ Relatórios
    ✅ Menu Principal
    🛠️ Melhorias na Interface e Design
    🧪 Testes Finais

🛠️ Tecnologias Utilizadas
  💻 Linguagem: C#
  🖥️ IDE: Visual Studio
  📂 Banco de Dados: MySql
