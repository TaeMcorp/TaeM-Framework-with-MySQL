using System;
using System.Collections.Generic;
using System.Data;

using MySql.Data;
using MySql.Data.MySqlClient;

using TaeM.Framework.Data;
using Memberships.Entity;

namespace Memberships.DataAccess
{
    public class DacMemberHistory
    {
        private static readonly string PROVIDER_NAME = "MySql.Data.MySqlClient";

        private static readonly string CONNECTION_STRING
            = "Server=localhost;Port=32785;Database=Product;Uid=root;Pwd=qwert12345!;ConnectionLifeTime=60;AllowUserVariables=true;";


        private string providerName;
        private MySqlConnection connection;


        public DacMemberHistory() : this(PROVIDER_NAME, CONNECTION_STRING)
        {
        }
        public DacMemberHistory(string providerName, string connectionString)
        {
            this.providerName = providerName;
            this.connection = new MySqlConnection(connectionString);
        }
        public DacMemberHistory(string providerName, MySqlConnection connection)
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
                            CommandType.Text,
                            @"INSERT INTO Product.MemberHistory " +
                            @"( MemberID, MemberName, Successful, Message, InsertedDate ) " +
                            @"VALUES " +
                            @"( @MemberID, @MemberName, @Successful, @Message, SYSDATE() ); " +
                            @" " +
                            @"SELECT LAST_INSERT_ID(); ",
                            MySqlParameterHelperFactory.CreateParameter<MySqlDbType>(providerName, "@MemberID", memberHistory.MemberID, MySqlDbType.Int32, ParameterDirection.Input),
                            MySqlParameterHelperFactory.CreateParameter(providerName, "@MemberName", memberHistory.MemberName, ParameterDirection.Input),
                            MySqlParameterHelperFactory.CreateParameter(providerName, "@Successful", memberHistory.IsSuccess, ParameterDirection.Input),
                            MySqlParameterHelperFactory.CreateParameter(providerName, "@Message", memberHistory.Message, ParameterDirection.Input)
                            )
                        );

                    MemberHistory ret = (MemberHistory)MySqlDataHelperFactory.SelectSingleEntity<MemberHistory>(connection,
                        typeof(MemberHistory),
                        CommandType.Text,
                        @"SELECT Sequence, MemberID, MemberName, Successful, Message, InsertedDate " +
                        @"FROM Product.MemberHistory " +
                        @"WHERE Sequence = @Sequence; ",
                        MySqlParameterHelperFactory.CreateParameter<MySqlDbType>(providerName, "@Sequence", insertedSequence, MySqlDbType.Int32, ParameterDirection.Input)
                        );

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
                        CommandType.Text,
                        @"SELECT Sequence, MemberID, MemberName, Successful, Message, InsertedDate " +
                        @"FROM Product.MemberHistory " +
                        @"WHERE InsertedDate >= @FromDate AND InsertedDate <= @ToDate " +
                        @"ORDER BY InsertedDate DESC; ",
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