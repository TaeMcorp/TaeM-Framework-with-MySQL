using System;
using TaeM.Framework.Data.MySql;

namespace Memberships.Entity
{
    public class Member
    {
        public Member() : this(string.Empty, false,
            string.Empty, string.Empty, string.Empty)
        {
        }

        public Member(string memberName, bool isAvailable,
            string email, string phoneNumber, string address)
            : this(-1, memberName, isAvailable, email, phoneNumber, address, DateTime.MinValue, DateTime.MinValue)
        {
        }

        public Member(int memberID, string memberName, bool isAvailable,
            string email, string phoneNumber, string address,
            DateTime insertedDate, DateTime updatedDate)
        {
            this.MemberID = memberID;
            this.MemberName = memberName;
            this.IsAvailable = isAvailable;

            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.Address = address;

            this.InsertedDate = insertedDate;
            this.UpdatedDate = updatedDate;
        }

        [MySqlDataBinder("MemberID")]
        public int MemberID { get; set; }

        [MySqlDataBinder("MemberName")]
        public string MemberName { get; set; }

        [MySqlDataBinder("IsAvailable")]
        public bool IsAvailable { get; set; }

        [MySqlDataBinder("Email")]
        public string Email { get; set; }

        [MySqlDataBinder("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [MySqlDataBinder("Address")]
        public string Address { get; set; }

        [MySqlDataBinder("InsertedDate")]
        public DateTime InsertedDate { get; set; }

        [MySqlDataBinder("UpdatedDate")]
        public DateTime UpdatedDate { get; set; }
    }
}