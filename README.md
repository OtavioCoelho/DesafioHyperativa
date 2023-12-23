# Desafio Hyperativa

## Desafio proposto

 - Cadastrar um lote de cartões;
 - Cadastrar um único cartão;
 - Efetuar a consulta através do número do cartão e obter a sua identificação único (ID);

## Sobre o projeto
O projeto cumpre o proposto, inclusive salvando os dados de forma segura no banco de dados, **criptografando** os dados ao salvar, desta forma não deixando ilegível, caso alguém sem permissão tenha acesso ao banco de dados.
O projeto também conta com uma pasta de **logs**, nos quais salvos e divididos por data, tendo um arquivo por dia. Nos logs ficam registrados todas as solicitações requisitadas pelo endpoint, eventuais falhas, retorno das requisições, consulta realizada no banco de dados. Os logs também podem ser observados através do console que é aberto ao iniciar a aplicação pelo visual studio.
Para garantir a integridade e evitar "bugs" o projeto conta com vários **testes unitários**, verificando as *constrollers*, *Dtos, Services, Repository.*


## Executando o projeto

**Requisitos**

 - .NET 7.0 Core SDK - [Baixar](https://dotnet.microsoft.com/pt-br/download/dotnet/7.0)
 - Visual Studio 2022 [recomendado]- [Baixar](https://visualstudio.microsoft.com/pt-br/downloads/)  | Visual Code - [Baixar](https://code.visualstudio.com/download)

Após o baixar e instalar os requisitos acima, faça o clone deste repositório para sua máquina e execute o projeto.
**O projeto já contém um banco de dados incluso** com as tabelas necessárias e com 2 usuários pré-cadastrados.

**Caso deseje utilizar um novo banco de dados**, abra o arquivo `appsettings.Development.json`, localizado em `DesafioHyperativa.API/appsettings.Development.json`e altere a sessão `SqliteConnection`informando o caminho do banco de dados a frete de `Data Source=`
 

     "ConnectionStrings": {
        "SqliteConnection": "Data Source=ENDERECO_BANCO_DE_DADOS"
      },
Após isso, no visual studio abra o *Package Manager Console*, localizado em `Tools > Nuget Package Manager`, altere o *Default project* para `DesafioHyperativa.Repository`e execute o comando `Update-Database.`

>Se optou por deixar utilizar o banco que já se encontra juntamente com o projeto, ignore as instruções informadas acima.

Certifique-se que esteja com o projeto para Startup seja o **`DesafioHyperativa.API`** e rode projeto, indo em `Debug > Start Debugging`.
Com isso se rá aberto o console do computador, onde será possível ver os logs em tempo de execução, e o navegador mostrando a documentação do Swagger.

## Endpoints
Os endpoints disponíveis são:

**Authorize**

Utilizado para gerar o token para os usuários cadastrados, lembrando que a aplicação já está incluso dois usuários pré-cadastrados.
Utiliza-se a rota abaixo para gerar um novo token de acesso;
> POST - /api/Authorize

Tendo que ser informado o e-mail e senha dos usuários.
Os dados dos usuários cadastrados são:

*Usuário 01*

    {
      "email": "01@user.com",
      "password": "Abcd1234"
    }


*Usuário 02*

    {
      "email": "02@user.com",
      "password": "Efgh5678"
    }
Podendo utilizar qualquer um deles para gerar o token.
Para consumir os demais endpoints é necessário inserir o token gerado no cabeçalho das requisições.

      -H 'Authorization: Bearer TOKEN_GERADO'


**Card**

Insere um cartão único, informando apenas o seu número.

> POST - /api/Card

Pesquisa a identificação única **`ID`** através do número completo do cartão.
> GET - /api/Card

**Lot**
Insere um lote de cartões por meio de um arquivo.
*OBS: O arquivo deve estar no padrão e não pode conter cartões duplicados.*
>POST - /api/Lot

## Considerações

O desafio foi concluído com excelência utilizando boas práticas de programação para manter o projeto com fácil compreensão e manutenibilidade.
