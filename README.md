Motorcycle Maintence Schedule CRUD

Especificações:
- Dotnet Core 8.0
- Entity Framework
- Postgres
- RabbitMQ
- Docker
- Docker compose
- Polly
- NUnit
- Moq
- Fluent Assertion

#How to run
1. Abra o terminal, preferencialmente na raiz C:/ (opcional)
$ mkdir Projetos

2. No terminal clonar:
$ git clone git@github.com:VitorGiamm5/MotorcycleMaintenanceSchedule.git

3. Entrar na raíz do projeto:
$ cd MotorcycleMaintenanceSchedule

4. Executar o docker compose:
$ docker-compose -f deploy/docker-compose.yml down
$ docker-compose -f deploy/docker-compose.yml up --build

Observação:
- Depois que o docker baixar todas as imagens ele auto inicia os serviços
- A aplicação .Net possui a injeção do Polly, isso maximiza que a conexão entre os serviços sejam realizadas dentro do docker

5. Verificar se os serviços estão online (91612379773_postgre, 91612379773_rabbitmq e 41864645926_api_maintenanceschedule)
$ docker ps -a

7. Executar as migrations (esteja com seu terminal na raíz do projeto "c:/Projetos/CoreGoDelivery")
$ dotnet ef database update -s src\MotorcycleMaintenanceSchedule.Api -p src\MotorcycleMaintenanceSchedule.Infrastructure

8. Para conectar o Postgres recomenda-se usar o DBeaver, para facilitar a importação de dados que serão necessários!

Host: localhost
Port: 9000
Bando de dados: dbmaintenanceschedule
Nome de usuário: randandan
Senha: randandan_XLR

Certifique-se de possuir o EF instalado localmente 
$ dotnet tool install --global dotnet-ef

Gerar migration, considere abrir o Powershell na pasta raiz do projeto: 
$ dotnet ef migrations add InicialBase -s src\MotorcycleMaintenanceSchedule.Api -p src\MotorcycleMaintenanceSchedule.Infrastructure

Atualizar o banco:
$ dotnet ef database update -s src\MotorcycleMaintenanceSchedule.Api -p src\MotorcycleMaintenanceSchedule.Infrastructure

Referências:

- Página admin do RabbitMQ
http://localhost:9002/#/

Serviço					      Porta Interna	Porta Externa	Observação
PostgreSQL						5432	          9000	          Porta padrão do PostgreSQL
RabbitMQ (AMQP)					5672	          9001	          Porta para o protocolo AMQP
RabbitMQ (UI)					15672	          9002	          Porta para a interface de UI
CoreGoDelivery					9005           9005	          Porta da aplicação
MotorcycleMaintenanceSchedule         9103           9103	          Porta da aplicação principal


Estrutura
[tests]
- Domain
- Application
[src]
- Api
- Application
- Domain
- Infrastructure

