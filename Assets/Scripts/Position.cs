using System;
using UnityEngine;

public class Position {
    private float minX = -Mathf.PI;
    private float maxX = Mathf.PI;
    private Transform _transform;
    public float x { get; private set; }
    public float y { get; private set; }

    public Position(Transform transform, float x, float y) {
        _transform = transform;
        setX(x);
        this.y = y;
    }

    public void Translate(float x, float y) {
        setX(this.x + x);
        this.y += y;
        _transform.position = CircularPattern.CilPos(this.x, this.y);
    }

    private void setX(float newX) {
        //Don't even ask
        if (newX < minX)
            x = maxX - (minX - x);
        else if (newX > maxX)
            x = minX + (x - maxX);
        else
            x = newX;
    }

    public void SetPosition(float x, float y) {
        setX(x);
        this.y = y;
        _transform.position = CircularPattern.CilPos(x, y);
    }
}