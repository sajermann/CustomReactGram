# Backend

## Tecnologias

📁 Back-end: .Net 6.0 com Minimals API

🗄️ Banco de Dados: MongoDB

### Inicializando o Backend

**Variáveis de Ambiente**

Preencha a ConnectionString e Database dentro do appsettings.json com as informações do seu banco de dados MongoDB.

**Visual Studio 2022**

Caso você queira executar o backend com o Visual Studio 2022, navegue até a pasta `src/` e abra o arquivo `ReactGramBack.sln` com o Visual Studio 2022. Depois de aberto basta clicar no botão de Start.
OBS: Na primeira vez que tentar executar o projeto e der erro, é porque as vezes é necessário definir o projeto WebMininalApi como projeto de inicialização, basta clicar com o botão direito nele e selecionar a opção `Set as Startup Project`.

**Dotnet CLI**

Caso queira executar o backend com a CLI do dotnet, basta navegar até `\src\WebMinimalApi\` e executar `dotnet restore`, posteriormente executar `dotnet build` e caso nenhum erro seja apresentado você deve por último executar `dotnet run` para inicializar o backend.


