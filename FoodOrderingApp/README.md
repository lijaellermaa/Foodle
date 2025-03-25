# Generate db migration

~~~bash
# Install or update
dotnet tool install --global dotnet-ef
dotnet tool update --global dotnet-ef

# Create migration
dotnet ef migrations add Initial --project App.DAL.EF --startup-project WebApp --context AppDbContext

# Apply migration
dotnet ef database update --project App.DAL.EF --startup-project WebApp --context AppDbContext
~~~

# Generate rest controllers

~~~bash
# Install or update tooling
dotnet tool install --global dotnet-aspnet-codegenerator
dotnet tool update --global dotnet-aspnet-codegenerator

cd WebApp
# MVC
dotnet aspnet-codegenerator controller -m Location -name LocationsController -outDir Controllers -dc AppDbContext -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m Order -name OrdersController -outDir Controllers -dc AppDbContext -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m OrderItem -name OrderItemsController -outDir Controllers -dc AppDbContext -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m Price -name PricesController -outDir Controllers -dc AppDbContext -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m Product -name ProductsController -outDir Controllers -dc AppDbContext -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m ProductType -name ProductTypesController -outDir Controllers -dc AppDbContext -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m Restaurant -name RestaurantsController -outDir Controllers -dc AppDbContext -udl --referenceScriptLibraries -f

# use area
dotnet aspnet-codegenerator controller -m App.Domain.Location -name LocationsController -outDir Areas\Admin\Controllers -dc AppDbContext -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m App.Domain.Order -name OrdersController -outDir Areas\Admin\Controllers -dc AppDbContext -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m App.Domain.OrderItem -name OrderItemsController -outDir Areas\Admin\Controllers -dc AppDbContext -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m App.Domain.Price -name PricesController -outDir Areas\Admin\Controllers -dc AppDbContext -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m App.Domain.Product -name ProductsController -outDir Areas\Admin\Controllers -dc AppDbContext -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m App.Domain.ProductType -name ProductTypesController -outDir Areas\Admin\Controllers -dc AppDbContext -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m App.Domain.Restaurant -name RestaurantsController -outDir Areas\Admin\Controllers -dc AppDbContext -udl --referenceScriptLibraries -f

# Rest API
dotnet aspnet-codegenerator controller -m App.Domain.Location -name LocationsController -outDir ApiControllers -api -dc AppDbContext -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m App.Domain.Order -name OrdersController -outDir ApiControllers -api -dc AppDbContext -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m App.Domain.OrderItem -name OrderItemsController -outDir ApiControllers -api -dc AppDbContext -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m App.Domain.Price -name PricesController -outDir ApiControllers -api -dc AppDbContext -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m App.Domain.Product -name ProductsController -outDir ApiControllers -api -dc AppDbContext -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m App.Domain.ProductType -name ProductTypesController -outDir ApiControllers -api -dc AppDbContext -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m App.Domain.Restaurant -name RestaurantsController -outDir ApiControllers -api -dc AppDbContext -udl --referenceScriptLibraries -f
~~~

Generate Identity UI
~~~bash
cd WebApp
dotnet aspnet-codegenerator identity -dc App.DAL.EF.AppDbContext --userClass AppUser -f
~~~
