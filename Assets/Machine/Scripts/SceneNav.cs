using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneNav : MonoBehaviour
{
    public string scene;
    private Animator buttonSelected;


    private void Start() // Escolhas de cenas entre o menu e jogos. E a opção de sair
    {
        buttonSelected = GetComponent<Animator>();

        if(scene == "GameStart")
        {
            buttonSelected.SetBool("ButtonMenuSelected", false);
        }
    }

    private void OnMouseDown()
    {
        switch(scene)
        {
            case "GameStart":
                StartCoroutine(changeScene());
                break;
            case "Menu":
                StartCoroutine(changeScene());
                break;
            case "Quit":
                Debug.Log("Quit Game");
                buttonSelected.SetBool("ButtonMenuSelected", true);
                Application.Quit();
                break;
        }
        
    }

        private IEnumerator changeScene()
    {

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(scene);

    }

}
