namespace back_cursos.models
{
    public class CourseDTO
    {
        public string name {get; set;} = string.Empty;
        public String teachName {get; set;} = string.Empty;
        public int slots {get; set;}
        public int takenSlots {get; set;}
    }
}