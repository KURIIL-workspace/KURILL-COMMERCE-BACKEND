using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCommerceAPI.Models
{
    public class UpdateStatusModel
    {
        public short Status { get; set; }

        public Guid? EmployeeId { get; set; }

        public DateTime PerformedDate { get; set; }

        public string Description { get; set; }
    }

    public class UpdateStockReadingIdsStatusModel : UpdateStatusModel
    {
        public List<Guid> StockReadingIds { get; set; }
    }
}
