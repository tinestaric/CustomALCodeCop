using Microsoft.Dynamics.Nav.CodeAnalysis;
using Microsoft.Dynamics.Nav.CodeAnalysis.Diagnostics;
using Microsoft.Dynamics.Nav.CodeAnalysis.Syntax;
using System;
using System.Collections.Immutable;

namespace CustomALCodeCop.Design
{
    [DiagnosticAnalyzer]
    public class Rule0002GlobalVariableNaming : DiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = ImmutableArray.Create<DiagnosticDescriptor>(DiagnosticDescriptors.Rule0002GlobalVariableNaming);

        public override void Initialize(AnalysisContext context) => context.RegisterSyntaxNodeAction(new Action<SyntaxNodeAnalysisContext>(this.AnalyzeGlobalVariableNaming), SyntaxKind.VariableDeclaration);

        private void AnalyzeGlobalVariableNaming(SyntaxNodeAnalysisContext ctx)
        {
            if (ctx.ContainingSymbol.IsObsoletePending || ctx.ContainingSymbol.IsObsoleteRemoved) return;
            if (ctx.ContainingSymbol.GetContainingObjectTypeSymbol().IsObsoletePending || ctx.ContainingSymbol.GetContainingObjectTypeSymbol().IsObsoleteRemoved) return;

            VariableDeclarationSyntax syntax = ctx.Node as VariableDeclarationSyntax;
            if (syntax != null)
            {
                if (syntax.Parent.IsKind(SyntaxKind.VarSection)) return;

                string variableName = syntax.GetNameStringValue();

                if (!variableName.StartsWith("g") && !variableName.StartsWith("Temp"))
                {
                    ctx.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.Rule0002GlobalVariableNaming, syntax.Name.GetLocation()));
                }
            }
        }
    }
}