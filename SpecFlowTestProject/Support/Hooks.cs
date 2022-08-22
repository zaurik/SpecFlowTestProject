using System.Configuration;

namespace SpecFlowTestProject.Support
{
    [Binding]
    public sealed class Hooks
    {
        private readonly ScenarioContext scenarioContext;

        public Hooks(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            if (Directory.Exists(ConfigurationManager.AppSettings["ScreenshotFolderPath"]))
                Directory.Delete(ConfigurationManager.AppSettings["ScreenshotFolderPath"], true);
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            if (!scenarioContext.ScenarioInfo.Tags.Contains("ApiTest"))
            {
                BaseTest.InitialiseBrowser(); 
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (!scenarioContext.ScenarioInfo.Tags.Contains("ApiTest"))
            {
                BaseTest.CloseBrowser(); 
            }
        }
    }
}
