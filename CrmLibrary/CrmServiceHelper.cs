using Microsoft.Xrm.Client;
using Microsoft.Xrm.Client.Services;
using Microsoft.Xrm.Sdk;

namespace CrmLibrary
{
    public class CrmServiceHelper
    {

        #region 单例

        // 静态自动hold实例
        private static volatile CrmServiceHelper _instance;
        // Lock对象，线程安全用
        private static readonly object SyncRoot = new object();

        private CrmServiceHelper()
        {
        }

        public static CrmServiceHelper Instance
        {
            get
            {
                if (_instance != null) return _instance;
                lock (SyncRoot)
                {
                    _instance = new CrmServiceHelper();
                }
                return _instance;
            }
        }

        #endregion

        #region 字段

        private IOrganizationService _service;
        //private string _connectionString = @"Url=https://azdmsabm07.azureabdms.local/XRMServices/2011/Organization.svc; Username=azureabdms\speng;Password=ZAQ!2wsx;";
        private string _connectionString = @"Url=http://192.168.137.138:5555/CrmOrg/XRMServices/2011/Organization.svc; Username=mscrm\administrator;Password=Passw0rd;";

        #endregion

        #region 属性



        /// <summary>
        /// CRM连接串
        /// </summary>
        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 获取IOrganizationService实例
        /// </summary>
        /// <returns></returns>
        public IOrganizationService GetService()
        {
            if (_service == null)
            {
                var connection = CrmConnection.Parse(ConnectionString);
                _service = new OrganizationService(connection);
            }
            return _service;
        }

        /// <summary>
        /// 获取IOrganizationService实例(根据CRM连接串)
        /// </summary>
        /// <param name="connectionString">CRM连接串</param>
        /// <returns></returns>
        public IOrganizationService GetService(string connectionString)
        {
            var connection = CrmConnection.Parse(connectionString);
            _service = new OrganizationService(connection);
            return _service;
        }



        #endregion 
    }
}
