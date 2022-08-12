using BBS.Dto;
using BBS.Interactors;
using BBS.Middlewares;
using BBS.Models;
using BBS.Services.Contracts;
using BBS.Services.Repository;
using BBS.Utils;
using EmailSender;
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
            Config = BuildConfiguration();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .WithOrigins(Config["Origin:BaseUrl"])
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

                x.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("IS-TOKEN-EXPIRED", "true");
                        }
                        return Task.CompletedTask;
                    }
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

            services.AddScoped<RegisterShareInteractor>();

            services.AddScoped<AuthInteractor>();
            services.AddScoped<ForgotPasscodeInteractor>();
            services.AddScoped<SendOtpInteractor>();

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
            services.AddScoped<GetDigitalCertificateOfIssuedShareInteractor>();
            services.AddScoped<GetAllCertificatesIssuedByUserInteractor>();
            services.AddScoped<RefreshTokenInteractor>();
            services.AddScoped<GetOfferTimeLimitsInteractor>();
            services.AddScoped<GetPrivatelyOfferedShareInteractor>();
            services.AddScoped<GetBusraFeeInteractor>();
            services.AddScoped<GetAllPaymentTypesInteractor>();

            services.AddScoped<GetAllIssuedSharesInteractor>();
            services.AddScoped<GetIssuedDigitalSharesUtils>();

            services.AddScoped<OfferShareInteractor>();
            services.AddScoped<GetAllOfferedSharesInteractor>();
            services.AddScoped<GetAllOfferedSharesUtils>();

            services.AddScoped<GenerateHtmlCertificate>();
            services.AddScoped<EmailHelperUtils>();


            services.AddScoped<OfferPaymentInteractor>();
            services.AddScoped<OfferPaymentUtils>();


            services.AddScoped<GetAllOfferPaymentsInteractor>();
            services.AddScoped<GetCompaniesWithShareOfferedInteractor>();
            services.AddScoped<BidShareInteractor>();

            services.AddScoped<GetAllBidSharesInteractor>();
            services.AddScoped<GetBidShareUtils>();

            services.AddScoped<GetAllInvestorsDetailsInteractor>();
            services.AddScoped<GetAllShareDetailsInteractor>();
            services.AddScoped<ChangeUserStatusToCompletedInteractor>();
            services.AddScoped<ChangeShareStatusToCompletedInteractor>();
            services.AddScoped<GetAllBidsForShareInteractor>();

            services.AddScoped<GetOfferedShareWithBidInformationInteractor>();
            services.AddScoped<GetOfferedShareWithBidInformationUtils>();

            services.AddScoped<GetAllCategoriesInteractor>();
            services.AddScoped<GetSecondaryOfferDataInteractor>();
            services.AddScoped<GetPrimaryOfferDataInteractor>();
            services.AddScoped<UpdateSecondaryOfferContentInteractor>();
            services.AddScoped<GetCategoriesUtils>();

            services.AddScoped<BidOnPrimaryOfferInteractor>();
            services.AddScoped<AddPrimaryOfferContentInteractor>();
            services.AddScoped<UpdatePrimaryOfferContentInteractor>();
            services.AddScoped<ChangePrimaryShareStatusToCompletedInteractor>();

            services.AddScoped<GetAllBidsOnPrimaryOfferInteractor>();
            services.AddScoped<GetBidOnPrimaryOfferUtils>();
            services.AddScoped<GetBursaFeesUtil>();
         
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
            services.AddTransient<IEmailSender, MailGunEmailSender>();
            services.AddTransient<INewEmailSender, NewEmailSender>();

            var TwilioSID = Config["Twilio:SID"];
            var TwilioApiKey = Config["Twilio:ApiKey"];
            var twilioSMSSender = new TwilioSmsSender(TwilioSID, TwilioApiKey);
            services.AddTransient<ISmsSender>(sms => twilioSMSSender);

            services.Configure<MailGunSenderOptions>(options =>
            {
                options.ApiKey = Config["ExternalProviders:MailGun:ApiKey"];
                options.BaseUri = Config["ExternalProviders:MailGun:BaseUri"];
                options.RequestUri = Config["ExternalProviders:MailGun:RequestUri"];
                options.From = Config["ExternalProviders:MailGun:From"];
            });

            services.Configure<EmailHelperModel>(options =>
            {
                options.EmailProvider = Config["ExternalProviders:EmailHelperModel:EmailProvider"];
                options.EmailFrom = Config["ExternalProviders:EmailHelperModel:EmailFrom"];
                options.Password = Config["ExternalProviders:EmailHelperModel:Password"];
                options.User = Config["ExternalProviders:EmailHelperModel:User"];
                options.PortNumber = Config["ExternalProviders:EmailHelperModel:PortNumber"];
                options.AdminEmail = Config["ExternalProviders:EmailHelperModel:AdminEmail"];
            });
            services.AddTransient<SwaggerAuthenticationMiddleware>();
            services.Configure<SwaggerAccount>(options =>
            {
                options.UserName = Config["SwaggerAccount:UserName"];
                options.Password = Config["SwaggerAccount:Password"];
            });

           

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
            throw new NotSupportedException();
        }

        public override void Write(
            Utf8JsonWriter writer,
            Type value,
            JsonSerializerOptions options
            )
        {
            string assemblyQualifiedName = value.AssemblyQualifiedName!;
            writer.WriteStringValue(assemblyQualifiedName);
        }
    }
}


