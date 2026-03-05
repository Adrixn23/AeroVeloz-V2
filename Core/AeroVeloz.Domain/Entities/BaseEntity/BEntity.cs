namespace AeroVeloz.Domain.Entities.BaseEntity
{
    public abstract class BEntity <TiD>
    {
        public TiD? Id { get;  protected set; }
    
    }
}
