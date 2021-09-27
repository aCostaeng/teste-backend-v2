# Teste Backend V2

![Aiko](img/aiko.png)

Neste teste serão avaliados seus conhecimentos em .NET, C# e Banco de dados e a metodologia aplicada no desenvolvimento, arquitetura e organização da aplicação final.

## O Desafio

Você é o desenvolvedor backend de uma empresa que coleta dados de equipamentos utilizados em uma operação florestal. Dentre esses dados estão o histórico de posições e estados desses equipamentos. O estado de um equipamento é utilizado para saber o que o equipamento estava fazendo em um determinado momento, seja *Operando*, *Parado* ou em *Manutenção*. O estado é alterado de acordo com o uso do equipamento na operação, já a posição do equipamento é coletada através do GPS e é enviada e armazenada de tempo em tempo pela aplicação.

Seu objetivo é, de posse desses dados, desenvolver uma aplicação backend que exponha esses dados através de uma API e uma página razor que trate e exibida essas informações para os gestores da operação.

## Requisitos

Esses requisitos são obrigatórios e devem ser desenvolvidos para a entrega do teste.

### Requisitos técnicos

* **.NET CORE ou .NET 5**: A aplicação deve ser desenvolvida utilizando o framework .NET na versão CORE ou 5.

* **ORM**: Como interface com o banco de dados você deve utilizar um ORM (Entity Framework, NHibernate, etc).

* **PostgreSQL**: Para o banco de dados deve ser utilizado o PostgreSQL.

* **Desenvolver a aplicação com base em um arquitetura**: A aplicação deve ser desenvolvida seguindo os padrões de uma arquitetura, seja Clean, Onion, DDD, MVC ou outra que achar que se encaixa no desafio proposto.

### Requisitos de négocio

* **API de CRUD**: Você deve desenvolver uma API que exponha os métodos de Criar, Excluir, Editar e Ler para as seguintes entidades:
  * Equipamento.
  * Estado de equipamento.
  * Modelo de Equipamento.
  * Ganhos por hora por estado.
  * Histórico de posições de um equipamento.
  * Histórico de estados de um equipamento.

* **Estado atual do equipamento**: Endpoint na API que deve retornar o estado mais recente dos equipamentos.

* **Posição atual por equipamento**: Endpoint na API que deve retornar a posição mais recente dos equipamentos.

* **Página Histórico de estados**: Página símples em **Razor** que liste o histórico de estados por equipamento.

## Como restaurar o banco de dados

O dump do banco de dados necessário para realizar o teste está na pasta `/data`.

Exemplo de como restaurar o banco utilizando o `pg_restore`:
```sh
pg_restore -U [user] -d [dbname] -1 data.backup
```

## Dados 

![Diagrama](img/diagram.png)

### equipment
Contém todos os equipamentos da aplicação.

* **id**: Identificador único do equipamento.
* **equipment_model_id**: Chave estrangeira, utilizada para referenciar de qual modelo é esse equipamento.
* **name**: Nome do equipamento.

### equipment_state
Contém todos os estados dos equipamentos.

* **id**: Identificador único do estado de equipamento
* **name**: Nome do estado.
* **color**: Cor utilizada para representar o estado.

### equipment_model
Contém todos os modelos de equipamentos.

* **id**: Identificador único do modelo de equipamento.
* **name**: Nome do modelo de equipamento.

### equipment_model_state_hourly_earnings
Informação de qual é o valor por hora do modelo de equipamento em cada um dos estados.

* **equipment_model_id**: Chave estrangeira, utilizada para referenciar de qual modelo é esse valor.
* **equipment_state_id**: Chave estrangeira, utilizada para referenciar de qual valor é esse estado.
* **value**: Valor gerado por hora nesse estado.

### equipment_state_history
O histórico de estados por equipamento.

* **equipment_id**: Chave estrangeira, utilizada para referenciar de qual equipamento é esse estado.
* **date**: Data em que o equipamento declarou estar nesse estado.
* **equipment_state_id**: Chave estrangeira, utilizada para referenciar qual é o estado que o equipamento estava nesse momento.

### equipment_position_history
O histórico de posições dos equipamentos.

* **equipment_id**: Chave estrangeira, utilizada para referenciar de qual equipamento é essa posição.
* **date**: Data em que a posição foi registrada.
* **lat**: Latitude em WGS84.
* **lon**: Longitude em WGS84.


## O que é permitido

* Qualquer tecnologia complementar as citadas nos "Requisitos técnicos" são permitidas desde que seu uso seja justificável.

## O que não é permitido

* Utilizar códigos de terceiros que implementem algum dos requisitos.

## Extras

Aqui são listados algumas sugestões para você que quer ir além do desafio inicial. Lembrando que você não precisa se limitar a essas sugestões, se tiver pensado em outra funcionalidade que considera relevante ao escopo da aplicação fique à vontade para implementá-la.

* **Swagger**: Utilizar o swagger para documentar a API.

* **CRUD em Razor**: Implementar o CRUD das entidades em páginas Razor.

* **Conteinerização**: Utilizar o docker para realizar a conteinerização da aplicação.

* **Mapa de Equipamentos**: Exibir uma página em Razor com os equipamentos no Mapa utilizando a biblioteca javascript Leaflet.

* **Histórico de estados**: Exibir uma página em Razor que liste o histórico de estados de um equipamento.

* **Percentual de Produtividade do equipamento**: Expor um endpoint na API que retorne a produtividade do equipamento, que consiste em uma relação das horas produtivas (em estado "Operando") em relação ao total de horas. Exemplo se um equipamento teve 18 horas operando no dia a formula deve ser `18 / 24 * 100 = 75% de produtividade`.

* **Ganho por equipamento**: Export um endpoint na API que retorne o calculo de ganho do equipamento com base no valor recebido por hora informado no Modelo de Equipamento. Exemplo se um modelo de equipamento gera 100 por hora em operando e -20 em manutenção, então se esse equipamento ficou 10 horas em operação e 4 em manutenção ele gerou `10 * 100 + 4 * -20 = 920`.

* **Testes**: Desenvolva testes que achar necessário para a aplicação, seja testes unitários, testes automatizados, etc.

* **Documentação**: Gerar uma documentação da aplicação. A documentação pode incluir detalhes sobre as decisões tomadas, especificação dos componentes desenvolvidos, instruções de uso dentre outras informações que achar relevantes.

## Entregas

Para realizar a entrega do teste você deve:

* Relizar o fork e clonar esse repositório para sua máquina.
  
* Criar uma branch com o nome de `teste/[NOME]`.
  * `[NOME]`: Seu nome.
  * Exemplos: `teste/fulano-da-silva`; `teste/beltrano-primeiro-gomes`.
  
* Faça um commit da sua branch com a implementação do teste.
  
* Realize o pull request da sua branch nesse repositório.
