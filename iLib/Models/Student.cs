namespace iLib.Models
{
    public class Student : User
    {
        public Student()
        {
            UserRole = "Student";
        }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public string StudentFaculty { get; set; }
        
    }
}
