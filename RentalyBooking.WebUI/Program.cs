using RentalyBooking.BusinessLayer.Abstract;
using RentalyBooking.BusinessLayer.Concrete;
using RentalyBooking.DataAccessLayer.Abstract;
using RentalyBooking.DataAccessLayer.Concrete;
using RentalyBooking.DataAccessLayer.EntityFramework;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Registerlar

builder.Services.AddHttpClient<IFuelPriceServiceDal, ScraperFuelPriceServiceDal>();

builder.Services.AddScoped<IFuelPriceService, FuelPriceManager>();

builder.Services.AddHostedService<FuelPriceScraperWorker>();

builder.Services.AddDbContext<RentalyContext>();

builder.Services.AddScoped<IBrandDal, EfBrandDal>();
builder.Services.AddScoped<IBrandService, BrandManager>();

builder.Services.AddScoped<ICarDal, EfCarDal>();
builder.Services.AddScoped<ICarService, CarManager>();

builder.Services.AddScoped<IBranchDal, EfBranchDal>();
builder.Services.AddScoped<IBranchService, BranchManager>();

builder.Services.AddScoped<ICarModelDal, EfCarModelDal>();
builder.Services.AddScoped<ICarModelService, CarModelManager>();

builder.Services.AddScoped<ICategoryDal, EfCategoryDal>();
builder.Services.AddScoped<ICategoryService,  CategoryManager>();

builder.Services.AddScoped<ICustomerDal, EfCustomerDal>();
builder.Services.AddScoped<ICustomerService, CustomerManager>();

builder.Services.AddScoped<IRentalyDal, EfRentalyDal>();
builder.Services.AddScoped<IRentalyService, RentalyManager>();

builder.Services.AddScoped<IFuelTypeDal, EfFuelTypeDal>();
builder.Services.AddScoped<IFuelTypeService, FuelTypeManager>();

builder.Services.AddScoped<IOurFeatureDal, EfOurFeatureDal>();
builder.Services.AddScoped<IOurFeatureService, OurFeatureManager>();

builder.Services.AddScoped<IProcessDal, EfProcessDal>();
builder.Services.AddScoped<IProcessService, ProcessManager>();

builder.Services.AddScoped<IGeneralFeatureDal, EfGeneralFeatureDal>();
builder.Services.AddScoped<IGeneralFeatureService, GeneralFeatureManager>();

builder.Services.AddScoped<IFAQDal, EfFAQDal>();
builder.Services.AddScoped<IFAQService, FAQManager>();

builder.Services.AddScoped<ICouponDal, EfCouponDal>();
builder.Services.AddScoped<ICouponService, CouponManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/Error/Error404", "?code={0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
