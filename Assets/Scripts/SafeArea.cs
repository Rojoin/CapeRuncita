using UnityEngine;

public class SafeArea : MonoBehaviour
{
    private RectTransform rect;
    private Rect safeArea;
    private Vector2 minAnchor;
    private Vector2 maxAnchor;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        safeArea = Screen.safeArea;
        minAnchor = safeArea.position;
        maxAnchor = minAnchor + safeArea.size;

        minAnchor.x /= Screen.width;
        minAnchor.y /= Screen.height;
        maxAnchor.x /= Screen.width;
        maxAnchor.y /= Screen.height;
        rect.anchorMin = minAnchor;
        rect.anchorMax = maxAnchor;
    }

}
