using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	public class AudioManager : MonoBehaviour
	{
		[SerializeField]
		private AudioClip spaceShipEngineSound;
		[SerializeField]
		private AudioClip spaceShipJumpSound;
		[SerializeField]
		private AudioClip moveSpaceShipSound;
		[SerializeField]
		private AudioClip spaceShipTroughCilinderSound;
		[SerializeField]
		private AudioClip brokenCilinderSound;
		[SerializeField]
		private AudioClip mainTheme;

		private AudioSource spaceShipEngineLoop;
		private AudioSource mainThemeLoop;
		
		private bool isPlayingMainLoop = false;

		private void Start() {
			mainThemeLoop = GetComponent<AudioSource>();
			mainThemeLoop.loop = true;
			mainThemeLoop.clip = mainTheme;
			
			spaceShipEngineLoop = GetComponent<AudioSource>();
			spaceShipEngineLoop.loop = true;
			spaceShipEngineLoop.clip = spaceShipEngineSound;
			playMainTheme();
			playSpaceShipEngine();
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

		public void playBrokenCilinderSound()
		{
			GetComponent<AudioSource>().PlayOneShot(brokenCilinderSound, 0.2f);
		}
		
		public void playFlyOverCilinderSound()
		{
			GetComponent<AudioSource>().PlayOneShot(spaceShipTroughCilinderSound, 0.03f);
		}
		
		public void playMainTheme()
		{
			if (isPlayingMainLoop) return;
			mainThemeLoop.Play();
			isPlayingMainLoop = true;
		}

		public void playSpaceShipEngine()
		{
			spaceShipEngineLoop.Play();
		}

		public void stopSpaceShipEngine()
		{
			spaceShipEngineLoop.Stop();
		}
		
		public void stopMainTheme() {
			mainThemeLoop.Stop();
			isPlayingMainLoop = false;
		}
	}