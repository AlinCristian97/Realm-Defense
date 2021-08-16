using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] private int _startingBalance = 150;
    public int CurrentBalance { get; private set; }

    [SerializeField] private TextMeshProUGUI _displayBalance;

    private void Awake()
    {
        CurrentBalance = _startingBalance;
        UpdateDisplay();
    }

    public void Deposit(int amount)
    {
        CurrentBalance += Mathf.Abs(amount);
        UpdateDisplay();
    }

    public void Withdraw(int amount)
    {
        CurrentBalance -= Mathf.Abs(amount);
        UpdateDisplay();

        if (CurrentBalance < 0)
        {
            // Lose the game
            ReloadScene();
        }
    }

    private void UpdateDisplay()
    {
        _displayBalance.text = $"Gold: {CurrentBalance}";
    }
    

    private void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

}
