using KeduPayments.Application.DTOs;


namespace KeduPayments.Application.Interfaces
{
    // Interface para o serviço de Responsável Financeiro, definindo os métodos para operações CRUD.
    public interface IResponsavelFinanceiroService
    {

        /// <summary>
        /// Método para adicionar um novo Responsável Financeiro. Recebe um objeto ResponsavelFinanceiroDTO e retorna o objeto criado com seu ID gerado.
        /// </summary>
        /// <param name="responsavelFinanceiroDTO">Objeto ResponsavelFinanceiro a ser adicionado.</param>
        /// <returns></returns>
        Task<ResponsavelFinanceiroResponse> Add(ResponsavelFinanceiroRequest responsavelFinanceiroDTO);

        /// <summary>
        /// Metodo para atualizar um Responsável Financeiro existente. Recebe um objeto ResponsavelFinanceiroRequest  com as informações atualizadas e retorna uma tarefa assíncrona que representa a operação de atualização.
        /// </summary>
        /// <param name="responsavelFinanceiroDTO">Objeto ResponsavelFinanceiro com as informações atualizadas.</param>
        /// <returns></returns>
        Task Update(ResponsavelFinanceiroRequest responsavelFinanceiroDTO);
        /// <summary>
        /// Metodo para deletar um Responsável Financeiro pelo seu ID.
        /// </summary>
        /// <param name="id">ID do Responsável Financeiro a ser deletado.</param>
        /// <returns></returns>
        Task Delete(int id);

        /// <summary>
        /// Método para obter um Responsável Financeiro específico pelo seu ID.
        /// </summary>
        /// <param name="id">ID do Responsável Financeiro a ser obtido.</param>
        /// <returns></returns>
        Task<ResponsavelFinanceiroResponse> GetById(int id);

        /// <summary>
        /// Método para obter todos os Responsáveis Financeiros. Retorna uma coleção de objetos ResponsavelFinanceiroResponse.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ResponsavelFinanceiroResponse>> GetResponsaveisFinanceiros();
    }
}
