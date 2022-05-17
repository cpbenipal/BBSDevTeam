using BBS.Dto;
using BBS.Interactors;
using BBS.Middlewares;
using BBS.Services.Contracts;
using BBS.Services.Repository;
using BBS.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BBS.Swagger.Extensions
{
    public static class ServiceExtensions
    {
        public static IConfiguration? Config { get; set; }         

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()); 
            });
        }

        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {

            });
        }

        public static void ConfigureLoggerService(this IServiceCollection services) 
        {
            services.AddSingleton<ILoggerManager, LoggerManager>(); 
        }


        public static void ConfigureJWTToken(this IServiceCollection services)
        {
            // Initializing the IConfig so that it won't have null value

            Config = BuildConfiguration();
            var key = Encoding.ASCII.GetBytes(Config["AppSettings:Secret"]);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        private static IConfiguration BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                         .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json");

            return builder.Build();
        }

        public static void ConfigureRepositoryWrapper(this IServiceCollection services) 
        {            
            services.AddSingleton<ILoggerManager, LoggerManager>();
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddScoped<ITokenManager, JwtTokenManager>();
            services.AddScoped<IHashManager, HashManager>();
            services.AddScoped<IApiResponseManager, ApiResponseManager>();

            services.AddScoped<RegisterUserInteractor>();
            services.AddScoped<RegisterUserUtils>();

            services.AddScoped<RegisterShareInteractor>();
            services.AddScoped<RegisterShareUtils>();

            services.AddScoped<LoginUserInteractor>();
            services.AddScoped<ForgotPasscodeInteractor>();
            services.AddScoped<SendOTPInteractor>();

            services.AddScoped<GetProfileInformationInteractor>();
            services.AddScoped<GetProfileInformationUtils>();

            services.AddScoped<GetRegisteredSharesInteractor>();
            services.AddScoped<GetRegisteredSharesUtils>();
            
            services.AddScoped<IssueDigitalSharesInteractor>();
            services.AddScoped<IssueDigitalShareUtils>();

            services.AddScoped<GetAllCountriesInteractor>();
            services.AddScoped<GetAllDebtRoundsInteractor>();
            services.AddScoped<GetAllEquityRoundsInteractor>();
            services.AddScoped<GetAllGrantTypesInteractor>();
            services.AddScoped<GetAllNationalitiesInteractor>();
            services.AddScoped<GetAllRestrictionsInteractor>();
            services.AddScoped<GetAllStorageLocationsInteractor>();
            services.AddScoped<GetAllCompaniesInteractor>();
            services.AddScoped<GetAllEmployementTypesInteractor>();
            services.AddScoped<GetIssuedDigitalCertificateInteractor>();


            services.AddScoped<GetAllIssuedSharesInteractor>();
            services.AddScoped<GetIssuedDigitalSharesUtils>();

            services.AddScoped<OfferShareInteractor>();
            services.AddScoped<GetAllOfferedSharesInteractor>();
            services.AddScoped<GetAllOfferedSharesUtils>();

            services.AddScoped<GenerateHtmlCertificate>();



            Config = BuildConfiguration();

            var ConnectionString = Config["AzureStorage:ConnectionString"];
            var TimeOutInMinutes = Config["AzureStorage:TimeOutInMinutes"];
            var ConsumerName = Config["AzureStorage:ConsumerName"];

            var azureFileUploader = new AzureBlobFileUploadService(
                connectionString: ConnectionString,
                containerName: ConsumerName,
                timeSpan: int.Parse(TimeOutInMinutes)
            );

            services.AddScoped<IFileUploadService>(uploader => azureFileUploader);
            services.AddTransient<IEmailSender, SendGridEmailSender>();

            var TwilioSID = Config["Twilio:SID"];
            var TwilioApiKey = Config["Twilio:ApiKey"];
            var twilioSMSSender = new TwilioSMSSender(TwilioSID, TwilioApiKey);
            services.AddTransient<ISMSSender>(sms => twilioSMSSender);

            services.Configure<SendGridEmailSenderOptions>(options =>
            {
                options.ApiKey = Config["ExternalProviders:SendGrid:ApiKey"];
                options.SenderEmail = Config["ExternalProviders:SendGrid:SenderEmail"];
                options.SenderName = Config["ExternalProviders:SendGrid:SenderName"];
            });
           
            services.AddTransient<SwaggerAuthenticationMiddleware>();
            
        }
    }


    public class CustomJsonConverterForType : JsonConverter<Type>
    {
        public override Type Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
            )
        {
            // Caution: Deserialization of type instances like this 
            // is not recommended and should be avoided
            // since it can lead to potential security issues.

            // If you really want this supported (for instance if the JSON input is trusted):
            // string assemblyQualifiedName = reader.GetString();
            // return Type.GetType(assemblyQualifiedName);
            throw new NotSupportedException();
        }

        public override void Write(
            Utf8JsonWriter writer,
            Type value,
            JsonSerializerOptions options
            )
        {
            string assemblyQualifiedName = value.AssemblyQualifiedName!;
            // Use this with caution, since you are disclosing type information.
            writer.WriteStringValue(assemblyQualifiedName);
        }
    }
}


