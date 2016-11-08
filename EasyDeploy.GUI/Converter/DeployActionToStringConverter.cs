using EasyDeploy.Core.Model;
using EasyDeploy.GUI.Properties;
using System;
using System.Globalization;
using System.Windows.Data;

namespace EasyDeploy.GUI.Converter
{
    public class DeployActionToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var deployAction = value as DeployAction;

            if (deployAction is CopyFileDeployAction)
            {
                return Resources.DeployActionCopyFile;
            }
            else if (deployAction is RemoveFileDeployAction)
            {
                return Resources.DeployActionRemoveFile;
            }
            else if (deployAction is ExecuteFileDeployAction)
            {
                return Resources.DeployActionExecuteFile;
            }
            else if (deployAction is UnzipDeployAction)
            {
                return Resources.DeployActionUnzip;
            }

            return "N/A";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
