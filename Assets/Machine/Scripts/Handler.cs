using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Handler : MonoBehaviour
{
    private Animator anim;

    private void Start() //Animações da alavanca na hora de puxar
    {
        anim = GetComponent<Animator>();
    }
    
    public void OnButtonPlayClick()
    {
        anim.SetBool("click", true);
    }

    public void OffButtonPlayClick()
    {
        anim.SetBool("click", false);
    }
    
}
