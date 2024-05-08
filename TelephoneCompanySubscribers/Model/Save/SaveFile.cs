using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneCompanySubscribers.Model.Save
{
    public class SaveFile
    {
        private ISaveFile saveStrategy;

        public SaveFile(ISaveFile strategy)
        {
            saveStrategy = strategy;
        }

        public void SetStrategy(ISaveFile strategy)
        {
            saveStrategy = strategy;
        }

        public void Save(AbonentsTable abonentsTable)
        {
            saveStrategy.SaveFile(abonentsTable);
        }
    }
}
