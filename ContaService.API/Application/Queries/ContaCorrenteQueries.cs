using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace  ContaService.API.Application.Queries
{
    public class ContaCorrenteQueries : IContaCorrenteQueries
    {
        private string _connectionString = string.Empty;
        
        public ContaCorrenteQueries(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr 
            : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<IEnumerable<ContaLancamento>> GetLancamentosAsync(string contaNumero)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                return await connection.QueryAsync<ContaLancamento>(
                   @"select lc.DataLancamento as data, lc.Valor as valor
                        FROM ContaCorrente cc
                        LEFT JOIN Lancamento lc ON cc.Id = lc.contaCorrenteId 
                        WHERE cc.numero=@contaNumero"
                        , new { contaNumero }
                    );
            }
        }

        public async Task<ContaSaldo> GetSaldoAsync(string contaNumero)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                return await connection.QueryFirstOrDefaultAsync<ContaSaldo>(
                   @"select cc.saldo 
                        FROM ContaCorrente cc
                        WHERE cc.numero=@contaNumero"
                        , new { contaNumero }
                    );
            }
        }
    }
}