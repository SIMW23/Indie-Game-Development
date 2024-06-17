using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyButton : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPostion = Camera.main.ScreenToWorldPoint(touch.position);
        }
    }

    public void DebugMsg()
    {
        Debug.Log("Pressed");
    }
   // dm, dung tao may script ma co chung name vs dèault c?a unity em =))
}
