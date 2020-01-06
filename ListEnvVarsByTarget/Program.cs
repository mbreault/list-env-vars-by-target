namespace ListEnvVarsByTarget
{
    class Program
    {
        static void Main(string[] args)
        {
            EnvVarHelper envVarHelper = new EnvVarHelper();

            envVarHelper.Delete("APPINSIGHTS_INSTRUMENTATIONKEY");

            envVarHelper.List();
        }
    }
}
