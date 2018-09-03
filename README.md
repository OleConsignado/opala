![Opala](https://github.com/OleConsignado/opala/blob/master/opala.png) Opala: Arquitetura Hexagonal
=========
Opala é um modelo de serviço que irá ajudá-lo a criar aplicativos mais adaptáveis e de fácil manutenção utilizando como base a arquitetura hexagonal.

![Hexagonal Architecture](https://github.com/OleConsignado/otc-projectmodel/blob/master/hexagonal.png)

Arquitetura hexagonal, consiste em dividir uma aplicação em camadas de acordo com suas responsabilidades e enfatizar uma camada em especial, onde ficará a lógica principal da aplicação, a camada de domínio ou domain (do termo original).

O objetivo da arquitetura hexagonal é encapsular a lógica, de maneira que nada externo acesse-a diretamente, então, o meio de um usuário acessar uma informação gerada pela camada de domínio é através de um serviço. Ou seja, externamente, conheceremos apenas a camada de serviço, o objetivo e não expor publicamente nenhuma informação sequer diretamente da camada de domínio

## Projeto Opala
A ideia deste projeto é ter uma base para a criação de Apis seguindo um modelo maduro e de fácil adaptação.

A estrutura do projeto está dividida em 03 (três) camadas, sendo elas: Core, Infra e Presenter.

### Core
Camada responsável por toda a regra de negócio. Nela estão contidos os projetos de:

#### Domain: 
 > Projeto na qual são trabalhados os modelos de negócio além das interfaces de Serviços, Repositórios, Adaptadores. No projeto de domínio criamos também as classes responsável por gerenciar nossas exceções.

#### Application:
 > Projeto responsável por trabalhar todas nossas regras de negócio. Nele realizamos a implementação da interface IService na qual orquestramos nossos modelos, interfaces de repositório e adaptadores.
 > Neste projeto também encontra-se uma pasta chamada "Microsoft.Extensions.DependencyInjection" e uma classe com o sufixo "ServiceCollectionExtensions". Esta classe é respnsável por realizar o registro das dependências do projeto.

#### Test: 
 > Projeto responsável por manter a integridade das regras de negócio do de nosso domínio através de testes de unidade.
 
### Infra
Camada responsável por fornecer acesso aos dados hospedados dentro dos limites do domínio. Nela está a implementação real das interfaces de repositório providas pelo domínio. Nela encontramos também implementação para envio de email, logs e qualquer comunicação com apis ou componentes de terceiro através de adaptadores - Adapters.
Nesta camada, temos os projetos de EmailAdapter, NotificationAdapter e Repository como exemplos.

