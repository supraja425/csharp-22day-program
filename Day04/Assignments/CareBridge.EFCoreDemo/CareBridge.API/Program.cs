using CareBridge.EFCoreDemo.Models.Generated;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Register EF Core DbContext.
// ASP.NET Core will automatically create and inject it when needed.
builder.Services.AddDbContext<CareBridgeScaffoldContext>();

// Add Swagger support.
// Swagger gives us a testing screen for APIs.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Allow Vue.js running on another port
// to call this API from the browser.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Enable Swagger.
app.UseSwagger();
app.UseSwaggerUI();

// Enable CORS.
app.UseCors();

// Simple health-check endpoint.
app.MapGet("/", () =>
{
    return "CareBridge API is running";
});

// Return first 20 patients.
// EF Core converts this LINQ query into SQL.
app.MapGet("/api/patients",
    (CareBridgeScaffoldContext db) =>
    {
        return db.Patients

                 // Select only columns we need.
                 .Select(p => new
                 {
                     p.PatientId,
                     p.FullName,
                     p.City,
                     p.IsActive
                 })

                 // Return only first 20 rows.
                 .Take(20)

                 // Execute query.
                 .ToList();
    });

app.MapGet("/api/patients/search",
    (CareBridgeScaffoldContext db, string? city, bool? isActive) =>
    {
        var query = db.Patients.AsQueryable();

        // Filter by city (if provided)
        if (!string.IsNullOrEmpty(city))
        {
            query = query.Where(p => p.City == city);
        }

        // Filter by active status (if provided)
        if (isActive.HasValue)
        {
            query = query.Where(p => p.IsActive == true);
        }

        return query
            .Select(p => new
            {
                p.PatientId,
                p.FullName,
                p.City,
                p.IsActive
            })
            
            .ToList();
    });

app.MapGet("/api/analytics/department-load",
    (CareBridgeScaffoldContext db) =>
    {
        var cutoffDate = DateTime.Now.AddDays(-60);

        var data =
            (from e in db.Encounters
             join d in db.Departments
                 on e.DepartmentId equals d.DepartmentId
             where e.AdmitDate >= cutoffDate 
             group e by d.Name into g
             select new
             {
                 departmentName = g.Key,
                 inpatient = g.Count(x => x.EncounterType == "Inpatient"),
                 outpatient = g.Count(x => x.EncounterType == "Outpatient"),
                 ed = g.Count(x => x.EncounterType == "ED"),
                 total = g.Count()
             })
            .OrderByDescending(x => x.total)
            .ToList();

        var grandTotal = new
        {
            departmentName = "Grand Total",
            inpatient = data.Sum(x => x.inpatient),
            outpatient = data.Sum(x => x.outpatient),
            ed = data.Sum(x => x.ed),
            total = data.Sum(x => x.total)
        };

        return new
        {
            fromDate = cutoffDate,
            departments = data,
            grandTotal = grandTotal
        };
    });

app.Run();
