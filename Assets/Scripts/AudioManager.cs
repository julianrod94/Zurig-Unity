using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

	public class AudioManager : MonoBehaviour
	{
		[SerializeField]
		private AudioClip spaceShipJumpSound;
		[SerializeField]
		private AudioClip spaceShipBoostSound;
		[SerializeField]
		private AudioClip spaceShipTroughCilinderSound;
		[SerializeField]
		private AudioClip spaceShipDesintegrationSound;
		[SerializeField]
		private AudioClip brokenCilinderSound;
		[SerializeField]
		private AudioClip shieldSound;
		[SerializeField]
		private AudioClip mainTheme;
		[SerializeField]
		private AudioClip buttonSound;
		[SerializeField]
		private AudioClip pickUpKeySound;
		[SerializeField]
		private AudioClip portalOpenedSound;

		private AudioSource mainThemeLoop;
		private AudioSource secondaryLoop;
		
		private bool isPlayingMainLoop = false;

		private void Awake() {
			if (_instance == null) {
				_instance = this;
				var sources = GetComponents<AudioSource>();
				mainThemeLoop = sources.First();
				secondaryLoop = sources.Last();

				secondaryLoop.loop = true;
				secondaryLoop.volume = 0.2f;

				mainThemeLoop.loop = true;
				mainThemeLoop.clip = mainTheme;
				mainThemeLoop.volume = 0.3f;

				playMainTheme();
				DontDestroyOnLoad(this);
			} else {
				_instance.secondaryLoop.Stop();
				Destroy(this);
			}
		}
		
		private static AudioManager _instance = null;

		public static AudioManager Instance
		{
			get { return _instance ?? (_instance = new AudioManager()); }
			private set { _instance = value; }
		}

		public void playExplosionSound()
		{
			mainThemeLoop.PlayOneShot(brokenCilinderSound, 1f);
		}
		
		public void playFlyOverCilinderSound()
		{
			mainThemeLoop.PlayOneShot(spaceShipTroughCilinderSound, 0.05f);
		}

		public void playJumpSound()
		{
			mainThemeLoop.PlayOneShot(spaceShipJumpSound, 0.6f);
		}
		
		public void playBoostSound()
		{
			mainThemeLoop.PlayOneShot(spaceShipBoostSound, 0.5f);
		}
		
		public void playShieldSound()
		{
			mainThemeLoop.PlayOneShot(shieldSound, 0.5f);
		}

		public void playDesintegrationSound()
		{
			mainThemeLoop.PlayOneShot(spaceShipDesintegrationSound, 1f);
		}
		
		public void playButtonSound()
		{
			mainThemeLoop.PlayOneShot(buttonSound, 1f);	
		}
		
		public void playPickUpKeySound()
		{
			mainThemeLoop.PlayOneShot(pickUpKeySound, 1f);	
		}
		
		public void playPortalOpenedSound()
		{
			secondaryLoop.clip = portalOpenedSound;
			secondaryLoop.Play();
		}
		
		public void playMainTheme()
		{
			if (isPlayingMainLoop) return;
			mainThemeLoop.Play();
			isPlayingMainLoop = true;
		}

		public void stopMainTheme() {
			mainThemeLoop.Stop();
			isPlayingMainLoop = false;
		}
	}