using RFD.Entities.Enum;

namespace RFD.Server.Infrastructer.Extensions
{
    public interface IRFDStarterExtension
    {
       // bool Start(string computerName, string appPath, string copyPath, ApplicationType type);
        bool Start(string computerName, ApplicationType applicationType);
    }
}