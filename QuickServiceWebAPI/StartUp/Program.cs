using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using QuickServiceWebAPI.Hubs;
using QuickServiceWebAPI.Hubs.Implements;
using QuickServiceWebAPI.Middlewares;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Repositories.Implements;
using QuickServiceWebAPI.Seeds;
using QuickServiceWebAPI.Services;
using QuickServiceWebAPI.Services.Authentication;
using QuickServiceWebAPI.Services.Implements;
using QuickServiceWebAPI.Utilities;

var builder = WebApplication.CreateBuilder(args);
//Get connection string from appsettings
var connection = String.Empty;

var localConnection = String.Empty;
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.Development.json");
    connection = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
    localConnection = builder.Configuration.GetConnectionString("SQL_SERVER_CONNECTIONSTRING");

}
else
{
    connection = Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING");
    localConnection = Environment.GetEnvironmentVariable("SQL_SERVER_CONNECTIONSTRING");
}



// Add services to the container.
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers().AddNewtonsoftJson(
          options =>
          {
              options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
          });
builder.Services.AddCors();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
 {
     {
           new OpenApiSecurityScheme
             {
                 Reference = new OpenApiReference
                 {
                     Type = ReferenceType.SecurityScheme,
                     Id = "Bearer"
                 }
             },
             new string[] {}
     }
 });
});
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));
builder.Services.Configure<AzureStorageConfig>(builder.Configuration.GetSection("AzureStorageConfig"));

//add signalR
builder.Services.AddSignalR(opt => opt.EnableDetailedErrors = true);
builder.Services.AddSingleton<IUserIdProvider, UserCustomProvider>();

//Add service scoped
builder.Services.AddScoped<IAssetService, AssetService>();
builder.Services.AddScoped<IAssetRepository, AssetRepository>();
builder.Services.AddScoped<IAssetAssignmentService, AssetAssignmentService>();
builder.Services.AddScoped<IAssetAssignmentRepository, AssetAssignmentRepository>();
builder.Services.AddScoped<IAssetHistoryService, AssetHistoryService>();
builder.Services.AddScoped<IAssetHistoryRepository, AssetHistoryRepository>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<IServiceTypeRepository, ServiceTypeRepository>();
builder.Services.AddScoped<IServiceTypeService, ServiceTypeService>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<IServiceCategoryRepository, ServiceCategoryRepository>();
builder.Services.AddScoped<IServiceCategoryService, ServiceCategoryService>();
builder.Services.AddScoped<IServiceDeskHourRepository, ServiceDeskHourRepository>();
builder.Services.AddScoped<IServiceDeskHourService, ServiceDeskHourService>();
builder.Services.AddScoped<IServiceItemRepository, ServiceItemRepository>();
builder.Services.AddScoped<IServiceItemService, ServiceItemService>();
builder.Services.AddScoped<IBusinessHourRepository, BusinessHourRepository>();
builder.Services.AddScoped<IBusinessHourService, BusinessHourService>();
builder.Services.AddScoped<ISlaRepository, SlaRepository>();
builder.Services.AddScoped<ISlaService, SlaService>();
builder.Services.AddScoped<ISlametricRepository, SlametricRepository>();
builder.Services.AddScoped<ISlametricService, SlametricService>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IWorkflowRepository, WorkflowRepository>();
builder.Services.AddScoped<IWorkflowService, WorkflowService>();
builder.Services.AddScoped<IWorkflowTaskRepository, WorkflowTaskRepository>();
builder.Services.AddScoped<IWorkflowTaskService, WorkflowTaskService>();
builder.Services.AddScoped<IWorkflowAssignmentRepository, WorkflowAssignmentRepository>();
builder.Services.AddScoped<IWorkflowAssignmentService, WorkflowAssignmentService>();
builder.Services.AddScoped<IWorkflowTransitionRepository, WorkflowTransitionRepository>();
builder.Services.AddScoped<IWorkflowTransitionService, WorkflowTransitionService>();
builder.Services.AddScoped<IYearlyHolidayListRepository, YearlyHolidayListRepository>();
builder.Services.AddScoped<IYearlyHolidayListService, YearlyHolidayListService>();
builder.Services.AddScoped<IAttachmentRepository, AttachmentRepository>();
builder.Services.AddScoped<IAttachmentService, AttachmentService>();
builder.Services.AddScoped<ICustomFieldRepository, CustomFieldRepository>();
builder.Services.AddScoped<ICustomFieldService, CustomFieldService>();
builder.Services.AddScoped<IServiceItemCustomFieldRepository, ServiceItemCustomFieldRepository>();
builder.Services.AddScoped<IServiceItemCustomFieldService, ServiceItemCustomFieldService>();
builder.Services.AddScoped<IRequestTicketRepository, RequestTicketRepository>();
builder.Services.AddScoped<IRequestTicketService, RequestTicketService>();
builder.Services.AddScoped<IRequestTicketExtRepository, RequestTicketExtRepository>();
builder.Services.AddScoped<IRequestTicketExtService, RequestTicketExtService>();
builder.Services.AddScoped<IRequestTicketHistoryRepository, RequestTicketHistoryRepository>();
builder.Services.AddScoped<IRequestTicketHistoryService, RequestTicketHistoryService>();
builder.Services.AddScoped<IChangeRepository, ChangeRepository>();
builder.Services.AddScoped<IChangeService, ChangeService>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationService, NotificationService>();

builder.Services.AddLazyResolution();

builder.Services.AddScoped<IProblemRepository, ProblemRepository>();
builder.Services.AddScoped<IProblemService, ProblemService>();
builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IQueryRepository, QueryRepository>();
builder.Services.AddScoped<IQueryService, QueryService>();
builder.Services.AddScoped<IJWTUtils, JWTUtils>();
builder.Services.AddScoped<IDbInitializer, DbInitializer>();


//Add db context to sql database
builder.Services.AddDbContext<QuickServiceContext>(options =>
    options.UseSqlServer(connection));

//Background jobs configuration
builder.Services.AddHangfire(configuration => configuration
       .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
       .UseSimpleAssemblyNameTypeSerializer()
       .UseRecommendedSerializerSettings()
       .UseSqlServerStorage(connection, new SqlServerStorageOptions
       {
           CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
           SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
           QueuePollInterval = TimeSpan.Zero,
           UseRecommendedIsolationLevel = true,
           DisableGlobalLocks = true
       }));
builder.Services.AddHangfireServer();

//Add authen and author
builder.Services.AddAuthentication(UserAuthenticationHandler.Schema)
    .AddScheme<UserAuthenticationOptions, UserAuthenticationHandler>(UserAuthenticationHandler.Schema, null);

builder.Services.AddAuthorization();
builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHangfireDashboard("/hangfire");
}

app.UseHttpsRedirection();

// global error handler
app.UseMiddleware<ErrorHandlerMiddleware>();

// custom jwt auth middleware
app.UseMiddleware<AuthenticateHubMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.UseCors(x => x
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
           .SetIsOriginAllowed(origin => true));


//Do background jobs
var crons = "0 */12 * * *";
RecurringJob
    .AddOrUpdate<IRequestTicketService>("ticket-job",
    job => job.UpdateTicketStateJobAsync(), crons);

//Seed database
SeedDatabase();

//hub signalR
app.MapHub<NotificationHub>("/hub/notify");

app.MapControllers();

app.Run();

async void SeedDatabase() //can be placed at the very bottom under app.Run()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.SeedPermissions();
        dbInitializer.SeedServiceCategories();
        dbInitializer.SeedSla();
        await dbInitializer.SeedDefaultAdmin();
    }
}
