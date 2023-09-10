using Application.Dto.Student;
using Application.Validators.Abstractions;
using Core.Repositories;


namespace Application.Validators
{
    public class DepartmentValidator : IDepartmentValidator
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentValidator(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public void Validate(CreateDepartmentDto department)
        {
            ValidateDepartmentName(department.DepartmentName);
            ValidateBuildingNumber(department.BuildingNumber);
        }

        public void Validate(UpdateDepartmentDto department)
        {
            IsExist(department.DepartmentId);
            ValidateDepartmentName(department.DepartmentName);
            ValidateBuildingNumber(department.BuildingNumber);
        }

        public void Validate(int Id)
        {
            IsExist(Id);
        }

        private void IsExist(int Id)
        {
            var department = _departmentRepository.GetById(Id);

            if (department is null)
            {
                throw new Exception($"Department with ID {Id} does not exist.");
            }
        }

        private void ValidateDepartmentName(string DepartmentName)
        {
            if (string.IsNullOrEmpty(DepartmentName))
            {
                throw new Exception("Department can not have an empty name.");
            }
        }

        private void ValidateBuildingNumber(string BuildingNumber)
        {
            if (BuildingNumber == "")
            {
                return;
            }
        }
    }
}
