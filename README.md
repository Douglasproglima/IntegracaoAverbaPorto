# Integração Segura Porto Seguro (Averba Porto)
----

<h1 align="center">

<img src="./logo.gif" width="300"/>
<br />

<p align="center">
  <a href="#como-usar">Como Usar</a> |
  <a href="#funcionalidades">Funcionalidades</a>
</p>

## Resumo
___
Este simples projeto tem como objetivo ajudar as pessoas que desejam enviar o arquivo de XML do CT-e para a seguradora porto seguro, uma vez que, o manual e todos os exemplos que a empresa disponibiliza não retratam de forma simples e clara como realizar tal integração com o webservices dos mesmos.

## Métodos:
---
GetCookiePortalSES(string user, string pass): Retorna o cookiePortalSe que é retornado a realizar um request com sucesso cujo o parâmetro mod = login.

Esse método é obrigatório, pois é atráves dele que obtermos a chave de acesso para ser usada no requisição do "mod = Upload";

SendXMLAverbaPorto(): Método responsável por enviar o XML do CT-e, o trecho mais importante:

> string file = @"CAMINHO/NOME_ARQUIVO.xml";
> byte[] arry = File.ReadAllBytes(file);
> requestUpload.AddFile("file", arry, "NOME_ARQUIVO.xml", "application/xml");

Observação: requestUpload.AddFile -> O último parâmetro define "application/xml" o tipo de arquivo que será recebido pelo webservice.

## Como usar
---
Faça o download do código fonte desse repositório.
Em seguida abra o formulário e no método SendXMLAverbaPorto(), informe o usuário e senha do ambiente de produção ou homologação. No mesmo método informe na variável file o caminho/nome_arquivo.xml onde encontrasse o arquivo, aqui você pode melhorar essa rotina adicionando o openDialog para tal, o meu objetivo aqui foi apenas criar métodos que seriam chamados por outras rotinas e etc...

No Visual Studio adicione break points nos métodos GetCookiePortalSES() e SendXMLAverbaPorto().

Observação: O Webservice da Porto Seguro define o ambiente de produção/homologação pelo usuário e senha fornecidas pelos mesmos.

## Informações Complementares
---
Sistema web da Porto Seguro ao qual é possível realizar o envio dos arquivos:
https://wws.averbeporto.com.br/websys/?comp=5

Documentação Horrível com chamadas do webservices da Averba Porto com os exemplos do POSTMAN: https://documenter.getpostman.com/view/207913/RWgozeRg#25d229c1-bb82-41d7-aa7d-3f33139838e7

Manual Oficial:
https://docs.google.com/document/d/1da005UzBF1Wzm8LmiB4JJnaXaLXtFKgl6S_rErMlXF8/edit

