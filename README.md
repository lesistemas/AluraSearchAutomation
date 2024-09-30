<!DOCTYPE html>
<html lang="pt-BR">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Documentação das Decisões Técnicas</title>
</head>
<body>
    <h1>Documentação das Decisões Técnicas</h1>
    
    <h2>1. Estrutura do Projeto</h2>
    <p>O projeto foi organizado em uma estrutura de camadas para manter a separação de responsabilidades e facilitar a manutenção do código. As camadas foram definidas da seguinte forma:</p>
    <ul>
        <li><strong>Apresentação</strong>: Responsável por orquestrar a aplicação e iniciar os serviços necessários.</li>
        <li><strong>RPA</strong>: Contém a lógica de automação de tarefas usando Selenium WebDriver.</li>
        <li><strong>Domínio</strong>: Define as entidades de negócio.</li>
        <li><strong>Infraestrutura</strong>: Camada de persistência de dados usando Dapper e acesso ao banco de dados MySQL.</li>
        <li><strong>Injeção de Dependência</strong>: Configuração central para DI.</li>
    </ul>

    <h2>2. Decisões Técnicas Tomadas</h2>

    <h3>2.1. Escolha do Selenium WebDriver</h3>
    <p>O Selenium WebDriver foi escolhido para automação de tarefas na web, pois:</p>
    <ul>
        <li>Oferece suporte a várias linguagens de programação e navegadores.</li>
        <li>Lida bem com páginas dinâmicas.</li>
    </ul>

    <h3>2.2. Uso do Dapper para Persistência de Dados</h3>
    <p>Dapper foi escolhido por ser leve e permitir controle direto sobre as consultas SQL, sendo mais adequado ao projeto.</p>

    <h3>2.3. Banco de Dados MySQL</h3>
    <p>MySQL foi escolhido por sua robustez e facilidade de integração com Dapper.</p>

    <h3>2.4. Injeção de Dependências</h3>
    <p>A DI permite que as dependências sejam facilmente substituídas e injetadas no código, facilitando a manutenção.</p>

    <h3>2.5. Git Flow</h3>
    <p>O Git Flow foi adotado para organização das branches, com desenvolvimento paralelo e controlado.</p>

    <h2>3. Fluxo da Aplicação</h2>
    <ol>
        <li><strong>Execução de Busca com Selenium</strong>: O Selenium realiza a automação e coleta dados.</li>
        <li><strong>Persistência com Dapper</strong>: Os dados coletados são persistidos no banco MySQL.</li>
        <li><strong>Tratamento de Erros</strong>: Erros são tratados para manter o fluxo de forma controlada.</li>
    </ol>

    <h2>4. Tratamento de Erros e Casos Inesperados</h2>
    <p>Tratamento robusto para falhas no Selenium e conexão com o MySQL.</p>

    <h2>5. Decisão de Não Usar Entity Framework</h2>
    <p>Dapper foi escolhido em vez de Entity Framework por ser mais leve e adequado ao controle direto de SQL.</p>

    <hr>
    <p>Essa documentação foi criada para descrever as decisões técnicas e fluxo de desenvolvimento do projeto.</p>
</body>
</html>
