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


    public static MemberDeclarationSyntax GetInterfaceMethodSyntax(MethodDeclarationSyntax methodDeclarationSyntax)
    {
      return methodDeclarationSyntax
        .WithModifiers(TokenList())
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
