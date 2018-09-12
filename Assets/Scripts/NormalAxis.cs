using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAxis : AxisProvider {
    public float GetAxis(string axis) {
        return Input.GetAxis(axis);
    }
}
