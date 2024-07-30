using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ScreenShot : MonoBehaviour
{
    public Camera captureCamera;

    [DllImport("__Internal")]
    private static extern void SaveScreenShotJS(string base64);

    void Start()
    {
        // Add a listener to the button
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            TakeScreenShot();
        });
    }

    void TakeScreenShot()
    {
        RenderTexture currentRT = RenderTexture.active;

        RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        captureCamera.targetTexture = renderTexture;
        // RenderTexture.active = renderTexture;

        captureCamera.Render();

        // copy the camera's target texture to a new texture
        Texture2D texture = new Texture2D(captureCamera.targetTexture.width, captureCamera.targetTexture.height, TextureFormat.RGB24, false);
        RenderTexture.active = captureCamera.targetTexture;
        texture.ReadPixels(new Rect(0, 0, captureCamera.targetTexture.width, captureCamera.targetTexture.height), 0, 0);
        texture.Apply();

        // convert the texture to a PNG image
        var png = texture.EncodeToPNG();
        var base64 = Convert.ToBase64String(png);

        // save the texture to a file
        try
        {
            SaveScreenShotJS("data:image/png;base64," + base64);
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to save screenshot: " + e.Message);
        }

        // clean up
        RenderTexture.active = currentRT;
        RenderTexture.active = null;
        captureCamera.targetTexture = null;
    }
}
