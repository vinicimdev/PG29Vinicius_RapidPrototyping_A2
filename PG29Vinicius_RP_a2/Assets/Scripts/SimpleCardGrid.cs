using UnityEngine;

public class SimpleCardGrid : MonoBehaviour
{
    public int columnsPerRow = 6;
    public float cardWidth = 130;
    public float cardHeight = 180;
    public float spacingX = 10;
    public float spacingY = 10;
    public float paddingTop = 10;
    public float paddingLeft = 10;

    public void LayoutCards()
    {
        Debug.Log($"LayoutCards called! Child count: {transform.childCount}");

        RectTransform containerRect = GetComponent<RectTransform>();

        int column = 0;
        int row = 0;
        int cardCount = 0;

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            // Skip disabled cards
            if (!child.gameObject.activeSelf)
            {
                continue;
            }

            RectTransform childRect = child.GetComponent<RectTransform>();

            if (childRect == null)
            {
                continue;
            }

            // Set anchor to top-left for proper positioning
            childRect.anchorMin = new Vector2(0, 1);
            childRect.anchorMax = new Vector2(0, 1);
            childRect.pivot = new Vector2(0, 1);

            // Calculate position from top-left
            float x = paddingLeft + column * (cardWidth + spacingX);
            float y = -(paddingTop + row * (cardHeight + spacingY));

            // Set size and position
            childRect.sizeDelta = new Vector2(cardWidth, cardHeight);
            childRect.anchoredPosition = new Vector2(x, y);

            cardCount++;
            column++;
            if (column >= columnsPerRow)
            {
                column = 0;
                row++;
            }
        }

        // Set container height based on total rows
        int totalRows = Mathf.CeilToInt((float)cardCount / columnsPerRow);
        float totalHeight = paddingTop + totalRows * (cardHeight + spacingY) + paddingTop;

        containerRect.sizeDelta = new Vector2(containerRect.sizeDelta.x, totalHeight);

        Debug.Log($"Layout complete! Positioned {cardCount} active cards in {totalRows} rows. Container height: {totalHeight}");
    }
}