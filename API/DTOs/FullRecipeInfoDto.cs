namespace API.DTOs
{
    public class FullRecipeInfoDto
    {
        public DoctorRecipeDto Doctor { get; set; }
        public PacientRecipeDto Pacient { get; set; }
        public RecipeDto Recipe { get; set; }
    }
}