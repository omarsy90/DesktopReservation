namespace DeskReservationAPI.Utility
{
    public static class PreservedStringMessage
    {
        public static readonly string FailedStatus = "failed";
        public static readonly string SuccessStatus = "success";
        public static readonly string ErrorStatus = "error";
        public static readonly string EmailOrPasswordIncorrectMsg = "email or password is not correct";
        public static readonly string EmailTypingInCorrect = "email sent is not valid";
        public static readonly string EmailExist = "email exist";
        public static readonly string SeverErrorWhileCreatingUser = "user has not been created !";
        public static readonly string DeskAlreadyReserved = "the desk is already booked  in duration  you requested";
        public static readonly string EndTimeReservationNotRequstedMessage = " end time of reservation is required !!";
        public static readonly string FlexReservationTimeExceed = "only three days is allowed for flex reservation";
        public static readonly string UserHasAlreadyReservationInTimeRequested = "you have alread booked a desk in time requseted ";
        public static readonly string UserNotValid = "user not valid";
        public static readonly string DeskNotFound = "desk not found";
        public static readonly string UserHasNoAdminRole = "process needs admin's role";
        public static readonly string ReservationNotFound = "reservation not found";
        public static readonly string StartDtOrEndDtNotSet = "begin and end of reservation must be set";
        public static readonly string CancellingTimeShouldSetBeforeFinishingReservation = "the cancelling time after  reservation has finished not allowed";
        public static readonly string CancellingTimeNotAllowedDuringOccupation = "the cancelling time during occupation the desk not allowed";
        public static readonly string ReservationSuccessfullyDeleted = "reservation  has been successfully deleted";
        public static readonly string UserNotFound = "user not found";
    }
   
}
