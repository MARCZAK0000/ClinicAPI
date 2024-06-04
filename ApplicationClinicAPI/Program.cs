using ApplicationClinicAPI.Authentciation;
using ApplicationClinicAPI.Authorization;
using ApplicationClinicAPI.Entities;
using ApplicationClinicAPI.Middleware;
using ApplicationClinicAPI.Model.CreateAccount;
using ApplicationClinicAPI.Model.DoctorsModel;
using ApplicationClinicAPI.SeederDatabase;
using ApplicationClinicAPI.Services.ServicesDepartment;
using ApplicationClinicAPI.Services.ServicesDoctors;
using ApplicationClinicAPI.Services.ServicesUser;
using ApplicationClinicAPI.Services.ServicesVisits;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog.Web;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var authenticationSettings = new AuthenticationSettings();

builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);
builder.Services.AddSingleton(authenticationSettings);  //Umozliwienie odwolania do klasy z Program.cs do AccountServices.cs do generowania kodu JwtKey
//Authentciation
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "Bearer";
    options.DefaultScheme = "Bearer";
    options.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;  //nie wymuszamy protokolu https 
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
    };
});

// Add services to the container.
builder.Host.UseNLog(); //add nlog to program
builder.Services.AddControllers();
//CORS
builder.Services.AddCors(p=>p.AddPolicy("corspolicy", build =>
{
    build.WithOrigins("*")
    .AllowAnyMethod()
    .AllowAnyHeader();
}));

//Auhtorization
//builder.Services.AddAuthorization(
//    options => options.AddPolicy("MatureUser", builder => builder.AddRequirements(new MinimumAgeRequirement(20)))
//);
builder.Services.AddFluentValidationAutoValidation();   //add validation services
builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection"))); //create db
builder.Services.AddScoped<SeederDb>(); //add db
builder.Services.AddScoped<ErrorHandlingMiddleware>(); //Add middleware
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>(); //password hasher 
builder.Services.AddScoped<IValidator<CreateAcc>, CreateAccValidator>(); //validator to creat account
builder.Services.AddScoped<IValidator<DoctorsDto>, DoctorsDtoValidator>(); //validator to creat account
builder.Services.AddScoped<IValidator<UpdateDoctorDto>, UpdateDoctorDtoValidator>(); //validator to creat account
builder.Services.AddScoped<IUserServices, UserServices>(); //User services
builder.Services.AddScoped<IDepartmentServices, DepartmentServices>(); //User services
builder.Services.AddScoped<IDoctorsServices, DoctorsServices>();
builder.Services.AddScoped<IUserContextServices, UserContextServices>();
builder.Services.AddScoped<IVisitContextServices, VisitContextServices>();
builder.Services.AddScoped<IVisitServices, VisitServices>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<SeederDb>(); //use database seeder to push first seed to database
seeder.Seed();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ErrorHandlingMiddleware>(); //use middleware
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseCors("corspolicy");

app.MapControllers();

app.Run();
