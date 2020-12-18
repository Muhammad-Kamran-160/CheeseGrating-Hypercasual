using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeMenu : MonoBehaviour
{
    public GameObject scrollbar;
    private float scroll_pos = 0;
    float[] pos;

    // Start is called before the first frame update
    void Start ()
    {

    }

    public void SetScrollPos (float val)
    {
        scroll_pos = val;
    }

    // Update is called once per frame
    void Update ()
    {
        pos = new float[transform.childCount];
        float distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }

        if (Input.GetMouseButton (0))
        {
            scroll_pos = scrollbar.GetComponent<Scrollbar> ().value;
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
                {
                    scrollbar.GetComponent<Scrollbar> ().value = Mathf.Lerp (scrollbar.GetComponent<Scrollbar> ().value, pos[i], 0.1f);
                }
            }
        }

        if (scroll_pos < 0.5)
        {
            if (Gameplay_References._instance._shopManager.HavePage1GraterBeenUnlocked ())
                Gameplay_References._instance._uiManager.SetGraterUnlockButtonActive (false);
            else
                Gameplay_References._instance._shopManager.CheckAndSetGraterUnlockButtonActive ();

            Gameplay_References._instance._uiManager.EnableRightDisablerArrow (false);
            Gameplay_References._instance._uiManager.EnableLeftDisablerArrow (true);
        }
        else if (scroll_pos >= 0.5)
        {
            if (!Gameplay_References._instance._shopManager.HavePage1GraterBeenUnlocked ())
                Gameplay_References._instance._uiManager.SetGraterUnlockButtonActive (false);
            else
                Gameplay_References._instance._shopManager.CheckAndSetGraterUnlockButtonActive ();

            Gameplay_References._instance._uiManager.EnableRightDisablerArrow (true);
            Gameplay_References._instance._uiManager.EnableLeftDisablerArrow (false);
        }

        // for (int i = 0; i < pos.Length; i++)
        // {
        //     if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
        //     {
        //         Debug.LogWarning("Current Selected Level" + i);
        //         transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1.2f, 1.2f), 0.1f);
        //         for (int j = 0; j < pos.Length; j++)
        //         {
        //             if (j!=i)
        //             {
        //                 transform.GetChild(j).localScale = Vector2.Lerp(transform.GetChild(j).localScale, new Vector2(0.8f, 0.8f), 0.1f);
        //             }
        //         }
        //     }
        // }

    }
}