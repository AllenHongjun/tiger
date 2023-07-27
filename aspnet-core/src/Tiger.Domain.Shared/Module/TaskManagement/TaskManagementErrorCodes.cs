using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.TaskManagement
{
    public class TaskManagementErrorCodes
    {
        public const string Namespace = "TaskManagement";

        public const string JobNameAlreadyExists = Namespace + ":01000";

        public const string JobNotFoundInQueue = Namespace + ":01001";
    }
}
