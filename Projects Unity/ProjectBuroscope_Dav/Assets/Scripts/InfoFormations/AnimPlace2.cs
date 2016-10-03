using UnityEngine;
using System.Collections.Generic;

public class AnimPlace2 : MonoBehaviour {

    public int animIndex = 1, currentPart = 1; // 0 = none

    public float animSpeed;

    public float gapAndSizeLine = 0.133f, gapBlock = 1.8065f, maxSizeTextLine = 435.678f, maxSizeBlock = 1.801f;
    public float minPos = 1.103f, maxPos = -1.1025f;

    public List<RectTransform> listLine, listBlock;

    void Start()
    {
        listLine[0].localPosition = new Vector2 (0, minPos);
        listBlock[0].localPosition = new Vector2(0, minPos - gapBlock / 2 - gapAndSizeLine /2);

        for(int i = listLine.Count - 1; i > 0; i--)
        {
            int x =  listLine.Count - i - 1;
            listLine[i].localPosition = new Vector2(0, maxPos + (x * gapAndSizeLine));
            listBlock[i].localPosition = new Vector2(0, listLine[i].localPosition.y - gapAndSizeLine / 2);
            listBlock[i].sizeDelta = new Vector2(listBlock[i].sizeDelta.x, 0);
        }
    }

    void Update ()
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
                    if (listBlock[i].GetComponentInChildren<UnityEngine.UI.Text>().enabled)
                        listBlock[i].GetComponentInChildren<UnityEngine.UI.Text>().enabled = false;
                }
                else
                {
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
                listLine[i].GetComponent<ButtonStat>().targetPos = new Vector2(0, minPos - (gapAndSizeLine * i));

                listBlock[i].GetComponent<BlockStat>().targetPos = new Vector2(0, listLine[i].GetComponent<ButtonStat>().targetPos.y - gapBlock / 2 - gapAndSizeLine / 2);

                listBlock[i].GetComponent<BlockStat>().targetSize = new Vector2(listBlock[i].sizeDelta.x, maxSizeBlock);
            }
            else if (i < animIndex - 1)
            {
                listLine[i].GetComponent<ButtonStat>().targetPos = new Vector2(0, minPos - (gapAndSizeLine * i));

                listBlock[i].GetComponent<BlockStat>().targetPos = new Vector2(0, listLine[i].GetComponent<ButtonStat>().targetPos.y - gapAndSizeLine / 2);

                listBlock[i].GetComponent<BlockStat>().targetSize = new Vector2(listBlock[i].sizeDelta.x, 0);
            }
            else
            {
                listLine[i].GetComponent<ButtonStat>().targetPos = new Vector2(0, maxPos + ((listLine.Count - 1 - i) * gapAndSizeLine));

                listBlock[i].GetComponent<BlockStat>().targetPos = new Vector2(0, listLine[i].GetComponent<ButtonStat>().targetPos.y - gapAndSizeLine / 2);

                listBlock[i].GetComponent<BlockStat>().targetSize = new Vector2(listBlock[i].sizeDelta.x, 0);
            }

        }
    }
}
