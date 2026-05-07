namespace AeroVeloz.Domain.Entities.BaseEntity
{
    /// <summary>
    /// Clase base abstracta para todas las entidades del dominio.
    /// Proporciona un identificador genérico que permite definir el tipo de clave primaria
    /// de cada entidad concreta (Guid, int, short, byte, etc.).
    /// </summary>    
    public abstract class BEntity <TiD>
    {
        
        /// Identificador único de la entidad. Es de solo lectura tras la inicialización (init-only).
      
        public TiD? Id { get; init; }

    }
}
