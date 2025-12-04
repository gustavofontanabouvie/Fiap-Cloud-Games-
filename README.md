# FIAP Cloud Games - Microsservices Architecture

> **Projeto de Pós-Graduação em Arquitetura de Sistemas .NET**
> Fase 2: Decomposição de Monolito e Arquitetura Orientada a Eventos.

![Build Status](https://img.shields.io/badge/Build-Passing-brightgreen)
![.NET](https://img.shields.io/badge/.NET-8.0-purple)
![RabbitMQ](https://img.shields.io/badge/Message_Broker-RabbitMQ-orange)

## O Projeto

Este projeto demonstra a implementação de uma arquitetura de **Microsserviços** utilizando **.NET 8**, focada em escalabilidade e desacoplamento. O sistema gerencia a venda de jogos digitais, utilizando padrões como **Saga Coreografada**, **Clean Architecture** e **Event-Driven Architecture**.

O objetivo principal é sair de uma arquitetura monolítica para um ecossistema distribuído onde os serviços se comunicam de forma assíncrona.

---

## Arquitetura

O sistema adota o padrão **Monorepo** e está dividido nos seguintes Bounded Contexts:

### Microsserviços

| Serviço | Responsabilidade | Porta (Local) |
| :--- | :--- | :--- |
| **Payments.API** | Processamento de pagamentos e Gateway (Simulado). Publica eventos de `OrderPaid`. | `8080` / `8081` |
| **Users.API** | Gestão de usuários e Biblioteca de Jogos. Consome eventos de pagamento para liberar acesso. | `5000` / `5001` |
| **Catalog.API** | (Em breve) Gestão do catálogo de jogos e preços. Servirá dados via gRPC. | - |
| **Notification.API** | (Em breve) Envio de e-mails transacionais. | - |

### Fluxo de Mensageria (Saga)

O fluxo atual implementado cobre o cenário de **Compra com Sucesso**:

1.  **Checkout:** Cliente envia intenção de compra para `Payments.API`.
2.  **Processamento:** `Payments.API` valida (mock gRPC), processa e publica o evento `PaymentSucceededEvent` no **RabbitMQ**.
3.  **Reação:** `Users.API` escuta o evento e libera o jogo na biblioteca do usuário.
4.  **Feedback:** O cliente recebe `202 Accepted` imediatamente, garantindo alta disponibilidade.

---

## Tecnologias Utilizadas

* **Linguagem:** C# (.NET 8)
* **Mensageria:** MassTransit + RabbitMQ
* **Containerização:** Docker
* **Comunicação Síncrona:** gRPC (Planejado)
* **Documentação:** Swagger (OpenAPI)

---

## Como Rodar o Projeto

### Pré-requisitos
* [.NET 8 SDK](https://dotnet.microsoft.com/download)
* [Docker Desktop](https://www.docker.com/products/docker-desktop) (Obrigatório para o RabbitMQ)

### Passo 1: Subir a Infraestrutura (RabbitMQ)
Abra o terminal na raiz do projeto e garanta que o RabbitMQ esteja rodando:

```bash
docker run -d --hostname my-rabbit --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
```

Acesse o painel do RabbitMQ em: `http://localhost:15672` (User: `guest`, Pass: `guest`).

### Passo 2: Rodar os Microsserviços
Recomenda-se rodar via Visual Studio (configurando "Multiple Startup Projects") ou abrir dois terminais:

**Terminal 1 (Payments):**
```bash
cd src/PaymentsAPI
dotnet run
```

**Terminal 2 (Users):**
```bash
cd src/UsersAPI
dotnet run
```

### Passo 3: Testar o Fluxo
1. Abra o Swagger da Payments API (`https://localhost:PORTA/swagger`).
2. Utilize o endpoint `POST /api/CheckoutSimulation`.
3. Observe nos consoles das aplicações a troca de mensagens:
    * *Payments:* "Pagamento aprovado..."
    * *Users:* "Recebi confirmação e liberei o jogo..."


---

Developed by **Gustavo Fontana**.
