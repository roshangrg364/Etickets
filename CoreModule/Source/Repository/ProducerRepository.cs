using CoreModule.Base;
using CoreModule.DbContextConfig;
using CoreModule.Source.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Repository
{
    public class ProducerRepository:BaseRepository<Producer>,ProducerRepositoryInterface
    {
        public ProducerRepository(MyDbContext context):base(context)
        {

        }
    }
}
