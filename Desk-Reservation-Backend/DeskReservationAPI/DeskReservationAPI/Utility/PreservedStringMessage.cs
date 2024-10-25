namespace DeskReservationAPI.Utility
{
    public static class PreservedStringMessage
    {
        public static string FailedStatus = "failed";
        public static string SuccessStatus = "success";
        public static string ErrorStatus = "error";
        public static string EmailOrPasswordIncorrectMsg = "email or password is not correct";
        public static string EmailTypingInCorrect = "email sent is not valid";
        public static string EmailExist = "email exist";
        public static string SeverErrorWhileCreatingUser = "user has not been created !";
        public static string AlreadyOverlappedReservationMessage = "the desk is already booked  in the time the date you requested";
        public static string EndTimeReservationNotRequstedMessage = " end time of reservation is required !!";
        public static string FlexReservationTimeExceed = "only three days is allowed for flex reservation";
        public static string UserHasAlreadyReservationInTimeRequested = "you have alread booked a desk in time requseted ";
    }
}
