using RFD.Entities.Enum;

namespace RFD.WebUI.Infrastructer.Extensions
{
    public interface IRFDStarterExtension
    {
        bool Start(string computerName, int transectionId);
        bool Start(string computerName, ApplicationType applicationType);
    }
}