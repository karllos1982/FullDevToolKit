**Arquitetura da solução do template FullDevToolKit:

1. **Frontend**:
   - **Tecnologia**: AspNet Blazor Server / Fluent UI
   - **Descrição**: A interface do usuário é construída utilizando Blazor Server construído com base no framework FluentUI, proporcionando uma experiência interativa e responsiva. Componentes reutilizáveis são criados para facilitar a manutenção e escalabilidade do código.
 
2. **Backend**:
   - **Tecnologia**: AspNet Core Web API
   - **Descrição**: A API RESTful é desenvolvida com AspNet Core, fornecendo endpoints para comunicação entre o frontend e o banco de dados. A arquitetura segue o padrão MVC (Model-View-Controller) para separar as responsabilidades e facilitar a manutenção. 

3. **Banco de Dados**: 
   - **Tecnologia**: SQL Server
   - **Descrição**: O banco de dados relacional SQL Server é utilizado para armazenar dados persistentes. A estrutura do banco de dados é projetada para otimizar consultas e garantir a integridade dos dados.

 4. **Autenticação e Autorização**: 
   - **Tecnologia**: JWT Baerer Tokens
   - **Descrição**: A autenticação é implementada utilizando tokens JWT, garantindo que apenas usuários autorizados possam acessar determinados recursos da aplicação.
	
**Projetos da Solução:

- **FullDevToolKit.Models**: Projeto contendo as classes de modelos compartilhadas entre o frontend e o backend.
- **FullDevToolKit.Core**: Projeto responsável pela camada de acesso a dados e entidades do banco de dados e regras de negócio.
- **FullDevToolKit.Api**: Projeto do backend AspNet Core Web API.
- **FullDevToolKit.Proxy**: Projeto intermediário para comunicação entre o frontend e o backend.
- **FullDevToolKit.WebBases**: Projeto que contém os ViewModels e códigos comuns para o frontend.
- **FullDevToolKit.WebComponents**: Projeto que contém os componentes reutilizáveis do frontend.

**Função da classe IContext:
- A interface IContext define o contrato para o contexto de banco de dados, encapsulando a configuração e a conexão com o banco de dados SQL Server.
- Fornece métodos para iniciar e gerenciar transações, garantindo a integridade dos dados durante operações complexas.
- Permite a criação de conexões com o banco de dados, facilitando a execução de comandos SQL e consultas através do Dapper.
- Uma vez implementada, a classe DapperContext concretiza a interface IContext, fornecendo a funcionalidade necessária para interagir com qualquer banco de dados que implemente IDbConnection; 
- IConext é injetada nas classes de repositórios para permitir o acesso ao banco de dados de forma consistente e eficiente.

**Acesso a dados:
- Utiliza Dapper como ORM para mapeamento objeto-relacional, facilitando a interação com o banco de dados SQL Server.
- Implementa o padrão Repository para abstrair a lógica de acesso a dados, promovendo uma separação clara entre a camada de negócios e a camada de dados.
- As queries SQL são gerenciadas pelas classes de QueryBuilder, permitindo a construção dinâmica de consultas SQL de forma segura e eficiente.
- As operações de CRUD (Create, Read, Update, Delete) são encapsuladas em repositórios específicos para cada entidade, garantindo uma organização clara do código e facilitando a manutenção futura.
- As classes de repositórios são agrupadas na classe RepositorySet, que atua como um ponto central para acessar todos os repositórios da aplicação.

**Classes de Domain:
- As classes de Domain representam as entidades principais do sistema, encapsulando tanto os dados quanto a lógica de negócio associada a essas entidades.
- Cada classe de Domain é responsável por validar suas próprias regras de negócio, garantindo que os dados estejam sempre em um estado consistente.
- As classes de Domain utilizam a classe RepositorySet para acessar os repositórios necessários para operações de persistência e recuperação de dados.
- Regras de negócio e validações primárias de dados são implementadas diretamente nas classes de Domain, promovendo uma arquitetura orientada a objetos e facilitando a manutenção do código.
- As classes de Domain são agrupadas na classe DomainSet, que serve como um ponto central para acessar todas as classes de Domain da aplicação.

**Classes de MainBusiness:
- As classes de MainBusiness são responsáveis por implementar a lógica de negócio de determinada parte da aplicação, atuando como intermediárias entre as classes de Domain e os controladores da API.
- Cada classe de MainBusiness utiliza a classe DomainSet para realizar operações complexas que envolvem múltiplas entidades ou regras de negócio.
- A classe MyAppManager atua como um ponto central para acessar todas as classes de MainBusiness da aplicação, facilitando a organização e o gerenciamento da lógica de negócio.

**Camada de API:
- A camada de API é composta por controladores que expõem os endpoints REST (apenas get e post) para consumo pelo frontend.
- Cada controlador utiliza as classes de MainBusiness para processar as requisições recebidas, garantindo uma separação clara entre a lógica de negócio e a camada de apresentação.
- Permissões de acesso são gerenciadas através de atributos de autorização, garantindo que apenas usuários autenticados possam acessar determinados recursos da API.

**Camada de Proxy:
- A camada de Proxy atua como um intermediário entre o frontend e a API, facilitando a comunicação e o consumo dos serviços REST.
- Reflete os mesmos métodos disponíveis na API, permitindo que o frontend interaja com a API de forma transparente.
- Pode ser reutilizada em vários tipos de clientes, promovendo a reutilização de código e facilitando a manutenção futura.

**Camada de Frontend:
- A camada de frontend é construída utilizando AspNet Blazor Server com componentes do Fluent UI, proporcionando uma interface de usuário moderna e responsiva.
- Utiliza ViewModels para gerenciar o estado da interface e facilitar a interação com os dados.
- Componentes reutilizáveis são criados para promover a consistência visual e funcional em toda a aplicação.
- Disponibiliza um módulo de super administração, permitindo a gestão de usuários, permissões e outras configurações do sistema.

** Roteiro para implementação dos artefatos de negócio:

- 1: Camada Core:
- 1.1: Gerar os scripts de crição de tabelas;
- 1.2: Criar as classes de Models correspondentes, que representam os dados da entidade;
	- Classes de models são divididas em 4 funções: 
	   -- Param: campos para filtragem nos métodos de Search e List.
	   -- Entry: contem os campos para entrada de dados.
	   -- List: contem alguns campos usados em listagens de combo.
	   -- Result: contem os mesmos campos da Entry, mas usados para retorno de dados.
- 1.3: Criar as classes de QueryBuilder para gerenciar as queries SQL relacionadas à entidade;
- 1.4: Implementar as classes de Repository para encapsular a lógica de acesso a dados;
- 1.5: Agrupar as classes de Repository na classe RepositorySet;
- 1.6: Criar as classes de Domain para representar a entidade e implementar suas regras de negócio;
- 1.7: Agrupar as classes de Domain na classe DomainSet.
- 1.8: Implementar as classes de MainBusiness para gerenciar a lógica de negócio relacionada à módulos de negócio com funções comuns.

- 2: Camada API:
- 2.1: Criar os controladores para expor os endpoints REST relacionados à entidade, utilizando o MyAppManager para acessar as classes de MainBusiness;

- 3: Camada Proxy:
- 3.1: Implementar os métodos no Proxy para refletir os endpoints da API relacionados à entidade;

- 4: Camada Frontend:
- 4.1: Criar os ViewModels para gerenciar o estado da interface relacionado à entidade;
- 4.2: Criar as páginas e componentes reutilizáveis para a interface do usuário, utilizando os ViewModels e o Proxy para interagir com os dados.
