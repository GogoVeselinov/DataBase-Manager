using System.IO;
using System.Security.AccessControl;

public static class FilePermissionHelper
{
    public static void GrantWritePermission(string directoryPath, string sqlServiceAccount = "NT Service\\MSSQLSERVER")
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
        DirectorySecurity directorySecurity = directoryInfo.GetAccessControl();

        FileSystemAccessRule accessRule = new FileSystemAccessRule(
            sqlServiceAccount,
            FileSystemRights.Write | FileSystemRights.ReadData | FileSystemRights.CreateFiles,
            InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
            PropagationFlags.None,
            AccessControlType.Allow);

        directorySecurity.AddAccessRule(accessRule);
        directoryInfo.SetAccessControl(directorySecurity);
    }

    public static void GrantReadPermission(string directoryPath, string sqlServiceAccount = "NT Service\\MSSQLSERVER")
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
        DirectorySecurity directorySecurity = directoryInfo.GetAccessControl();

        FileSystemAccessRule accessRule = new FileSystemAccessRule(
            sqlServiceAccount,
            FileSystemRights.Read | FileSystemRights.ReadData,
            InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
            PropagationFlags.None,
            AccessControlType.Allow);

        directorySecurity.AddAccessRule(accessRule);
        directoryInfo.SetAccessControl(directorySecurity);
    }
}
