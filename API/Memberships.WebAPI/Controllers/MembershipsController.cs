using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;

using Memberships.Business;
using Memberships.Entity;

namespace Memberships.WebAPI.Controllers
{
    public class MembershipsController : ApiController
    {
        private static string MY_SQL_PROVIDER_NAME = ConfigurationManager.ConnectionStrings["MYSQLDB"].ProviderName;
        private static string MY_SQL_CONN_STR = ConfigurationManager.ConnectionStrings["MYSQLDB"].ConnectionString;

        [HttpPost]
        [Route("Memberships/CreateMember")]
        public Member CreateMember(Member member)
        {
            Member ret = null;

            try
            {
                ret = new BizMemberShipSP(MY_SQL_PROVIDER_NAME, MY_SQL_CONN_STR).CreateMember(member);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ret;
        }

        [HttpGet]
        [Route("Memberships/GetMember")]
        public Member GetMember(int memberID)
        {
            Member ret = null;

            try
            {
                ret = new BizMemberShipSP(MY_SQL_PROVIDER_NAME, MY_SQL_CONN_STR).GetMember(memberID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ret;
        }

        [HttpPost]
        [Route("Memberships/GetMembers")]
        public List<Member> GetMembers([FromBody]string memberName)
        {
            List<Member> ret = null;

            try
            {
                ret = new BizMemberShipSP(MY_SQL_PROVIDER_NAME, MY_SQL_CONN_STR).GetMembers(memberName);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ret;
        }

        [HttpPost]
        [Route("Memberships/GetNumsOfMembers")]
        public int GetNumsOfMembers([FromBody]string memberName)
        {
            int ret = -1;

            try
            {
                ret = new BizMemberShipSP(MY_SQL_PROVIDER_NAME, MY_SQL_CONN_STR).GetNumsOfMembers(memberName);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ret;
        }

        [HttpPut]
        [Route("Memberships/SetMember")]
        public bool SetMember(Member member)
        {
            bool ret = false;

            try
            {
                ret = new BizMemberShipSP(MY_SQL_PROVIDER_NAME, MY_SQL_CONN_STR).SetMember(member);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ret;
        }

        [HttpPut]
        [Route("Memberships/RemoveMember")]
        public bool RemoveMember([FromBody]int memberID)
        {
            bool ret = false;

            try
            {
                ret = new BizMemberShipSP(MY_SQL_PROVIDER_NAME, MY_SQL_CONN_STR).RemoveMember(memberID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ret;
        }
    }
}