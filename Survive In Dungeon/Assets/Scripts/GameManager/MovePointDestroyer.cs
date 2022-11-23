using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePointDestroyer : MonoBehaviour
{
    public float cursorDeleteTime;
    void Start()
    {
        Destroy(this.gameObject, cursorDeleteTime);
    }

}
