using Microsoft.Dynamics.Nav.CodeAnalysis;
using Microsoft.Dynamics.Nav.CodeAnalysis.Diagnostics;
using Microsoft.Dynamics.Nav.CodeAnalysis.Syntax;
using System;
using System.Collections.Immutable;

namespace CustomALCodeCop.Design
{
    [DiagnosticAnalyzer]
    public class Rule0008ToolTipPunctuation : DiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = ImmutableArray.Create<DiagnosticDescriptor>(DiagnosticDescriptors.Rule0008ToolTipPunctuation);

        public override void Initialize(AnalysisContext context) => context.RegisterSyntaxNodeAction(new Action<SyntaxNodeAnalysisContext>(this.AnalyzeToolTipPunctuation), SyntaxKind.PageField, SyntaxKind.PageAction);

        private void AnalyzeToolTipPunctuation(SyntaxNodeAnalysisContext ctx)
        {
            PropertySyntax tooltipProperty = ctx.Node.GetProperty("ToolTip");

            if (tooltipProperty != null)
            {
                string tooltipValue = tooltipProperty.Value.GetText().ToString();
                if (!tooltipValue.EndsWith(".'") && !tooltipValue.EndsWith(".)'"))
                    ctx.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.Rule0008ToolTipPunctuation, tooltipProperty.GetLocation()));
            }

        }
    }
}