namespace Catalogo.Domain.Entities
{
    /// <summary>
    /// Classe abstrata base para todas as entidades.
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// Identificador único da entidade.
        /// </summary>
        public int Id { get; protected set; }
    }
}
