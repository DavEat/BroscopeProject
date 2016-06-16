using UnityEngine;
using UnityEngine.EventSystems; // 1

public class OnClick : MonoBehaviour
    , IPointerClickHandler // 2
    , IDragHandler
    , IPointerEnterHandler
    , IPointerExitHandler
// ... And many more available!
{
    public bool grow;
    public int speed = 3, buttonId;

    public Vector2 baseScale, targetScale;

    private Animator anim;

    void Awake()
    {
        baseScale = transform.localScale;

        anim = transform.parent.GetComponent<Animator>();
    }

    void Update()
    {
        if (grow)
            if (Input.GetMouseButtonDown(1) || Input.touchCount >= 2)
            {
                grow = false;
                anim.Play(string.Concat("animButtonReturn", buttonId));
            }

    }

    public void OnPointerClick(PointerEventData eventData) // 3
    {
        if (eventData.button == PointerEventData.InputButton.Left && !grow)
        {
            grow = true;
            anim.Play(string.Concat("animButton", buttonId));
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        print("I'm being dragged! : " + transform.name);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!grow)
            ButtonInteraction.ButtonEnter(transform);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!grow)
            ButtonInteraction.ButtonExit(transform);
    }
}