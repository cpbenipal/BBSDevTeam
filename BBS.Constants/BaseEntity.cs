using System;

namespace BBS.Constants
{
    public class BaseEntity 
    {
        public int AddedById
        {
            get;
            set;
        }
        public DateTime AddedDate
        {
            get;
            set;
        } = DateTime.Now;
        public int ModifiedById
        {
            get;
            set;
        }
        public DateTime ModifiedDate
        {
            get;
            set;
        } = DateTime.Now;
        public string IPAddress
        {
            get;
            set;
        }
        public bool IsDeleted
        {
            get;
            set; 
        }
    }
}
