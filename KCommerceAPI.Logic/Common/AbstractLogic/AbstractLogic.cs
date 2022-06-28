using KCommerceAPI.DataAccess.EfCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCommerceAPI.Logic.Common.AbstractLogic
{
    public class AbstractLogic
    {
        protected readonly KComDbContext kComDbContext;

        public AbstractLogic(KComDbContext kComDbContext)
        {
            this.kComDbContext = kComDbContext;
        }
    }
}
