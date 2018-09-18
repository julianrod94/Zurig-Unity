using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class BulletManager : MonoBehaviour {

	public static BulletManager Instance;
	
	public BulletLife BulletPrefab;
	public int AmountBullet;
	public float BulletLifeTime;
	
	private BulletLife[] _bullets;
	
	public void Awake() {
		if (Instance == null) {
			Instance = this;
			Initialize();
		} else {
			Destroy(gameObject);
		}
	}

	private void Initialize() {
		_bullets = new BulletLife[AmountBullet];
		for (int i = 0; i < AmountBullet; i++) {
			var bullet = Instantiate(BulletPrefab.gameObject);
			bullet.SetActive(false);
			_bullets[i] = bullet.GetComponent<BulletLife>();
		}
	}

	public void GenerateBullet(Position position, Quaternion rotation) {
		var bullet = FindFirstDisabled();
		if (bullet != null) {
			bullet.gameObject.SetActive(true);
			bullet.position.SetPosition(position.x, position.y);
			bullet.transform.rotation = rotation;
			bullet.SetupLifeTime(BulletLifeTime);
		}
	}

	[CanBeNull]
	private BulletLife FindFirstDisabled() {
		return _bullets.FirstOrDefault(bullet => !bullet.gameObject.activeSelf);
	}
	
}
