# 🏬 VShop — Sistema de E-commerce em .NET

Aplicação VShop, desenvolvida com base nos vídeos do canal José Carlos Macoratti
, é um sistema de e-commerce completo construído em .NET 8, aplicando boas práticas de arquitetura em camadas, autenticação JWT, e comunicação entre serviços.

# 🚀 Visão Geral

O projeto é composto por três aplicações que se integram para formar o sistema:

Projeto	Descrição	Tecnologia Principal
VShop.Identity	Responsável pela autenticação e autorização de usuários.	ASP.NET Core Identity + JWT
VShop.ProductAPI	Gerencia os produtos e fornece endpoints REST para o front-end.	ASP.NET Core Web API
VShop.Web	Interface web do sistema, consumindo os serviços via API.	ASP.NET Core MVC + Razor

# 🧱 Arquitetura e Estrutura

O VShop segue uma arquitetura em camadas com separação de responsabilidades:
VShop.Identity/  
│── Controllers/  
│── Data/  
│── Models/  
│── Services/  
│── DTOs/  
│── wwwroot/  
│── Program.cs  
│── appsettings.json  
  
VShop.ProductAPI/  
│── Controllers/  
│── Context/  
│── Models/  
│── Repositories/  
│── Services/  
│── DTOs/Mappings/  
│── Migrations/  
│── Program.cs  
  
VShop.Web/  
│── Controllers/  
│── Models/  
│── Services/Contracts/  
│── Views/  
│── wwwroot/  
│── Program.cs  
  
# ⚙️ Principais Tecnologias e Recursos
🧩 Backend (API & Identity)

.NET 8 / ASP.NET Core

Entity Framework Core com Migrations

SQL Server

JWT Authentication

AutoMapper para conversão de DTOs

Dependency Injection

Repository Pattern e Services

ASP.NET Core Identity para gestão de usuários e roles

# 💻 Frontend (Web)

ASP.NET Core MVC

Razor Views

Bootstrap 5

jQuery + jQuery Validation

Consumo de APIs REST

Sessão e autenticação integrada ao Identity

# 🧠 Conceitos Aplicados

Clean Code & SOLID

Injeção de Dependência (DI)

Mapeamento de DTOs

Arquitetura em camadas (Controllers → Services → Repositories → Data)

Separação de responsabilidades por projeto

Migrations e versionamento de banco de dados

# 👨‍💻 Autor

Desenvolvido com base no projeto do professor José Carlos Macoratti, com adaptações práticas e aprimoramentos pessoais.
