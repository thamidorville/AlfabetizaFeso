# 📚 Sistema de Apoio à Alfabetização de Jovens e Adultos

Este projeto é uma **API RESTful** desenvolvida em **.NET 9** utilizando **Entity Framework Core** com **PostgreSQL**.  
O objetivo é apoiar a alfabetização de jovens e adultos, permitindo que **educadores** se cadastrem na plataforma para oferecer aulas, e que **alunos** possam futuramente consultar e escolher educadores.

---

## 🚀 Tecnologias utilizadas
- [.NET 9](https://dotnet.microsoft.com/)  
- [Entity Framework Core 9](https://learn.microsoft.com/ef/core/)  
- [PostgreSQL 17](https://www.postgresql.org/)  
- [Npgsql Provider](https://www.npgsql.org/efcore/)  
- [Swagger / Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)

---

## ⚙️ Pré-requisitos

Antes de rodar o projeto, você precisa ter instalado:

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)  
- [PostgreSQL](https://www.postgresql.org/download/)  
- [pgAdmin 4](https://www.pgadmin.org/download/) (opcional, para visualizar o banco)  
- [Git](https://git-scm.com/)  

---

## 📂 Estrutura inicial

AlfabetizaFeso.Api/
├── Controllers/ -> Controllers da API
├── Data/ -> DbContext (conexão com o banco)
├── Models/ -> Entidades do domínio (Educador, etc.)
├── Migrations/ -> Histórico de migrações do EF Core
├── appsettings.json -> Configurações (connection string)
└── Program.cs -> Ponto de entrada da aplicação

1. Crie um banco no PostgreSQL:
   ```sql
   CREATE DATABASE alfabetizafeso;

2. No arquivo appsettings.json configure a connectionstring: 

"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=alfabetizafeso;Username=postgres;Password=SuaSenhaAqui"
}


3. Executar o comando abaixo pra rodar as migrations:
dotnet ef database update
