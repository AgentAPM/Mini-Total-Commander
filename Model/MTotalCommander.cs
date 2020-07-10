using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Total_Commander.Model
{
    class MTotalCommander
    {
        #region Pola prywatne
        private List<Services.ListService> services;
        #endregion
        #region Konstruktory
        public MTotalCommander()
        {
            services = new List<Services.ListService>();
        }
        #endregion
        #region Własności publiczne
        public void AddService(Services.ListService S)
        {
            services.Add(S);
        }
        #endregion
    }
}
