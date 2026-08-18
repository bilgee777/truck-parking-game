
// This script used for load game settings
using UnityEngine;
using System.Collections;
namespace BilgeKorkmaz
{

	public class SettingsLoader : MonoBehaviour
	{


		public AudioSource AmbiantSound;

		[Header("You need to edit script for Amplify Color support")]
		[Space(3)]
		public Camera mainCamera;

		void Start()
		{
			if (PlayerPrefs.GetInt("AmbientSound") == 3)
				AmbiantSound.Play();
			else
				AmbiantSound.Stop();

			// Amplify color integeration

			if (!mainCamera)
				mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		}
	}
}