# Unity_TransparentWindow

Make Unity's window transparent and overlay on desktop.

![](https://github.com/XJINE/Unity_TransparentWindow/blob/master/screenshot.png)

## Limitation

This is for Windows, not works on Mac or any others.

Transparency is ignored when use window mode in some laptop (especially when it use mobile type GPU).
Need to use full-screen mode in such case.

In another way, use command-line option like a following
and make the popup-window which has more over full-screen resolution.

```
-popupwindow -screen-width xxxx -screen-height xxxx
```

## To Make Transparent Window

To make transparent window, set Camera.ClearFlags to "SolidColor", and the Camera.Background to (0,0,0,0).
When set Camera.Background to (1,1,1,0), the result gets wrong.

## Anti-Aliasing

Some anti-aliasing shader gets wrong result.

<table>
<tr><th>DLAA</th><td>Wrong edges appears in the result.</td></tr>
<tr><th>FXAA2</th><td>Some non-transparent pixel gets wrong transparency.</td></tr>
<tr><th>FXAA3</th><td>All of the transparent pixel (in backgrounds) losts the transparency in the result.</td></tr>
</table>

## Note

Sometimes tearing occurs and I can't find the cause.
However, this is not serious.

There is a little jaggy even if use anti-aliasing, especially in low-dpi.
