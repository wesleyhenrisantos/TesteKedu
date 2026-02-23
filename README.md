# KeduPayments - Clean Architecture (.NET 10)

Estrutura Clean Architecture/Clean Code baseada no desafio (Responsável Financeiro, Centro de Custo, Plano de Pagamento, Cobrança, Pagamento).

## Projetos
- **KeduPayments.Application**: DTOs, Mapping, Interfaces e Serviços.
- **KeduPayments.Domain**: regras, entidades e enums.
- **KeduPayments.Infrastructure**: EF Core + PostgreSQL, repositórios, .
- **KeduPayments**: Web API + Swagger.

## Requisitos
- .NET SDK 10 
- PostgreSQL 


### Migração + Seed

Ajustar o ConnectionStrings para ter seu banco conectado no appsettings.json

Endpoint:

POST /planos-de-pagamento
Request body
	{
	  "responsavelId": 0,
	  "centroCustoId": 0,
	  "cobrancas": [
		{
		  "valor": 0.1,
		  "dataVencimento": "2026-02-23T13:00:15.910Z",
		  "metodoPagamento": 0
		}
	  ]
	}
	
POST /cobrancas/{id}/pagamentos 
Query params:
- `status` (string)
- `valor` (decimal)

GET /planos-de-pagamento/{id}

GET /planos-de-pagamento/{id}/total


POST /responsaveis

GET /responsaveis/{id}/planos-de-pagamento

GET /responsaveis/{id}/cobrancas

GET /responsaveis/{id}/cobrancas/quantidade


POST /centros-de-custo 
Request body
{
  "natureza": "string"
}

GET /centros-de-custo

## Filtros de cobrança
GET /responsaveis/{responsavelId}/cobrancas

Query params:
- `status` (1=Emitida, 2=Paga, 3=Cancelada)
- `metodoPagamento` (1=Boleto, 2=Pix)
- `vencidasSomente` (true/false)
- `vencimentoDe` (YYYY-MM-DD)
- `vencimentoAte` (YYYY-MM-DD)

