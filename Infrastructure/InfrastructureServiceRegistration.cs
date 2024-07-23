using Application.Interfaces.ImageService;
using Application.Interfaces.Services;
using Application.Settings;
using Infrastructure.ImageService;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureServiceRegistration
{
	public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.Configure<IdentitySettings>(options => configuration.GetSection("IdentitySettings").Bind(options));

        services.AddScoped<ITeacherService, TeacherService>();
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<ITeacherCourseService, TeacherCourseService>();
		services.AddScoped<ICourseService, CourseService>();
		services.AddScoped<IFacultyService, FacultyService>();
		services.AddScoped<IEnrollmentService, EnrollmentService>();
        services.AddScoped(typeof(ISaveImageService<>), typeof(SaveImageService<>));

        return services;
	}
}
