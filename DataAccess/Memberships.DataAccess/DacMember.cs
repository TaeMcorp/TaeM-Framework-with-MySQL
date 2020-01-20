using System;
using System.Collections.Generic;
using System.Data;

using MySql.Data;
using MySql.Data.MySqlClient;

using TaeM.Framework.Data;
using Memberships.Entity;

namespace Memberships.DataAccess
{
    public class DacMember
    {
        private static readonly string PROVIDER_NAME = "MySql.Data.MySqlClient";

        private static readonly string CONNECTION_STRING
            = "Server=localhost;Port=32785;Database=Product;Uid=root;Pwd=qwert12345!;ConnectionLifeTime=60;AllowUserVariables=true;";


        private string providerName;
        private MySqlConnection connection;


        public DacMember() : this(PROVIDER_NAME, CONNECTION_STRING)
        {
        }
        public DacMember(string providerName, string connectionString)
        {
            this.providerName = providerName;
            this.connection = new MySqlConnection(connectionString);
        }
        public DacMember(string providerName, MySqlConnection connection)
        {
            this.providerName = providerName;
            this.connection = connection;
        }


        /// <summary>
        /// InsertMember method
        /// - Insert Member table row from member information
        /// </summary>
        /// <param name="member">Member information</param>
        /// <returns></returns>
        public Member InsertMember(Member member)
        {
            try
            {
                using (connection)
                {
                    if (string.IsNullOrEmpty(MySqlParameterHelperFactory.ProviderName))
                        MySqlParameterHelperFactory.ProviderName = providerName;

                    connection.Open();

                    int insertedMemberID = Convert.ToInt32(
                        MySqlDataHelperFactory.SelectScalar(connection,
                            CommandType.Text,
                            @"INSERT INTO Product.Member " +
                            @"( MemberName, IsAvailable, Email, PhoneNumber, Address, InsertedDate, UpdatedDate ) " +
                            @"VALUES " +
                            @"( @MemberName, @IsAvailable, @Email, @PhoneNumber, @Address, SYSDATE(), NULL ); " +
                            @" " +
                            @"SELECT LAST_INSERT_ID(); ",
                            MySqlParameterHelperFactory.CreateParameterWOProviderName("@MemberName", member.MemberName, ParameterDirection.Input),
                            MySqlParameterHelperFactory.CreateParameterWOProviderName("@IsAvailable", member.IsAvailable, ParameterDirection.Input),
                            MySqlParameterHelperFactory.CreateParameterWOProviderName("@Email", member.Email, ParameterDirection.Input),
                            MySqlParameterHelperFactory.CreateParameterWOProviderName("@PhoneNumber", member.PhoneNumber, ParameterDirection.Input),
                            MySqlParameterHelperFactory.CreateParameterWOProviderName("@Address", member.Address, ParameterDirection.Input)
                            )
                        );

                    Member ret = (Member)MySqlDataHelperFactory.SelectSingleEntity<Member>(connection,
                        typeof(Member),
                        CommandType.Text,
                        @"SELECT MemberID, MemberName, IsAvailable, Email, PhoneNumber, Address, InsertedDate, UpdatedDate " +
                        @"FROM Product.Member " +
                        @"WHERE MemberID = @MemberID; ",
                        MySqlParameterHelperFactory.CreateParameterWOProviderName<MySqlDbType>("@MemberID", insertedMemberID, MySqlDbType.Int32, ParameterDirection.Input)
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
        /// SelectMember method
        /// - Select Member table row by memberID 
        /// </summary>
        /// <param name="memberID">Member ID</param>
        /// <returns></returns>
        public Member SelectMember(int memberID)
        {
            try
            {
                using (connection)
                {
                    connection.Open();

                    Member ret = (Member)MySqlDataHelperFactory.SelectSingleEntity<Member>(connection,
                        typeof(Member),
                        CommandType.Text,
                        @"SELECT MemberID, MemberName, IsAvailable, Email, PhoneNumber, Address, InsertedDate, UpdatedDate " +
                        @"FROM Product.Member " +
                        @"WHERE MemberID = @MemberID; ",
                        MySqlParameterHelperFactory.CreateParameter<MySqlDbType>(providerName, "@MemberID", memberID, MySqlDbType.Int32, ParameterDirection.Input)
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
        /// SelectMembers method
        /// - Select Member table rows by memberName 
        /// </summary>
        /// <param name="memberName">Member name</param>
        /// <returns></returns>
        public List<Member> SelectMembers(string memberName)
        {
            try
            {
                using (connection)
                {
                    connection.Open();

                    List<Member> ret = (List<Member>)MySqlDataHelperFactory.SelectMultipleEntities<Member>(connection,
                        typeof(Member),
                        CommandType.Text,
                        @"SELECT MemberID, MemberName, IsAvailable, Email, PhoneNumber, Address, InsertedDate, UpdatedDate " +
                        @"FROM Product.Member " +
                        @"WHERE MemberName like @MemberName; ",
                        MySqlParameterHelperFactory.CreateParameter(providerName, "@MemberName", String.Format("%{0}%", memberName), ParameterDirection.Input)
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
        /// SelectNumsOfMembers method
        /// - Select number of Member table rows by memberName 
        /// </summary>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public int SelectNumsOfMembers(string memberName)
        {
            try
            {
                using (connection)
                {
                    connection.Open();

                    int ret = Convert.ToInt32(MySqlDataHelperFactory.SelectScalar(connection,
                        CommandType.Text,
                        @"SELECT COUNT(*) " +
                        @"FROM Product.Member " +
                        @"WHERE MemberName like @MemberName; ",
                        MySqlParameterHelperFactory.CreateParameter(providerName, "@MemberName", String.Format("%{0}%", memberName), ParameterDirection.Input)
                        ));

                    return ret;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// UpdateMember method
        /// - Update Member table row by member information
        /// </summary>
        /// <param name="member">Member information</param>
        /// <returns></returns>
        public bool UpdateMember(Member member)
        {
            try
            {
                using (connection)
                {
                    connection.Open();

                    int ret = MySqlDataHelperFactory.Execute(connection,
                        CommandType.Text,
                        @"UPDATE Product.Member " +
                        @"SET MemberName = @MemberName, IsAvailable = @IsAvailable, Email = @Email, " +
                        @"  PhoneNumber = @PhoneNumber, Address = @Address, UpdatedDate = SYSDATE() " +
                        @"WHERE MemberID = @MemberID; ",
                        MySqlParameterHelperFactory.CreateParameter(providerName, "@MemberName", member.MemberName, ParameterDirection.Input),
                        MySqlParameterHelperFactory.CreateParameter(providerName, "@IsAvailable", member.IsAvailable, ParameterDirection.Input),
                        MySqlParameterHelperFactory.CreateParameter(providerName, "@Email", member.Email, ParameterDirection.Input),
                        MySqlParameterHelperFactory.CreateParameter(providerName, "@PhoneNumber", member.PhoneNumber, ParameterDirection.Input),
                        MySqlParameterHelperFactory.CreateParameter(providerName, "@Address", member.Address, ParameterDirection.Input),
                        MySqlParameterHelperFactory.CreateParameter(providerName, "@MemberID", member.MemberID, ParameterDirection.Input)
                        );

                    return (ret == 1) ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// DeleteMember() method
        /// - Delete Member table row by memberID
        /// </summary>
        /// <param name="memberID">Member ID</param>
        /// <returns></returns>
        public bool RemoveMember(int memberID)
        {
            try
            {
                using (connection)
                {
                    connection.Open();

                    int ret = MySqlDataHelperFactory.Execute(connection,
                        CommandType.Text,
                        @"DELETE FROM Product.Member " +
                        @"WHERE MemberID = @MemberID; ",
                        MySqlParameterHelperFactory.CreateParameter<MySqlDbType>(providerName, "@MemberID", memberID, MySqlDbType.Int32, ParameterDirection.Input)
                        );

                    return (ret == 1) ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}