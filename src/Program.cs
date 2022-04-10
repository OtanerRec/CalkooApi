using CalkooApi.DTOS;
using CalkooApi.Extensions;
using CalkooApi.Services.Implementations;
using CalkooApi.Services.Interfaces;
using CalkooApi.Services.Strategy;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddFluentValidation(v => v.RegisterValidatorsFromAssemblyContaining<Program>());
builder.Services.AddSingleton<ICalculationService, CalculationService>();
builder.Services.AddSingleton<ICalculator, CalculationThroughPriceIncludedVat>();
builder.Services.AddSingleton<ICalculator, CalculationThroughPriceWithoutVat>();
builder.Services.AddSingleton<ICalculator, CalculationThroughValueTax>();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/calculation",
    (PurchaseRequest purchaseRequest, ICalculationService calculcationService, IValidator<PurchaseRequest> validator) =>
    {
        CalculationThroughPriceIncludedVat calculationThroughPriceIncludeVat = new();
        CalculationThroughPriceWithoutVat calculationBasedOnPricesWithoutVat = new();
        CalculationThroughValueTax calculationBasedOnValueTax = new();

        var result = new CalculationResponse();

        var validationResult = validator.Validate(purchaseRequest);
        if (validationResult.IsValid)
        {
            if (purchaseRequest.Type == CalkooApi.DTOS.Enums.AumontType.Gross)
            {
                CalculationService calculator = new CalculationService(new CalculationThroughPriceIncludedVat());

                result = calculator.Calculate(purchaseRequest);
            }

            if (purchaseRequest.Type == CalkooApi.DTOS.Enums.AumontType.Net)
            {
                CalculationService calculator = new CalculationService(new CalculationThroughPriceWithoutVat());

                result = calculator.Calculate(purchaseRequest);
            }

            if (purchaseRequest.Type == CalkooApi.DTOS.Enums.AumontType.Vat)
            {
                CalculationService calculator = new CalculationService(new CalculationThroughValueTax());

                result = calculator.Calculate(purchaseRequest);
            }

            return Results.Ok(result);
        }

        return Results.ValidationProblem(validationResult.ToDictionary());
    });

app.Run();