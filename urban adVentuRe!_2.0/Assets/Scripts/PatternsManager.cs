using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternsManager : MonoBehaviour
{
    public GameObject[] tiles;
    public Renderer[] tileRenderers;
    public GameObject[] targets;
    public Renderer[] targetRenderers;
    public int RoundsToWin = 2;
    public PuzzleDoor door;
    private Color[] colors = new Color[] {Color.red, Color.green, Color.blue, Color.magenta, Color.yellow, Color.cyan };
    private bool isGameStarted = false;
    private bool isRoundRunning = false;
    private bool isPlayerTurn = false;
    private int round = 0;
    private int [] sequence;
    private int [] playerSequence;
    private int playerIndex = 0;
    private float lerpDuration = 0.3f;
    private int frame = 0;

    // Start is called before the first frame update
    void Start()
    {
        tiles = GameObject.FindGameObjectsWithTag("PatternTiles");
        tileRenderers = new Renderer[tiles.Length];

        targets = GameObject.FindGameObjectsWithTag("PatternTargets");
        targetRenderers = new Renderer[targets.Length];

        for (int i = 0; i < tiles.Length; ++i){
            tileRenderers[i] = tiles[i].GetComponent<Renderer>();
            tileRenderers[i].material.color = colors[i];
            Debug.Log("Name: " + tiles[i].name + ", index: " + i);

            tiles[i].AddComponent<PatternTile>();
        }

        for(int i = 0; i < targets.Length; ++i){
            targetRenderers[i] = targets[i].GetComponent<Renderer>();
//            targetRenderers[i].material.color = colors[i];

            targets[i].AddComponent<PatternTarget>();
        }
        //StartGame();
    }

    int findTile(string name){
        for(int i = 0; i < tiles.Length; ++i){
            if(name == tiles[i].name){
                return i;
            }
        }
        return -1;
    }

    void TileWasActivated(string name){
        if(!isGameStarted) {
            StartGame();
            return;
        }
        int index = findTile(name);
        if(!isPlayerTurn || playerIndex >= sequence.Length){
            return;
        }
        playerSequence[playerIndex] = index;
        if(index != sequence[playerIndex]){
            playerIndex = 0;
            StartCoroutine(LoseRound());    
            return; 
        }   
        playerIndex++;  
        if(playerIndex == sequence.Length){
            playerIndex = 0;
            isPlayerTurn = false;
            StartCoroutine(WinRound());
            return;
        }
    }

    IEnumerator LoseRound(){
        isPlayerTurn = false;
        playerIndex = 0;
        round = 0;
        
        yield return FlashAllTiles(Color.white, Color.red, 3);

        isGameStarted = false;
        isRoundRunning = false;
        StartGame();
    }

    IEnumerator FlashAllTiles(Color start, Color end, int reps){
        //Set all targets to start
        for(int target = 0; target < targetRenderers.Length; ++target){
            targetRenderers[target].material.color = start;
        }
        //Flash them red-white-red etc a few times
        float elapsedtime;
        float transitiontime = 0.5f;
        for(int i = 0; i < reps; ++i){
            elapsedtime = 0;
            while(elapsedtime < transitiontime){
                for(int target = 0; target < targetRenderers.Length; ++target){
                    targetRenderers[target].material.color = Color.Lerp(start, end, elapsedtime / transitiontime);
                }
                elapsedtime += Time.deltaTime;
                yield return null;
            }
            elapsedtime = 0;
            while(elapsedtime < transitiontime){
                for(int target = 0; target < targetRenderers.Length; ++target){
                    targetRenderers[target].material.color = Color.Lerp(end, start, elapsedtime / transitiontime);
                }
                elapsedtime += Time.deltaTime;
                yield return null;
            }
        }
        for(int target = 0; target < targetRenderers.Length; ++target){
            targetRenderers[target].material.color = Color.white;
        }
    }

    void StartGame(){
        if(isGameStarted)
            return;
        isGameStarted = true;
        StartCoroutine(StartRound());
        
    }
    IEnumerator WinRound(){
        yield return FlashAllTiles(Color.white, Color.green, 3);
        if(round < RoundsToWin){
            yield return StartRound();
        } else {
            WinGame();
        }
    } 

    IEnumerator StartRound(){
        isPlayerTurn = false;
        playerIndex = 0;
        isRoundRunning = true;
        round++;
        sequence = new int[round*2];
        string stringquence = "";
        System.Random rand = new System.Random();
        for(int i = 0; i < sequence.Length; ++i){
            sequence[i] = rand.Next(tiles.Length);
            stringquence += sequence[i];
            stringquence += ", ";
        }
        Debug.Log(stringquence);
        playerSequence = new int[round*2];
        for(int i = 0; i < sequence.Length; ++i){
            int target = sequence[i];
            float elapsedtime = 0;
            while(elapsedtime < .75){
                elapsedtime += Time.deltaTime;
                yield return null;
            }
            elapsedtime = 0;
            while(elapsedtime < lerpDuration){
                targetRenderers[target].material.color = Color.Lerp(Color.white, colors[target], elapsedtime / lerpDuration);
                elapsedtime += Time.deltaTime;
                yield return null;
            }
            elapsedtime = 0;
            while(elapsedtime < 0.5){
                elapsedtime += Time.deltaTime;
                yield return null;
            }
            elapsedtime = 0;
            while(elapsedtime < lerpDuration){
                targetRenderers[target].material.color = Color.Lerp(colors[target], Color.white, elapsedtime / lerpDuration);
                elapsedtime += Time.deltaTime;
                yield return null;
            }
        }

        isPlayerTurn = true;
    }

    void EndGame(){
        isGameStarted = false;
        isRoundRunning = false;
        round = 0;
    }
    void WinGame(){
        EndGame();
        StartCoroutine(door.RaiseDoor());
    }

    // Update is called once per frame
    void Update()
    {
        tileRenderers[frame].material.color =  colors[frame];
        frame = (frame + 1) % tileRenderers.Length;
    }
}
