using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour {
    // Управление первой сценой
    public void LoadSceneGame()
    {
        SceneManager.LoadScene(1);
    }
	
}
