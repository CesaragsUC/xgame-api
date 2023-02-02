Funcionalidade:  Cadastro do usuario
	Como visitante do site
	Eu desejo me cadastrar no site
	Para que eu possa ter acesso as funcionalidades

Cenario: Cadastrar usuario com sucesso
Dado Que o visitante esta acessando o site
E Clicar em se registrar registrar
E Preencher o formulario
	| Dados           |
	| Email           |
	| Senha           |
	| Confirmar Senha |
E Clicar no botao registrar
Entao  Ele Sera redirecionado para pagina inicial
E Sera exibido seu email no topo do site


Cenario: Cadastro com senha letras maiusculas
Dado Que o visitante esta acessando o site
E Clicar em se registrar registrar
E Preencher o formulario com senha sem letras maiusculas
	| Dados           |
	| Email           |
	| Senha           |
	| Confirmar Senha |
E Clicar no botao registrar
Entao  ele recebera uma mensagem de erro dizendo que a senha precisa de letras maiusculas

Cenario: Cadastro com senha sem caracteres especiais
Dado Que o visitante esta acessando o site
E Clicar em se registrar registrar
E Preencher o formulario com senha sem caracteres especiais
	| Dados           |
	| Email           |
	| Senha           |
	| Confirmar Senha |
E Clicar no botao registrar
Entao  ele recebera uma mensagem de erro dizendo que a senha precisa conter caracteres especiais
