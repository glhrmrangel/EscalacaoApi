# EscalacaoApi

### O projeto está dividido em:
  + Controllers
      * onde estão centralizadas as chamadas da API, sendo JogadorController, TimeController e TreinadorController;
  + Data
      * onde estão os DTOs e o Context da aplicação;
  + Models
      * onde estão definidos os modelos de Jogador, Time e Treinador com seus campos;
  + Profiles
      * onde estão centralizados os mapeamentos;
  + Services
      * onde está centralizada a lógica que foi separada do Controller.

#### O objetivo do código é permitir a inserção e atualização de dados em um banco SQL Server.
Acompanhando o projeto estão as Migrations para criação das tabelas e Collection para testagem via Postman caso desejado.
Pode ser necessária a atualização da ConnectionString no appsettings.json com o nome do Server que será utilizado.

O Swagger também contém explicação de cada chamada, bem como dos campos necessários e opcionais.

O fluxo de testagem envolve a criação de um `Time`, e então a criação de um `Jogador` e `Treinador`, que independem um do outro. Estão implementados comportamentos básicos de Create, Read, Update e Delete. O armazenamento de dados é feito tendo em vista o SQL Server.
