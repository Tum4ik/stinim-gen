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
namespace Tum4ik.StinimGen.Specs.Features.Field
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class FieldFeature : object, Xunit.IClassFixture<FieldFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "Field.feature"
#line hidden
        
        public FieldFeature(FieldFeature.FixtureData fixtureData, Tum4ik_StinimGen_Specs_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features/Field", "Field", null, ProgrammingLanguage.CSharp, featureTags);
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
  testRunner.Given("source declaration", "using System;\r\nnamespace Fields;\r\npublic class FieldHolder\r\n{\r\n  <member>\r\n}", ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 14
  testRunner.And("attribute usage", "using Tum4ik.StinimGen.Attributes;\r\nusing Fields;\r\nnamespace Attribute.Usage;\r\n[I" +
                    "IFor(typeof(FieldHolder), \"FieldHolderWrapper\")]\r\ninternal partial interface IFi" +
                    "eldHolder { }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
        }
        
        void System.IDisposable.Dispose()
        {
            this.TestTearDown();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Non-nullable field with keyworded type")]
        [Xunit.TraitAttribute("FeatureTitle", "Field")]
        [Xunit.TraitAttribute("Description", "Non-nullable field with keyworded type")]
        public void Non_NullableFieldWithKeywordedType()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Non-nullable field with keyworded type", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 24
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
#line 25
  testRunner.Given("source member declaration", "public static object StaticField;", ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 29
  testRunner.When("run generator", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 30
  testRunner.Then("there must not be generation exception", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 31
  testRunner.And("generated interface member must be", "object StaticField { get; set; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 35
  testRunner.And("generated implementation member must be", "public object StaticField { get => Fields.FieldHolder.StaticField; set => Fields." +
                        "FieldHolder.StaticField = value; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Nullable field with keyworded type")]
        [Xunit.TraitAttribute("FeatureTitle", "Field")]
        [Xunit.TraitAttribute("Description", "Nullable field with keyworded type")]
        public void NullableFieldWithKeywordedType()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Nullable field with keyworded type", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 41
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
#line 42
  testRunner.Given("source member declaration", "public static object? StaticField;", ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 46
  testRunner.When("run generator", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 47
  testRunner.Then("there must not be generation exception", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 48
  testRunner.And("generated interface member must be", "object? StaticField { get; set; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 52
  testRunner.And("generated implementation member must be", "public object? StaticField { get => Fields.FieldHolder.StaticField; set => Fields" +
                        ".FieldHolder.StaticField = value; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Non-nullable field")]
        [Xunit.TraitAttribute("FeatureTitle", "Field")]
        [Xunit.TraitAttribute("Description", "Non-nullable field")]
        public void Non_NullableField()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Non-nullable field", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 58
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
#line 59
  testRunner.Given("source member declaration", "public static Delegate StaticField;", ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 63
  testRunner.When("run generator", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 64
  testRunner.Then("there must not be generation exception", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 65
  testRunner.And("generated interface member must be", "global::System.Delegate StaticField { get; set; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 69
  testRunner.And("generated implementation member must be", "public global::System.Delegate StaticField { get => Fields.FieldHolder.StaticFiel" +
                        "d; set => Fields.FieldHolder.StaticField = value; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Nullable field")]
        [Xunit.TraitAttribute("FeatureTitle", "Field")]
        [Xunit.TraitAttribute("Description", "Nullable field")]
        public void NullableField()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Nullable field", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 75
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
#line 76
  testRunner.Given("source member declaration", "public static Delegate? StaticField;", ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 80
  testRunner.When("run generator", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 81
  testRunner.Then("there must not be generation exception", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 82
  testRunner.And("generated interface member must be", "global::System.Delegate? StaticField { get; set; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 86
  testRunner.And("generated implementation member must be", "public global::System.Delegate? StaticField { get => Fields.FieldHolder.StaticFie" +
                        "ld; set => Fields.FieldHolder.StaticField = value; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
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
                FieldFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                FieldFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
