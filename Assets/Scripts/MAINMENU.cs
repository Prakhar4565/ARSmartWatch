using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MAINMENU : MonoBehaviour
{
    public void PlayButtonOnClick()
    {
        SceneManager.LoadScene("Level1");
    }

    public void QUitApplication()
    {
        Application.Quit();
    }
}
