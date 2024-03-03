using ExtraModels;
using Models;

namespace Interface
{
    public interface IStudentDA
    {
        public Task<List<Student>?> getAllStudent(int page, int pagesize);
        public Task<Student?> getStudentByCode(string codeStudent);
        public Task<Student?> getStudentById(int id_student);
        public Task<int> addStudent(Student student);
        public Task<int> updateStudent(string codeStudent, UpdateStudent newStudent);
        public Task<int> changeRoom(string code, int id_room);
    }
}
