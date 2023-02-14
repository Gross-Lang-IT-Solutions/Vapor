using System.Reflection.Metadata;
using System.Runtime.InteropServices;

namespace VaporExperiment;

internal static partial class FormExtensions
{
    [StructLayout(LayoutKind.Sequential)]
    private struct WindowCompositionAttributeData
    {
        public WindowCompositionAttribute Attribute;
        public IntPtr Data;
        public int SizeOfData;
    }

    private enum WindowCompositionAttribute
    {
        WCA_ACCENT_POLICY = 19
    }

    private enum AccentState
    {
        ACCENT_DISABLED = 0,
        ACCENT_ENABLE_GRADIENT = 1,
        ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
        ACCENT_ENABLE_BLURBEHIND = 3,
        ACCENT_INVALID_STATE = 4
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct AccentPolicy
    {
        public AccentState AccentState;
        public int AccentFlags;
        public int GradientColor;
        public int AnimationId;
    }

    [LibraryImport("user32.dll")]
    private static partial int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

    public static unsafe void EnableTransparentBlur(this Form self)
    {
        var accent = new AccentPolicy
        {
            AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND
        };

        var accentStructSize = sizeof(AccentPolicy);

        var data = new WindowCompositionAttributeData
        {
            Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
            SizeOfData = accentStructSize,
            Data = new nint(&accent)
        };
        SetWindowCompositionAttribute(self.Handle, ref data);

        self.BackColor = Color.LightPink;
        self.TransparencyKey = Color.LightPink;
    }
}
