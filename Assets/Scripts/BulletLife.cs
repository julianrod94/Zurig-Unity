using System;
using System.Collections;
using System.Linq.Expressions;
using UnityEngine;

public class BulletLife: MonoBehaviour {
    public Position position;
    public float Speed;

    public void Awake() {
        position = new Position(transform, 0, 0);
    }

    public void Update() {
        //delta time
        position.Translate(0, Speed);
    }

    public void SetupLifeTime(float time) {
        StartCoroutine(SetLifeTime(time));
    }

    private IEnumerator SetLifeTime(float lifeTime) {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }
}
