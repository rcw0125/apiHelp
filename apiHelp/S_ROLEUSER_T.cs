//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace apiHelp
{
    using System;
    using System.Collections.Generic;
    
    public partial class S_ROLEUSER_T
    {
        public string GUID { get; set; }
        public string ROLEID { get; set; }
        public string USERID { get; set; }
        public string TS { get; set; }
        public string DR { get; set; }
        public string BEGINTIME { get; set; }
        public string ENDTIME { get; set; }
        public string FGUID { get; set; }
        public string REMARK { get; set; }
    
        public virtual S_ROLE_T S_ROLE_T { get; set; }
        public virtual S_USER_T S_USER_T { get; set; }
    }
}