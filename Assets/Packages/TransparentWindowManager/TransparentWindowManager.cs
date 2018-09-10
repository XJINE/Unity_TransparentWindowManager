using System;
using System.Runtime.InteropServices;

public class TransparentWindowManager : SingletonMonoBehaviour<TransparentWindowManager>
{
    #region Enum

    internal enum WindowCompositionAttribute
    {
        WCA_ACCENT_POLICY = 19
    }

    internal enum AccentState
    {
        ACCENT_DISABLED = 0,
        ACCENT_ENABLE_GRADIENT = 1,
        ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
        ACCENT_ENABLE_BLURBEHIND = 3,
        ACCENT_INVALID_STATE = 4
    }

    #endregion Enum

    #region Struct

    private struct MARGINS
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    }

    internal struct WindowCompositionAttributeData
    {
        public WindowCompositionAttribute Attribute;
        public IntPtr Data;
        public int SizeOfData;
    }

    internal struct AccentPolicy
    {
        public AccentState AccentState;
        public int AccentFlags;
        public int GradientColor;
        public int AnimationId;
    }

    #endregion Struct

    #region DLL Import

    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

    [DllImport("Dwmapi.dll")]
    private static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);

    #endregion DLL Import

    #region Method

    // CAUTION:
    // To control enable or disable, use Start method instead of Awake.

    protected virtual void Start()
    {
        #if !UNITY_EDITOR && UNITY_STANDALONE_WIN

        const int  GWL_STYLE = -16;
        const uint WS_POPUP = 0x80000000;
        const uint WS_VISIBLE = 0x10000000;

        // NOTE:
        // https://msdn.microsoft.com/ja-jp/library/cc410861.aspx

        var windowHandle = GetActiveWindow();

        // NOTE:
        // https://msdn.microsoft.com/ja-jp/library/cc411203.aspx
        // 
        // "SetWindowLong" is used to update window parameter.
        // The arguments shows (target, parameter, value).

        SetWindowLong(windowHandle, GWL_STYLE, WS_POPUP | WS_VISIBLE);

        // NOTE:
        // https://msdn.microsoft.com/ja-jp/library/windows/desktop/aa969512(v=vs.85).aspx
        // https://msdn.microsoft.com/ja-jp/library/windows/desktop/bb773244(v=vs.85).aspx
        // 
        // DwmExtendFrameIntoClientArea will spread the effects
        // which attached to window frame to contents area.
        // So if the frame is transparent, the contents area gets also transparent.
        // MARGINS is structure to set the spread range.
        // When set -1 to MARGIN, it means spread range is all of the contents area.

        MARGINS margins = new MARGINS()
        {
            cxLeftWidth = -1
        };

        DwmExtendFrameIntoClientArea(windowHandle, ref margins);

        #endif // !UNITY_EDITOR && UNITY_STANDALONE_WIN
    }

    #endregion Method
}