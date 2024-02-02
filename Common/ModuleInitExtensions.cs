using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Commons;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ModuleInitExtensions
    {
		public static IServiceCollection RunModuleInitializers(this IServiceCollection services,
		 IEnumerable<Assembly> assemblies)
		{
			foreach (var asm in assemblies)
			{
				Type[] types = asm.GetTypes();
				var moduleInitializerTypes = types.Where(t => !t.IsAbstract && typeof(IModuleInit).IsAssignableFrom(t));
				foreach (var implType in moduleInitializerTypes)
				{
					var initializer = (IModuleInit?)Activator.CreateInstance(implType);
					if (initializer == null)
					{
						throw new ApplicationException($"Cannot create ${implType}");
					}
					initializer.Initialize(services);
				}
			}
			return services;
		}
	}
}
