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
    public partial class StaticRead_OnlyFieldFeature : object, Xunit.IClassFixture<StaticRead_OnlyFieldFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "StaticReadonlyField.feature"
#line hidden
        
        public StaticRead_OnlyFieldFeature(StaticRead_OnlyFieldFeature.FixtureData fixtureData, Tum4ik_StinimGen_Specs_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features/Field", "Static read-only field", null, ProgrammingLanguage.CSharp, featureTags);
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
        
        void System.IDisposable.Dispose()
        {
            this.TestTearDown();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Non-nullable field with keyworded type")]
        [Xunit.TraitAttribute("FeatureTitle", "Static read-only field")]
        [Xunit.TraitAttribute("Description", "Non-nullable field with keyworded type")]
        public void Non_NullableFieldWithKeywordedType()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Non-nullable field with keyworded type", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 4
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 5
  testRunner.Given("source member declaration", "public static readonly float StaticReadonlyField;", ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 9
  testRunner.When("run generator for field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 10
  testRunner.Then("generated for interface", "float StaticReadonlyField { get; }", ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 14
  testRunner.And("generated for struct implementation", "public float StaticReadonlyField { get => @Namespace.@TypeName.StaticReadonlyFiel" +
                        "d; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 18
  testRunner.And("generated for class implementation", "public new float StaticReadonlyField { get => @Namespace.@TypeName.StaticReadonly" +
                        "Field; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 22
  testRunner.And("generated for sealed class implementation", "public float StaticReadonlyField { get => @Namespace.@TypeName.StaticReadonlyFiel" +
                        "d; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 26
  testRunner.And("generated for static class implementation", "public float StaticReadonlyField { get => @Namespace.@TypeName.StaticReadonlyFiel" +
                        "d; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Nullable field with keyworded type")]
        [Xunit.TraitAttribute("FeatureTitle", "Static read-only field")]
        [Xunit.TraitAttribute("Description", "Nullable field with keyworded type")]
        public void NullableFieldWithKeywordedType()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Nullable field with keyworded type", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 32
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 33
  testRunner.Given("source member declaration", "public static readonly float? StaticReadonlyField;", ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 37
  testRunner.When("run generator for field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 38
  testRunner.Then("generated for interface", "float? StaticReadonlyField { get; }", ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 42
  testRunner.And("generated for struct implementation", "public float? StaticReadonlyField { get => @Namespace.@TypeName.StaticReadonlyFie" +
                        "ld; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 46
  testRunner.And("generated for class implementation", "public new float? StaticReadonlyField { get => @Namespace.@TypeName.StaticReadonl" +
                        "yField; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 50
  testRunner.And("generated for sealed class implementation", "public float? StaticReadonlyField { get => @Namespace.@TypeName.StaticReadonlyFie" +
                        "ld; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 54
  testRunner.And("generated for static class implementation", "public float? StaticReadonlyField { get => @Namespace.@TypeName.StaticReadonlyFie" +
                        "ld; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Non-nullable field")]
        [Xunit.TraitAttribute("FeatureTitle", "Static read-only field")]
        [Xunit.TraitAttribute("Description", "Non-nullable field")]
        public void Non_NullableField()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Non-nullable field", null, tagsOfScenario, argumentsOfScenario, featureTags);
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
#line 61
  testRunner.Given("usings", "using System;", ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 65
  testRunner.And("source member declaration", "public static readonly StringComparer StaticReadonlyField;", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 69
  testRunner.When("run generator for field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 70
  testRunner.Then("generated for interface", "global::System.StringComparer StaticReadonlyField { get; }", ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 74
  testRunner.And("generated for struct implementation", "public global::System.StringComparer StaticReadonlyField { get => @Namespace.@Typ" +
                        "eName.StaticReadonlyField; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 78
  testRunner.And("generated for class implementation", "public new global::System.StringComparer StaticReadonlyField { get => @Namespace." +
                        "@TypeName.StaticReadonlyField; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 82
  testRunner.And("generated for sealed class implementation", "public global::System.StringComparer StaticReadonlyField { get => @Namespace.@Typ" +
                        "eName.StaticReadonlyField; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 86
  testRunner.And("generated for static class implementation", "public global::System.StringComparer StaticReadonlyField { get => @Namespace.@Typ" +
                        "eName.StaticReadonlyField; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Nullable field")]
        [Xunit.TraitAttribute("FeatureTitle", "Static read-only field")]
        [Xunit.TraitAttribute("Description", "Nullable field")]
        public void NullableField()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Nullable field", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 92
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 93
  testRunner.Given("usings", "using System;", ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 97
  testRunner.And("source member declaration", "public static readonly StringComparer? StaticReadonlyField;", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 101
  testRunner.When("run generator for field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 102
  testRunner.Then("generated for interface", "global::System.StringComparer? StaticReadonlyField { get; }", ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 106
  testRunner.And("generated for struct implementation", "public global::System.StringComparer? StaticReadonlyField { get => @Namespace.@Ty" +
                        "peName.StaticReadonlyField; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 110
  testRunner.And("generated for class implementation", "public new global::System.StringComparer? StaticReadonlyField { get => @Namespace" +
                        ".@TypeName.StaticReadonlyField; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 114
  testRunner.And("generated for sealed class implementation", "public global::System.StringComparer? StaticReadonlyField { get => @Namespace.@Ty" +
                        "peName.StaticReadonlyField; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 118
  testRunner.And("generated for static class implementation", "public global::System.StringComparer? StaticReadonlyField { get => @Namespace.@Ty" +
                        "peName.StaticReadonlyField; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
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
                StaticRead_OnlyFieldFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                StaticRead_OnlyFieldFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
