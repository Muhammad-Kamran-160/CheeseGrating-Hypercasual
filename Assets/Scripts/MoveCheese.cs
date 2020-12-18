using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCheese : MonoBehaviour
{
    private Touch _touch;
    public float speed;

    private void Update ()
    {
        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch (0);

            if (_touch.phase == TouchPhase.Moved)
            {
                // transform.position = new Vector3 (transform.position.x + _touch.deltaPosition.x * speed, transform.position.y,
                //     transform.position.z + _touch.deltaPosition.y * speed);

                transform.localPosition = new Vector3 (transform.localPosition.x+ _touch.deltaPosition.x * speed, transform.localPosition.y,
                    transform.localPosition.z);
            }
            if (_touch.phase == TouchPhase.Ended)
            {
                // Gameplay_References._instance._gameplayManger.RelocateCheeseBlock ();
                Gameplay_References._instance._gameplayManger.RelocateToOriginalPosition ();
            }
        }
    }
}