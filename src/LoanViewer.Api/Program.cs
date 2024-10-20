using LoanViewer.Api.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<LoanCalculationService>();
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.MapGet(
        "/calculate-loan",
        (
            [FromQuery] double amount,
            [FromQuery] double rate,
            [FromQuery] int terms,
            [FromServices] LoanCalculationService loanService
        ) =>
        {
            loanService.SetLoanDetails(amount, rate, terms);
            return Results.Ok(loanService.AmortizationTable);
        }
    )
    .WithName("GetLoanCalculation")
    .WithOpenApi();

app.Run();

