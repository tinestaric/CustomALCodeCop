using Microsoft.Dynamics.Nav.CodeAnalysis;
using Microsoft.Dynamics.Nav.CodeAnalysis.Diagnostics;
using Microsoft.Dynamics.Nav.CodeAnalysis.Syntax;
using System;
using System.Collections.Immutable;

namespace CustomALCodeCop.Design
{
    [DiagnosticAnalyzer]
    public class Rule0005ExitKeywordCapitalization : DiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = ImmutableArray.Create<DiagnosticDescriptor>(DiagnosticDescriptors.Rule0005ExitKeywordCapitalization);

        public override void Initialize(AnalysisContext context) => context.RegisterSyntaxNodeAction(new Action<SyntaxNodeAnalysisContext>(this.AnalyzeExitKeywordCapitalization), SyntaxKind.ExitStatement);

        private void AnalyzeExitKeywordCapitalization(SyntaxNodeAnalysisContext ctx)
        {
            if (ctx.ContainingSymbol.IsObsoletePending || ctx.ContainingSymbol.IsObsoleteRemoved) return;
            if (ctx.ContainingSymbol.GetContainingObjectTypeSymbol().IsObsoletePending || ctx.ContainingSymbol.GetContainingObjectTypeSymbol().IsObsoleteRemoved) return;
            
            ExitStatementSyntax syntax = ctx.Node as ExitStatementSyntax;

            if (syntax.ExitKeywordToken.ToString() != syntax.ExitKeywordToken.ToString().ToUpper())
            {
                ctx.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.Rule0005ExitKeywordCapitalization, syntax.ExitKeywordToken.GetLocation()));
            }
        }
    }
}