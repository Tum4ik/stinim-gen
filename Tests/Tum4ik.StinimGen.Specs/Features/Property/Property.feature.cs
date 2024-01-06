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
namespace Tum4ik.StinimGen.Specs.Features.Property
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class PropertyFeature : object, Xunit.IClassFixture<PropertyFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "Property.feature"
#line hidden
        
        public PropertyFeature(PropertyFeature.FixtureData fixtureData, Tum4ik_StinimGen_Specs_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features/Property", "Property", null, ProgrammingLanguage.CSharp, featureTags);
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
  testRunner.Given("source declaration", "using System;\r\nusing System.IO;\r\nnamespace Properties;\r\npublic class PropertyHold" +
                    "er\r\n{\r\n  <member>\r\n}", ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 15
  testRunner.And("attribute usage", "using Tum4ik.StinimGen.Attributes;\r\nusing Properties;\r\nnamespace Attribute.Usage;" +
                    "\r\n[IIFor(typeof(PropertyHolder), \"PropertyHolderWrapper\")]\r\ninternal partial int" +
                    "erface IPropertyHolder { }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
        }
        
        void System.IDisposable.Dispose()
        {
            this.TestTearDown();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Property with only getter")]
        [Xunit.TraitAttribute("FeatureTitle", "Property")]
        [Xunit.TraitAttribute("Description", "Property with only getter")]
        public void PropertyWithOnlyGetter()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Property with only getter", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 25
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
#line 26
  testRunner.Given("source member declaration", "public static int Property { get; }", ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 30
  testRunner.When("run generator", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 31
  testRunner.Then("there must not be generation exception", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 32
  testRunner.Then("generated interface member must be", "int Property { get; }", ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 36
  testRunner.And("generated implementation member must be", "public int Property { get => Properties.PropertyHolder.Property; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Property with only setter")]
        [Xunit.TraitAttribute("FeatureTitle", "Property")]
        [Xunit.TraitAttribute("Description", "Property with only setter")]
        public void PropertyWithOnlySetter()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Property with only setter", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 42
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
#line 43
  testRunner.Given("source member declaration", "private static float? _property;\r\npublic static float? Property { set => _propert" +
                        "y = value; }", ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 48
  testRunner.When("run generator", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 49
  testRunner.Then("there must not be generation exception", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 50
  testRunner.Then("generated interface member must be", "float? Property { set; }", ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 54
  testRunner.And("generated implementation member must be", "public float? Property { set => Properties.PropertyHolder.Property = value; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Property with getter and setter")]
        [Xunit.TraitAttribute("FeatureTitle", "Property")]
        [Xunit.TraitAttribute("Description", "Property with getter and setter")]
        public void PropertyWithGetterAndSetter()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Property with getter and setter", null, tagsOfScenario, argumentsOfScenario, featureTags);
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
  testRunner.Given("source member declaration", "public static Stream Property { get; set; }", ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 65
  testRunner.When("run generator", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 66
  testRunner.Then("there must not be generation exception", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 67
  testRunner.Then("generated interface member must be", "global::System.IO.Stream Property { get; set; }", ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 71
  testRunner.And("generated implementation member must be", "public global::System.IO.Stream Property { get => Properties.PropertyHolder.Prope" +
                        "rty; set => Properties.PropertyHolder.Property = value; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Property with getter and private setter")]
        [Xunit.TraitAttribute("FeatureTitle", "Property")]
        [Xunit.TraitAttribute("Description", "Property with getter and private setter")]
        public void PropertyWithGetterAndPrivateSetter()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Property with getter and private setter", null, tagsOfScenario, argumentsOfScenario, featureTags);
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
  testRunner.Given("source member declaration", "public static Stream? Property { get; private set; }", ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 82
  testRunner.When("run generator", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 83
  testRunner.Then("there must not be generation exception", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 84
  testRunner.Then("generated interface member must be", "global::System.IO.Stream? Property { get; }", ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 88
  testRunner.And("generated implementation member must be", "public global::System.IO.Stream? Property { get => Properties.PropertyHolder.Prop" +
                        "erty; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Property with getter and protected setter")]
        [Xunit.TraitAttribute("FeatureTitle", "Property")]
        [Xunit.TraitAttribute("Description", "Property with getter and protected setter")]
        public void PropertyWithGetterAndProtectedSetter()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Property with getter and protected setter", null, tagsOfScenario, argumentsOfScenario, featureTags);
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
  testRunner.Given("source member declaration", "public static Stream? Property { get; protected set; }", ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 99
  testRunner.When("run generator", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 100
  testRunner.Then("there must not be generation exception", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 101
  testRunner.Then("generated interface member must be", "global::System.IO.Stream? Property { get; }", ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 105
  testRunner.And("generated implementation member must be", "public global::System.IO.Stream? Property { get => Properties.PropertyHolder.Prop" +
                        "erty; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Property with private getter and setter")]
        [Xunit.TraitAttribute("FeatureTitle", "Property")]
        [Xunit.TraitAttribute("Description", "Property with private getter and setter")]
        public void PropertyWithPrivateGetterAndSetter()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Property with private getter and setter", null, tagsOfScenario, argumentsOfScenario, featureTags);
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
  testRunner.Given("source member declaration", "public static float? Property { private get; set; }", ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 116
  testRunner.When("run generator", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 117
  testRunner.Then("there must not be generation exception", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 118
  testRunner.Then("generated interface member must be", "float? Property { set; }", ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 122
  testRunner.And("generated implementation member must be", "public float? Property { set => Properties.PropertyHolder.Property = value; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Property with protected getter and setter")]
        [Xunit.TraitAttribute("FeatureTitle", "Property")]
        [Xunit.TraitAttribute("Description", "Property with protected getter and setter")]
        public void PropertyWithProtectedGetterAndSetter()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Property with protected getter and setter", null, tagsOfScenario, argumentsOfScenario, featureTags);
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
  testRunner.Given("source member declaration", "public static string Property { protected get; set; }", ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 133
  testRunner.When("run generator", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 134
  testRunner.Then("there must not be generation exception", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 135
  testRunner.Then("generated interface member must be", "string Property { set; }", ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 139
  testRunner.And("generated implementation member must be", "public string Property { set => Properties.PropertyHolder.Property = value; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
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
                PropertyFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                PropertyFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
