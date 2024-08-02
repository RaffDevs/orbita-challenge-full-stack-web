## Resolução

### Arquitetura e Padrões Utilizados

## Clean Architecture
A Clean Architecture foi escolhida para a aplicação ASP.NET Core de gerenciamento de alunos para garantir uma separação clara entre as responsabilidades. Isso melhora a organização do código, facilita testes e aumenta a manutenibilidade ao isolar a lógica de negócio da infraestrutura e da interface do usuário.

## CQRS (Command Query Responsibility Segregation)
O padrão CQRS foi implementado para separar as operações de leitura e escrita. Essa abordagem otimiza o desempenho e escalabilidade, reduz a complexidade do código e permite aplicar estratégias especializadas para consultas e comandos, tornando a aplicação mais flexível e eficiente.

## Dependências Utilizadas

### ASP.NET Core

- **Microsoft.EntityFrameworkCore**: 8.0.7
- **Microsoft.EntityFrameworkCore.Design**: 8.0.7
- **Swashbuckle.AspNetCore**: 6.4.0
- **FluentValidation**: 11.9.2
- **FluentValidation.AspNetCore**: 11.3.0
- **MediatR**: 12.4.0
- **Microsoft.Extensions.Configuration**: 8.0.0
- **Microsoft.Extensions.DependencyInjection**: 8.0.0
- **Npgsql.EntityFrameworkCore.PostgreSQL**: 8.0.4
- **Xunit** (para testes)

### Vue.js

- **axios**: ^1.7.3
- **core-js**: ^3.8.3
- **vue**: ^2.6.14
- **vue-router**: ^3.6.5
- **vuetify**: ^2.6.0

#### DevDependencies

- **@babel/core**: ^7.12.16
- **@babel/eslint-parser**: ^7.12.16
- **@vue/cli-plugin-babel**: ~5.0.0
- **@vue/cli-plugin-eslint**: ~5.0.0
- **@vue/cli-service**: ~5.0.0
- **eslint**: ^7.32.0
- **eslint-plugin-vue**: ^8.0.3
- **sass**: ~1.32.0
- **sass-loader**: ^10.0.0
- **vue-cli-plugin-vuetify**: ~2.5.8
- **vue-template-compiler**: ^2.6.14
- **vuetify-loader**: ^1.7.0

## Melhorias Planejadas

### Separação dos Componentes Vue.js

Atualmente, alguns componentes do Vue.js estão agrupados em arquivos únicos, o que pode dificultar a manutenção e a escalabilidade do código. Planejo separar os componentes em arquivos distintos para melhorar a organização e a clareza do projeto. Isso permitirá um gerenciamento mais eficiente do código e facilitará a colaboração em equipe.

### Implementação de Autenticação e Login

Para aumentar a segurança da aplicação, será implementada uma funcionalidade de autenticação e login. Isso permitirá validar usuários administradores e garantir que apenas usuários autorizados possam acessar funcionalidades específicas da aplicação. A autenticação ajudará a proteger dados sensíveis e a controlar o acesso de forma mais eficaz.

### Limitação de Requests

Para prevenir sobrecarga e proteger a aplicação contra ataques de negação de serviço (DoS), será implementado um mecanismo de limitação de requests. Essa medida ajudará a garantir que a aplicação possa lidar com um grande volume de solicitações sem comprometer a performance ou a estabilidade do sistema.

Essas melhorias visam aprimorar a organização do código, a segurança e a eficiência da aplicação, preparando-a melhor para enfrentar desafios futuros e atender às necessidades dos usuários.

### Requisitos para rodar o projeto
- Docker
- Docker Compose
- .NET Core SDK
- Nodejs
- Vue Cli
- .NET instalado para executar os testes

### Como rodar o projeto
1. **Tenha os requisitos instalados.**
    **OBS:** para rodar a aplicação basta ter o docker. Caso queira rodar para desenvolvimento, necessário ter os requisitos citados acima.
2. **Clone este repositório.**
   
   ```sh
   git clone https://github.com/RaffDevs/orbita-challenge-full-stack-web
   cd pasta/raiz/do/projeto
   ```
3. **Na pasta raiz do projeto, execute**:

   ```sh
   docker-compose build --build-args MIGRATE=true
   ```
   OBS: essa variavel é somente para aplicar a migration e criar as tabelas
   no container do postgres. Caso já tenha feito isso, rodar somente: **docker compose build**

4. **E depois execute**:

    ```sh
    docker-compose up -d
    ```
   
5. **Pronto! O projeto já deve estar rodando. Para testar, acesse:**
   ```sh
   http://localhost:3002
   ```

### Como rodar os testes

1. **Acesse a pasta StudentAdmin e, tendo o .NET instaldo, execute:**
   
   ```sh
   dotnet test
   ```
 
**Dessa forma, os testes unitários serão executados. O esperado é que todos passem!**