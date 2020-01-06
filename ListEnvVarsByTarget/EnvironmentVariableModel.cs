using System;

namespace ListEnvVarsByTarget
{
    public class EnvironmentVariableModel
    {
        public EnvironmentVariableModel(object key, object value, EnvironmentVariableTarget target)
        {
            Key = key.ToString();
            Value = value.ToString();
            Target = target.ToString();
        }

        public string Key { get; }
        public string Value { get; }
        public string Target { get; }
    }
}
