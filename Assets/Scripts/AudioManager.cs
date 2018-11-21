using System.Collections;
using System.Collections.Generic;
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

		private AudioSource mainThemeLoop;
		
		private bool isPlayingMainLoop = false;

		private void Start() {
			mainThemeLoop = GetComponent<AudioSource>();
			mainThemeLoop.loop = true;
			mainThemeLoop.clip = mainTheme;
			mainThemeLoop.volume = 0.3f;
			
			playMainTheme();
		}
		
		private static AudioManager _instance = null;

		public static AudioManager Instance
		{
			get { return _instance ?? (_instance = new AudioManager()); }
			private set { _instance = value; }
		}

		// Use this for initialization
		void Awake()
		{
			_instance = this;
		}

		public void playExplosionSound()
		{
			GetComponent<AudioSource>().PlayOneShot(brokenCilinderSound, 1f);
		}
		
		public void playFlyOverCilinderSound()
		{
			GetComponent<AudioSource>().PlayOneShot(spaceShipTroughCilinderSound, 0.05f);
		}

		public void playJumpSound()
		{
			GetComponent<AudioSource>().PlayOneShot(spaceShipJumpSound, 0.6f);
		}
		
		public void playBoostSound()
		{
			GetComponent<AudioSource>().PlayOneShot(spaceShipBoostSound, 0.5f);
		}
		
		public void playShieldSound()
		{
			GetComponent<AudioSource>().PlayOneShot(shieldSound, 0.5f);
		}

		public void playDesintegrationSound()
		{
			GetComponent<AudioSource>().PlayOneShot(spaceShipDesintegrationSound, 1f);
		}
		
		public void playButtonSound()
		{
			GetComponent<AudioSource>().PlayOneShot(buttonSound, 1f);	
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