using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleReceiver : MonoBehaviour
{
    public void HelloWorld() {
        Debug.Log("hello world");
    }

    public void HelloNumber(object number) {
        Debug.Log($"hello {number}");
    }
}
