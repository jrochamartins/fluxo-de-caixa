# Fluxo de caixa: Projeto de exemplo
Este repositório apresenta um projeto de exemplo que destaca habilidades técnicas de desenvolvimento de software.
Os detalhes incluem o desenho solução, boas práticas, frameworks utilizados e instruções para execução local do projeto.

## Descritivo do problema
Um comerciante precisa controlar o seu fluxo de caixa diário com os lançamentos (débitos e créditos), também precisa de um relatório que disponibilize o saldo diário consolidado.

### Requisitos de negócio
- Serviço que faça o controle de lançamentos
- Serviço do consolidado diário

## Desenho da Solução
### Diagrama de dependencias
A solução faz uso de arquitetura de portas e adaptadores e padões de serparação de responsabilidade em 3 camadas, contendo:
- <strong>FluxoDeCaixa.Domain:</strong> Domínio de negócio da solução, possúi regras de negocio da aplciação desacopladas das tecnologias usadas para entrada e persistencia de dados.
- <strong>FluxoDeCaixa.Data:</strong> Projeto responsável por realizar interface com banco de dados. Conhece as regras de domínio mas é agnóstico das fontes de entrada da aplicação.
- <strong>FluxoDeCaixa.Queue:</strong> Projeto responsável por realizar interface sistema de mensageria e implementar padrão pub/sub, representa adaptador de saída da aplicação.
- <strong>FluxoDeCaixa.Api:</strong> Projeto responsável por expor camada de API, reprentando as portas de entrada da aplicação. Por questões didáticas essa camada também é responsável por resolver todas as dependencias necessárias para inciar o sistema.

<img src="./docs/dependencias.jpg" width="400px"/>

### Diagrama de fluxo de dados
<img src="./docs/fluxo.jpg" width="400px"/>