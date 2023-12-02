using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image _image;
    [HideInInspector] public Transform parentAfterDrag;
    [SerializeField] Vector2 _dragScale;
    [SerializeField] Color _hoverColor;
    Color _lastColor;
    public void OnBeginDrag(PointerEventData eventData){
        Debug.Log("BeginDrag");
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        _image.raycastTarget = false;
        _image.rectTransform.sizeDelta = _dragScale;
    }

    public void OnDrag(PointerEventData eventData){
        Debug.Log("Dragging");
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData){
        Debug.Log("EndDrag");
        transform.SetParent(parentAfterDrag);
        _image.raycastTarget = true;
        _image.SetNativeSize();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _lastColor = _image.color;
        _image.color = _hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _image.color = _lastColor;
    }
}
