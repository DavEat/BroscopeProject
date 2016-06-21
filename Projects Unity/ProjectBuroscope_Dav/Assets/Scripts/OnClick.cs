using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class OnClick : MonoBehaviour
    , IPointerClickHandler // 2
    , IDragHandler
    , IPointerEnterHandler
    , IPointerExitHandler
// ... And many more available!
{
    public bool grow, childGrow;
    public int speed = 3, buttonId;
    public string animName;

    public Transform parentGrow, animatorParent;

    public Vector2 baseScale, targetScale;

    public List<RectTransform> listSameLevelElem;

    private Animator anim;

    void Awake()
    {
        baseScale = transform.localScale;
        targetScale = baseScale + baseScale / 10;
        anim = animatorParent.GetComponent<Animator>();
    }

    void Update()
    {
        if (!childGrow && grow)
            if (Input.GetMouseButtonDown(1) || Input.touchCount >= 2)
            {
                grow = false;
                anim.Play(string.Concat(animName, "Return", buttonId));
                if (parentGrow != null)
                    parentGrow.GetComponent<OnClick>().childGrow = false;

                if (listSameLevelElem.Count > 0)
                    foreach (RectTransform elem in listSameLevelElem)
                        elem.GetComponent<OnClick>().enabled = true;
            }

    }

    public void OnPointerClick(PointerEventData eventData) // 3
    {
        if (eventData.button == PointerEventData.InputButton.Left && !grow)
        {
            grow = true;
            anim.Play(string.Concat(animName, buttonId));
            if (parentGrow != null)
                parentGrow.GetComponent<OnClick>().childGrow = true;

            ButtonInteraction.ButtonChangeSize(transform, baseScale);

            if (listSameLevelElem.Count > 0)
                foreach (RectTransform elem in listSameLevelElem)
                    elem.GetComponent<OnClick>().enabled = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        print("I'm being dragged! : " + transform.name);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!grow)
            ButtonInteraction.ButtonChangeSize(transform, targetScale);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!grow)
            ButtonInteraction.ButtonChangeSize(transform, baseScale);
    }
}