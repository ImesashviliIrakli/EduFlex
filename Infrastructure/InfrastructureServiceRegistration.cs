using Application.Interfaces.Cache;
using Application.Interfaces.Facades;
using Application.Interfaces.FileService;
using Application.Interfaces.Services;
using Application.Interfaces.StripeService;
using Application.Settings;
using Infrastructure.Facades;
using Infrastructure.fileService;
using Infrastructure.Redis;
using Infrastructure.Services;
using Infrastructure.StripeService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<IdentitySettings>(options => configuration.GetSection("IdentitySettings").Bind(options));

        // Redis
        services.AddStackExchangeRedisCache(redisOptions =>
        {
            redisOptions.Configuration = configuration.GetConnectionString("Redis");
        });

        services.AddScoped<ICacheService, RedisCacheService>();

        // Service Registration
        services.AddScoped<ITeacherService, TeacherService>();
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<ITeacherCourseService, TeacherCourseService>();
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<IFacultyService, FacultyService>();
        services.AddScoped<IEnrollmentService, EnrollmentService>();
        services.AddScoped<IAssignmentService, AssignmentService>();
        services.AddScoped<IHomeworkService, HomeworkService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped(typeof(IFileService<>), typeof(FileService<>));

        // Facade Registration
        services.AddScoped<IHomeworkFacade, HomeworkFacade>();

        // Stripe Registration
        var stripeSettings = new StripeSettings();
        configuration.GetSection("StripeOptions").Bind(stripeSettings);
        services.AddSingleton(stripeSettings);

        Stripe.StripeConfiguration.ApiKey = stripeSettings.ApiKey;

        services.AddScoped<IStripeService, StripeService.StripeService>();

        return services;
    }
}
