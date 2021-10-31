using System.Collections.Generic;

namespace Backend {
    public interface ISettings {
        string DatabaseConnectionString { get; }
        string GitRepo { get; }
        string LocalPath { get; }
        bool SvgCompressed { get; }
    }
}