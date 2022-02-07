using Microsoft.Dynamics.Nav.CodeAnalysis;
using Microsoft.Dynamics.Nav.CodeAnalysis.Diagnostics;
using Microsoft.Dynamics.Nav.CodeAnalysis.Syntax;
using System;
using System.Collections.Immutable;

namespace CustomALCodeCop.Design
{
    [DiagnosticAnalyzer]
    public class Rule0011LabelPunctuation : DiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = ImmutableArray.Create<DiagnosticDescriptor>(DiagnosticDescriptors.Rule0011LabelPunctuation);

        public override void Initialize(AnalysisContext context) => context.RegisterSyntaxNodeAction(new Action<SyntaxNodeAnalysisContext>(this.AnalyzeLabelPunctuation), SyntaxKind.VariableDeclaration);

        private void AnalyzeLabelPunctuation(SyntaxNodeAnalysisContext ctx)
        {
            if (ctx.ContainingSymbol.IsObsoletePending || ctx.ContainingSymbol.IsObsoleteRemoved) return;
            if (ctx.ContainingSymbol.GetContainingObjectTypeSymbol().IsObsoletePending || ctx.ContainingSymbol.GetContainingObjectTypeSymbol().IsObsoleteRemoved) return;

            VariableDeclarationSyntax syntax = ctx.Node as VariableDeclarationSyntax;
            if (syntax != null)
            {
                string variableName = syntax.GetNameStringValue();
                if (syntax.Type.DataType.Kind != SyntaxKind.LabelDataType) return;
                if (!variableName.EndsWith("Msg") && !variableName.EndsWith("Err")) return;

                dynamic labelDataType = syntax.Type.DataType;
                string labelText = labelDataType.Label.LabelText.Value.Text;

                if (!labelText.EndsWith(".'"))
                {
                    ctx.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.Rule0011LabelPunctuation, syntax.Name.GetLocation()));
                }
            }
        }
    }
}