using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleTransition : MonoBehaviour
{
    bool isAlone;
     public GameObject buttons,bTwo;
    private int onAndOffTing;
    

    void Start()
    {
        onAndOffTing = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            onExitButton();
        }
    }
    public void OnAloneButton()
    {
        //this is ai in abled 
        isAlone = true;
    }
    public void OnMultiButton()
    {
        isAlone = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAlone == false)
        {
            if (collision.CompareTag("Player1"))
            {
                StartCoroutine(SwitchScnes("Battle"));
            }
        }
        else
        {
            if (collision.CompareTag("Player1"))
            {
                StartCoroutine(SwitchScnes("Demo"));
            }
        }
       
    }
    public void OnStartButton()
    {

        StartCoroutine(SwitchScnes("Demo"));
    }
    IEnumerator SwitchScnes(string sceneName)
    {

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneName);
    }


    public void OnAndOff()
    {
        onAndOffTing++;
        buttons.active = false;
        bTwo.active = false;
        if (onAndOffTing % 2 == 0)
        {
            buttons.active = true;
        }
    }
    public void onExitButton()
    {
        Application.Quit();
    }
}
