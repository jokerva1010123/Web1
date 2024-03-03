using ApplicationDbContext;
using ExtraModels;
using Interface;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAcess
{
    public class StudentDA : IStudentDA
    {
        private readonly AppDbContext appContext;
        public StudentDA(AppDbContext appContext)
        {
            this.appContext = appContext;
        }
        public async Task<List<Student>?> getAllStudent(int page, int pagesize)
        {
            int total = appContext.Students.Count();
            int totalPages = (int)Math.Ceiling((double)total /pagesize);
            if (totalPages < page)
                return null;
            return await appContext.Students.Skip((page - 1) * pagesize).Take(pagesize).ToListAsync();
		}
        public async Task<Student?> getStudentByCode(string codeStudent)
        {
            return await appContext.Students.AsNoTracking().FirstOrDefaultAsync(u => u.StudentCode == codeStudent);
        }
        public async Task<Student?> getStudentById(int id_student)
        {
            return await appContext.Students.AsNoTracking().FirstOrDefaultAsync(u => u.Id_student == id_student);
        }
        public async Task<int> addStudent(Student student)
        {
			List<Student>? lst = appContext.Students.Count() > 0 ? appContext.Students.ToList() : null;
			int maxid = 0;
			foreach (Student temp in lst)
				if (temp.Id_student > maxid)
					maxid = temp.Id_student;
			student.Id_student = maxid + 1;
            student.Date = DateTime.Now.ToString("MMM dd yyyy");
			appContext.Students.Add(student);
			await appContext.SaveChangesAsync();
			return student.Id_student;
        }
        public async Task<int> updateStudent(string codeStudent, UpdateStudent newStudent)
        {
            Student? student = await appContext.Students.Where(s => s.StudentCode == codeStudent).FirstOrDefaultAsync();
            if (student == null)
                return -1;
            student.StudentCode = newStudent.Code;
            student.Name = newStudent.Name;
            student.GroupStudent = newStudent.Group;
            await appContext.SaveChangesAsync();
            return 1;
        }
        public async Task<int> changeRoom(string code, int id_room)
        {
            Student? student = await appContext.Students.Where(s => s.StudentCode == code).FirstOrDefaultAsync();
            if (student == null)
                return -1;
            student.Id_room = id_room;
            await appContext.SaveChangesAsync();
            return 1;
        }
    }
}
