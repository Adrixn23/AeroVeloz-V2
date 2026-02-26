namespace AeroVeloz.Domain.Common.Enums.auditoria
{
    public enum AuditType
    {
      
        UserLogin = 1,
        UserLogout = 2, 
        FlightStateChange = 3, 
        OperationalChange = 4, 
        SystemAccess = 5, 
        DataModification = 6,
        SecurityEvent = 7,
        SubscriptionCreated = 8,
        SubscriptionCanceled = 9,
        NotificationGeneratedOrSent = 10,
        FlightCorrected = 11,
        BatchValidationFailed = 12,
    }
}
