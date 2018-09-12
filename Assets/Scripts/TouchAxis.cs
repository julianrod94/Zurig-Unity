using System;
using UnityEngine;

public class TouchAxis : MonoBehaviour, AxisProvider {
	public float GetAxis(string axis) {
		return 1f;
	}
}
