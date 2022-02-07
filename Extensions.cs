﻿using Microsoft.Dynamics.Nav.CodeAnalysis;
using Microsoft.Dynamics.Nav.CodeAnalysis.Diagnostics;
using Microsoft.Dynamics.Nav.CodeAnalysis.Syntax;
using System.Text.RegularExpressions;
using System.Collections.Immutable;
using System.Threading;


namespace CustomALCodeCop
{
  internal static class Extensions
  {
    private static readonly Regex CamelCaseRegex = new Regex("^[a-z][a-zA-Z0-9]*$", RegexOptions.Compiled);

    internal static bool IsTestCodeunit(this IApplicationObjectTypeSymbol symbol) => symbol is ICodeunitTypeSymbol codeunitTypeSymbol && codeunitTypeSymbol.Subtype == CodeunitSubtypeKind.Test;

    internal static bool IsUpgradeCodeunit(this IApplicationObjectTypeSymbol symbol) => symbol is ICodeunitTypeSymbol codeunitTypeSymbol && codeunitTypeSymbol.Subtype == CodeunitSubtypeKind.Upgrade;

    internal static bool IsAllowedLowerPermissionObject(this IApplicationObjectTypeSymbol symbol) => symbol.Kind == SymbolKind.Codeunit && (symbol.Id == 132218 && SemanticFacts.IsSameName(symbol.Name, "Permission Test Catalog") || symbol.Id == 132230 && SemanticFacts.IsSameName(symbol.Name, "Library - E2E Role Permissions"));

    internal static int GetTokenLine(this SyntaxToken token) => token.GetLocation().GetMappedLineSpan().StartLinePosition.Line;

    internal static bool IsValidCamelCase(this string str) => !string.IsNullOrEmpty(str) && Extensions.CamelCaseRegex.IsMatch(str);

    internal static bool HasKind(this SyntaxTriviaList triviaList, SyntaxKind kind)
    { 
        foreach(SyntaxTrivia trivia in triviaList)
            {
                if (trivia.IsKind(kind)) return true;
            }
            return false;
    }
    internal static IPropertySymbol GetProperty(this ImmutableArray<IPropertySymbol> propertyList, PropertyKind kind)
    {
        foreach (IPropertySymbol property in propertyList)
            if (property.PropertyKind == kind) return property;
            return null;
    }

        internal static IdentifierNameSyntax GetIdentifierNameSyntax(
      this SyntaxNodeAnalysisContext context)
    {
        if (context.Node.IsKind(SyntaxKind.IdentifierName))
        return (IdentifierNameSyntax) context.Node;
        return !context.Node.IsKind(SyntaxKind.IdentifierNameOrEmpty) ? (IdentifierNameSyntax) null : ((IdentifierNameOrEmptySyntax) context.Node).IdentifierName;
    }

    internal static bool TryGetSymbolFromIdentifier(
      SyntaxNodeAnalysisContext syntaxNodeAnalysisContext,
      IdentifierNameSyntax identifierName,
      SymbolKind symbolKind,
      out ISymbol symbol)
    {
        symbol = (ISymbol) null;
        SymbolInfo symbolInfo = syntaxNodeAnalysisContext.SemanticModel.GetSymbolInfo((ExpressionSyntax) identifierName, new CancellationToken());
        ISymbol symbol1 = symbolInfo.Symbol;
        if ((symbol1 != null ? (symbol1.Kind != symbolKind ? 1 : 0) : 1) != 0)
        return false;
        symbol = symbolInfo.Symbol;
        return true;
    }
  }
}
