using UnityEngine;

public class CanvasAdjuster : MonoBehaviour
{
    public Canvas canvas;

    void Start()
    {
        AdjustCanvas();
    }

    void AdjustCanvas()
    {
        if (canvas == null) canvas = GetComponent<Canvas>();

        float screenHeight = Screen.height;
        float screenWidth = Screen.width;
        float screenAspect = screenWidth / screenHeight;
        float canvasAspect = canvas.GetComponent<RectTransform>().rect.width / canvas.GetComponent<RectTransform>().rect.height;

        if (screenAspect > canvasAspect)
        {
            float normalizedWidth = screenAspect / canvasAspect;
            canvas.GetComponent<RectTransform>().sizeDelta = new Vector2(normalizedWidth * 100, 100);
        }
        else
        {
            float normalizedHeight = canvasAspect / screenAspect;
            canvas.GetComponent<RectTransform>().sizeDelta = new Vector2(100, normalizedHeight * 100);
        }
    }
}
