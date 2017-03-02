using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopClient.Interfaces;

namespace DesktopClient.Services
{
    public class ServiceLocator
    {
        private static ServiceLocator _instance;
        private static IDataService _dataService;

        public static ServiceLocator Instance
        {
            get
            {
                if (_instance == null)
                {
                    Initialize();
                }
                return _instance;
            }
        }

        private static void Initialize()
        {
            _instance = new ServiceLocator();
            _dataService = new DataService();
        }

        public IDataService GetDataService()
        {
            return _dataService;
        }
    }
}
