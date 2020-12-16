using Microsoft.Extensions.DependencyInjection;
using System;

namespace DataStorage
{
    public static class DbInitializer
    {
		public static void Initialize(IServiceProvider services)
		{
			using (var scope = services.CreateScope())
			{
				var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
				context.Database.EnsureDeleted();
				context.Database.EnsureCreated();
			}
		}
	}
}
