// using Application.Dto.Student;
// using Application.Services.Abstractions;
// using Application.Validators.Abstractions;
// using AutoMapper;
// using Core.Entities;
// using Core.Repositories;
//
//
// namespace Application.Services
// {
//     public class DepartmentService : IDepartmentService
//     {
//         private readonly IDepartmentRepository _departmentRepository;
//         private readonly IDepartmentValidator _departmentValidator;
//         private readonly IMapper _mapper;
//         
//         public DepartmentService(IDepartmentRepository departmentRepository, IDepartmentValidator departmentValidator, IMapper mapper)
//         {
//             _departmentRepository = departmentRepository;
//             _departmentValidator = departmentValidator;
//             _mapper = mapper;
//         }
//
//         public ListDepartmentsDto GetAllDepartments()
//         {
//             var departments = _departmentRepository.GetAll();
//             return _mapper.Map<ListDepartmentsDto>(departments);
//         }
//
//         public DepartmentDetailDto GetDepartmentById(int DepartmentId)
//         {
//             var department = _departmentRepository.GetById(DepartmentId);
//             return _mapper.Map<DepartmentDetailDto>(department);
//         }
//
//         public DepartmentDto CreateDepartment(CreateDepartmentDto newDepartment)
//         {
//             _departmentValidator.Validate(newDepartment);
//
//             var department = _mapper.Map<Department>(newDepartment);
//
//             _departmentRepository.Add(department);
//
//             return _mapper.Map<DepartmentDto>(department);
//         }
//
//         public void UpdateDepartment(UpdateDepartmentDto updateDepartment)
//         {
//             _departmentValidator.Validate(updateDepartment);
//
//             var existingDepartment = _departmentRepository.GetById(updateDepartment.DepartmentId);
//
//             var department = _mapper.Map(updateDepartment, existingDepartment);
//
//             _departmentRepository.Update(department);
//         }
//
//         public void DeleteDepartment(int DepartmentId)
//         {
//             _departmentValidator.Validate(DepartmentId);
//
//             var product = _departmentRepository.GetById(DepartmentId);
//
//             _departmentRepository.Delete(product);
//         }
//     }
// }
