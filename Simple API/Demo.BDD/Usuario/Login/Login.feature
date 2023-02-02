Funcionalidade: Fazer login no sistema
	Como um usuario
	Eu desejo fazer login no site

@FazerLogin
Cenario: Realizar Login
Dado Que o visitante esta acessando o site
Quando Ele clicar no botao login
E Preencher o formulario de login
	| Dados |
	| Email |
	| Senha |
E Clicar no botao login
Entao  Ele Sera redirecionado para pagina inicial
E Sera exibido seu email no topo do site
