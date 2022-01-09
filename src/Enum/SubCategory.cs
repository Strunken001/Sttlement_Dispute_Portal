using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace src.Enum
{
    public enum SubCategory1
    {
        THEFT = 0,
        [EnumMember(Value = "COMPROMISED DEVICE")]
        COMPROMISEDDEVICE = 1,
        OTHERS = 2
    }
    public enum SubCategory2
    {
        [EnumMember(Value = "TOKEN REQUEST")]
        TOKENREQUEST = 0,
        [EnumMember(Value = "ERRONEUS PAYMENT")]
        ERRONEUSPAYMENT = 1,
        [EnumMember(Value = "VALUE NOT GIVEN")]
        VALUENOTGIVEN = 2,
        OTHERS = 3
    }
    public enum SubCategory3
    {
        [EnumMember(Value = "UNSETTLED TRANSACTION")]
        UNSETTLEDTRANSACTION = 0,
        INQUIRY = 1,
        [EnumMember(Value = "SETTLEMENT STATUS")]
        SETTLEMENTSTATUS = 2,
        [EnumMember(Value = "SETTLEMENT REPORT")]
        SETTLEMENTREPORT = 3,
        COMPLAINTS = 4,
        [EnumMember(Value = "ERRONEOUS PAYMENT")]
        ERRONEOUSPAYMENT = 5
    }
}
