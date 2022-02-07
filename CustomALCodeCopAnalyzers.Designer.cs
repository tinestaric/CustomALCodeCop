using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace CustomALCodeCop
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  internal class CustomALCodeCopAnalyzers
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal CustomALCodeCopAnalyzers()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (CustomALCodeCopAnalyzers.resourceMan == null)
          CustomALCodeCopAnalyzers.resourceMan = new ResourceManager("CustomALCodeCop.CustomALCodeCopAnalyzers", typeof (CustomALCodeCopAnalyzers).Assembly);
        return CustomALCodeCopAnalyzers.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => CustomALCodeCopAnalyzers.resourceCulture;
      set => CustomALCodeCopAnalyzers.resourceCulture = value;
    }

    internal static string AnalyzerPrefix => CustomALCodeCopAnalyzers.ResourceManager.GetString(nameof (AnalyzerPrefix), CustomALCodeCopAnalyzers.resourceCulture);
  }
}
