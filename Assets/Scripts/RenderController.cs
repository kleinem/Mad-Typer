using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RenderController : MonoBehaviour, IPointerClickHandler
{

    private RectTransform rectTransform;

    void Awake() {

        rectTransform = GetComponent<RectTransform>();

    }

    public void OnPointerClick(PointerEventData data) {

        Vector2 position;
        
        RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(), data.position, null, out position);
        position.y = (rectTransform.rect.yMin * -1) - (position.y * -1);
        Vector2 viewPosition = new Vector2((position.x / rectTransform.rect.xMax + 1f) / 2f, position.y / (rectTransform.rect.yMin * -1));
        Ray ray = Camera.main.ViewportPointToRay((Vector3)viewPosition);
        Physics.Raycast(ray, out RaycastHit hit);
        //Debug.DrawLine(ray.GetPoint(0), hit.point, Color.blue, 9999);
        //Debug.Log(hit.point);
        Collider2D hitObject = Physics2D.OverlapPoint((Vector2)hit.point);
        if (hitObject != null && hitObject.gameObject.CompareTag("Letter")) {

            hitObject.gameObject.GetComponent<LetterController>().click();

        }
        
    }

}
