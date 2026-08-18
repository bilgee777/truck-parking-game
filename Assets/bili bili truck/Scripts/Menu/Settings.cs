

// This script used for game settings menu

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace BilgeKorkmaz
{


	public class Settings : MonoBehaviour
	{

		public Toggle AmbientSound, SSR;

		public Dropdown resolutionQuality;


		void Start()
		{
			// Read starting setting values
			if (PlayerPrefs.GetInt("AmbientSound") == 3)
				AmbientSound.isOn = true;
			else
				AmbientSound.isOn = false;


			if (PlayerPrefs.GetInt("SSR") == 3)
				SSR.isOn = true;
			else
				SSR.isOn = false;

			resolutionQuality.value = PlayerPrefs.GetInt("ResQuality");

			Update_SSR();

		}

		// Public function for ambient sound toggle
		public void Set_AmbientSound()
		{
			StartCoroutine(AmbiantSound_Save());
		}

		public void SetResolution()
		{
			StartCoroutine(UpdateResolution());
		}

		IEnumerator UpdateResolution()
		{
			yield return new WaitForEndOfFrame();

			if (resolutionQuality.value == 0)
			{
				Screen.SetResolution((int)(PlayerPrefs.GetInt("OriginalX") * 0.3f),
					(int)(PlayerPrefs.GetInt("OriginalY") * 0.3f), true);
			}
			if (resolutionQuality.value == 1)
			{
				Screen.SetResolution((int)(PlayerPrefs.GetInt("OriginalX") * 0.5f),
					(int)(PlayerPrefs.GetInt("OriginalY") * 0.5f), true);
			}
			if (resolutionQuality.value == 2)
			{
				Screen.SetResolution((int)(PlayerPrefs.GetInt("OriginalX") * 0.7f),
					(int)(PlayerPrefs.GetInt("OriginalY") * 0.7f), true);
			}
			if (resolutionQuality.value == 3)
			{
				Screen.SetResolution((int)(PlayerPrefs.GetInt("OriginalX") * 1),
					(int)(PlayerPrefs.GetInt("OriginalY") * 1), true);
			}

			PlayerPrefs.SetInt("ResQuality", resolutionQuality.value);

		}

		IEnumerator AmbiantSound_Save()
		{
			yield return new WaitForEndOfFrame();
			if (AmbientSound.isOn)
			{
				PlayerPrefs.SetInt("AmbientSound", 3);  //3 = true;
				GameObject.FindFirstObjectByType<SettingsLoader>().AmbiantSound.Play();
			}
			else
			{
				PlayerPrefs.SetInt("AmbientSound", 0);//0 = false;

				GameObject.FindFirstObjectByType<SettingsLoader>().AmbiantSound.Stop();
			}
		}
		public void Set_SSR()
		{
			StartCoroutine(SSR_Save());
		}
		IEnumerator SSR_Save()
		{
			yield return new WaitForEndOfFrame();
			if (SSR.isOn)
				PlayerPrefs.SetInt("SSR", 3);  //3 = true;
			else
				PlayerPrefs.SetInt("SSR", 0);//0 = false;

			Update_SSR();
		}
		public void Update_SSR()
		{
			if (PlayerPrefs.GetInt("SSR") == 3)
			{
				foreach (Camera cam in FindObjectsOfType<Camera>())
					cam.renderingPath = RenderingPath.DeferredShading;

				UnityEngine.Rendering.PostProcessing.PostProcessVolume volume
			= FindFirstObjectByType<UnityEngine.Rendering.PostProcessing.PostProcessVolume>();

				UnityEngine.Rendering.PostProcessing.ScreenSpaceReflections ssr;

				volume.profile.TryGetSettings(out ssr);

				ssr.enabled.overrideState = true;
				ssr.enabled.value = true;
			}
			else
			{
				foreach (Camera cam in FindObjectsOfType<Camera>())
					cam.renderingPath = RenderingPath.Forward;

				UnityEngine.Rendering.PostProcessing.PostProcessVolume volume
					= FindFirstObjectByType<UnityEngine.Rendering.PostProcessing.PostProcessVolume>();

				UnityEngine.Rendering.PostProcessing.ScreenSpaceReflections ssr;

				volume.profile.TryGetSettings(out ssr);

				ssr.enabled.overrideState = true;
				ssr.enabled.value = false;
			}
		}
	}
}