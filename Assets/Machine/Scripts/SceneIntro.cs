using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneIntro : MonoBehaviour
{
    public string iScene = "Menu";
    private Animator animScreen;
    private GameControl gameControl;

    void Start()  //Cena inicial - quando o game for iniciado
    {
        if(iScene == "Menu")
        {
            animScreen = GetComponent<Animator>();
            StartCoroutine(changeSceneIntro());
        } 
        else 
        {
            Debug.Log("Quit Game");
            Application.Quit();
        }


    }

    private IEnumerator changeSceneIntro()
    {

        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Menu");

    }

}

