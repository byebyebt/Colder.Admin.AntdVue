using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using System;
using System.Collections.Generic;

namespace Coldairarrow.Business.Base_Manage
{
    public class Base_LogBusiness : BaseBusiness<Base_Log>, IBase_LogBusiness, IDependency
    {
        #region �ⲿ�ӿ�

        /// <summary>
        /// ��ȡ��־�б�
        /// </summary>
        /// <param name="logContent">��־����</param>
        /// <param name="logType">��־����</param>
        /// <param name="level">��־����</param>
        /// <param name="opUserName">�������û���</param>
        /// <param name="startTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <param name="pagination">��ҳ����</param>
        /// <returns></returns>
        public List<Base_Log> GetLogList(
            Pagination pagination,
            string logContent,
            string logType,
            string level,
            string opUserName,
            DateTime? startTime,
            DateTime? endTime)
        {
            ILogSearcher logSearcher = null;

            if (GlobalSwitch.LoggerType.HasFlag(LoggerType.RDBMS))
                logSearcher = new RDBMSTarget();
            else if (GlobalSwitch.LoggerType.HasFlag(LoggerType.ElasticSearch))
                logSearcher = new ElasticSearchTarget();
            else
                throw new Exception("��ָ����־����ΪRDBMS��ElasticSearch!");

            return logSearcher.GetLogList(pagination, logContent, logType, level, opUserName, startTime, endTime);
        }

        public void DeleteLog(string logContent, string logType, string level, string opUserName, DateTime? startTime, DateTime? endTime)
        {
            ILogDeleter logDeleter;
            if (GlobalSwitch.LoggerType.HasFlag(LoggerType.RDBMS))
            {
                logDeleter = new RDBMSTarget();
                logDeleter.DeleteLog(logContent, logType, level, opUserName, startTime, endTime);
            }
            if (GlobalSwitch.LoggerType.HasFlag(LoggerType.ElasticSearch))
            {
                logDeleter = new ElasticSearchTarget();
                logDeleter.DeleteLog(logContent, logType, level, opUserName, startTime, endTime);
            }
        }

        #endregion

        #region ˽�г�Ա

        #endregion

        #region ����ģ��

        #endregion
    }
}