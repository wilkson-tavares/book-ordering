# BookOrdering

Serviço em .NET 10 que ordena uma coleção de livros por um ou mais critérios,
configuráveis por arquivo, sem qualquer alteração de código. Solução da **Avaliação
Técnica FGV - TIC**.

## Sobre o projeto

O caso de uso define um único ator, o CSO (Cliente do Serviço de Ordenação), que envia
um conjunto de livros — cada um descrito por `Title`, `AuthorName` e `EditionYear` — e
recebe de volta o mesmo conjunto ordenado.

O requisito central é que o serviço suporte **um ou mais atributos de ordenação, cada um
com sua própria direção (ascendente ou descendente)**, definidos em um arquivo de
configuração — trocar os critérios não exige nenhuma alteração de código.

Por instrução explícita da FGV, o escopo **não inclui** persistência de dados, interface
visual, nem exposição como WebService/API: o projeto é um componente/serviço,
demonstrado através de uma aplicação de console.

## Arquitetura

O projeto segue Clean Architecture, com as dependências sempre apontando para dentro (em
direção ao Domain):

```
Infrastructure  --->  Application  --->  Domain
      |                                     ^
      +------------- ConsoleApp ------------+
```

| Camada | Projeto | Responsabilidade |
|---|---|---|
| Domain | `BookOrdering.Domain` | Regras e contratos centrais: `Book`, `IBookOrderingService`, `SortCriterion`, `BookSortAttribute`, `SortDirection`, `BookOrderingException`. Não depende de nenhuma outra camada. |
| Application | `BookOrdering.Application` | Caso de uso `BookOrderingService`, que combina os critérios (obtidos via a porta `ISortCriteriaProvider`) com as regras de comparação. |
| Infrastructure | `BookOrdering.Infrastructure` | Adaptador `ConfigurationSortCriteriaProvider`, que implementa `ISortCriteriaProvider` lendo de um arquivo de configuração. |
| Apresentação | `BookOrdering.ConsoleApp` | Composition root: liga as camadas via injeção de dependência manual e demonstra o fluxo completo. |
| Testes | `BookOrdering.Tests` | Suíte xUnit que cobre os cenários do gabarito de teste da FGV. |

Para o racional completo das decisões de projeto e o diagrama de classes UML, veja
[docs/design.md](docs/design.md).

## Estrutura do repositório

```
book-ordering/
├── docs/
│   ├── design.md                # documento de projeto completo
│   └── uml-class-diagram.png    # diagrama de classes UML
└── src/
    ├── BookOrdering.slnx
    ├── BookOrdering.Domain/
    ├── BookOrdering.Application/
    ├── BookOrdering.Infrastructure/
    ├── BookOrdering.ConsoleApp/
    └── BookOrdering.Tests/
```

## Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

## Como executar

A partir da pasta `src/`:

```bash
dotnet build BookOrdering.slnx                  # compila os 5 projetos
dotnet test BookOrdering.slnx                   # roda toda a suíte de testes
dotnet run --project BookOrdering.ConsoleApp    # demonstra o fluxo completo via console
```

O `ConsoleApp` imprime os critérios de ordenação lidos de `appsettings.json`, a lista de
livros de exemplo já ordenada, e uma demonstração da `BookOrderingException` lançada ao
tentar ordenar uma coleção nula.

## Configuração dos critérios de ordenação

Os critérios são declarados em `src/BookOrdering.ConsoleApp/appsettings.json`:

```json
{
  "BookOrdering": {
    "SortCriteria": [
      { "Attribute": "Title", "Direction": "Ascending" },
      { "Attribute": "AuthorName", "Direction": "Ascending" }
    ]
  }
}
```

Trocar atributos ou direções, ou adicionar um critério extra de desempate, é só editar
esse JSON — nenhuma linha de código precisa mudar.

## Testes

`BookOrdering.Tests` (xUnit) implementa, um a um, os cenários de
`fgv-avaliacao-caso-de-teste.pdf`, além de cobrir as validações do value object `Book` e o
binding real de `appsettings.json` para `SortCriterion`. Detalhes de cada caso de teste
estão na seção "Estratégia de testes" de [docs/design.md](docs/design.md).

## Documentação adicional

- [docs/design.md](docs/design.md) — documento de projeto completo: visão geral,
  arquitetura, implementação da ordenação, decisões de projeto e justificativas.
- [docs/uml-class-diagram.png](docs/uml-class-diagram.png) — diagrama de classes UML.
