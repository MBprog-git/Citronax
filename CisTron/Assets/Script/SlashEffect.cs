﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("Slash");
    }

    // Update is called once per frame
    private void LateUpdate()
    
   
    {
        if (!GetComponent<Animation>().isPlaying)
        {
            Destroy(this.gameObject);
        }
    }
}
