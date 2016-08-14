using UnityEngine;
using System.Collections.Generic;

public class AnimPlace1 : MonoBehaviour {

    public int animIndex = 1, currentPart = 1; // 0 = none

    public float animSpeed;

    public float gapAndSizeLine = 0.295499f, gapBlock = 3.135502f, maxSizeTextLine = 435.678f, maxSizeBlock = 2.836f;
    public float minPos = -1.863252f, maxPos = 1.86325f;

    public List<RectTransform> listLine, listBlock;

    void Start()
    {
        /*listLine[0].localPosition = new Vector2(minPos, 0);
        listBlock[0].localPosition = new Vector2(minPos - gapBlock / 2 - gapAndSizeLine / 2, 0);

        for (int i = listLine.Count - 1; i > 0; i--)
        {
            int x = listLine.Count - i - 1;
            listLine[i].localPosition = new Vector2(maxPos + (x * gapAndSizeLine), 0);
            listBlock[i].localPosition = new Vector2(listLine[i].localPosition.y - gapAndSizeLine / 2, 0);
            listBlock[i].sizeDelta = new Vector2(0, listBlock[i].sizeDelta.y);
        }*/
    }

    void Update()
    {
        if (animIndex != 0)
        {
            for (int i = 0; i < listLine.Count; i++)
            {
                listLine[i].localPosition = Vector2.Lerp(listLine[i].localPosition, listLine[i].GetComponent<ButtonStat>().targetPos, animSpeed);
                listBlock[i].localPosition = Vector2.Lerp(listBlock[i].localPosition, listBlock[i].GetComponent<BlockStat>().targetPos, animSpeed);
                listBlock[i].sizeDelta = Vector2.Lerp(listBlock[i].sizeDelta, listBlock[i].GetComponent<BlockStat>().targetSize, animSpeed);

                if (i != animIndex - 1)
                {
                    listBlock[i].gameObject.SetActive(false);
                    if (listBlock[i].GetComponentInChildren<UnityEngine.UI.Text>().enabled)
                        listBlock[i].GetComponentInChildren<UnityEngine.UI.Text>().enabled = false;
                }
                else
                {
                    listBlock[i].gameObject.SetActive(true);
                    if (!listBlock[i].GetComponentInChildren<UnityEngine.UI.Text>().enabled)
                        listBlock[i].GetComponentInChildren<UnityEngine.UI.Text>().enabled = true;
                }
            }
        }
    }

    public void SetElementValue(int index)
    {
        for (int i = 0; i < listLine.Count; i++)
        {
            if (i == animIndex - 1)
            {
                listLine[i].GetComponent<ButtonStat>().targetPos = new Vector2(minPos + (gapAndSizeLine * i), 0);

                listBlock[i].GetComponent<BlockStat>().targetPos = new Vector2(listLine[i].GetComponent<ButtonStat>().targetPos.x + gapBlock / 2 /*+ gapAndSizeLine / 2*/, 0);

                listBlock[i].GetComponent<BlockStat>().targetSize = new Vector2(maxSizeBlock, listBlock[i].sizeDelta.y);
            }
            else if (i < animIndex - 1)
            {
                listLine[i].GetComponent<ButtonStat>().targetPos = new Vector2(minPos + (gapAndSizeLine * i), 0);

                listBlock[i].GetComponent<BlockStat>().targetPos = new Vector2(listLine[i].GetComponent<ButtonStat>().targetPos.x + gapAndSizeLine / 2, 0);

                listBlock[i].GetComponent<BlockStat>().targetSize = new Vector2(0, listBlock[i].sizeDelta.x);
            }
            else
            {
                listLine[i].GetComponent<ButtonStat>().targetPos = new Vector2(maxPos - ((listLine.Count - 1 - i) * gapAndSizeLine), 0);

                listBlock[i].GetComponent<BlockStat>().targetPos = new Vector2(listLine[i].GetComponent<ButtonStat>().targetPos.x + gapAndSizeLine / 2, 0);

                listBlock[i].GetComponent<BlockStat>().targetSize = new Vector2(0, listBlock[i].sizeDelta.y);
            }
        }
    }
}
