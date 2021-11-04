namespace CarryLoad.Models
{
    public static class Constants
    {
        public const Enums.LanguageTypes DefaultLanguage = Enums.LanguageTypes.English;

        public static class ClaimNames
        {
            public const string EmailVerified = "email_verified";
            public const string PhoneNumberVerified = "phone_number_verified";
            public const string VehicleAssigned = "vehicle_assigned";
        }

        public static class Policy
        {
            public const string Admin = "User.Admin";
            public const string Driver = "User.Driver";
            public const string UnassignedDriver = "User.UnassignedDriver";
            public const string NaturalPerson = "User.NaturalPerson";
            public const string Company = "User.Company";
            public const string Carrier = "User.Carrier";
            public const string Customer = "User.Customer";
            public const string AccessTokenCustomer = "AccessToken.User.Customer";
        }

        public static class Templates
        {
            public const string SMSConfirmTextKey = "SMSConfirmTextKey";

            public const string EmailConfirmTextKey = "EmailConfirmTextKey";
            public const string EmailConfirmSubjectKey = "EmailConfirmSubjectKey";
        }
    }
}
