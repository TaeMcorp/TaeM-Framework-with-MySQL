using System;
using TaeM.Framework.Data.MySql;

namespace Memberships.Entity
{
    public class MemberHistory
    {
        public MemberHistory()
            : this(-1, string.Empty, false, string.Empty)
        {
        }

        public MemberHistory(int memberID, string memberName, bool isSuccess, string message)
            : this(-1, memberID, memberName, isSuccess, message, DateTime.MinValue)
        {
        }

        public MemberHistory(int seq, int memberID, string memberName,
            bool isSuccess, string message, DateTime insertedDate)
        {
            this.Seq = seq;
            this.MemberID = memberID;
            this.MemberName = memberName;

            this.IsSuccess = isSuccess;
            this.Message = message;
            this.InsertedDate = insertedDate;
        }

        [MySqlDataBinder("Sequence")]
        public int Seq { get; set; }

        [MySqlDataBinder("MemberID")]
        public int MemberID { get; set; }

        [MySqlDataBinder("MemberName")]
        public string MemberName { get; set; }

        [MySqlDataBinder("Successful")]
        public bool IsSuccess { get; set; }

        [MySqlDataBinder("Message")]
        public string Message { get; set; }

        [MySqlDataBinder("InsertedDate")]
        public DateTime InsertedDate { get; set; }
    }
}