using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        //Set Cursor to not be visible
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void PlayLevel(int level){
        SceneManager.LoadScene(level);
    }

    public void QuitGame(){
        Debug.Log("Quit");
        Application.Quit();
    }
}
