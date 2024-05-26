﻿using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance.Data;
using Persistance.Repository;

namespace Persistance;

public static class PersistanceServiceRegistration
{
	public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuartion)
	{
		services.AddDbContext<AppDBContext>
		(
			options =>
			{
				options.UseSqlServer
				(
					configuartion.GetConnectionString("DefaultConnection")
				);
			}
		);

		services.AddScoped<ICourseRepository, CourseRepository>();
		services.AddScoped<IFacultyRepository, FacultyRepository>();
		services.AddScoped<ITeacherRepository, TeacherRepository>();
		services.AddScoped<IStudentRepository, StudentRepository>();
		services.AddScoped<ITeacherCourseRepository, TeacherCourseRepository>();

		return services;
	}
}
