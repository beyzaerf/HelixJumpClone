using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private int score;

    public int Score { get => score; set => score = value; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
}