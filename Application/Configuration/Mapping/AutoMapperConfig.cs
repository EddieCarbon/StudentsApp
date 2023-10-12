using System.Collections.Generic;
using System.Linq;
using Application.Dto.Student;
using Application.Dto.StudentAddress;
using AutoMapper;
using Core.Entities;
using Application.Dto.StudentDepartment;
using Application.Configuration.Commands.Departments.CreateDepartment;
using Application.Configuration.Commands.Departments.UpdateDepartment;
using Application.Configuration.Commands.Students.CreateStudent;
using Application.Configuration.Commands.Students.UpdateStudent;

namespace Application.Configuration.Mapping
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
           => new MapperConfiguration(cfg =>
           {
               #region Product    

               cfg.CreateMap<Student, StudentDto>();
               cfg.CreateMap<Student, StudentDetailDto>();
               cfg.CreateMap<CreateStudentDto, Student>()
               .ForMember(dest => dest.Address, act => act.MapFrom(src => src.CreateStudentAddress));
               cfg.CreateMap<UpdateStudentDto, Student>();
               cfg.CreateMap<IEnumerable<Student>, ListStudentsDto>()
                .ForMember(dest => dest.Students, act => act.MapFrom(src => src))
                .ForMember(dest => dest.Count, act => act.MapFrom(src => src.Count()));
               cfg.CreateMap<CreateStudentAddress, StudentAddress>();


               cfg.CreateMap<Department, DepartmentDto>();
               cfg.CreateMap<Department, DepartmentDetailDto>();
               cfg.CreateMap<CreateDepartmentDto, Department>();
               cfg.CreateMap<UpdateDepartmentDto, Department>();
               cfg.CreateMap<IEnumerable<Department>, ListDepartmentsDto>()
                .ForMember(dest => dest.Departments, act => act.MapFrom(src => src))
                .ForMember(dest => dest.Count, act => act.MapFrom(src => src.Count()));

               cfg.CreateMap<CreateStudentCommand, Student>();
               cfg.CreateMap<CreateStudentAddress, StudentAddress>();

               cfg.CreateMap<UpdateStudentCommand, Student>();
               cfg.CreateMap<CreateStudentAddress, StudentAddress>();
               cfg.CreateMap<CreateStudentDepartment, Department>();

               cfg.CreateMap<CreateDepartmentCommand, Department>();
               cfg.CreateMap<UpdateDepartmentCommand, Department>();


               #endregion

           })
           .CreateMapper();
    }
}
