using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { private set; get; }
    [SerializeField] private CurrentTurnIndicator turnIndicator;
    [Header("Misc")][SerializeField] private ObjectSelector objectSelector;
    [SerializeField] private GameObject gameUI;
    private int currentPlayer = -1;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
        if (turnIndicator != null)
        {
            turnIndicator.gameObject.SetActive(false);
        }
        if (objectSelector != null)
        {
            objectSelector.allowMove = false;
            objectSelector.enabled = false;
        }
        if (gameUI != null)
        {
            gameUI.SetActive(false);
        }
        currentPlayer = 0;
        turnIndicator.SetText($"It's Player {currentPlayer + 1}'s Turn");
        turnIndicator.gameObject.SetActive(true);
    }
    
    public void endTurn()
    {
        objectSelector.enabled = false;
        currentPlayer++;
        if (currentPlayer >= 4)
        {
            currentPlayer = 0;
        }
        turnIndicator.SetText($"It's Player {currentPlayer + 1}'s Turn");
        turnIndicator.gameObject.SetActive(true);
        gameUI.SetActive(false);
    }

    public void handleTurn()
    {
        objectSelector.enabled = true;
        objectSelector.allowMove = true;
        turnIndicator.gameObject.SetActive(false);
        gameUI.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
