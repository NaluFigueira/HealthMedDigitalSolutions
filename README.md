# Health&Med Digital Solutions

## Requisitos

A aplicação tem como principal objetivo possibilitar o agendamento de consultas médicas, com as seguintes funcionalidades:

- Permitir o cadastro de novos médicos e pacientes.
- O paciente terá acesso ao catálogo de médicos disponíveis na plataforma, e às suas respectivas agendas. Podendo agendar uma consulta a qualquer momento, mediante confirmação do médico.
- O paciente pode cancelar uma consulta a qualquer momento.

## Solução proposta

### Arquitetura da solução
![Arquitetura da solução](https://github.com/user-attachments/assets/505b27e6-ed5f-4ef3-b254-fed46005d308)

### Arquitetura técnica
![Arquitetura técnica](https://github.com/user-attachments/assets/e9a0dc0c-9e73-4f9c-bd37-032394932ec2)

### Arquitetura de dados
![Arquitetura de dados](https://github.com/user-attachments/assets/2fcfd445-5768-492a-9492-84aa3988d6e4)

### Processo de desenvolvimento
![Processo de desenvolvimento](https://github.com/user-attachments/assets/ef6f9009-f6ee-429c-a111-50d01f06da1e)

## Tecnologias e ferramentas

Desenvolvimento:

- .NET 8.0
- FluentResults
- FluentValidation
- OpenAPI
- EntityFrameworkCore
- ILogger
- Identity
- JWT Bearer Token

Testes:

- XUnit
- Bogus
- Moq
- FluentAssertions

## Como executar o projeto localmente

### Requerimentos

- Ter versões mais recentes do [Docker e docker-compose](https://docs.docker.com/manuals/) instaladas.

### Instruções

1. Baixar o projeto localmente.
2. Na pasta raiz do projeto executar o comando

```bash
docker-compose up -d
```

3. Na pasta raiz de cada MS (users, appointments) executar os comandos:

```bash
docker-compose build
docker-compose up -d
```

4. Acessar a url http://localhost:8080/swagger/index.html para ter acesso a inteface do Swagger do MS users e fazer requisições.
5. Acessar a url http://localhost:8081/swagger/index.html para ter acesso a inteface do Swagger do MS appointments e fazer requisições.
