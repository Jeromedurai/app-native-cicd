using System;
namespace CICD.Utils
{
	public class Constants
	{
		public const string AUTHENTICATION_BASE_URL = "http://internal-prod-useast1-auth-xtrachef-com-2016288420.us-east-1.elb.amazonaws.com/api.authentication-query/api/1.0/authentication/tenant-token";
        public const string CICD_BASE_URL = "https://ecs-api-prod.sa.toasttab.com/api.sa-cicd-query/";
		public const string AUTHENTICATION_URL = "api/1.0/cicd/authenticate";
		public const string SYSTEM_TOKEN = "systemToken";
        public const string HEADER_LABEL = "xtraCHEF by Toast";
        public const string TAB_TITLE_FOR_XTRACHEF_BY_TOAST = "CICD Application";
        public const string LOGIN_HEADER_LABEL = "To access SA CICD, log into xtraCHEF BY Toast CICD.";
        public const string WELCOME_LABEL = "Welcome";
        public const string USER_NAME_LABEL = "User Name";
        public const string PASSWORD_LABEL = "Password";
        public const string KEEP_LOGGED_LABEL = "Keep me logged In.";
        public const string LOGIN_LABEL = "Log in";
    }
}

