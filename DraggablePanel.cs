using Platformer;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggablePanel : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler,IPointerUpHandler 
{
    private RectTransform panelRectTransform;
    private Canvas canvas;
    //private CanvasGroup canvasGroup;
    private Transform initialParent;
    private Vector3 initialPosition;
    private float scaleAmount = 1.05f;
    private Vector2 defaultScale; 
    private void Start()
    {
        panelRectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        //canvasGroup = GetComponent<CanvasGroup>();
        initialParent = transform.parent;
        initialPosition = transform.position;
        defaultScale = transform.localScale; 
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        //canvasGroup.blocksRaycasts = false;
       
    }

    public void OnDrag(PointerEventData eventData)
    {
        panelRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        // Find the nearest slot
        AnchorPanels nearestSlot = FindNearestSlot(eventData.pointerEnter);
        SwapWithSlot(nearestSlot);

        //if (nearestSlot != null)
        //{
        //    SwapWithSlot(nearestSlot);
        //}
        //else
        //{
        //    SnapToInitialPosition();
        //}
    }

    private AnchorPanels FindNearestSlot(GameObject pointerEnterObject)
    {
        AnchorPanels[] slots = FindObjectsOfType<AnchorPanels>();
        AnchorPanels closestSlot = null; 
        float minDistance = float.MaxValue;
        //Transform closestSlot = null;

        // Iterate through slots to find the nearest one
        foreach (AnchorPanels slot in slots)
        {
            //if (slot != this)
            //{
                float distance = Vector3.Distance(transform.position, slot.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestSlot = slot; 
                }
            //}
        }
        return closestSlot; 
    }
    private DraggablePanel getChildPanel(AnchorPanels anchor)
    {
        if (anchor == null) return null;
        DraggablePanel draggableChild = anchor.transform.GetComponentInChildren<DraggablePanel>();
        if (!draggableChild.tag.Contains("DraggablePanel")) return null; 
        //Transform child = anchor.transform.find("panel");
        //if (child == null) return null; 
        //draggablepanel draggablechild =  child.GetComponent<DraggablePanel>();
        Debug.Log("Found draggable child:" + draggableChild.GetInstanceID()); 
        return draggableChild; 
    }
    private void SwapWithSlot(AnchorPanels otherPanel)
    {
        
        if (otherPanel == null)
        {
            transform.position = transform.parent.transform.position;
            return; 
        }
        Debug.Log("Nearest slot:" + otherPanel.name);
        DraggablePanel draggableChildOfOtherPanel = getChildPanel(otherPanel);
        Vector3 tempPosition = transform.parent.transform.position;
        Transform tempParent = transform.parent;
        if (draggableChildOfOtherPanel != null)
        {
            // Swap positions between current DraggablePanel and child DraggablePanel of otherPanel
            transform.position = otherPanel.transform.position;
            transform.SetParent(draggableChildOfOtherPanel.transform.parent);

            draggableChildOfOtherPanel.transform.position = tempPosition;
            draggableChildOfOtherPanel.transform.SetParent(tempParent);

            Debug.Log("Swapped positions and parents.");
        }
        else
        {
            transform.position = tempParent.transform.position; 
            Debug.Log("Child DraggablePanel of otherPanel not found.");
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        panelRectTransform.localScale = defaultScale * scaleAmount;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        panelRectTransform.localScale = defaultScale;
    }
    //DraggablePanel targetPanel = getChildPanel(otherPanel);
    /*
     * 1. Myself stick to target anchor 
     * 2. Child panel of the target anchor stick to my anchor 
     * 
     */
    // Vector3 targetAnchorPos = otherPanel.transform.position;
    // Transform tempParent = otherPanel.transform.parent;
    //targetPanel.transform.position = initialPosition;
    //    targetPanel.transform.SetParent(initialParent); 

    //    transform.position = otherPanel.transform.position;
    //    transform.SetParent(otherPanel.transform.parent);
    //private void SnapToInitialPosition()
    //{
    //    transform.position = initialPosition;
    //    transform.SetParent(initialParent);
    //}
}
