using EventStore.AcceptanceTests;
using TechTalk.SpecFlow;

namespace PwC.GTT.Platform.EventStore.Web.IntegrationTests.Support
{
    [Binding]
    public class RunHooks
    {
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            Bootstrapper.Initialize();
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            //featureContext.Add("streams", Bootstrapper.EventStore.Streams);
        }
    }
}
