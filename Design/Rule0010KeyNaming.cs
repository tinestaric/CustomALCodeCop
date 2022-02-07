using Microsoft.Dynamics.Nav.CodeAnalysis;
using Microsoft.Dynamics.Nav.CodeAnalysis.Diagnostics;
using Microsoft.Dynamics.Nav.CodeAnalysis.Syntax;
using System;
using System.Collections.Immutable;

namespace CustomALCodeCop.Design
{
    [DiagnosticAnalyzer]
    public class Rule0010KeyNaming : DiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = ImmutableArray.Create<DiagnosticDescriptor>(DiagnosticDescriptors.Rule0010KeyNaming);

        public override void Initialize(AnalysisContext context) => context.RegisterSyntaxNodeAction(new Action<SyntaxNodeAnalysisContext>(this.AnalyzeKeyNaming), SyntaxKind.TableObject);

        private void AnalyzeKeyNaming(SyntaxNodeAnalysisContext ctx)
        {
            TableSyntax syntax = ctx.Node as TableSyntax;
            KeyListSyntax keyList = syntax.Keys;

            if (keyList == null) return;

            string objectName = syntax.GetNameStringValue().Split('_')[1];
            bool primaryKey = true;
            int i = 0;

            foreach (KeySyntax key in keyList.Keys)
            {
                if (primaryKey)
                {
                    if (key.GetNameStringValue() != "PK_" + objectName)
                        ctx.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.Rule0010KeyNaming, key.Name.GetLocation(), "PK_" + objectName));
                }
                else
                {
                    i += 1;
                    if (key.GetNameStringValue() != "SK0" + i.ToString() + "_" + objectName)
                        ctx.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.Rule0010KeyNaming, key.Name.GetLocation(), "SK0" + i.ToString() + "_" + objectName));
                }
                primaryKey = false;
            }
        }
    }
}