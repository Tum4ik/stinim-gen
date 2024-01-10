﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.9.0.0
//      SpecFlow Generator Version:3.9.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Tum4ik.StinimGen.Specs.Features.Event
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class EventFeature : object, Xunit.IClassFixture<EventFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "Event.feature"
#line hidden
        
        public EventFeature(EventFeature.FixtureData fixtureData, Tum4ik_StinimGen_Specs_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features/Event", "Event", null, ProgrammingLanguage.CSharp, featureTags);
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public void TestInitialize()
        {
        }
        
        public void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void FeatureBackground()
        {
#line 4
#line hidden
#line 5
  testRunner.Given("source declaration", "using System;\r\nnamespace Events;\r\npublic class EventHolder\r\n{\r\n  <member>\r\n}\r\n\r\np" +
                    "ublic delegate void CustomEventHandler(int count, string search);", ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 16
  testRunner.And("attribute usage", "using Tum4ik.StinimGen.Attributes;\r\nusing Events;\r\nnamespace Attribute.Usage;\r\n[I" +
                    "IFor(typeof(EventHolder), \"EventHolderWrapper\")]\r\ninternal partial interface IEv" +
                    "entHolder { }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
        }
        
        void System.IDisposable.Dispose()
        {
            this.TestTearDown();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Event with EventHandler type")]
        [Xunit.TraitAttribute("FeatureTitle", "Event")]
        [Xunit.TraitAttribute("Description", "Event with EventHandler type")]
        public void EventWithEventHandlerType()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Event with EventHandler type", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 26
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 4
this.FeatureBackground();
#line hidden
#line 27
  testRunner.Given("source member declaration", "public static event EventHandler EventMember;", ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 31
  testRunner.When("run generator", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 32
  testRunner.Then("there must not be generation exception", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 33
  testRunner.And("generated interface member must be", "event global::System.EventHandler EventMember;", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 37
  testRunner.And("generated implementation member must be", "public event global::System.EventHandler EventMember { add => Events.EventHolder." +
                        "EventMember += value; remove => Events.EventHolder.EventMember -= value; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Event with custom type")]
        [Xunit.TraitAttribute("FeatureTitle", "Event")]
        [Xunit.TraitAttribute("Description", "Event with custom type")]
        public void EventWithCustomType()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Event with custom type", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 43
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 4
this.FeatureBackground();
#line hidden
#line 44
  testRunner.Given("source member declaration", "public static event CustomEventHandler EventMember;", ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 48
  testRunner.When("run generator", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 49
  testRunner.Then("there must not be generation exception", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 50
  testRunner.And("generated interface member must be", "event global::Events.CustomEventHandler EventMember;", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 54
  testRunner.And("generated implementation member must be", "public event global::Events.CustomEventHandler EventMember { add => Events.EventH" +
                        "older.EventMember += value; remove => Events.EventHolder.EventMember -= value; }" +
                        "", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Event with Action type")]
        [Xunit.TraitAttribute("FeatureTitle", "Event")]
        [Xunit.TraitAttribute("Description", "Event with Action type")]
        public void EventWithActionType()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Event with Action type", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 60
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 4
this.FeatureBackground();
#line hidden
#line 61
  testRunner.Given("source member declaration", "public static event Action EventMember;", ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 65
  testRunner.When("run generator", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 66
  testRunner.Then("there must not be generation exception", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 67
  testRunner.And("generated interface member must be", "event global::System.Action EventMember;", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 71
  testRunner.And("generated implementation member must be", "public event global::System.Action EventMember { add => Events.EventHolder.EventM" +
                        "ember += value; remove => Events.EventHolder.EventMember -= value; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Event with Action type with keyworded generic parameter")]
        [Xunit.TraitAttribute("FeatureTitle", "Event")]
        [Xunit.TraitAttribute("Description", "Event with Action type with keyworded generic parameter")]
        public void EventWithActionTypeWithKeywordedGenericParameter()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Event with Action type with keyworded generic parameter", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 77
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 4
this.FeatureBackground();
#line hidden
#line 78
  testRunner.Given("source member declaration", "public static event Action<int> EventMember;", ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 82
  testRunner.When("run generator", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 83
  testRunner.Then("there must not be generation exception", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 84
  testRunner.And("generated interface member must be", "event global::System.Action<int> EventMember;", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 88
  testRunner.And("generated implementation member must be", "public event global::System.Action<int> EventMember { add => Events.EventHolder.E" +
                        "ventMember += value; remove => Events.EventHolder.EventMember -= value; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Event with Action type with keyworded nullable generic parameter")]
        [Xunit.TraitAttribute("FeatureTitle", "Event")]
        [Xunit.TraitAttribute("Description", "Event with Action type with keyworded nullable generic parameter")]
        public void EventWithActionTypeWithKeywordedNullableGenericParameter()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Event with Action type with keyworded nullable generic parameter", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 94
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 4
this.FeatureBackground();
#line hidden
#line 95
  testRunner.Given("source member declaration", "public static event Action<string?> EventMember;", ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 99
  testRunner.When("run generator", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 100
  testRunner.Then("there must not be generation exception", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 101
  testRunner.And("generated interface member must be", "event global::System.Action<string?> EventMember;", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 105
  testRunner.And("generated implementation member must be", "public event global::System.Action<string?> EventMember { add => Events.EventHold" +
                        "er.EventMember += value; remove => Events.EventHolder.EventMember -= value; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Event with Action type with non-keyworded generic parameter")]
        [Xunit.TraitAttribute("FeatureTitle", "Event")]
        [Xunit.TraitAttribute("Description", "Event with Action type with non-keyworded generic parameter")]
        public void EventWithActionTypeWithNon_KeywordedGenericParameter()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Event with Action type with non-keyworded generic parameter", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 111
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 4
this.FeatureBackground();
#line hidden
#line 112
  testRunner.Given("source member declaration", "public static event Action<DateTime> EventMember;", ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 116
  testRunner.When("run generator", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 117
  testRunner.Then("there must not be generation exception", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 118
  testRunner.And("generated interface member must be", "event global::System.Action<global::System.DateTime> EventMember;", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 122
  testRunner.And("generated implementation member must be", "public event global::System.Action<global::System.DateTime> EventMember { add => " +
                        "Events.EventHolder.EventMember += value; remove => Events.EventHolder.EventMembe" +
                        "r -= value; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Event with Action type with non-keyworded nullable generic parameter")]
        [Xunit.TraitAttribute("FeatureTitle", "Event")]
        [Xunit.TraitAttribute("Description", "Event with Action type with non-keyworded nullable generic parameter")]
        public void EventWithActionTypeWithNon_KeywordedNullableGenericParameter()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Event with Action type with non-keyworded nullable generic parameter", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 128
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 4
this.FeatureBackground();
#line hidden
#line 129
  testRunner.Given("source member declaration", "public static event Action<DayOfWeek?> EventMember;", ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 133
  testRunner.When("run generator", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 134
  testRunner.Then("there must not be generation exception", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 135
  testRunner.And("generated interface member must be", "event global::System.Action<global::System.DayOfWeek?> EventMember;", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 139
  testRunner.And("generated implementation member must be", "public event global::System.Action<global::System.DayOfWeek?> EventMember { add =" +
                        "> Events.EventHolder.EventMember += value; remove => Events.EventHolder.EventMem" +
                        "ber -= value; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Event with Func")]
        [Xunit.TraitAttribute("FeatureTitle", "Event")]
        [Xunit.TraitAttribute("Description", "Event with Func")]
        public void EventWithFunc()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Event with Func", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 145
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 4
this.FeatureBackground();
#line hidden
#line 146
  testRunner.Given("source member declaration", "public static event Func<DayOfWeek?, double, int?> EventMember;", ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 150
  testRunner.When("run generator", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 151
  testRunner.Then("there must not be generation exception", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 152
  testRunner.And("generated interface member must be", "event global::System.Func<global::System.DayOfWeek?, double, int?> EventMember;", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 156
  testRunner.And("generated implementation member must be", "public event global::System.Func<global::System.DayOfWeek?, double, int?> EventMe" +
                        "mber { add => Events.EventHolder.EventMember += value; remove => Events.EventHol" +
                        "der.EventMember -= value; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                EventFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                EventFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
