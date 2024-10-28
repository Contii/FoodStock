# FoodStock

FoodStock é um sistema de gerenciamento de inventário doméstico, projetado para otimizar a organização de produtos, listas de compras e o preparo de refeições. Desenvolvido como um conjunto de microsserviços, o sistema oferece uma interface interativa tanto para web quanto para dispositivos Android.

Este documento tem como objetivo ilustrar o escopo geral do sistema e suas principais funcionalidades, bem como as tecnologias que serão utilizadas no desenvolvimento do mesmo.

---

## Objetivo do sistema

- **Organização**: Facilitar a gestão de alimentos e produtos de uso doméstico, permitindo um controle preciso dos itens em estoque.
- **Planejamento**: Auxiliar na criação de listas de compras e na elaboração de cardápios personalizados, com base nos ingredientes disponíveis.
- **Redução de desperdício**: Minimizar o descarte de alimentos, através de alertas sobre produtos próximos do vencimento e sugestões de receitas.

---

## Funcionalidades Principais

- **CRUD completo**: Cadastro, leitura, atualização e exclusão de itens, com informações detalhadas como nome, quantidade, data de validade e categoria.
- **Interface**: Acesso fácil e rápido às informações, tanto para o controle manual do banco de dados quanto para a utilização cotidiana do aplicativo, através de uma interface web e mobile responsiva.
- **Leitor QR**: Faça o upload de seus novos produtos diretamente pelo QR code de suas notas fiscais.
- **Notificações**: Alertas personalizados para produtos em falta ou próximos do vencimento, enviados diretamente para o dispositivo do usuário.
- **Sugestões de receitas**: Criação de receitas personalizadas, com base nos ingredientes disponíveis no estoque, promovendo a utilização de todos os alimentos.
- **Sugestões de listas de compras**: Criação de listas personalizadas, com base nos produtos faltantes e nas intenções de gastos do usuário.
- **Monitoramento de consumo**: Registro do consumo diário de cada item do estoque.

---

## Tecnologias

- **Microsserviços**: Arquitetura modular que permite a escalabilidade e a manutenção independente de cada componente do sistema.
- **Tecnologias utilizadas**:
  - DOT.NET EFCore 8.0 
  - Blazor
  - Docker

---

## Público-alvo

- **Donas de casa**: Facilitando o planejamento de refeições e a organização de alimentos.
- **Estudantes**: Auxiliando na gestão do orçamento e na alimentação saudável.
- **Famílias**: Facilitando a visualização e organização de produtos de maneira geral.
- ~**Pessoas com restrições alimentares**: Permitindo a criação de cardápios personalizados e a identificação de ingredientes alérgenos.~

---

## Conclusão

O FoodStock tem o potencial de revolucionar a forma como as pessoas gerenciam seus alimentos em casa. Com uma interface intuitiva e funcionalidades inovadoras, o sistema oferece uma solução para quem busca praticidade, organização e economia.