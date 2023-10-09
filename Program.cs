using System.Reflection;
using System.Text;
using API_Project.Context;
using API_Project.Controllers;
using API_Project.Interfaces;
using API_Project.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<IRestaurant,RestaurantRepository>();
builder.Services.AddScoped<ICustomer, CustomerRepository>();
builder.Services.AddScoped<IAddress, AddressRepository>();
builder.Services.AddScoped<IFoodCategory, FoodCategoryRepository>();
builder.Services.AddScoped<IFoodItem,FoodItemRepository>();
builder.Services.AddScoped<IFoodOrder,FoodOrderRepository>();
builder.Services.AddScoped<ILogin, LoginRepository>();
builder.Services.AddScoped<IUser,UserRepository>();

//Add authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
     .AddJwtBearer(options =>
     {
          options.TokenValidationParameters = new TokenValidationParameters
          {
               ValidateIssuer = true,
               ValidateAudience = true,
               ValidateLifetime = true,
               ValidateIssuerSigningKey = true,
               ValidIssuer = builder.Configuration["Jwt:Issuer"],
               ValidAudience = builder.Configuration["Jwt:Audience"],
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))

          };
     });
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
     c.SwaggerDoc("v1", new OpenApiInfo
     {
          Title = "Demo Jwt",
          Version = "v1"
     });

     c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
     {
          In = ParameterLocation.Header,
          Description = "Insert Jwt with Beard intro field",
          Name = "Authorization",
          Type = SecuritySchemeType.ApiKey
     });
     c.AddSecurityRequirement(new OpenApiSecurityRequirement{
     {
          new OpenApiSecurityScheme
          {
               Reference = new OpenApiReference
               {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
               }
          },
          Array.Empty<string>()    
     }});
     //Api Documentation
     var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
     var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
     c.IncludeXmlComments(xmlPath);

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
     app.UseSwagger();
     app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
