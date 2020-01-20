using System;
using System.Collections.Generic;
using System.Data;

using MySql.Data;
using MySql.Data.MySqlClient;

using TaeM.Framework.Data;
using Memberships.Entity;

namespace Memberships.DataAccess
{
    public class DacMemberHistorySP
    {
        private static readonly string PROVIDER_NAME = "MySql.Data.MySqlClient";

        private static readonly string CONNECTION_STRING
            = "Server=localhost;Port=32785;Database=Product;Uid=root;Pwd=qwert12345!;ConnectionLifeTime=60;AllowUserVariables=true;";


        private string providerName;
        private MySqlConnection connection;


        public DacMemberHistorySP() : this(PROVIDER_NAME, CONNECTION_STRING)
        {
        }
        public DacMemberHistorySP(string providerName, string connectionString)
        {
            this.providerName = providerName;
            this.connection = new MySqlConnection(connectionString);
        }
        public DacMemberHistorySP(string providerName, MySqlConnection connection)
        {
            this.providerName = providerName;
            this.connection = connection;
        }

        /// <summary>
        /// InsertMemberHistory method
        /// - Insert MemberHistory table row from member history information
        /// </summary>
        /// <param name="member">Member history information</param>
        /// <returns></returns>
        public MemberHistory InsertMemberHistory(MemberHistory memberHistory)
        {
            try
            {
                using (connection)
                {
                    connection.Open();

                    int insertedSequence = Convert.ToInt32(
                        MySqlDataHelperFactory.SelectScalar(connection,
                            CommandType.StoredProcedure,
                            "Product.sp_Memberships_Insert_MemberHistory",
                            MySqlParameterHelperFactory.CreateParameter(providerName, "@MemID", memberHistory.MemberID, ParameterDirection.Input),
                            MySqlParameterHelperFactory.CreateParameter(providerName, "@MemName", memberHistory.MemberName, ParameterDirection.Input),
                            MySqlParameterHelperFactory.CreateParameter(providerName, "@MemSuccessful", memberHistory.IsSuccess, ParameterDirection.Input),
                            MySqlParameterHelperFactory.CreateParameter(providerName, "@MemMessage", memberHistory.Message, ParameterDirection.Input)
                            )
                        );

                    MemberHistory ret = (MemberHistory)MySqlDataHelperFactory.SelectSingleEntity<MemberHistory>(connection,
                        typeof(MemberHistory),
                        CommandType.StoredProcedure,
                        "Product.sp_Memberships_Select_MemberHistory_By_Sequence",
                        MySqlParameterHelperFactory.CreateParameter<MySqlDbType>(providerName, "@Seq", insertedSequence, MySqlDbType.Int32, ParameterDirection.Input));

                    return ret;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// SelectMemberHistories() method
        /// - Select MemberHistory table row by fromDate and toDate 
        /// </summary>
        /// <param name="fromDate">From date</param>
        /// <param name="toDate">To date</param>
        /// <returns></returns>
        public List<MemberHistory> SelectMemberHistories(DateTime fromDate, DateTime toDate)
        {
            try
            {
                using (connection)
                {
                    connection.Open();

                    return (List<MemberHistory>)MySqlDataHelperFactory.SelectMultipleEntities<MemberHistory>(connection,
                        typeof(MemberHistory),
                        CommandType.StoredProcedure,
                        "Product.sp_Memberships_Select_MemberHistories_By_FromToDate",
                        MySqlParameterHelperFactory.CreateParameter(providerName, "@FromDate", fromDate, ParameterDirection.Input),
                        MySqlParameterHelperFactory.CreateParameter(providerName, "@ToDate", toDate, ParameterDirection.Input)
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}