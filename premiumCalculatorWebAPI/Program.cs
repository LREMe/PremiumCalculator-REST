using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using TranzactProgrammingChallengeWebAPI.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "DataSource=Data\\app.db";

//builder.
//builder.Services.AddSqlite<PremiumRolDbContext>

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


/// <summary>
/// adding logging
/// </summary>
var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();

});

ILogger logger = loggerFactory.CreateLogger<ILogger>();



/// <summary>
///  SQL Lite Support
/// </summary>
builder.Services.AddSqlite<PremiumRolDbContext>(connectionString);

/// <summary>
/// CORS
/// </summary>
var MyAllowSpecificOrigins = "_AllowSpecificOrigins";

/// <summary>
/// to configure the origin, go to the appsettings.json section
/// </summary>
var WebAddress = builder.Configuration.GetValue<string>("WebClientConfig:WebAddress");

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins(WebAddress)
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                      });
});

//end

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

/// <summary>
/// Configure CORS
/// </summary>
app.UseCors(MyAllowSpecificOrigins);


app.MapGet("/PlanList", async (PremiumRolDbContext db) => await db.Plans.ToListAsync());
app.MapGet("/PeriodList", async (PremiumRolDbContext db) => await db.Periods.ToListAsync());
/// <summary>
/// Select the state list, with an ascending order
/// </summary>
app.MapGet("/StateList", async (PremiumRolDbContext db) =>
{
    var select = from state in db.States
                 orderby state.StateName ascending
                 select new
                 {
                     state.StateId,
                     state.StateName
                 };
    return Results.Ok(await select.ToListAsync());
}
);


/// <summary>
/// Get the premium information
/// </summary>
app.MapPut("/Premium", async ([FromBody] PremiumQ q,
                               [FromServices] PremiumRolDbContext db) =>
{



    string month = "";
    var today = DateTime.Today;
    // Calculate the age.
    var age = today.Year - q.DoB.Year;
    //not the same agen
    if (age != q.Age)
    {
        return Results.NotFound();
    }

    //get the month
    CultureInfo ci = new CultureInfo("en-US");
    month = q.DoB.ToString("MMMM", ci);

    try
    {
        //Usage of Entity framework to make it independent from the DB
        //If I was going to use a defined database, I will use stored procedures to allow the database to optimize the query.
        var select = from premiumRol in db.PremiumRols
                     where (premiumRol.MonthOfBirth.Equals(month) || premiumRol.MonthOfBirth.Equals("*"))
                      && (premiumRol.State.Equals(q.State) || premiumRol.State.Equals("*"))
                      && (premiumRol.Plan.Contains(q.Plan) || premiumRol.Plan.Equals("*"))
                      && (
                              (
                              premiumRol.Age.Contains("-")
                              &&
                              String.Compare(q.Age.ToString(), premiumRol.Age.Substring(0, premiumRol.Age.IndexOf("-"))) >= 0
                              &&
                              String.Compare(q.Age.ToString(), premiumRol.Age.Substring(premiumRol.Age.IndexOf("-") + 1)) <= 0
                              )
                              ||
                              (
                              premiumRol.Age.EndsWith("+")
                              &&
                              String.Compare(q.Age.ToString(), premiumRol.Age.Substring(0, premiumRol.Age.IndexOf("+"))) >= 0
                              )
                      )
                     select new
                     {
                         carrier = premiumRol.Carrier,
                         premium = premiumRol.Premium
                     };
        logger.LogInformation("Premium success!");
        return Results.Ok(await select.ToListAsync());
    }
    catch (Exception e)
    {
        logger.LogError(e, "Premium Exception");
        return Results.NotFound();
    }
}
);

app.Run();
