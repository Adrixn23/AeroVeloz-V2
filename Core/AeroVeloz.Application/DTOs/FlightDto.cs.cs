using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroVeloz.Domain.Entities.BaseEntity
{
    public abstract class BEntity<TiD>
    {
        public TiD? Id {get; protected set;}

    }
}
