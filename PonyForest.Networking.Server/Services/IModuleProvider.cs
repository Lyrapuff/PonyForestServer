using System.Collections.Generic;

namespace PonyForestServer.Core.Services
{
    internal interface IModuleProvider
    {
        List<Modules.ServerModule> Modules { get; }
        void Load();
    }
}