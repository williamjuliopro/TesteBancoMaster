# TesteBancoMaster

Representa uma rota entre dois destinos, contendo as propriedades Origem, Destino e Valor (custo da rota).

Função EncontrarRotaMaisBarata: Realiza a busca recursiva (backtracking) para encontrar a rota com o menor custo.

A função percorre todas as rotas possíveis e, quando atinge o destino, verifica se o custo da rota encontrada é menor que o custo mínimo registrado.
Evita loops no caminho, garantindo que a mesma cidade não seja visitada duas vezes.
Função LerRotas: Lê as rotas a partir de um arquivo de texto. Cada linha do arquivo segue o formato "Origem,Destino,Valor" e é armazenada como uma instância da classe Rota.

## Explicando 
Uma viajem de **GRU** para **CDG** existem as seguintes rotas:

1. GRU - BRC - SCL - ORL - CDG ao custo de $40
2. GRU - ORL - CDG ao custo de $61
3. GRU - CDG ao custo de $75
4. GRU - SCL - ORL - CDG ao custo de $45

O melhor preço é da rota **1**, apesar de mais conexões, seu valor final é menor.
O resultado da consulta no programa deve ser: **GRU - BRC - SCL - ORL - CDG ao custo de $40**.

### Projeto 
- o sistema deverá conter 2 funcionalidades:
    - Registro de novas rotas. Essas novas rotas devem ser persistidas para futuras consultas
    - Consulta de melhor rota entre dois pontos.
    
- Implementação de testes unitários

- Exemplo:
  ```
  Digite a rota: GRU-CGD
  Melhor Rota: GRU - BRC - SCL - ORL - CDG ao custo de $40
  Digite a rota: BRC-SCL
  Melhor Rota: BRC - SCL ao custo de $5
  
#### Endpoints
- GET/Rotas?{Origem}&{Destino} 
- Post/Rotas?{Origem}&{Destino}&{Valor}
  
##### Técnologias utilizadas 
- Banco de dados: PostgreSQL 9.0.23
- Aplicação: .Net 9.0




