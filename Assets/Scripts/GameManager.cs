using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public GameObject life;

    public GameObject death;

    [HideInInspector] public GameObject activeTilemap;

    private GameObject player;
    public GameObject deadPlayer;

    // Start is called before the first frame update
    void Start()
    {
        activeTilemap = life;

        player = GameObject.FindGameObjectWithTag("Player");

        death.SetActive(false); life.SetActive(true);
    }

    public void SwitchTilemap()
    {
        // get position from player
        // make dead player at activeTilemap
        GameObject body = Instantiate(deadPlayer, activeTilemap.transform);
        body.transform.position = player.transform.position;
        body.GetComponent<SpriteRenderer>().flipX = player.GetComponent<SpriteRenderer>().flipX;

        if (activeTilemap == life)
        {
            death.SetActive(true);
            life.SetActive(false);
            activeTilemap = death;
            return;
        }

        if (activeTilemap == death)
        {
            life.SetActive(true);
            death.SetActive(false);
            activeTilemap = life;
            return;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public bool HaveNextStage()
    {
        return (SceneManager.GetActiveScene().buildIndex + 1) < SceneManager.sceneCountInBuildSettings;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
