using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Tum4ik.StinimGen.Models;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Tum4ik.StinimGen;
partial class IIGenerator
{
  internal static class Execute
  {
    public static MemberDeclarationSyntax GetInterfacePropertySyntax(PropertyInfo propertyInfo)
    {
      var accessorsList = new List<AccessorDeclarationSyntax>(2);

      if (propertyInfo.HasGetter)
      {
        accessorsList.Add(
          AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
        );
      }

      if (propertyInfo.HasSetter)
      {
        accessorsList.Add(
          AccessorDeclaration(SyntaxKind.SetAccessorDeclaration).WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
        );
      }

      return PropertyDeclaration(
          IdentifierName(propertyInfo.TypeNameWithNullabilityAnnotations),
          Identifier(propertyInfo.PropertyName)
        )
        .AddAccessorListAccessors(accessorsList.ToArray());
    }


    public static MemberDeclarationSyntax GetImplementationPropertySyntax(PropertyInfo propertyInfo, IIInfo iiInfo)
    {
      var underlyingCallee = IdentifierName($"{iiInfo.SourceFullyQualifiedName}.{propertyInfo.PropertyName}");

      var accessorsList = new List<AccessorDeclarationSyntax>(2);

      if (propertyInfo.HasGetter)
      {
        accessorsList.Add(
          AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
            .WithExpressionBody(ArrowExpressionClause(underlyingCallee))
            .WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
        );
      }

      if (propertyInfo.HasSetter)
      {
        accessorsList.Add(
          AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
            .WithExpressionBody(ArrowExpressionClause(
              AssignmentExpression(
                SyntaxKind.SimpleAssignmentExpression,
                underlyingCallee,
                IdentifierName("value")
              )
            ))
            .WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
        );
      }

      return PropertyDeclaration(
          IdentifierName(propertyInfo.TypeNameWithNullabilityAnnotations),
          Identifier(propertyInfo.PropertyName)
        )
        .AddModifiers(Token(SyntaxKind.PublicKeyword))
        .AddAccessorListAccessors(accessorsList.ToArray());
    }


    public static MemberDeclarationSyntax GetInterfaceEventSyntax(EventInfo eventInfo)
    {
      return EventDeclaration(
          IdentifierName(eventInfo.TypeNameWithNullabilityAnnotations),
          Identifier(eventInfo.EventName)
        )
        .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
    }


    public static MemberDeclarationSyntax GetImplementationEventSyntax(EventInfo eventInfo, IIInfo iiInfo)
    {
      var underlyingCallee = IdentifierName($"{iiInfo.SourceFullyQualifiedName}.{eventInfo.EventName}");
      
      return EventDeclaration(
          IdentifierName(eventInfo.TypeNameWithNullabilityAnnotations),
          Identifier(eventInfo.EventName)
        )
        .AddModifiers(Token(SyntaxKind.PublicKeyword))
        .AddAccessorListAccessors(
          AccessorDeclaration(SyntaxKind.AddAccessorDeclaration)
            .WithExpressionBody(ArrowExpressionClause(
              AssignmentExpression(
                SyntaxKind.AddAssignmentExpression,
                underlyingCallee,
                IdentifierName("value")
              )
            ))
            .WithSemicolonToken(Token(SyntaxKind.SemicolonToken)),
          AccessorDeclaration(SyntaxKind.RemoveAccessorDeclaration)
            .WithExpressionBody(ArrowExpressionClause(
              AssignmentExpression(
                SyntaxKind.SubtractAssignmentExpression,
                underlyingCallee,
                IdentifierName("value")
              )
            ))
            .WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
        );
    }


    public static MemberDeclarationSyntax GetInterfaceMethodSyntax(MethodInfo methodInfo)
    {
      var returnType = GetMethodReturnType(methodInfo);
      var parameters = GetMethodParameters(methodInfo);
      return MethodDeclaration(returnType, Identifier(methodInfo.MethodName))
        .AddParameterListParameters(parameters.ToArray())
        .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
    }


