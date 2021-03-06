using Microsoft.Dynamics.Nav.CodeAnalysis;
using Microsoft.Dynamics.Nav.CodeAnalysis.Diagnostics;
using Microsoft.Dynamics.Nav.CodeAnalysis.Syntax;
using System;
using System.Collections.Immutable;

namespace CustomALCodeCop.Design
{
    [DiagnosticAnalyzer]
    public class Rule0007InternalProcedure : DiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = ImmutableArray.Create<DiagnosticDescriptor>(DiagnosticDescriptors.Rule0007InternalProcedure);

        public override void Initialize(AnalysisContext context) => context.RegisterSyntaxNodeAction(new Action<SyntaxNodeAnalysisContext>(this.AnalyzeInternalProcedures), SyntaxKind.MethodDeclaration);

        private void AnalyzeInternalProcedures(SyntaxNodeAnalysisContext ctx)
        {
            if (ctx.ContainingSymbol.IsObsoletePending || ctx.ContainingSymbol.IsObsoleteRemoved) return;
            if (ctx.ContainingSymbol.GetContainingObjectTypeSymbol().IsObsoletePending || ctx.ContainingSymbol.GetContainingObjectTypeSymbol().IsObsoleteRemoved) return;

            MethodDeclarationSyntax syntax = ctx.Node as MethodDeclarationSyntax;
            SyntaxNodeOrToken firstToken = syntax.ProcedureKeyword.GetPreviousToken();

            if (firstToken.Kind != SyntaxKind.LocalKeyword && firstToken.Kind != SyntaxKind.InternalKeyword)            
                ctx.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.Rule0007InternalProcedure, syntax.ProcedureKeyword.GetLocation()));
            
        }
    }
}