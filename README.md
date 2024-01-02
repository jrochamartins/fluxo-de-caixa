# Fluxo de caixa: Projeto de exemplo
Este reposit�rio apresenta um projeto de exemplo que destaca habilidades t�cnicas de desenvolvimento de software.
Os detalhes incluem o desenho solu��o, boas pr�ticas, frameworks utilizados e instru��es para execu��o local do projeto.

## Descritivo do problema
Um comerciante precisa controlar o seu fluxo de caixa di�rio com os lan�amentos (d�bitos e cr�ditos), tamb�m precisa de um relat�rio que disponibilize o saldo di�rio consolidado.

### Requisitos de neg�cio
- Servi�o que fa�a o controle de lan�amentos
- Servi�o do consolidado di�rio

## Desenho da Solu��o
### Diagrama de dependencias
A solu��o faz uso de arquitetura de portas e adaptadores e pad�es de serpara��o de responsabilidade em 3 camadas, contendo:
- <strong>FluxoDeCaixa.Domain:</strong> Dom�nio de neg�cio da solu��o, poss�i regras de negocio da aplcia��o desacopladas das tecnologias usadas para entrada e persistencia de dados.
- <strong>FluxoDeCaixa.Data:</strong> Projeto respons�vel por realizar interface com banco de dados. Conhece as regras de dom�nio mas � agn�stico das fontes de entrada da aplica��o.
- <strong>FluxoDeCaixa.Queue:</strong> Projeto respons�vel por realizar interface sistema de mensageria e implementar padr�o pub/sub, representa adaptador de sa�da da aplica��o.
- <strong>FluxoDeCaixa.Api:</strong> Projeto respons�vel por expor camada de API, reprentando as portas de entrada da aplica��o. Por quest�es did�ticas essa camada tamb�m � respons�vel por resolver todas as dependencias necess�rias para inciar o sistema.

<img src="./docs/dependencias.jpg" width="400px"/>

### Diagrama de fluxo de dados
<img src="./docs/fluxo.jpg" width="400px"/>