using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField nameInput;

    public void StartGame()
    {
        MainManager.Instance.playerName = nameInput.text;

        SceneManager.LoadScene(1);
    }
}
