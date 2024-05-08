using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneCompanySubscribers.Model.Save
{
    public interface ISaveFile
    {
        public void SaveFile(AbonentsTable abonentsTable);
    }
}
