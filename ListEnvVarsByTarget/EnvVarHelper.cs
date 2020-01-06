using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ListEnvVarsByTarget
{
    public class EnvVarHelper
    {
        public void Delete(string key)
        {
            Environment.SetEnvironmentVariable(key, null, EnvironmentVariableTarget.Machine);
            Environment.SetEnvironmentVariable(key, null, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable(key, null, EnvironmentVariableTarget.User);
        }

        public void List()
        {
            List<EnvironmentVariableModel> environmentVariableModels = new List<EnvironmentVariableModel>();

            // get list of all variables across all target types
            environmentVariableModels.AddRange(GetEnvironmentVariablesByTarget(EnvironmentVariableTarget.Machine));
            environmentVariableModels.AddRange(GetEnvironmentVariablesByTarget(EnvironmentVariableTarget.Process));
            environmentVariableModels.AddRange(GetEnvironmentVariablesByTarget(EnvironmentVariableTarget.User));

            environmentVariableModels = environmentVariableModels.OrderBy(x => x.Key).ThenBy(y => y.Target).ToList();

            // format the list for output
            StringBuilder stringBuilder = new StringBuilder();

            foreach (EnvironmentVariableModel environmentVariableModel in environmentVariableModels)
            {
                stringBuilder.AppendLine(String.Format("{0}\t{1}\t{2}", environmentVariableModel.Key, environmentVariableModel.Target, environmentVariableModel.Value));
            }

            // write to a file in the documents folder
            string outputFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            outputFolder = Path.Combine(outputFolder, "list-env-vars-by-target");

            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            string fileName = String.Format("{0}.txt", System.DateTime.Now.ToFileTime().ToString());

            string outputPath = Path.Combine(outputFolder, fileName);

            if (System.IO.File.Exists(outputPath))
            {
                System.IO.File.Delete(outputPath);
                System.Threading.Thread.Sleep(20);
            }

            System.IO.File.WriteAllText(outputPath, stringBuilder.ToString());
        }
        public List<EnvironmentVariableModel> GetEnvironmentVariablesByTarget(EnvironmentVariableTarget environmentVariableTarget)
        {
            List<EnvironmentVariableModel> environmentVariableModels = new List<EnvironmentVariableModel>();
            foreach (DictionaryEntry dictionaryEntry in Environment.GetEnvironmentVariables(environmentVariableTarget))
            {
                environmentVariableModels.Add(new EnvironmentVariableModel(dictionaryEntry.Key, dictionaryEntry.Value, environmentVariableTarget));
            }

            return environmentVariableModels;
        }
    }
}
