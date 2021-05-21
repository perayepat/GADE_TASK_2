using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleTransition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            onExitButton();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1"))
        {
            StartCoroutine(SwitchScnes("Battle"));
        }
    }
    IEnumerator SwitchScnes(string sceneName)
    {

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneName);
    }

    public void onExitButton()
    {
        Application.Quit();
    }
}
