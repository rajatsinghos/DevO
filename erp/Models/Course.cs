namespace ERP.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        public string CourseName { get; set; }

        public int Duration { get; set; }

        public int DepartmentId { get; set; }

        public Department ? Department { get; set; }
    }
}
