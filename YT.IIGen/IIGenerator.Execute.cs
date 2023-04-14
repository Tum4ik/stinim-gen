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
        .AddModifiers(Token(SyntaxKind.PublicKeyword))
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
  }
}
