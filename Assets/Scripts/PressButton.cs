using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressButton : MonoBehaviour
{
    Animator anim;
    bool pressed = false;
    int pressingCount = 0;
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Box") || collision.CompareTag("Player"))
        {
            if (collision.CompareTag("Box")) 
            {
                var rb = collision.GetComponent<Rigidbody2D>();
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }


            pressingCount++;

            if (!pressed)
            {
                anim.SetBool("isOn", true);
                pressed = true;
                On();
            }
                
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Box") || collision.CompareTag("Player"))
        {
            pressingCount--;

            if (pressingCount == 0)
            {
                anim.SetBool("isOn", false);
                pressed = false;
                Off();
            }
        }
    }

    public void On() 
    {
        if (target) target.GetComponent<ITriggerable>().On();
    }

    public void Off()
    {
        if (target) target.GetComponent<ITriggerable>().Off();
    }
}
