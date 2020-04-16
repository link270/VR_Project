using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Valve.VR.InteractionSystem;


public class RetractIfStatuesRemoved : MonoBehaviour
{

    public GameObject ballDetector, trapdoor, Entrance, Wall;
    public GameObject[] pedestals;
    public GameObject[] LockedTeleports;
    private List<ItemRemoved> removed;
    private Player player;
    public GameObject Ball;
    private DetectBall detectBall;
    private Vector3 TrapInitPos, TrapCurPos, TrapEndPos;
    private Vector3 EntranceInitPos, EntranceCurPos, EntranceEndPos;
    private Vector3 WallInitPos, WallCurPos,  WallEndPos;
    private Vector3 ExitInitPos, ExitCurPos, ExitEndPos;

    private Vector3 TrapDoorPos;

    // Start is called before the first frame update
    void Start()
    {
        player = Player.instance;

        pedestals = GameObject.FindGameObjectsWithTag("IndianaPedestals");
        removed = new List<ItemRemoved>();
        foreach(GameObject pedestal in pedestals){
            removed.Add(pedestal.GetComponent<ItemRemoved>());
        }

        InitTrap();
        InitEntrance();
        InitWall();

        TrapDoorPos = trapdoor.transform.position;

        detectBall = ballDetector.GetComponent<DetectBall>();
    }

    void InitTrap(){
        TrapInitPos = new Vector3(trapdoor.transform.position.x,trapdoor.transform.position.y,trapdoor.transform.position.z);
        TrapCurPos = TrapInitPos;
        TrapEndPos = TrapInitPos;
        TrapEndPos.z += 5f;
    }

    void InitEntrance(){
        EntranceInitPos = new Vector3(Entrance.transform.position.x,Entrance.transform.position.y,Entrance.transform.position.z);
        EntranceCurPos = EntranceInitPos;
        EntranceEndPos = EntranceInitPos;
        EntranceEndPos.x -= 1.88f;
    }

    void InitWall(){
        WallInitPos = new Vector3(Wall.transform.position.x,Wall.transform.position.y,Wall.transform.position.z);
        WallCurPos = WallInitPos;
        WallEndPos = WallInitPos;
        WallEndPos.y -= 3.21f;
    }

    // Update is called once per frame
    void Update()
    {

        bool allRemoved = removed.All(pedestal => pedestal.Removed == true);
        
//        Debug.Log(Vector3.Distance(player.transform.position, TrapDoorPos));

        if(Vector3.Distance(player.transform.position, TrapDoorPos) < 8){
            CloseEntrance();
        }


        if(allRemoved){
            RetractTrapDoor();
            LowerWall();
            UnlockTeleports();
        }

        if(detectBall.BallPresent)
        {
            CloseTrap();
            Destroy(Ball);
        }

        if(removed.Where(pedestal => pedestal.Removed == true).ToList().Count == 1){
            Ball.GetComponent<Rigidbody>().useGravity = true;
        }
        
    }

    void UnlockTeleports()
    {
        LockedTeleports = GameObject.FindGameObjectsWithTag("Indiana_Locked_Teleportarea");
        foreach(var teleportArea in LockedTeleports)
        {
            teleportArea.GetComponent<TeleportArea>().SetLocked(false);
        }
    }

    void RetractTrapDoor(){
        StartCoroutine(MoveTrapDoor(TrapEndPos)); 
    }

    void CloseTrap(){
        StartCoroutine(MoveTrapDoor(TrapInitPos));
    }

    void CloseEntrance(){
        StartCoroutine(MoveEntrance(EntranceEndPos)); 
    }


    void LowerWall(){
        StartCoroutine(MoveWall(WallEndPos)); 
    }



    private IEnumerator MoveTrapDoor(Vector3 newPos){
        float startTime = Time.time;
        float overTime = 2f;
        float endTime = startTime + overTime;

        while (Time.time < endTime)
        {
            trapdoor.transform.position = Vector3.Slerp(TrapCurPos, newPos, (Time.time - startTime) / overTime);
            yield return null;
        }

        TrapCurPos = newPos;
    }
    private IEnumerator MoveEntrance(Vector3 newPos){
        float startTime = Time.time;
        float overTime = 2f;
        float endTime = startTime + overTime;

        while (Time.time < endTime)
        {
            Entrance.transform.position = Vector3.Slerp(EntranceCurPos, newPos, (Time.time - startTime) / overTime);
            yield return null;
        }

        EntranceCurPos = newPos;
    }

    private IEnumerator MoveWall(Vector3 newPos){
        float startTime = Time.time;
        float overTime = 2f;
        float endTime = startTime + overTime;

        while (Time.time < endTime)
        {
            Wall.transform.position = Vector3.Slerp(WallCurPos, newPos, (Time.time - startTime) / overTime);
            yield return null;
        }

        WallCurPos = newPos;
    }
}
