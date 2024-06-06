using UnityEngine;
using UnityEngine.UI;

public class ScrollbarVisibilityController : MonoBehaviour
{
    public ScrollRect scrollRect; // Reference to the ScrollRect component
    public Scrollbar verticalScrollbar; // Reference to the vertical scrollbar
    public RectTransform content; // Reference to the content RectTransform

    private void Update()
    {
        // Check the vertical content size against the ScrollRect's viewport height
        if (scrollRect != null && content != null && verticalScrollbar != null)
        {
            float contentHeight = content.rect.height;
            float viewportHeight = scrollRect.viewport.rect.height;

            // Show or hide the vertical scrollbar based on content size
            if(contentHeight > viewportHeight)
            {
                //verticalScrollbar.gameObject.SetActive(true);
            }
            else
            {
                //verticalScrollbar.gameObject.SetActive(false);
            }
        }

    }
}
