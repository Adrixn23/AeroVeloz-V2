using System;
using AeroVeloz.Domain.Entities.BaseEntity; 

namespace AeroVeloz.Domain.Entities.Flights;

public abstract class BEntity<TiD>
{
    public TiD Id { get; protected set; }


}


