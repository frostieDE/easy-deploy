using EasyDeploy.Core.Model;
using System;
using System.Windows;
using System.Windows.Controls;

namespace EasyDeploy.GUI.Selector
{
    public class DeployActionTemplateSelector : DataTemplateSelector
    {
        public DataTemplate CopyFileDeployActionTemplate { get; set; }

        public DataTemplate RemoveFileDeployActionTemplate { get; set; }

        public DataTemplate ExecuteFileDeployActionTemplate { get; set; }

        public DataTemplate UnzipDeployActionTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is CopyFileDeployAction)
            {
                return CopyFileDeployActionTemplate;
            }
            else if (item is RemoveFileDeployAction)
            {
                return RemoveFileDeployActionTemplate;
            }
            else if (item is ExecuteFileDeployAction)
            {
                return ExecuteFileDeployActionTemplate;
            }
            else if (item is UnzipDeployAction)
            {
                return UnzipDeployActionTemplate;
            }

            if (item != null)
            {
                throw new ArgumentException($"Unknown deploy action type {item.GetType()}");
            }

            return base.SelectTemplate(item, container);
        }
    }
}
