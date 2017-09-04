using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsSale.DataAccess.DTO
{
    public abstract class BaseDto<TEntity>
    {
        private TEntity _entity;

        public TEntity Entity
        {
            get => _entity;
            private set => _entity = value;
        }

        public BaseDto(TEntity entity)
        {
            Entity = entity;
        }
    }
}
