using System.Collections;
using UnityEngine;
using System;

public class Snake : MonoBehaviour{
    private Snake next;
    static public Action<String> hit;
    public string objectTag = "";

    //When a collison occurs with an object the method will be run
    public void OnTriggerEnter(Collider other)
    {
        if(hit != null)
        {
            objectTag = other.tag;
        }
        //Destroys the current food object
        if(other.tag == "Food")
        {
           Destroy(other.gameObject);
        }
    }

    public void SetNext(Snake IN)
    {
        next = IN;
    }

    public Snake GetNext()
    {
        return next;
    }

    //Removes current tail for movement purposes
    public void RemoveTail()
    {
        Destroy(this.gameObject);
    }
}
