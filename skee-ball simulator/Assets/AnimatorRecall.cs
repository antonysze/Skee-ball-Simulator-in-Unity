using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorRecall : MonoBehaviour
{
    public GameObject callbackObj;
    public string callbackFunction;

    public void callback() {
        callbackObj.SendMessage(callbackFunction, SendMessageOptions.DontRequireReceiver);
    }
}
