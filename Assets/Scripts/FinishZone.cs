using UnityEngine;

public class FinishZone : MonoBehaviour {
	public GameObject Portal;

	public void openPortal() {
		AudioManager.Instance.playPortalOpenedSound();
		Portal.SetActive(false);
	}
}