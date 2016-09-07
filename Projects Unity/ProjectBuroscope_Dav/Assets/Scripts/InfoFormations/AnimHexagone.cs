using UnityEngine;
using System.Collections.Generic;

public class AnimHexagone : MonoBehaviour {

    //[HideInInspector]
    public bool inAnim, displayContent;
    public float speed;

    [SerializeField]
    private Transform listElem, title;

    public Vector3 maxScale;

    [HideInInspector]
    public Vector3 initialPos, targetPos;
    [HideInInspector]
    public Vector3 startlPos;
    [HideInInspector]
    public Vector3 initialScale, targetScale;

    [SerializeField]
    private List<Transform> otherElements; //---All the other elements to unactive tu display this---

    void Awake()
    {
        initialPos = transform.localPosition;
        initialScale = transform.localScale;
    }

	void Update ()
    {
        if (inAnim)
            Animation();
    }


    private void Animation()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, speed);
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, speed);


        if (displayContent)
        {
            if (Vector2.Distance(startlPos, targetPos) / 2 > Vector2.Distance(transform.localPosition, targetPos))
            {
                displayContent = false;
                title.gameObject.SetActive(!title.gameObject.activeSelf);
                listElem.gameObject.SetActive(!listElem.gameObject.activeSelf);

                foreach (Transform t in otherElements)
                    t.gameObject.SetActive(!t.gameObject.activeSelf);
            }
        }    
        else if (Vector2.Distance(startlPos, targetPos) / 5000 > Time.deltaTime * speed)
        {
            inAnim = false;
        }
    }
}
