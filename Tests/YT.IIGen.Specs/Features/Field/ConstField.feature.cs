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
    public partial class ConstFieldFeature : object, Xunit.IClassFixture<ConstFieldFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "ConstField.feature"
#line hidden
        
        public ConstFieldFeature(ConstFieldFeature.FixtureData fixtureData, Tum4ik_StinimGen_Specs_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features/Field", "Const field", null, ProgrammingLanguage.CSharp, featureTags);
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
        [Xunit.TraitAttribute("FeatureTitle", "Const field")]
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
  testRunner.Given("source member declaration", "public const int ConstField;", ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 9
  testRunner.When("run generator for field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 10
  testRunner.Then("generated for interface", "int ConstField { get; }", ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 14
  testRunner.And("generated for struct implementation", "public int ConstField { get => @Namespace.@TypeName.ConstField; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 18
  testRunner.And("generated for class implementation", "public new int ConstField { get => @Namespace.@TypeName.ConstField; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 22
  testRunner.And("generated for sealed class implementation", "public int ConstField { get => @Namespace.@TypeName.ConstField; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 26
  testRunner.And("generated for static class implementation", "public int ConstField { get => @Namespace.@TypeName.ConstField; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Nullable field with keyworded type")]
        [Xunit.TraitAttribute("FeatureTitle", "Const field")]
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
  testRunner.Given("source member declaration", "public const int? ConstNullableField;", ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 37
  testRunner.When("run generator for field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 38
  testRunner.Then("generated for interface", "int? ConstNullableField { get; }", ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 42
  testRunner.And("generated for struct implementation", "public int? ConstNullableField { get => @Namespace.@TypeName.ConstNullableField; " +
                        "}", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 46
  testRunner.And("generated for class implementation", "public new int? ConstNullableField { get => @Namespace.@TypeName.ConstNullableFie" +
                        "ld; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 50
  testRunner.And("generated for sealed class implementation", "public int? ConstNullableField { get => @Namespace.@TypeName.ConstNullableField; " +
                        "}", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 54
  testRunner.And("generated for static class implementation", "public int? ConstNullableField { get => @Namespace.@TypeName.ConstNullableField; " +
                        "}", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Non-nullable field")]
        [Xunit.TraitAttribute("FeatureTitle", "Const field")]
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
  testRunner.And("source member declaration", "public const Double ConstField;", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 69
  testRunner.When("run generator for field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 70
  testRunner.Then("generated for interface", "double ConstField { get; }", ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 74
  testRunner.And("generated for struct implementation", "public double ConstField { get => @Namespace.@TypeName.ConstField; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 78
  testRunner.And("generated for class implementation", "public new double ConstField { get => @Namespace.@TypeName.ConstField; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 82
  testRunner.And("generated for sealed class implementation", "public double ConstField { get => @Namespace.@TypeName.ConstField; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 86
  testRunner.And("generated for static class implementation", "public double ConstField { get => @Namespace.@TypeName.ConstField; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Nullable field")]
        [Xunit.TraitAttribute("FeatureTitle", "Const field")]
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
  testRunner.And("source member declaration", "public const Double? ConstNullableField;", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 101
  testRunner.When("run generator for field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 102
  testRunner.Then("generated for interface", "double? ConstNullableField { get; }", ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 106
  testRunner.And("generated for struct implementation", "public double? ConstNullableField { get => @Namespace.@TypeName.ConstNullableFiel" +
                        "d; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 110
  testRunner.And("generated for class implementation", "public new double? ConstNullableField { get => @Namespace.@TypeName.ConstNullable" +
                        "Field; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 114
  testRunner.And("generated for sealed class implementation", "public double? ConstNullableField { get => @Namespace.@TypeName.ConstNullableFiel" +
                        "d; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 118
  testRunner.And("generated for static class implementation", "public double? ConstNullableField { get => @Namespace.@TypeName.ConstNullableFiel" +
                        "d; }", ((TechTalk.SpecFlow.Table)(null)), "And ");
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
                ConstFieldFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                ConstFieldFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
