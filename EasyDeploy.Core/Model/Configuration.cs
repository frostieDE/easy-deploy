using System.Collections.Generic;

namespace EasyDeploy.Core.Model
{
    public class Configuration
    {
        /// <summary>
        /// Flag which indicates that executing should be proceed
        /// in case of any error
        /// </summary>
        public bool IsSoftFailureEnabled { get; set; } = true;

        /// <summary>
        /// List of action which are performed.
        /// They are executed in the exact order of this list.
        /// </summary>
        public List<DeployAction> Actions { get; set; } = new List<DeployAction>();

    }
}
