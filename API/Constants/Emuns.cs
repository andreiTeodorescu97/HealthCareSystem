namespace API.Constants
{
    public enum AppoinmentStatuses
    {
        Pending = 1,
        Approved = 2,
        CanceledByPacient = 3,
        CanceledByDoctor = 4,
        Finalized = 5
    }

    public enum EmailTemplates
    {
        ResetPassword = 1,
        Welcome = 2,
        ConfirmAccount = 3,
        AppoinmentConfirmation = 4,
        RecipeStylesheet = 5
    }
}