# CustomReactGram

Projeto criado no curso React do Zero a Maestria (c/ hooks, router, API, Projetos) do Matheus Battisti.

O projeto do curso se Chama React Gram, porém eu renomeie para Custom React Gram pois decidi fazer várias modificações.

Como o meu foco principal era aprender Redux, eu escolhi fazer as coisas um pouco diferente como no caso do backend na linguagem que eu conheço mais e incluir o typescript para evitar erros que poderiam atrasar.

Caso você esteja procurando um projeto de referência, não utilize esse devido a várias modificações que realizei, que são:

## Projeto Original / Minha modificação

* Criado com CRA / Criado com Vite
* Sem EsLint e Prettier / Com EsLint e Prettier
* Javascript / Typescript
* Backend feito em Node / Backend feito em .Net (Minimals API)
* Retorno da api a meu gosto em alguns endpoints
* Inclusão de Tailwind (a intenção no início era utilizar, mas acabei desistindo e deixei configurado para caso eu queira incluir no futuro)

Utilizei o Typescript do jeito mais simples possível, como por exemplo utilizar a typagem any em vários lugares, fiz isso para acelerar o curso, a intenção era só localizar os bugs no momento da codificação e não focando em manutenção.

## Tecnologias

📁 Back-end: .Net 6.0 com Minimals API

🗄️ Banco de Dados: MongoDB

🖥️ Front-end: ReactJs (Vite e Typescript)

### Para rodar a aplicação

No backend, preencha a ConnectionString e Database dentro do appsettings.json com as informações do seu banco de dados MongoDB, abra o projeto com o Visual Studio e execute a aplicação.

No frontend, execute `npm install` para baixar as dependências, e depois execute `npm start` para iniciar a aplicação.

### Demonstração

**Tela de Login**
![LoginScreen](https://lh3.googleusercontent.com/pw/AL9nZEWIkSQs5UzK6-CEZMcdSaKUpXtrb13KLvsqfn4kV-H-Mq9bFY8SggjCR23kyzu9YXpbUGCQFedpX6LxGyrjfiBAryD3aFuGxW0vMp6SEgYcsKJcXdWq3J25PYmCPIfXmJH6nIocU97URq5pqQ3ThAbu=w1440-h866-no?authuser=0)

**Tela de Cadastro**
![RegisterScreen](https://lh3.googleusercontent.com/pw/AL9nZEURpYCFQX1yYhEEEOdttLJ909fKAak6MDEj_dIebz4zVm9ZPXC1r4OMKxdPOXU9iB7cFKpSVSo5XHddbRyqrnPHd4faViRyceiy1JHaPJa8CnvJZrwx6MsgxqEy_7TIoqINuRxxR-YSKgVcT_D_yIjY=w1461-h881-no?authuser=0)

**Tela Home**
![HomeScreen](https://lh3.googleusercontent.com/pw/AL9nZEU4lgNPtrqoL_Pk9ynVPZBV3NSHOkO5PsbjAePWPysJFOkmDyUlCSbZh9UY8PW_04c5Dev0hdAxg1ptNBpS_9QhR-Cj5sBKvXGRkc8ezoqc6Lqa5UEN5RkrgK_GV1BibCj5i-C9oHa9uKBTJGy_wXgA=w1447-h889-no?authuser=0)

**Tela Detalhes da Foto**
![DetailsScreen](https://lh3.googleusercontent.com/pw/AL9nZEVicEFxtIpbpUm1Q5DMi-mEZNMmjO74BBiDLLPQSw86i2LGGFGOZ0_IuK0YQrF1qJaVNQw6c3VyBLEbZCZPAHv15cy6fVeh5sswV2BiTPXThA81Elv_IDCUrW0lmbpMtnZUIgV3RAKd72cT0lDFuqKY=w1408-h937-no?authuser=0)

**Tela Minhas Fotos**
![MyPhotosScreen](https://lh3.googleusercontent.com/pw/AL9nZEWnKNjYDM-dYaQ2msatpNodvKoRtRAYz5Sc6LoHKnJ4RyyaVQcWS9y06UnVHVfE7aBIybeL6nLDoMyLhAmzbUD9yO-tLfIdFusPOVdVJcZjdyYb0nRiFzq_hZI7hqYmeG1lhpWsOH-Z5DHYkP3RyV4p=w1456-h861-no?authuser=0)

**Tela Atualizar Perfil**
![UpdatePerfilScreen](https://lh3.googleusercontent.com/pw/AL9nZEVFMXI1-B2jmlljPZOChHqLdpPDRtCRXvAZssFJASfWy9al17o-8nkSmZAvSVieRxEcrXdaUMdQ40qrmj0yO3G8ml57FP8EBgaMg91qU5Qypz3NDvODGV1LttJeSkGyyMybtR7gyLJn0yhUwVjro2Cz=w1432-h931-no?authuser=0)
