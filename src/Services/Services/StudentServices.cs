using Exceptions;
using ExtraModels;
using Interface;
using Models;

namespace Services
{
    public class StudentServices
    {
        protected IStudentDA studentDA;
        protected IRoomDA roomDA;
        public StudentServices(IStudentDA studentDA, IRoomDA roomDA)
        {
            this.studentDA = studentDA;
            this.roomDA = roomDA;
        }
        public async Task<List<Student>> getAllStudent(int page, int pagesize)
        {
            List<Student>? allStudents =  await studentDA.getAllStudent(page, pagesize);
            if (allStudents == null)
                throw new StudentPageException();
            return allStudents;
        }
        public async Task<Student> getStudentByCode(string codeStudent)
        {
            Student? student = await studentDA.getStudentByCode(codeStudent);
            if (student == null)
                throw new StudentNotFoundException();
            return student;
        }
        public async Task<Student> getStudentById(int idStudent)
        {
            Student? student = await studentDA.getStudentById(idStudent);
            if (student == null)
                throw new StudentNotFoundException();
            return student;
        }
        public async Task<int> addStudent(string name, string group, string code, int id_user)
        {
            return await studentDA.addStudent(new Student { Name = name, GroupStudent = group, StudentCode = code, Id_user = id_user, Id_room = 0 });
        }
        public async Task<int> updateStudent(string codeStudent, UpdateStudent newStudent)
        {
            int code = await studentDA.updateStudent(codeStudent, newStudent);
            if (code == -1)
                throw new StudentNotFoundException();
            return 1;
        }
        public async Task<int> changeRoom(string codeStudent, int id_room)
        {
            Student student = await getStudentByCode(codeStudent);
            if (id_room > 0)
            {
                Room? room = await roomDA.getRoomById(id_room);
                if (room == null)
                    throw new RoomNotFoundException();
                if (student.Id_room == id_room)
                    throw new StudentInRoomException();
                await studentDA.changeRoom(codeStudent, id_room);
                return 1;
            }
            if (student.Id_room < 1)
                throw new StudentNotLiveException();
            await studentDA.changeRoom(codeStudent, id_room);
            return 1;
        }
    }
}
