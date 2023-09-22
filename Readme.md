Exemplo bem simplificado de autenticação com JWT Token.

A classe LoginService faz a autenticação do usuário, buscando em UserRepository os dados do usuário (todos mockados)
O Token é gerado pela método GenerateToken dentro de TokenService. 

Cada endpoint possui a politica a qual o usuario precisa ter acesso para ler a mensagem.

Créditos:
Este exemplo foi baseado em um video do Balta.io. Para entender melhor, por favor acesse https://www.youtube.com/watch?v=eNeZHCEG5hE

