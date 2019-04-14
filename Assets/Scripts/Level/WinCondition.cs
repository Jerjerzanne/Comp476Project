using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{

    #region Methods

    public void CheckWin()
    {
        StartCoroutine("CheckIfWin");
    }

    private IEnumerator CheckIfWin()
    {
        yield return new WaitForSeconds(1.0f);
        if (GameObject.Find("AlienNest") == null && GameObject.Find("CommandCenter") == null && GameObject.Find("AlienNest(Clone)") == null)
        {
            Debug.Log("You win!!!");
            SceneManager.LoadScene("WinScreen");
        }
        yield return null;
    }

    #endregion
}
