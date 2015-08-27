using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDataAccessAdapter.Entities
{
    [Serializable]
    public abstract class BaseEntity
    {
        protected BaseEntity()
        {
        }

        protected BaseEntity(BaseEntity entity)
        {
            Id = entity.Id;
            CreatedOn = entity.CreatedOn;
            ModifiedOn = entity.ModifiedOn;
            IsDeleted = entity.IsDeleted;
        }

        public long Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public void SetIdIfNot()
        {
            if (Id == 0)
            {
                IdGenerator generator = null;
                var generatorInstance = GetIdGenerator(generator);
                this.Id = generatorInstance.CreateId();
            }
        }

        private static IdGenerator GetIdGenerator(IdGenerator generator)
        {
            if (generator == null)
            {
                // Let's say we take april 1st 2015 as our epoch
                var epoch = new DateTime(2015, 4, 1, 0, 0, 0, DateTimeKind.Utc);
                // Create a mask configuration of 45 bits for timestamp, 2 for generator-id 
                // and 16 for sequence
                var mc = new MaskConfig(45, 2, 16);
                // Create an IdGenerator with it's generator-id set to 0, our custom epoch 
                // and mask configuration
                generator = new IdGenerator(0, epoch, mc);
            }

            return generator;
        }
    }
}
