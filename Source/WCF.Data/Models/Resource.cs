using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace WCF.Data.Models
{
    public class Resource
    {
        [Key]
        public decimal ResID
        {
            get;
            set;
        }

        public decimal DepID
        {
            get;
            set;
        }

        public decimal OrgID
        {
            get;
            set;
        }

        public string ResNO
        {
            get;
            set;
        }

        public string ResName
        {
            get;
            set;
        }

        public string ResDesc
        {
            get;
            set;
        }

        public int Status
        {
            get;
            set;
        }
        public int Type
        {
            get;
            set;
        }

        public int ResType
        {
            get;
            set;
        }

        public bool IsDefault
        {
            get;
            set;
        }

        public string ResGroupNo
        {
            get;
            set;
        }
    }
}
