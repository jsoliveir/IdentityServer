using Service.Core.Weather.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddJwtSecurity(builder.Configuration);
builder.Services.AddSwaggerAuthorizationHeader();
builder.Services.AddHealthChecks();
builder.Services.AddControllers();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.UseHealthChecks("/healthz");
app.UseSwaggerBehindGateway();
app.UseJwtAuthentication();
app.MapControllers();
app.Run();
