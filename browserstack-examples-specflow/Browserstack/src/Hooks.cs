using BoDi;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TechTalk.SpecFlow;

namespace SpecflowBrowserStack.src.stepdefs
{
    [Binding]
    public class Hooks
    { 
        private static FeatureContext _featureContext;
        private static ScenarioContext _scenarioContext;

        public Hooks( FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            _featureContext = featureContext;
            _scenarioContext = scenarioContext;
        }

        [BeforeTestRun]
        public static void InitializeReport()
        {
            //Initialize Extent report before test starts
           
        }

        [AfterTestRun]
        public static void TearDownReport()
        {
            //Flush report once test completes
        
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            //Create dynamic feature name
            
        }

        [AfterStep]
        public void InsertReportingSteps()
        {
            // use for after hooks
        }


        [BeforeScenario]
        public static void Initialize(ScenarioContext scenarioContext)
        {
            //Create dynamic scenario name
         
        }
    }
}

