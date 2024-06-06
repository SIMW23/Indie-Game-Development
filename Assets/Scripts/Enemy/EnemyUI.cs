using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUI : MonoBehaviour
{
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.transform.parent.transform.localScale.x == -1)
        {
            this.gameObject.transform.localScale = new Vector3(-1, this.transform.localScale.y, this.transform.localScale.z);
        }
        else
        {
            this.gameObject.transform.localScale = new Vector3(1, this.transform.localScale.y, this.transform.localScale.z);
        }
    }
}
