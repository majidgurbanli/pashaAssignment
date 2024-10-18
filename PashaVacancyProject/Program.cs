using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PashaVacancyProject.Domain.DInfrastucture;
using PashaVacancyProject.Logic.FLogic;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<VacancyBusinessLogic>();
builder.Services.AddScoped<ApplicationBusinessLogic>();
builder.Services.AddScoped<AdminBusinessLogic>();
builder.Services.AddScoped<QuestionBusinessLogic>();
builder.Services.AddScoped<ChoiceBusinessLogic>();
builder.Services.AddScoped<UserBusinessLogic>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
  .AddJwtBearer(options =>
  {
      options.TokenValidationParameters = new TokenValidationParameters
      {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          ValidIssuer = "test",
          ValidAudience ="test",
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("a9380f3df9592842368e73e0bf39c5e3e9ef8f6b76992745f3d998b088ef360f9f85d313d61ab97579006c3bbd1855063ca2fd550251cf730368c5989ce8865fe73a6ff648bce3bfe0a8d62f0bfbfc01fc7a5b065ea0de0f11f1cc744efd67f587d8a510c84371dedb8eb29be83c68d94b683eb7db21e115994bf8e940a13bb3fc96dab729f67b04b30a481ac84353c69687f8643af2e8b352077f6971740423eda99707adaec1f593d9d1f4804fdc717c88b341acf95b6354353eb411429569295356cc8455ba928d2faad5d1206bdc8b1a7010adb41118abd062da210ce8a51d25e61b6d3ebc39a2cc40168edad0b4ab2ae124c0a80d72103daae7171a88c9"))
      };
  });
BoxDbContent.SetConnectionString();

builder.Services.AddDbContext<BoxDbContent>(ServiceLifetime.Scoped);
builder.Services.AddAutoMapper(Conf =>
{

    var ReferencedProfiles = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(s => Assembly.Load(s.Name)).SelectMany(s => s.GetTypes()
                                                          .Where(type => type.BaseType == typeof(Profile)).ToList()).ToList();


    foreach (var Type in ReferencedProfiles)
    {
        dynamic TypeInstance = Activator.CreateInstance(Type);

        Conf.AddProfile(TypeInstance);
    }


    var CurrentProfiles = Assembly.GetExecutingAssembly().GetTypes()
                                                           .Where(type => type.BaseType == typeof(Profile)).ToList();
    foreach (var Type in CurrentProfiles)
    {
        dynamic TypeInstance = Activator.CreateInstance(Type);

        Conf.AddProfile(TypeInstance);
    }

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
