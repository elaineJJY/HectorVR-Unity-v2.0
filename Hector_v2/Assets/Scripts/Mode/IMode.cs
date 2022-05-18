/*
 * Author: Jingyi Jia
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public interface  IMode{
    Vector2 Signal { get; }

    void UpdateSignal();


}
