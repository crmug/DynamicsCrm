using System;
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

        public OrganizationResponse Execute(OrganizationRequest request)
        {
            try
            {
                return Service.Execute(request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
