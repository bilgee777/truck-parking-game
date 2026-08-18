
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace BilgeKorkmaz
{

	public class MainUtility : MonoBehaviour
	{

		public GameObject Loading, exitMenu;

		public int startingScore = 1400;

		public int targetFPS = 60;

		public float timeScale = 1.5f;

		void Awake()
		{

			Application.targetFrameRate = targetFPS;

			PlayerPrefs.SetFloat("TimeScale", timeScale);

			// Is game first run?   3 => true    0 => false
			if (PlayerPrefs.GetInt("FirstRun") != 3)
			{

				PlayerPrefs.SetInt("OriginalX", Screen.width);
				PlayerPrefs.SetInt("OriginalY", Screen.height);

				// Set render scale as 0.89f by default (0 = 0.5f, 1 = 0.7f, 2 = 0.89f, 3 = 1.0f)
				PlayerPrefs.SetInt("ResQuality", 2);

				PlayerPrefs.SetFloat("accelSensibility", 10f);

				PlayerPrefs.SetInt("FirstRun", 3);

				// Open first level
				PlayerPrefs.SetInt("CarLevelNum", 1);
				PlayerPrefs.SetInt("BusLevelNum", 1);
				PlayerPrefs.SetInt("TruckLevelNum", 1);

				// Set starting color for each truck
				PlayerPrefs.SetInt("TruckColor0", 0); // red for truck 1
				PlayerPrefs.SetInt("TruckColor1", 1); // blue for truck 2

				// Set ambiant sound in settings true
				PlayerPrefs.SetInt("AmbientSound", 3);

				// Set Sea active in settings true
				PlayerPrefs.SetInt("Sea", 3);

				// Open first car
				PlayerPrefs.SetInt("Car0", 3);
				PlayerPrefs.SetInt("Bus0", 3);
				PlayerPrefs.SetInt("Truck0", 3);


				// Player starting first time coins
				PlayerPrefs.SetInt("Coins", startingScore);

				PlayerPrefs.SetInt("LevelXP", 1);

				// Unlock first customize items
				for (int a = 0; a < 10; a++)
				{
					PlayerPrefs.SetInt(a.ToString() + "Ring0", 3);
					PlayerPrefs.SetInt(a.ToString() + "Exhaust0", 3);
					PlayerPrefs.SetInt(a.ToString() + "Guard0", 3);
					PlayerPrefs.SetInt(a.ToString() + "Horn0", 3);
					PlayerPrefs.SetInt(a.ToString() + "Roof0", 3);
				}
			}

			Time.timeScale = PlayerPrefs.GetFloat("TimeScale");

			if (PlayerPrefs.GetInt("ResQuality") == 0)
			{
				Screen.SetResolution((int)(PlayerPrefs.GetInt("OriginalX") * 0.3f),
					(int)(PlayerPrefs.GetInt("OriginalY") * 0.3f), true);
			}
			if (PlayerPrefs.GetInt("ResQuality") == 1)
			{
				Screen.SetResolution((int)(PlayerPrefs.GetInt("OriginalX") * 0.5f),
					(int)(PlayerPrefs.GetInt("OriginalY") * 0.5f), true);
			}
			if (PlayerPrefs.GetInt("ResQuality") == 2)
			{
				Screen.SetResolution((int)(PlayerPrefs.GetInt("OriginalX") * 0.7f),
					(int)(PlayerPrefs.GetInt("OriginalY") * 0.7f), true);
			}
			if (PlayerPrefs.GetInt("ResQuality") == 3)
			{
				Screen.SetResolution((int)(PlayerPrefs.GetInt("OriginalX") * 1),
					(int)(PlayerPrefs.GetInt("OriginalY") * 1), true);
			}
		}

		void Update()
		{
			// Exit with back button
			if (Input.GetKeyDown(KeyCode.Escape))
				exitMenu.SetActive(!exitMenu.activeSelf);

			if (Input.GetKeyDown(KeyCode.H))
			{
				PlayerPrefs.DeleteAll();
				Debug.Log("PlayerPrefs.DeleteAll ();");
			}
			if (Input.GetKeyDown(KeyCode.E))
				PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 14000);
		}

		public void Exit()
		{
			Application.Quit();
		}

		public void SetTrue(GameObject target)
		{
			target.SetActive(true);
		}

		public void SetFalse(GameObject target)
		{
			target.SetActive(false);
		}

		public void ToggleObject(GameObject target)
		{
			target.SetActive(!target.activeSelf);
		}

		public void LoadLevel(string name)
		{

			Loading.SetActive(true);
			SceneManager.LoadSceneAsync(name);
		}

		public void OpenURL(string val)
		{
			Application.OpenURL(val);
		}

		public void LoadSubmitPage()
		{
			PlayerPrefs.SetInt("StartMenu", 0);
		}
	}
}