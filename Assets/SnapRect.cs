using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnapRect : MonoBehaviour
{
    public Transform content;
    public Scrollbar scrollbar;
    public float[] pos;
    float distance;
    float oldPos;
    // Start is called before the first frame update
    void Start()
    {
        distance = 1f / (content.childCount - 1);
        pos = new float[content.childCount];

        for (int i = 0; i < content.childCount; i++)
        {
            pos[i] = distance * i ;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            oldPos = scrollbar.value;
        }
        else
        {
            for(int i =0; i<pos.Length; i++)
            {
                if (oldPos < pos[i] + (distance / 2) && oldPos > pos[i] - (distance / 2))
                {
                    scrollbar.value = Mathf.Lerp(scrollbar.value, pos[i], 0.3f);
                }
            }
        }
    }
}
