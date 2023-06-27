using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using gcsb.ecommerce.domain.Enums;
using gcsb.ecommerce.infrastructure;
using gcsb.ecommerce.webapi.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFilters();
var key = Encoding.ASCII.GetBytes(Settings.Secret);
builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("Cliente", policy => policy.RequireRole(Policies.USER.ToString()));
        options.AddPolicy("Admin", policy => policy.RequireRole(Policies.ADMIN.ToString()));
    });

       builder.Services.AddAuthentication(x =>
       {
           x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
           x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
       }).AddJwtBearer(e =>
       {
           e.RequireHttpsMetadata = false;
           e.SaveToken = true;
           e.TokenValidationParameters = new TokenValidationParameters()
           {
               ValidateIssuerSigningKey = true,
               IssuerSigningKey = new SymmetricSecurityKey(key),
               ValidateIssuer = false,
               ValidateAudience = false,
           };
       });
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>((d, builder) => builder.AddAutofacRegistration());
var app = builder.Build();
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
