using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {
    // Управление елементами отображения интерфейса. На Controller
    [Header("UI")]
    public Image enemyHealth;
    public Toggle sightToogle;
    public Text scoreText;

    public void ChangeHealthBar(float value)
    {
        enemyHealth.fillAmount = value;
    }

    public void SetScoreText(int value)
    {
        scoreText.text = value + "";
    }
}
