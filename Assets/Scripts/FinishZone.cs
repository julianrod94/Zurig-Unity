using UnityEngine;

public class FinishZone : MonoBehaviour {
	public GameObject Portal;

	public void openPortal() {
		Portal.SetActive(false);
	}
}