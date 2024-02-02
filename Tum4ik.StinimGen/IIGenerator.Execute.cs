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
    public static MemberDeclarationSyntax GetInterfacePropertySyntax(PropertyInfo propertyInfo, IIInfo iiInfo)
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

      var typeFullName = iiInfo.SourceFullyQualifiedName;
      var propertyName = propertyInfo.PropertyName;
      var signature = $"{typeFullName}.{propertyName}"
        .Replace('<', '{')
        .Replace('>', '}');

      return PropertyDeclaration(
          IdentifierName(propertyInfo.TypeNameWithNullabilityAnnotations),
          Identifier(propertyName)
        )
        .WithLeadingTrivia(Comment($"/// <inheritdoc cref=\"{signature}\" />"))
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
        .WithLeadingTrivia(Comment("/// <inheritdoc />"))
        .AddAccessorListAccessors(accessorsList.ToArray());
    }


    public static MemberDeclarationSyntax GetInterfaceEventSyntax(EventInfo eventInfo, IIInfo iiInfo)
    {
      var typeFullName = iiInfo.SourceFullyQualifiedName;
      var eventName = eventInfo.EventName;
      var signature = $"{typeFullName}.{eventName}"
        .Replace('<', '{')
        .Replace('>', '}');
      return EventDeclaration(
          IdentifierName(eventInfo.TypeNameWithNullabilityAnnotations),
          Identifier(eventName)
        )
        .WithLeadingTrivia(Comment($"/// <inheritdoc cref=\"{signature}\" />"))
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
        .WithLeadingTrivia(Comment("/// <inheritdoc />"))
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


    public static MemberDeclarationSyntax GetInterfaceMethodSyntax(MethodDeclarationSyntax methodDeclarationSyntax,
                                                                   IIInfo iiInfo)
    {
      var typeFullName = iiInfo.SourceFullyQualifiedName;
      var methodName = methodDeclarationSyntax.Identifier.ValueText;
      var genericParameters = methodDeclarationSyntax.TypeParameterList?
        .Parameters
        .Select(p => p.Identifier.ValueText)
        .ToList();
      var parameters = methodDeclarationSyntax.ParameterList
        .Parameters
        .Select(p =>
        {
          var mod = p.Modifiers.FirstOrDefault(
            m => m.IsKind(SyntaxKind.RefKeyword)
              || m.IsKind(SyntaxKind.OutKeyword)
              || m.IsKind(SyntaxKind.InKeyword)
          );
          return $"{(mod == default ? "" : mod.ValueText + " ")}{p.Type}";
        })
        .ToList();

      var genericParametersPart = genericParameters is null || genericParameters.Count == 0
        ? string.Empty
        : $"{{{string.Join(", ", genericParameters)}}}";
      var parametersPart = parameters.Count == 0
        ? string.Empty
        : string.Join(", ", parameters);
      var signature = $"{typeFullName}.{methodName}{genericParametersPart}({parametersPart})"
        .Replace('<', '{')
        .Replace('>', '}');

      return methodDeclarationSyntax
        .WithModifiers(TokenList())
        .WithLeadingTrivia(Comment($"/// <inheritdoc cref=\"{signature}\" />"))
        .WithBody(null)
        .WithExpressionBody(null)
        .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
    }


    public static MemberDeclarationSyntax GetImplementationMethodSyntax(MethodDeclarationSyntax methodDeclarationSyntax,
                                                                        IIInfo iiInfo)
    {
      var typeParameterList = methodDeclarationSyntax.TypeParameterList;
      ExpressionSyntax invocationExpressionIdentifier;
      if (typeParameterList is not null && typeParameterList.Parameters.Count > 0)
      {
        invocationExpressionIdentifier = GenericName(
          $"{iiInfo.SourceFullyQualifiedName}.{methodDeclarationSyntax.Identifier}"
        )
        .WithTypeArgumentList(
          TypeArgumentList(
            SeparatedList(typeParameterList.Parameters.Select(p => ParseTypeName(p.Identifier.ValueText)))
          )
        );
      }
      else
      {
        invocationExpressionIdentifier = IdentifierName(
          $"{iiInfo.SourceFullyQualifiedName}.{methodDeclarationSyntax.Identifier}"
        );
      }
      return methodDeclarationSyntax
        .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
        .WithLeadingTrivia(Comment("/// <inheritdoc />"))
        .WithBody(null)
        .WithExpressionBody(
          ArrowExpressionClause(
            InvocationExpression(
              invocationExpressionIdentifier,
              ArgumentList(
                SeparatedList(methodDeclarationSyntax.ParameterList.Parameters.Select(
                  p =>
                  {
                    var refKeywork = p.Modifiers.FirstOrDefault(
                      m => m.IsKind(SyntaxKind.RefKeyword)
                        || m.IsKind(SyntaxKind.OutKeyword)
                        || m.IsKind(SyntaxKind.InKeyword)
                    );
                    return Argument(IdentifierName(p.Identifier)).WithRefKindKeyword(refKeywork);
                  }
                ))
              )
            )
          )
        )
        .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
    }
  }
}
