using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayGameObjects : MonoBehaviour
{
    int index,counter = 0;
    public List<GameObject> pickableObjects = new List<GameObject>();
    public List<GameObject> tempGameObjects = new List<GameObject>();
    public GameObject[] gameObjectNames = new GameObject[5];
    public static DisplayGameObjects instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        pickableObjects.AddRange(GameObject.FindGameObjectsWithTag("Pickable"));
        RemoveCollider();
    }

    // Update is called once per frame
    void Update()
    {
        if (counter == 0)
        {
            AddObjects();
            foreach (var go in tempGameObjects)
            {
                if(index<5)
                {
                    gameObjectNames[index].GetComponent<Text>().text = go.name;
                }
                ++index;
            }
        }
        if (ObjectMatching.instance.taskCompleted == true)
        {
            tempGameObjects.Clear();
            index = 0;
            counter = 0;
            ObjectMatching.instance.taskCompleted = false;
        }
    }
    
    public void AddObjects()
    {
        foreach (GameObject g in pickableObjects.GetRange(0,5))
        {
            if (counter < 5)
            {
                tempGameObjects.Add(g);
                pickableObjects.Remove(g);
                ++counter;
            }
            //Debug.Log("counter "+counter);
        }
        //resets taskcomplete to false immediately after adding into tempgameobjects since counter is 5
        if (counter == 5)
        {
            ObjectMatching.instance.taskCompleted = false;
            //Debug.Log("task complete set to false");
        }
            
        AddCollider();
    }

    public void RemoveCollider()
    {
        foreach(GameObject removeCollider in pickableObjects)
        {
            removeCollider.GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void AddCollider()
    {
        foreach (GameObject addCollider in tempGameObjects)
        {
            addCollider.GetComponent<BoxCollider>().enabled = true;
            //Debug.Log("collider added");
        }
    }
}
