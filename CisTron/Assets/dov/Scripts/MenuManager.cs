using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public TextMeshProUGUI textPopUp;
    
    public void Play()
    {
        StartCoroutine(PopUp());
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator PopUp()
    {
        textPopUp.enabled = true;
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("MainScene");
    }
}
