using UnityEngine;
using UnityEngine.UI;

public class ResizePanelsToGrid : MonoBehaviour
{
    private GridLayoutGroup gridLayout;

    private void Start()
    {
        // Get reference to the GridLayoutGroup component
        gridLayout = GetComponent<GridLayoutGroup>();

        // Resize the child panels according to the grid cell size
        ResizeChildPanels();
    }

    private void ResizeChildPanels()
    {
        if (gridLayout != null)
        {
            // Calculate the size of each cell in the grid
            Vector2 cellSize = gridLayout.cellSize;

            // Loop through each child (panel) within the GridLayoutGroup
            for (int i = 0; i < transform.childCount; i++)
            {
                RectTransform childRectTransform = transform.GetChild(i).GetComponent<RectTransform>();

                if (childRectTransform != null)
                {
                    // Set the size of the child panel to match the cell size
                    childRectTransform.sizeDelta = new Vector2(cellSize.x, cellSize.y);
                }
            }
        }
        else
        {
            Debug.LogError("GridLayoutGroup component not found.");
        }
    }
}
