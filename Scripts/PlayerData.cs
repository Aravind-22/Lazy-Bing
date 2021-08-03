using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public int task=0;
    public int cash=0;
    public float timer=0;
    public float[] position= { 0,0,0};
    public bool[] objectNames;

    public PlayerData(TaskManager taskM)
    {
        task = taskM.taskCount;
        cash = taskM.cashCount;
        timer = taskM.timer;

        position = new float[3];
        position[0] = taskM.transform.position.x;
        position[1] = taskM.transform.position.y;
        position[2] = taskM.transform.position.z;
        //Debug.Log("" + position[0]+ ","+ position[1]+","+ position[2]);

        objectNames = new bool[5];
        objectNames[0] = taskM.names[0];
        objectNames[1] = taskM.names[1];
        objectNames[2] = taskM.names[2];
        objectNames[3] = taskM.names[3];
        objectNames[4] = taskM.names[4];
    }
}
