using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject gamePlay;
    public GameObject pauseMenu;
    public GameObject[] points ;
    private SpriteRenderer render; 

    // Start is called before the first frame update
    void Start(){
        points = GameObject.FindGameObjectsWithTag("PatrolPoints");
        foreach(GameObject point in points){
           render = point.GetComponent<SpriteRenderer>();
           render.enabled = false;
        }
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            Debug.Log("Pause");
            gamePlay.SetActive(false);
            pauseMenu.SetActive(true);
        }
    }

    public void Resume(){
        gamePlay.SetActive(true);
        pauseMenu.SetActive(false);
    }
    public void QuitGame(){
        Debug.Log("Quit");
        SceneManager.LoadScene("Main Menu");
    }
}
