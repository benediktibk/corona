using System.Collections.Generic;

namespace Backend
{
    public interface ISettings
    {
        IReadOnlyList<string> AdminUsers { get; }
        string DatabaseConnectionString { get; }
        string GitRepo { get; }
        string LocalPath { get;  }
        bool SvgCompressed { get; }
    }
}