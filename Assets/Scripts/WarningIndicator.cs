using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningIndicator : MonoBehaviour
{

    [SerializeField] private GameObject rightIndicator;
    [SerializeField] private GameObject leftIndicator;
    // Start is called before the first frame update
    void Start()
    {
        rightIndicator.SetActive(false);
        leftIndicator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            if(this.gameObject.tag == "LeftTrigger")
            {
                leftIndicator.SetActive(true);
            }
            else if(this.gameObject.tag == "RightTrigger")
            {
                rightIndicator.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (this.gameObject.tag == "LeftTrigger")
            {
                leftIndicator.SetActive(false);
            }
            else if (this.gameObject.tag == "RightTrigger")
            {
                rightIndicator.SetActive(false);
            }
        }
    }
}
