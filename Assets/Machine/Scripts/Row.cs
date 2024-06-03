using System.Collections.Generic;
using System.Collections;
using UnityEngine;

// *************************************** Este arquivo será reproduzido individualmente para cada slot

public class Row : MonoBehaviour
{
    private int randomValue; //Quantidade de vezes que cada row(slot) roda
    private float timeInterval; // Tempo de intervalo entre a cada randomValue
    private float orderSlotY; //Posição Y das sprites da row
    private float[] framesWin = {-5f, -3f, 1f, 1f, 3f, 5f}; //Posições de cada item

    public bool rowStopped; //Verifica se a row(slot) está parado
    public string stoppedSlot; // Pega o valor do item que está parado para ser validado

    private GameControl gameControl;

    [SerializeField]
    private Handler handler; //Alavanca

    void Start()
    {
        rowStopped = true;
        gameControl = FindObjectOfType<GameControl>();
        handler = FindObjectOfType<Handler>();
        GameControl.HandlePulled += StartRotating;
    }

    private void StartRotating()
    {
        stoppedSlot = "";
        StartCoroutine(Rotate(gameControl));
    }

    private IEnumerator Rotate(GameControl gameControl)
    {
        rowStopped = false;
        randomValue = Random.Range(60, 90);
        timeInterval = Random.Range(0.2f, 0.05f);
        transform.localPosition = new Vector2(transform.localPosition.x, -5f); //Deixa na posição inicial toda vez que houver o click na alavanca.

        for (int i = 0; i <= randomValue; i++)
        {
            if(transform.localPosition.y > 5f)
            {
                yield return StartCoroutine(AnimateYPosition(-5f, -0.25f));
            }

            //Movimentação dos itens em tela
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y+0.25f);

            //Ajusta os intervalos de tempo
            if (i > Mathf.RoundToInt(randomValue * 0.25f)) timeInterval = 0.05f;
            if (i > Mathf.RoundToInt(randomValue * 0.5f)) timeInterval = 0.1f;
            if (i > Mathf.RoundToInt(randomValue * 0.75f)) timeInterval = 0.15f;
            if (i > Mathf.RoundToInt(randomValue * 0.95f)) timeInterval = 0.2f;

            if (i >= randomValue) // Verifica se chegou ao fim do laço de repetição e se a posição parada contém algum item.
            {
                if (IsInWinningFrames(transform.localPosition.y))
                {
                    yield return StartCoroutine(AnimateYPosition(transform.localPosition.y, 0.5f));
                }
                else
                {
                    int newItem = Random.Range(0, framesWin.Length);
                    yield return StartCoroutine(AnimateYPosition(framesWin[newItem], 0.5f));
                }
            }
            else
            {
                yield return new WaitForSeconds(timeInterval);
            }
            
        }

        //Verifica os itens para a pontuação no GameControl
        if(transform.localPosition.y == -5f) stoppedSlot = "cherry";
        if(transform.localPosition.y == -3f) stoppedSlot = "grape";
        if(transform.localPosition.y == -1f) stoppedSlot = "pear";
        if(transform.localPosition.y == 1f) stoppedSlot = "banana";
        if(transform.localPosition.y == 3f) stoppedSlot = "apple";
        if(transform.localPosition.y == 5f) stoppedSlot = "watermelon";

        Debug.Log(transform.localPosition.y+", "+stoppedSlot);

        rowStopped = true;   
        handler.OffButtonPlayClick();     
    }

    private bool IsInWinningFrames(float value) // Compara arrays e verifica se a posição está entre as sprites
    {
        foreach (float frame in framesWin)
        {
            if (frame == value) // Verifica se os valores são praticamente iguais
            {
                return true;
            }
        }
        return false;
    }

    private IEnumerator AnimateYPosition(float targetY, float duration)
    {
        float startY = transform.localPosition.y;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float newY = Mathf.Lerp(startY, targetY, elapsed / duration);
            transform.localPosition = new Vector2(transform.localPosition.x, newY);
            yield return null; // Espera até o próximo frame
        }

        transform.localPosition = new Vector2(transform.localPosition.x, targetY); // Certifica-se de que a posição final seja exatamente o targetY
    }
    
    private void OnDestroy()
    {
        GameControl.HandlePulled -= StartRotating;
    }

}
