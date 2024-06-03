using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

public class GameControl : MonoBehaviour
{
    public static event Action HandlePulled = delegate{};

    public string[] scene = { "Intro", "Menu", "GameStart" };

    [SerializeField]
    private TextMeshProUGUI prizeText; // Retorno ao jogador sobre o valor ganho ao girar a roleta

    [SerializeField]
    private Row[] rows; //Classes de rows(slots) - onde contém os itens de cada roleta, é um array, possui 3 slots que serão definidos na plataforma

    [SerializeField]
    private Handler Handler; //Alavanca

    [SerializeField]
    private decimal prizeValue; // O valor ganho ao girar a roleta

    [SerializeField]
    private TextMeshProUGUI totalGoldText; //Retorno ao jogador sobre o dinheiro que possui

    [SerializeField]
    private decimal totalGold; //Valor em dinheiro que o jogador possui

    [SerializeField]
    private TextMeshProUGUI betValueText; //Retorno ao jogador do Valor da aposta 
    
    [SerializeField]
    private bool resultsChecked; //Verifica os resultados dos itens dos rows(slots)

    private bool prizeClaimed = false; //trava e destrava a contagens do score

    void Start()
    {
        resultsChecked = false;
        betValueText.text = "R$0.15";
        totalGoldText.text = "R$15.00";
        totalGold = 15.15m;
    }

    void Update()
    {
        if(!rows[0].rowStopped)
        {
            prizeValue = 0;
            prizeText.enabled = false;
            resultsChecked = false;
            prizeClaimed = false;
        }

        if(rows[0].rowStopped)
        {
            CheckResults();
            prizeText.enabled = true;
            prizeText.text = "Win: R$" + prizeValue.ToString("F2");
            if (!prizeClaimed) // Contagem do score
            {
                setGold(); //Altera os valores do score em tela
                prizeClaimed = true; //Desativa a contagem dos score

            }
            
        }
    }

    private void OnMouseDown()//Monitora o que irá acontecer quando o jogador puxar a alavanca.
    {
        if(rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped) //Só funcionase os rows(slots) estiverem parados.
        {
            Handler.OnButtonPlayClick();
            HandlePulled();
            totalGold = totalGold-0.15m;
        }
    }

    private void setGold()
    {
        resultsChecked = false;
        totalGold = totalGold+prizeValue; // Soma os valores atuais com o novo valor
        totalGoldText.text = "R$"+totalGold; //Altera o valor total em tela para o jogador.

    }

    private void CheckResults() //Checking de resultados
    {
        if(rows[0].stoppedSlot == "watermelon" && rows[1].stoppedSlot == "watermelon" && rows[2].stoppedSlot == "watermelon" )
        {
            prizeValue = 1.00m;
        }
        else if(rows[0].stoppedSlot == "apple" && rows[1].stoppedSlot == "apple" && rows[2].stoppedSlot == "apple" )
        {
            prizeValue = 0.30m;
        }
        else if(rows[0].stoppedSlot == "banana" && rows[1].stoppedSlot == "banana" && rows[2].stoppedSlot == "banana" )
        {
            prizeValue = 0.15m;
        }
        else if(rows[0].stoppedSlot == "pear" && rows[1].stoppedSlot == "pear" && rows[2].stoppedSlot == "pear" )
        {
            prizeValue = 0.15m;
        }
        else if(rows[0].stoppedSlot == "grape" && rows[1].stoppedSlot == "grape" && rows[2].stoppedSlot == "grape" )
        {
            prizeValue = 1.50m;
        }
        else if(rows[0].stoppedSlot == "cherry" && rows[1].stoppedSlot == "cherry" && rows[2].stoppedSlot == "cherry" )
        {
            prizeValue = 3.00m;
        }
        else
        {
            prizeValue = 0;
        }
        resultsChecked = true;
    }

}
