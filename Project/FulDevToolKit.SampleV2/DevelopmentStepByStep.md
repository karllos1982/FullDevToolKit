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
- 4.3: Adicionar links de navegação à página razor correspondente à entidade no menu lateral (componente NavMenu.razor) e configurar as rotas.