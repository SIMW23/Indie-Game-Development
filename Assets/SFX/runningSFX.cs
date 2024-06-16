using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class runningSFX : MonoBehaviour
{
    [SerializeField] private AudioClip playerRun;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayRunSFX()
    {
        AudioManager.Instance.PlaySFX(playerRun);
    }
}
