using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, ITriggerable
{
    public bool DisableWhenOn = true;

    public void Off()
    {
        if (DisableWhenOn)
        {
            spirte.enabled = true;
            box.enabled = true;
        }
        else
        {
            spirte.enabled = false;
            box.enabled = false;
        }
        
    }

    public void On()
    {
        if (DisableWhenOn)
        {
            spirte.enabled = false;
            box.enabled = false;
        }
        else
        {
            spirte.enabled = true;
            box.enabled = true;
        }
        
    }

    SpriteRenderer spirte;
    BoxCollider2D box;

    // Start is called before the first frame update
    void Start()
    {
        spirte = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();

        spirte.enabled = DisableWhenOn;
        box.enabled = DisableWhenOn;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
