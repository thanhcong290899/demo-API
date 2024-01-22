using AutoMapper;
using DeMoAuthen.Data;
using DeMoAuthen.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddIdentity<ApplicationUser,IdentityRole>().AddEntityFrameworkStores<BookDbContext>().AddDefaultTokenProviders();

builder.Services.AddEntityFrameworkNpgsql().AddDbContext<BookDbContext>(otp => otp.UseNpgsql(builder.Configuration.GetConnectionString("BookStore")));

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IBookRepository, BookRepository>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(otp =>
{
    //cho phép lưu token sau khi xác thực
    otp.SaveToken = true;
    //cho phép xác thực qua HTTP
    otp.RequireHttpsMetadata = false;
    //đối tượng chứa các tham số sd để xác thực token
    otp.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        //ValidateIssuer và ValidateAudience: Cho biết liệu Issuer (người phát hành) và Audience (đối tượng sử dụng token) có cần được kiểm tra hay không
        ValidateIssuer = true,
        ValidateAudience = true,
        //Các giá trị hợp lệ cho Audience và Issuer, thường đọc từ cấu hình ứng dụng.
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        //  sử dụng để ký và xác minh chữ ký của JWT trong quá trình tạo và xác thực JWT.
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:IssuerSigningKey"]))
    };
});
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
