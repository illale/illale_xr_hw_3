using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStateHandler : MonoBehaviour
{

    public enum State {
        START,
        PLAY,
    };

    public Player player;
    public EnemyManager enemyManager;
    public GameObject startPanel;
    public GameObject timer;
    public GameObject winScreenText;
    public GameObject endPanel;

    public State currentState;
    private DateTime startTime;
    private DateTime endTime;
    private double totalSeconds;
    private Vector3 startScreenOriginalPos;
    private Vector3 endScreenPosition = new Vector3(0, 2.5f, 5.0f);

    void Start()
    {
        currentState = State.START;
        startScreenOriginalPos = startPanel.transform.position;
    }

    void Update()
    {
        UpdateState();

    }

    public void UpdateState() {
        switch(currentState) {
            case State.START:
                checkStart();
            break;
            case State.PLAY:
                UpdateClock();
                checkGameState();
            break;
        }
    }

    public void checkStart() {
        if (player.GetStart()) {
            player.SetStart(false);
            currentState = State.PLAY;
            startTime = DateTime.UtcNow;
            startPanel.transform.position = new Vector3(0, -10, 4);
            endPanel.transform.position = startPanel.transform.position;
            player.Reset();
            enemyManager.SetStop(false);
            enemyManager.SpawnEnemies();
        }
    }

    public void checkGameState() {
        if (player.GetHitpoints() <= 0) {
            currentState = State.START;
            startPanel.transform.position = startScreenOriginalPos;
            endPanel.transform.position = endScreenPosition;
            winScreenText.GetComponent<TextMeshProUGUI>().SetText(
                String.Format("Congratulations! \nTime: {0}\nScore: {1}", totalSeconds.ToString("F"), player.GetScore())
            );
            enemyManager.DestroyAll();
        }
    }

    public void UpdateClock() {
        endTime = DateTime.UtcNow;
        totalSeconds = (endTime - startTime).TotalSeconds;
        timer.GetComponent<TextMeshProUGUI>().SetText(
            String.Format("Time: {0}\n Score: {1}", totalSeconds.ToString("F"), player.GetScore())
        );
        
    }

}
