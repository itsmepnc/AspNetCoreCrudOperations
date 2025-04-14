using AspNetCoreCRUD.DbConnection;
using AspNetCoreCRUD.Models.EntityModels;
using AspNetCoreCRUD.Models.ViewModels;
using AspNetCoreCRUD.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class StudentRepository : IStudentRepository
{
    private readonly DbConnect _dbContext;
    private readonly ILogger<StudentRepository> _logger;

    public StudentRepository(DbConnect dbContext, ILogger<StudentRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<List<StudentRegistrationDto>> GetListAsync()
    {
        try
        {
            return await _dbContext.StudentRegistration
                .AsNoTracking()
                .Where(s => s.is_active)
                .Select(s => new StudentRegistrationDto
                {
                    Id = s.StudentId,
                    FirstName = s.StudentFirstName,
                    LastName = s.StudentLastName,
                    Dob = s.StudentDob,
                    Gender = s.Gender,
                    Fee = s.StudentFee
                })
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching student list.");
            return new();
        }
    }

    public async Task<bool> CreateOrUpdateAsync(StudentRegistrationDto dto)
    {
        if (dto == null)
        {
            _logger.LogWarning("Null student data received.");
            return false;
        }

        try
        {
            var student = await _dbContext.StudentRegistration
                .FirstOrDefaultAsync(s => s.StudentId == dto.Id);

            if (student == null)
                await AddStudentAsync(dto);
            else
                UpdateStudent(student, dto);

            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create or update student.");
            return false;
        }
    }

    public async Task<StudentRegistrationDto?> GetByIdAsync(int id)
    {
        try
        {
            var student = await FindActiveStudentById(id);
            return student == null ? null : MapToDto(student);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching student by Id {Id}", id);
            return null;
        }
    }

    public async Task<bool> RemoveByIdAsync(int id)
    {
        try
        {
            var student = await FindActiveStudentById(id);
            if (student == null)
            {
                _logger.LogWarning("Student with Id {Id} not found for deletion.", id);
                return false;
            }

            MarkStudentAsInactive(student);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error removing student with Id {Id}", id);
            return false;
        }
    }

    #region Private Helpers

    private async Task<StudentRegistration?> FindActiveStudentById(int id)
    {
        return await _dbContext.StudentRegistration
            .FirstOrDefaultAsync(s => s.StudentId == id && s.is_active);
    }

    private async Task AddStudentAsync(StudentRegistrationDto dto)
    {
        var student = new StudentRegistration
        {
            StudentFirstName = dto.FirstName,
            StudentLastName = dto.LastName,
            StudentDob = dto.Dob,
            Gender = dto.Gender,
            StudentFee = dto.Fee,
            is_active = true,
            created_by = 007,
            created_date = DateTime.Now
        };

        await _dbContext.StudentRegistration.AddAsync(student);
        _logger.LogInformation("Created new student: {@Student}", student);
    }

    private void UpdateStudent(StudentRegistration student, StudentRegistrationDto dto)
    {
        student.StudentFirstName = dto.FirstName;
        student.StudentLastName = dto.LastName;
        student.StudentDob = dto.Dob;
        student.Gender = dto.Gender;
        student.StudentFee = dto.Fee;
        student.modified_by = 007;
        student.modified_date = DateTime.Now;

        _logger.LogInformation("Updated student: {@Student}", student);
    }

    private void MarkStudentAsInactive(StudentRegistration student)
    {
        student.is_active = false;
        student.modified_by = 007;
        student.modified_date = DateTime.Now;
    }

    private static StudentRegistrationDto MapToDto(StudentRegistration s)
    {
        return new StudentRegistrationDto
        {
            Id = s.StudentId,
            FirstName = s.StudentFirstName,
            LastName = s.StudentLastName,
            Dob = s.StudentDob,
            Gender = s.Gender,
            Fee = s.StudentFee
        };
    }

    #endregion
}
