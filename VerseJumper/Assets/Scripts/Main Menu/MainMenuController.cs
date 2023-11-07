using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;
    // Start is called before the first frame update
    public void playGame(){
        SceneManager.LoadScene("Level 1 JSTN 3");
    }
    public void options(){
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }
    public void back(){
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }
    public void credits(){
        SceneManager.LoadScene("Credits");
    }
    public void level1(){
        SceneManager.LoadScene("Level 1");
    }
    public void level2(){
        SceneManager.LoadScene("Level 2");
    }
    public void levelBack(){
        SceneManager.LoadScene("Main Menu");
    }
}
