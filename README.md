# Solar Sync API

### Advanced Business Development with .NET

[![.NET Core](https://img.shields.io/badge/.NET-8.0-blue)](https://dotnet.microsoft.com/download/dotnet/8.0) [![MongoDB](https://img.shields.io/badge/MongoDB-Database-green)](https://www.mongodb.com/) [![Swagger](https://img.shields.io/badge/Swagger-API%20Docs-brightgreen)](https://swagger.io/) [![Build](https://img.shields.io/badge/Build-Passing-brightgreen)](https://github.com/solar-sync/api)

## 🏠 Sobre o Projeto

Solar Sync API é uma solução poderosa desenvolvida em .NET Core 8 com o objetivo de promover eficiência energética e incentivar o uso de energia renovável. Esta API foi projetada para atender às demandas do futuro da sustentabilidade, fornecendo uma plataforma robusta para conectar consumidores e provedores de energia solar, além de realizar o monitoramento e otimização do consumo de energia.

### Principais Características
- **Eficiência Energética**: Monitoramento em tempo real do consumo de energia, com sugestões para redução do uso diário.
- **Sustentabilidade**: Oferece soluções inovadoras para alugar, comprar ou investir em instalações de painéis solares.
- **Inteligência Artificial Generativa**: Implementação de IA para otimização de processos e recomendações automatizadas para os usuários.
- **Arquitetura Moderna**: Desenvolvido seguindo os princípios de Arquitetura de Software, como modularidade, escalabilidade e separação de responsabilidades.

## 🛠️ Tecnologias Utilizadas
- **.NET Core 8**: Utilizado como framework principal para criar uma API robusta e escalável.
- **MongoDB**: Banco de dados NoSQL que oferece flexibilidade e alta performance para armazenamento de dados.
- **Swagger**: Ferramenta para documentação da API, proporcionando uma interface de usuário clara e acessível.
- **Moq e xUnit**: Utilizados para testes automatizados, assegurando a qualidade e estabilidade do código.
- **Design Patterns**: Utiliza padrões como Repository, Factory e Dependency Injection para garantir organização e manutenção simplificada.

## 📈 Objetivos do Projeto
1. **Melhorar Processos de Energia Sustentável**: Fornecer uma API que permita aos usuários monitorar e reduzir o consumo de energia, contribuindo para a sustentabilidade.
2. **Garantir Modularidade e Escalabilidade**: Implementar princípios de Arquitetura de Software que facilitem a expansão da aplicação no futuro.
3. **Utilizar IA Generativa**: Melhorar os processos da aplicação através de IA, fornecendo soluções inteligentes para os consumidores.

## 🛡️ Arquitetura e Padrões Utilizados

- **Repository Pattern**: Para isolar a camada de acesso ao banco de dados, facilitando a troca e manutenção dos repositórios.
- **Factory Pattern**: Criado para instanciar objetos complexos, garantindo uma arquitetura modular e ética.
- **Dependency Injection**: Utilizada para reduzir o acoplamento entre classes, promovendo a reutilização do código e a manutenção.

## 🧠 IA Generativa
O projeto inclui a integração de recursos de IA generativa que analisam o consumo de energia de cada cliente e geram recomendações automáticas para otimizar o consumo, baseando-se em modelos de aprendizado de máquina que identificam padrões no comportamento de consumo.

## 🔧 Configuração e Uso

### Pré-requisitos
- **.NET Core SDK 8.0**
- **MongoDB** instalado e em execução.

### Como Rodar o Projeto
1. Clone o repositório:
   ```sh
   git clone https://github.com/SolarSync-Org/solar-sync-API.git
   ```
2. Navegue até o diretório da API:
   ```sh
   cd solar-sync-API/SolarSync_API
   ```
3. Instale as dependências:
   ```sh
   dotnet restore
   ```
4. Configure as variáveis de ambiente, se necessário.
5. Rode a aplicação:
   ```sh
   dotnet run
   ```

## 🔢 Endpoints Principais

- **GET /api/clients**: Retorna todos os clientes registrados.
- **POST /api/clients**: Registra um novo cliente.
- **PUT /api/clients/{id}**: Atualiza as informações de um cliente.
- **DELETE /api/clients/{id}**: Remove um cliente do sistema.
- **GET /api/clientreports**: Retorna todos os relatórios de clientes registrados.
- **POST /api/clientreports**: Cria novos relatórios de clientes em massa.
- **GET /api/clientreports/{id}**: Retorna um relatório de cliente específico pelo ID.
- **PUT /api/clientreports/{id}**: Atualiza um relatório de cliente específico.
- **DELETE /api/clientreports/{id}**: Remove um relatório de cliente pelo ID.
- **POST /api/clientreports/train-and-update-labels**: Treina e atualiza as labels dos relatórios de clientes.
- **GET /api/companies**: Retorna todas as empresas registradas.
- **POST /api/companies**: Registra uma nova empresa.
- **GET /api/companies/{id}**: Retorna uma empresa específica pelo ID.
- **GET /api/companies/cnpj/{cnpj}**: Retorna uma empresa específica pelo CNPJ.
- **PUT /api/companies/{id}**: Atualiza uma empresa específica.
- **DELETE /api/companies/{id}**: Remove uma empresa do sistema.
- **GET /api/companyreports**: Retorna todos os relatórios de empresas registrados.
- **POST /api/companyreports**: Cria novos relatórios de empresas em massa.
- **GET /api/companyreports/{id}**: Retorna um relatório de empresa específico pelo ID.
- **PUT /api/companyreports/{id}**: Atualiza um relatório de empresa específico.
- **DELETE /api/companyreports/{id}**: Remove um relatório de empresa pelo ID.

Documentação detalhada dos endpoints está disponível via Swagger no caminho `/swagger` após iniciar o servidor.

## 🌐 Documentação da API
A API está documentada com Swagger. Acesse a documentação no seguinte link após inicializar o servidor:
```
http://localhost:<porta>/swagger
```

## 🐝 Testes Automatizados
Os testes automatizados são implementados utilizando **xUnit** e **Moq**. Para rodar os testes, use o comando:
```sh
   dotnet test
```
Os testes garantem que todas as funções essenciais do sistema estejam funcionando como esperado.

## 👨‍💻 Equipe de Desenvolvimento
- **Beatriz Svestka** - rm551534
- **Eduardo Violante** - rm550364
- **Nicholas Santos** - rm551809
- **Pedro Pacheco** - rm98043
- **Vitor Kubica** - rm98903

