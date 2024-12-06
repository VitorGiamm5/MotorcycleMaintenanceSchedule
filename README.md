Go Core Delivery

Especificações:
- Dotnet Core 8.0
- Entity Framework
- Postgres
- RabbitMQ
- Docker
- Docker compose
- Polly
- xUnit

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

8. Para conectar o Banco recomenda-se usar o DBeaver, para facilitar a importação de dados que serão necessários!

Host: localhost
Port: 9000
Bando de dados: dbgodelivery
Nome de usuário: randandan
Senha: randandan_XLR

Notas e dicas de uso:

Para os end-points que necessitam de imagem base64, elas estão disponíveis na pasta "Assets" e então na pasta "ImageCNH", nela contém uma imagem .png e uma .bmp e de brinde, arquivos de texto com as imagens já em base64!

Caso modifique alguma entidade, esse é o comando para criar a migration

Gerar migration, considere abrir o Powershell na pasta raiz do projeto: 
$ dotnet ef migrations add InicialBase -s src\MotorcycleMaintenanceSchedule.Api -p src\MotorcycleMaintenanceSchedule.Infrastructure

Atualizar o banco:
$ dotnet ef database update -s .\MotorcycleMaintenanceSchedule.Api -p .\MotorcycleMaintenanceSchedule.Infrastructure

Referências:

- Validador de documentos
https://www.nuget.org/packages/DocsBRValidator

- Página administrativa do RabbitMQ
http://localhost:15672/#/

Serviço	        Porta Interna	Porta Externa	Observação
PostgreSQL	    5432	        9000	        Porta padrão do PostgreSQL
RabbitMQ (AMQP)	5672	        9001	        Porta para o protocolo AMQP
RabbitMQ (UI)	15672	        9002	        Porta para a interface de UI
MinIO (API)	    9000	        9003	        Porta da API do MinIO
MinIO (Console)	9001	        9004	        Porta do console do MinIO
CoreGoDelivery	80	            9005	        Porta da aplicação principal
