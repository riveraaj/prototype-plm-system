using PLM.WebAPI.Helper;

var builder = WebApplication.CreateBuilder(args);

//Cors
string MyCors = "MyCors";

builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: MyCors, builder =>
    {
        builder.WithHeaders("*");
        builder.WithOrigins("*");
        builder.WithMethods("*");
    });
});

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddPLMServices();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();
else app.UseMiddleware<CustomExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(MyCors);

app.Run();