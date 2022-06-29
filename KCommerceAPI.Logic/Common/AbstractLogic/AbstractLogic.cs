using KCommerceAPI.DataAccess.EfCore;
using Microsoft.EntityFrameworkCore;
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

        public async Task<string> GetNextDocumentCode(DocumentType documentType, int recordCount)
        {
            var documentRef = await kComDbContext.DocumentRefs.Where(x => x.Type == documentType.ToString()).FirstOrDefaultAsync();
            if (documentRef == null) return null;

            var code = documentRef.Format;

            int codeLength = 0;

            if (int.TryParse(code.Substring(code.Length - 1), out codeLength))
                code = code.Substring(0, code.Length - 2);

            code = code.Replace("@@YEAR@@", DateTime.Now.Year.ToString());
            code = code.Replace("@@MONTH@@", DateTime.Now.Month.ToString("D2"));
            code = code.Replace("@@DAY@@", DateTime.Now.Day.ToString("D2"));
            code = code.Replace("@@NO@@", (recordCount + 1).ToString("D" + codeLength));

            return code;
        }
    }
}
