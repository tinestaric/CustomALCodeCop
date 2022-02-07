using Microsoft.Dynamics.Nav.CodeAnalysis;
using Microsoft.Dynamics.Nav.CodeAnalysis.Diagnostics;
using Microsoft.Dynamics.Nav.CodeAnalysis.Syntax;
using System;
using System.Collections.Immutable;

namespace CustomALCodeCop.Design
{
    [DiagnosticAnalyzer]
    public class Rule0003ParameterNaming : DiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = ImmutableArray.Create<DiagnosticDescriptor>(DiagnosticDescriptors.Rule0003ParameterNaming);

        public override void Initialize(AnalysisContext context) => context.RegisterSyntaxNodeAction(new Action<SyntaxNodeAnalysisContext>(this.AnalyzeParameterNaming), SyntaxKind.Parameter);

        private void AnalyzeParameterNaming(SyntaxNodeAnalysisContext ctx)
        {
            if (ctx.ContainingSymbol.IsObsoletePending || ctx.ContainingSymbol.IsObsoleteRemoved) return;
            if (ctx.ContainingSymbol.GetContainingObjectTypeSymbol().IsObsoletePending || ctx.ContainingSymbol.GetContainingObjectTypeSymbol().IsObsoleteRemoved) return;

            ParameterSyntax syntax = ctx.Node as ParameterSyntax;
            if (syntax.Parent.Parent.Kind == SyntaxKind.TriggerDeclaration) return;

            if (syntax != null)
            {
                string parameterName = syntax.GetNameStringValue();

                foreach (SyntaxNodeOrToken Token in syntax.Parent.Parent.GetAnnotatedNodesAndTokens(AnnotationKind.AttributeType))
                {
                    if (Token.ToString().Contains("EventSubscriber")) return;
                }
                if (!parameterName.StartsWith("v") && !parameterName.StartsWith("Temp"))
                {
                    ctx.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.Rule0003ParameterNaming, syntax.Name.GetLocation()));
                }
            }
        }
    }
}