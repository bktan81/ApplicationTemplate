using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Web
{
    public class StatusMessage
    {
        public object data { get; set; }

        public string status { get; set; }

        public string message { get; set; }


        public StatusMessage(object obj, string status, string message = "")
        {
            this.data = obj;
            this.status = status;
            this.message = message;
        }

        public StatusMessage(string status)
        {
            this.status = status;
        }

        public StatusMessage()
        {

        }
    }

    public class ReturnStatus
    {
        public const string Success = "S100";
        public const string AddSuccess = "S101";
        public const string SaveSuccess = "S102";
        public const string UpdateSuccess = "S103";
        public const string DeleteSuccess = "S104";
        public const string CancelSuccess = "S105";
        public const string Success_NeedChangePassword = "S106";
        public const string Success_RequireTFA = "S107";


        public const string Failed = "F100";
        public const string AddFailed = "F101";
        public const string SaveFailed = "F102";
        public const string UpdateFailed = "F103";
        public const string DeleteFailed = "F104";
        public const string CancelFailed = "F105";


        public const string IncompleteInput = "V100";
        public const string DataOutOfRange = "V101";
        public const string InvalidClient = "V102";
        public const string InvalidInput = "V103";
        public const string FieldRequired = "V104";


        public const string InvalidCaptcha = "E100";
        public const string SystemError = "E101";
        public const string SessionLost = "E102";
        public const string NoPermission = "E103";
        public const string LoginFromOtherPlace = "E104";
        public const string UserIsBlockedOrFreezed = "E105";
        public const string UserSuspended = "E106";
        public const string PasswordRequirement = "E107";
        public const string PasswordNotMatch = "E108";
        public const string SameAsLast3Password = "E109";
        public const string TFACodeExpired = "E110";
        public const string LoginFail = "E111";
        public const string OldPasswordNotMatch = "E112";
        public const string UserIdAlphaNumeric = "E113";
        public const string PasswordCannotContainUserName = "E114";
        public const string UserNameInvalid = "E115";
        public const string CreditLimitCannotBeNegative = "E116";
        public const string NotEnoughCreditBalance = "E117";
        public const string AddCreditFailed = "E118";
        public const string FailAssignGameToUser = "E119";
        public const string RecordNotFound = "E120";


        public const string VersionNotSupported = "E142";

        public const string SMSSent = "M100";


    }
}
