using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    GameObject player;
    AudioSource audioSource;
    SpriteRenderer spriteRenderer;

    private bool isOver = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isOver) return;

        if (collision.gameObject == player)
        {
            audioSource.Play();
            player.GetComponent<Player>().PlayerWin();
            spriteRenderer.enabled = false;
            Destroy(gameObject, 3f);
            isOver = true;
        }
    }
}
