using System;
using System.Collections.Generic;
using System.Data;

using MySql.Data;
using MySql.Data.MySqlClient;

using TaeM.Framework.Data;
using Memberships.Entity;

namespace Memberships.DataAccess
{
    public class DacMemberSP
    {
        private static readonly string PROVIDER_NAME = "MySql.Data.MySqlClient";

        private static readonly string CONNECTION_STRING
            = "Server=localhost;Port=32785;Database=Product;Uid=root;Pwd=qwert12345!;ConnectionLifeTime=60;AllowUserVariables=true;";


        private string providerName;
        private MySqlConnection connection;


        public DacMemberSP() : this(PROVIDER_NAME, CONNECTION_STRING)
        {
        }
        public DacMemberSP(string providerName, string connectionString)
        {
            this.providerName = providerName;
            this.connection = new MySqlConnection(connectionString);
        }
        public DacMemberSP(string providerName, MySqlConnection connection)
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
                            CommandType.StoredProcedure,
                            "Product.sp_Memberships_Insert_Member",
                            MySqlParameterHelperFactory.CreateParameterWOProviderName("@MemName", member.MemberName, ParameterDirection.Input),
                            MySqlParameterHelperFactory.CreateParameterWOProviderName("@MemIsAvailable", member.IsAvailable, ParameterDirection.Input),
                            MySqlParameterHelperFactory.CreateParameterWOProviderName("@MemEmail", member.Email, ParameterDirection.Input),
                            MySqlParameterHelperFactory.CreateParameterWOProviderName("@MemPhoneNumber", member.PhoneNumber, ParameterDirection.Input),
                            MySqlParameterHelperFactory.CreateParameterWOProviderName("@MemAddress", member.Address, ParameterDirection.Input)
                            )
                        );

                    Member ret = (Member)MySqlDataHelperFactory.SelectSingleEntity<Member>(connection,
                        typeof(Member),
                        CommandType.StoredProcedure,
                        "Product.sp_Memberships_Select_Member_By_MemberID",
                        MySqlParameterHelperFactory.CreateParameterWOProviderName<MySqlDbType>("@MemID", insertedMemberID, MySqlDbType.Int32, ParameterDirection.Input)
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
                        CommandType.StoredProcedure,
                        "Product.sp_Memberships_Select_Member_By_MemberID",
                        MySqlParameterHelperFactory.CreateParameter<MySqlDbType>(providerName, "@MemID", memberID, MySqlDbType.Int32, ParameterDirection.Input)
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
                        CommandType.StoredProcedure,
                        "Product.sp_Memberships_Select_Members_By_MemberName",
                        MySqlParameterHelperFactory.CreateParameter(providerName, "@MemName", String.Format("%{0}%", memberName), ParameterDirection.Input)
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
                        CommandType.StoredProcedure,
                        "Product.sp_Memberships_Select_Num_Of_Members_By_MemberName",
                        MySqlParameterHelperFactory.CreateParameter(providerName, "@MemName", String.Format("%{0}%", memberName), ParameterDirection.Input)
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
                        CommandType.StoredProcedure,
                        "Product.sp_Memberships_Update_Member",
                        MySqlParameterHelperFactory.CreateParameter(providerName, "@MemName", member.MemberName, ParameterDirection.Input),
                        MySqlParameterHelperFactory.CreateParameter(providerName, "@MemIsAvailable", member.IsAvailable, ParameterDirection.Input),
                        MySqlParameterHelperFactory.CreateParameter(providerName, "@MemEmail", member.Email, ParameterDirection.Input),
                        MySqlParameterHelperFactory.CreateParameter(providerName, "@MemPhoneNumber", member.PhoneNumber, ParameterDirection.Input),
                        MySqlParameterHelperFactory.CreateParameter(providerName, "@MemAddress", member.Address, ParameterDirection.Input),
                        MySqlParameterHelperFactory.CreateParameter(providerName, "@MemID", member.MemberID, ParameterDirection.Input)
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
                        CommandType.StoredProcedure,
                        "Product.sp_Memberships_Delete_Member_By_MemberID",
                        MySqlParameterHelperFactory.CreateParameter<MySqlDbType>(providerName, "@MemID", memberID, MySqlDbType.Int32, ParameterDirection.Input)
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