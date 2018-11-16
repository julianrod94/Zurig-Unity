using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAxis : AxisProvider {
    private int lastValue = 0;
    public float GetAxis(string axis) {
        if (Input.GetMouseButton(0)) {
            return 1;
        }
        
        if (Input.GetMouseButton(1)) {
            return -1;
        }
        return 0;
    }
}
