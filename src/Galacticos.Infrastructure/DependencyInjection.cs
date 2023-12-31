using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Galacticos.Infrastructure.Data;
using Galacticos.Application.Persistence.Contracts;
using Galacticos.Infrastructure.Persistence.Repositories.UserRepo;
using Galacticos.Persistence.Repositories;
using Galacticos.Infrastructure.Persistence.Repositories;
using Galacticos.Infrastructure.Repositories.RelationRepo;
using Galacticos.Infrastructure.Persistence.Repositories.PostRepo;
using Galacticos.Application.Common.Interface.Authentication;
using Galacticos.Infrastructure.Authentication;
using Galacticos.Application.Common.Interface.Services;
using Galacticos.Infrastructure.Services;
using Galacticos.Infrastructure.Persistence.Repositories.CommentRepo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.Text;
using Galacticos.Infrastructure.Persistence.Repositories.PostTagRepo;
using Galacticos.Infrastructure.Mail;
using Galacticos.Infrastructure.Persistence.Repositories.NotificationRepo;
using Microsoft.VisualBasic;
using Galacticos.Application.Cloudinary;
using Galacticos.Application.Services.ImageUpload;
using Galacticos.Application.Services.Authentication;
using Galacticos.Infrastructure.Configuration;
using Galacticos.Application.Services.OpenAI;

namespace Galacticos.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
        {

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApiDbContext>(opt =>
    opt.UseNpgsql(connectionString));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<INewsFeedRepository, NewsFeedRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IRelationRepository, RelationRepository>();
            services.AddScoped<ILikeRepository, LikeRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IPostTagRepository, PostTagRepository>();

            services.AddAuth(configuration);
            services.AddScoped<IDateTimeProvider, DateTimeProvider>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped<IJwtTokenValidation, JwtTokenValidation>();
            services.AddScoped<INotificationRepository, NotificationRepo>();
            
            services.AddScoped<IPasswordHashService, PasswordHashService>();

            // Setup Cloudinary
            services.Configure<CloudinarySettings>(configuration.GetSection(CloudinarySettings.SectionName));
            services.AddTransient<ICloudinaryService, CloudinaryService>();

            // Setup OpenAI
            services.Configure<OpenAIConfig>(configuration.GetSection("OpenAI"));
            services.AddTransient<IOpenAIService, OpenAiService>();

            return services;
        }

        public static IServiceCollection AddAuth(
        this IServiceCollection services,
        ConfigurationManager configuration
    )
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(JwtSettings.SectionName, jwtSettings);

            services.AddSingleton(Options.Create(jwtSettings));
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
                };
            });
            return services;
        }
    }
}