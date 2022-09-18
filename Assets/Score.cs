using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    #region singleton
    static Score _instance;
    void Awake()
    {
        if (_instance == null) _instance = this;
    }

    public TextMeshProUGUI text;
    
    public static Score Get()
    {
        return _instance;
    }
    #endregion

    public int score = 0;

    public void AddScore(int increment = 1)
    {
        score += increment;
        
    }

    private void Update()
    {
        text.text = $"Animals Crushed: {score}";
    }

    public void Reset()
    {
        score = 0;
    }
}
