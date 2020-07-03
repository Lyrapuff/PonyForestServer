using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using PonyForestServer.Core.Modules;
using PonyForestServer.Core.Tools;

namespace PonyForestServer.Core.Services.Implementation
{
    internal class ModuleProvider : IModuleProvider
    {
        private readonly Logger _logger;
        public List<ServerModule> Modules { get; }

        public ModuleProvider(ILoggerProvider loggerProvider)
        {
            _logger = loggerProvider.GetLogger("Server");
            Modules = new List<ServerModule>();

            Load();
        }
        
        public void Load()
        {
            _logger.LogInformation("Loading modules");

            string path = "Modules";
            
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            
            DirectoryInfo directory = new DirectoryInfo(path);

            int count = 0;

            try
            {
                foreach (FileInfo file in directory.GetFiles("*.dll"))
                {
                    Assembly assembly = Assembly.LoadFile(file.FullName);
                    LoadAssembly(assembly);

                    count++;
                }
                
                DirectoryInfo directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());

                foreach (FileInfo file in directoryInfo.GetFiles("*CoreModule.dll"))
                {
                    Assembly assembly = Assembly.LoadFile(file.FullName);
                    LoadAssembly(assembly);
                    
                    count++;
                }
            }
            catch (Exception e)
            {
                _logger.LogFailure(e);
            }

            _logger.LogInformation($"Loaded {count} modules");
        }

        private void LoadAssembly(Assembly assembly)
        {
            List<Type> modules = assembly
                .GetTypes()
                .Where(t => t.BaseType == typeof(ServerModule))
                .ToList();

            foreach (Type moduleType in modules)
            {
                if (ActivatorUtilities.CreateInstance(ServerSetup.ServiceProvider, moduleType) is ServerModule module)
                {
                    module.OnEnabled();
                    Modules.Add(module);
                }
            }
        }
    }
}