using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace CrmLibrary
{
    public abstract class BaseHelper
    {
        private IOrganizationService _service;
        public IOrganizationService Service
        {
            get
            {
                _service = CrmServiceHelper.Instance.GetService();
                return _service;
            }
            set { _service = value; }
        }
    }
}