    public static MemberDeclarationSyntax GetImplementationMethodSyntax(MethodInfo methodInfo, IIInfo iiInfo)
    {
      var returnType = GetMethodReturnType(methodInfo);
      var parameters = GetMethodParameters(methodInfo);
      var arguments = methodInfo.Parameters.Select(p =>
      {
        var argument = Argument(IdentifierName(p.ParameterName));
        if (p.RefKind != RefKind.None)
        {
          argument = argument.WithRefKindKeyword(Token(p.RefKind switch
          {
            RefKind.Ref => SyntaxKind.RefKeyword,
            RefKind.Out => SyntaxKind.OutKeyword,
            RefKind.In => SyntaxKind.InKeyword,
            _ => throw new NotImplementedException()
          }));
        }
        return argument;
      }).ToArray();
      var separatorsCount = arguments.Length - 1;
      if (separatorsCount < 0)
      {
        separatorsCount = 0;
      }
      var separators = new SyntaxToken[separatorsCount];
      for (var i = 0; i < separators.Length; i++)
      {
        separators[i] = Token(SyntaxKind.CommaToken);
      }

      return MethodDeclaration(returnType, Identifier(methodInfo.MethodName))
        .AddModifiers(Token(SyntaxKind.PublicKeyword))
        .AddParameterListParameters(parameters.ToArray())
        .WithExpressionBody(ArrowExpressionClause(
          InvocationExpression(
            IdentifierName($"{iiInfo.SourceFullyQualifiedName}.{methodInfo.MethodName}"),
            ArgumentList(SeparatedList(arguments, separators))
          )
        ))
        .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
    }


    private static TypeSyntax GetMethodReturnType(MethodInfo methodInfo)
    {
      return methodInfo.ReturnTypeNameWithNullabilityAnnotations is null
        ? PredefinedType(Token(SyntaxKind.VoidKeyword))
        : IdentifierName(methodInfo.ReturnTypeNameWithNullabilityAnnotations);
    }


    private static IEnumerable<ParameterSyntax> GetMethodParameters(MethodInfo methodInfo)
    {
      return methodInfo.Parameters.Select(p =>
      {
        var parameter = Parameter(Identifier(p.ParameterName))
          .WithType(IdentifierName(p.TypeNameWithNullabilityAnnotations));
        if (p.RefKind != RefKind.None)
        {
          parameter = parameter.AddModifiers(Token(p.RefKind switch
          {
            RefKind.Ref => SyntaxKind.RefKeyword,
            RefKind.Out => SyntaxKind.OutKeyword,
            RefKind.In => SyntaxKind.InKeyword,
            _ => throw new NotImplementedException(),
          }));
        }
        if (p.IsParams)
        {
          parameter = parameter.AddModifiers(Token(SyntaxKind.ParamsKeyword));
        }
        if (p.IsOptional)
        {
          ExpressionSyntax literal;
          if (p.ExplicitDefaultValue is null)
          {
            literal = LiteralExpression(SyntaxKind.DefaultLiteralExpression);
          }
          else if (p.TypeKind == TypeKind.Enum)
          {
            literal = CastExpression(
              IdentifierName(p.TypeNameWithNullabilityAnnotations),
              ParseExpression(p.ExplicitDefaultValue.ToString())
            );
          }
          else if (p.SpecialType == SpecialType.System_Decimal)
          {
            literal = LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal((decimal) p.ExplicitDefaultValue));
          }
          else if (p.SpecialType == SpecialType.System_Single)
          {
            literal = LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal((float) p.ExplicitDefaultValue));
          }
          else if (p.SpecialType == SpecialType.System_Double)
          {
            literal = LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal((double) p.ExplicitDefaultValue));
          }
          else if (p.SpecialType == SpecialType.System_String)
          {
            literal = LiteralExpression(SyntaxKind.StringLiteralExpression, Literal((string) p.ExplicitDefaultValue));
          }
          else
          {
            literal = ParseExpression(p.ExplicitDefaultValue.ToString());
          }
          parameter = parameter.WithDefault(EqualsValueClause(literal));
        }
        return parameter;
      });
    }
  }
}
