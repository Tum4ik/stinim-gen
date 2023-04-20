using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using YT.IIGen.Models;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace YT.IIGen;
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


    public static MemberDeclarationSyntax GetImplementationPropertySyntax(PropertyInfo propertyInfo,
                                                                          string underlyingCallee,
                                                                          bool isNewKeywordRequired = false)
    {
      var accessorsList = new List<AccessorDeclarationSyntax>(2);

      if (propertyInfo.HasGetter)
      {
        accessorsList.Add(
          AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
            .WithExpressionBody(ArrowExpressionClause(IdentifierName($"{underlyingCallee}.{propertyInfo.PropertyName}")))
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
                IdentifierName($"{underlyingCallee}.{propertyInfo.PropertyName}"),
                IdentifierName("value")
              )
            ))
            .WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
        );
      }

      var modifiers = new List<SyntaxToken>(2) { Token(SyntaxKind.PublicKeyword) };
      if (isNewKeywordRequired)
      {
        modifiers.Add(Token(SyntaxKind.NewKeyword));
      }

      return PropertyDeclaration(
          IdentifierName(propertyInfo.TypeNameWithNullabilityAnnotations),
          Identifier(propertyInfo.PropertyName)
        )
        .AddModifiers(modifiers.ToArray())
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


    public static MemberDeclarationSyntax GetImplementationEventSyntax(EventInfo eventInfo,
                                                                       string underlyingCallee)
    {
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
                IdentifierName($"{underlyingCallee}.{eventInfo.EventName}"),
                IdentifierName("value")
              )
            ))
            .WithSemicolonToken(Token(SyntaxKind.SemicolonToken)),
          AccessorDeclaration(SyntaxKind.RemoveAccessorDeclaration)
            .WithExpressionBody(ArrowExpressionClause(
              AssignmentExpression(
                SyntaxKind.SubtractAssignmentExpression,
                IdentifierName($"{underlyingCallee}.{eventInfo.EventName}"),
                IdentifierName("value")
              )
            ))
            .WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
        );
    }


    public static MemberDeclarationSyntax GetInterfaceMethodSyntax(MethodInfo methodInfo)
    {
      TypeSyntax returnType = methodInfo.ReturnTypeNameWithNullabilityAnnotations is null
        ? PredefinedType(Token(SyntaxKind.VoidKeyword))
        : IdentifierName(methodInfo.ReturnTypeNameWithNullabilityAnnotations);
      var parameters = methodInfo.Parameters.Select(p =>
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
        if (p.IsOptional && p.ExplicitDefaultValue is not null)
        {
          parameter = parameter.WithDefault(EqualsValueClause(ParseExpression(p.ExplicitDefaultValue.ToString())));
        }
        return parameter;
      });
      return MethodDeclaration(returnType, Identifier(methodInfo.MethodName))
        .AddParameterListParameters(parameters.ToArray())
        .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
    }


    public static MemberDeclarationSyntax GetImplementationMethodSyntax(MethodInfo methodInfo,
                                                                        string underlyingCallee)
    {
      TypeSyntax returnType = methodInfo.ReturnTypeNameWithNullabilityAnnotations is null
        ? PredefinedType(Token(SyntaxKind.VoidKeyword))
        : IdentifierName(methodInfo.ReturnTypeNameWithNullabilityAnnotations);
      var parameters = methodInfo.Parameters.Select(p =>
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
            _ => throw new NotImplementedException()
          }));
        }
        if (p.IsParams)
        {
          parameter = parameter.AddModifiers(Token(SyntaxKind.ParamsKeyword));
        }
        if (p.IsOptional && p.ExplicitDefaultValue is not null)
        {
          parameter = parameter.WithDefault(EqualsValueClause(ParseExpression(p.ExplicitDefaultValue.ToString())));
        }
        return parameter;
      });
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
            IdentifierName($"{underlyingCallee}.{methodInfo.MethodName}"),
            ArgumentList(SeparatedList(arguments, separators))
          )
        ))
        .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
    }
  }
}
