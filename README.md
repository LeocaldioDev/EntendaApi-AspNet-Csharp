# EntendaApi-AspNet-Csharp

Este projeto é uma API REST simples desenvolvida em ASP.NET Core, utilizando um banco de dados PostgreSQL. O objetivo principal é demonstrar a implementação de diversas funcionalidades comuns em APIs modernas, como autenticação JWT, manipulação de arquivos, paginação, tratamento de erros e Clean Architecture.

## Funcionalidades Principais

* **Gerenciamento de Usuários**:
    * Criação, leitura, atualização e exclusão (CRUD) de usuários.
    * Autenticação via JSON Web Tokens (JWT).
* **Banco de Dados**:
    * Utilização do PostgreSQL como banco de dados.
    * Entity Framework Core para mapeamento objeto-relacional (ORM).
    * Migrations para versionamento do banco de dados.
* **Manipulação de Arquivos**:
    * Upload e download de arquivos utilizando `File` e `FileStream`.
* **Recursos Adicionais**:
    * Paginação para listagem eficiente de dados.
    * Logging para registro de eventos e erros.
    * Tratamento de erros centralizado (Error Handler).
    * Clean Architecture para melhor organização e manutenção do código.
    * Utilização de DTOs (Data Transfer Objects) e AutoMapper para mapeamento de objetos.
    * Versionamento da API.
    * Configuração de CORS para permitir acesso de diferentes origens.
    * Front-end simples em Vue.js para consumir a API.

## Tecnologias Utilizadas

* ASP.NET Core
* Entity Framework Core
* PostgreSQL
* JWT (JSON Web Tokens)
* AutoMapper


## Observações

Este projeto é uma demonstração de conceitos e pode ser utilizado como base para projetos mais complexos.

## Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues e pull requests.
