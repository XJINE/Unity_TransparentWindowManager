using UnityEngine;
using System.Runtime.InteropServices;
using System;

public class FramelessWindowManager : SingletonMonoBehaviour<FramelessWindowManager>
{
    // NOTE:
    // Unity has a command-line option "-popupwindow" to make the window frameless.

    #region DLL Import

    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

    [DllImport("user32.dll")]
    static extern bool SetWindowPos
        (IntPtr hWnd, int hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

    #endregion DLL Import

    #region Field

    public Rect windowRect;

    #endregion Field

    #region Method

    protected virtual void Start ()
    {
        #if !UNITY_EDITOR && UNITY_STANDALONE_WIN

        const uint SWP_SHOWWINDOW = 0x0040;
        const int GWL_STYLE = -16;
        const uint WS_BORDER = 1;

        IntPtr windowHandle = GetActiveWindow();
        SetWindowLong(windowHandle, GWL_STYLE, WS_BORDER);
        SetWindowPos(windowHandle,
                     0,
                     (int)windowRect.x,
                     (int)windowRect.y,
                     (int)windowRect.width,
                     (int)windowRect.height,
                     SWP_SHOWWINDOW);

        #endif // !UNITY_EDITOR && UNITY_STANDALONE_WIN
    }

    #endregion Method
}