# Fluxo de caixa: Projeto de exemplo
Este reposit�rio apresenta um projeto de exemplo que destaca habilidades t�cnicas de desenvolvimento de software.
Os detalhes incluem o desenho solu��o, boas pr�ticas, frameworks utilizados e instru��es para execu��o local do projeto.

## Descritivo do problema
Um comerciante precisa controlar o seu fluxo de caixa di�rio com os lan�amentos (d�bitos e cr�ditos), tamb�m precisa de um relat�rio que disponibilize o saldo di�rio consolidado.

### Requisitos de neg�cio
- Servi�o que fa�a o controle de lan�amentos
- Servi�o do consolidado di�rio

## Desenho da Solu��o
### Diagrama estrutural
A solu��o faz uso de arquitetura de portas e adaptadores e pad�es de serpara��o de responsabilidade em 3 camadas, contendo:
- <strong>FluxoDeCaixa.Api:</strong> Projeto respons�vel por expor camada de API, reprentando as portas de entrada da aplica��o. Por quest�es did�ticas essa camada tamb�m � respons�vel por resolver todas as dependencias necess�rias para inciar o sistema.
- <strong>FluxoDeCaixa.Domain:</strong> Dom�nio de neg�cio da solu��o, poss�i regras de negocio da aplcia��o desacopladas das tecnologias usadas para entrada e persistencia de dados.
- <strong>FluxoDeCaixa.Data:</strong> Projeto respons�vel por realizar interface com banco de dados. Conhece as regras de dom�nio mas � agn�stico das fontes de entrada da aplica��o.
- <strong>MongoDB:</strong> Para manter o estado tanto dos lan�amentos quanto das informa��es consolidadas foi usado o MongoDB principalmente para sua simplicidade em termos de carga computacional.
- <strong>FluxoDeCaixa.Queue:</strong> Projeto respons�vel por realizar interface sistema de mensageria e implementar padr�o pub/sub, representa adaptador da aplica��o.
- <strong>RabbitMq:</strong> Como sistema de mensageria foi usado o RabbitMQ. A mensageria foi empregada visando o desacoplamento e resiliencia no processamento de carga da aplica��o.

<img src="./docs/dependencias.jpg" />

### Diagrama comportamental

Internamente, a solu��o faz uso de estilo arquitetural CQRS para prover as funcionalidades principais da aplica��o. O estilo foi escolhido por empregar um desacoplamento entre a escrita e leitura de informa��es, sendo adequado aos requisitos funcionais do neg�cio, bem como requisitos n�o fucionais de resiliencia e disponibilidade.

<img src="./docs/fluxo.jpg" />

- <strong>Inserir lan�amento:</strong> Quando o usu�rio envia os dados para cadastro de um lan�amento, a informa��o � recepcionada pela API que faz as convers�es necess�rias e envia para o dom�nio que � respons�vel por validar a consist�ncia da informa��o. Em caso de erro o dom�nio notifica a API sobre o problema, do contr�rio, este encaminha a informa��o para ser persitida e tamb�m publica uma mensagem em uma fila. Um componente que assina essa fila, recebe a mensagem e a repassa para o reposit�rio de informa��es consolidadas que sicroniza a informa��o com a base de dados de leitura.
- <strong>Consultar relat�rio di�rio consolidado:</strong> Como a informa��o j� foi sinronizada, essa opera��o apenas l�, a partir do repos�rio, a ultima informa��o salva e a retorna para o usu�rio.

Como pode ser notado, a solu��o tras consit�ncia eventual entre o momento de cadastro e leitura do relat�rio, o sincronismo depende da capacidade computacional do ambiente por�m, permite maior disponibildiade das opera��es, visto que est�o desacopladas, principalmente, pelo sistema de mensageria RabbitMQ.

## Prepara��o do ambiente local
### Pre-requisitos
A aplica��o � multi-plataforma, por�m os exemplos abaixo contemplam a execu��o em uma est�o com sistema operacioal Windows.

Para rodar o sistema localmente as aplica��es abaixo precisam estar instaladas na esta��o de trabalho. 

1. [Git Client](https://gitforwindows.org/)
2. [.NET SDK 8](https://dotnet.microsoft.com/en-us/download)
3. [Docker Desktop](https://www.docker.com/products/docker-desktop/)

## Instru��es para execu��o do projeto localmente
Ap�s a instala��o dos pre-requisitos, siga estas etapas para executar o projeto em sua m�quina local:

1. Clonar o Reposit�rio:
Abra o terminal no diret�rio de sua escolha e execute o seguinte comando para clonar o reposit�rio do GitHub:
```bash
  git clone https://github.com/jrochamartins/fluxo-de-caixa.git
```

2. Iniciar as dependencias do projeto:
- Navegue at� a pasta scripts e execute o arquivo em lotes ```start.bat```

<em>Obs: Mantenha esse terminal rodando at� o terminar o teste local.</em>

3. Iniciar a aplica��o:
- Volte a pasta raiz da aplica��o.
- Abra a aplica��o diretamente no Visual Studio 2022
- Ou ent�o execute, via linha de comando, as instru��es abaixo:
```shell
  dotnet restore
  dotnet build
  dotnet run --project .\src\FluxoDeCaixa.Api\FluxoDeCaixa.Api.csproj
```

4. Acessar a interface swagger:
* Ap�s executar o projeto, abra um navegador da web e acesse a URL: https://localhost:5001/swagger/index.html

5. Autenticando na API
* A API oferece um endpoint POST /Login onde � poss�vel gerar o token para ser trafegado no Header das demais requisi��es
* Para fins did�ticos, basta usar o texto ```admin``` para receber o token

<img src="./docs/login.png" />

* Com o token copiado, clique no bot�o <strong>Autorize</strong> no topo da tela e cole o token no campo de texto

<img src="./docs/post-header.png" /> 

### Servi�os dispon�veis
#### [POST] - Autenticar usu�rio: https://localhost:5001/Login
```json
{
	"user": "admin"
}
```
| Campo | Obrigat�rio | Descri��o |
| ------ | ------ | ------ |
| user | sim | Usu�rio que est� se autenticando na aplica��o, usar padr�o ```admin``` |


#### [POST] - Adicionar entrada: https://localhost:5001/Entries
```json
{
	"date": "2024-01-02T23:00:00.000Z",
	"entryType": 1,
	"description": "Decri��o",
	"value": 50
}
```
| Campo | Obrigat�rio | Descri��o |
| ------ | ------ | ------ |
| date | n�o | Data do lan�amento, se n�o for informado ser� consider�do a data e hora atuai. |
| entryType | sim | Tipo de lan�amento. 1 para c�dito, 2 para d�bito. |
| description | sim | Descri��o do lan�amento. |
| value | sim | Valor do lan�amento. |

#### [GET] - Relat�rio consolidado di�rio: https://localhost:5001/Balance?Day=day&Month=month&Year=year
| Campo | Obrigat�rio | Descri��o |
| ------ | ------ | ------ |
| day | sim | Dia do relat�rio.  |
| month | sim | M�s do relat�rio. |
| year | sim | Ano do relat�rio. |

* Resultado
```json
{
	"date": "2024-01-02",
	"credits": 150,
	"debts": 0,
	"value": 150
}
```

## Solu��o de problemas

Para solu��o de problemas � poss�vel consultar os logs da aplica��o diretamente no terminal ou tamb�m em arquivo de logs indexados por dia na pasta ```\src\FluxoDeCaixa.Api\Logs```

* Console
<img src="./docs/logs-console.png" /> 

* Arquivos de log
<img src="./docs/logs-file.png" /> 