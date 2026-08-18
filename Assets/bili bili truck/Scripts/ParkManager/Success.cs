
// This script used for success menu buttons

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace BilgeKorkmaz
{

	public class Success : MonoBehaviour
	{
		[Header("Success Menu Manager")]

		// Loading text for "Loading..."
		public Text LoadingTXT;

		// Parking Manager handler
		[HideInInspector] public ParkingManager manager;

		public string garageName = "Garage";
        public GameObject desktopMenu;
        public GameObject successMenu;

        // Activate parking place helper
        public void ActiveHelper()
        {
            if (manager != null && manager.Helper != null)
                manager.Helper.SetActive(!manager.Helper.activeSelf);
        }


        public IEnumerator Start()
		{

			// Delay and find manager
			yield return new WaitForEndOfFrame();

            GameObject managerObj = GameObject.FindGameObjectWithTag("Manager");

            if (managerObj != null)
            {
                manager = managerObj.GetComponent<ParkingManager>();
            }
        }

        // SuccessMenu continue button
        public void Continue()
        {
            LoadingTXT.text = "Loading...";

            int currentLevel = PlayerPrefs.GetInt("TruckLevelID", 0);

            if (currentLevel >= 4)
            {
                successMenu.SetActive(false);
                desktopMenu.SetActive(true);
                return;
            }

            PlayerPrefs.SetInt("TruckLevelID", currentLevel + 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


        // SuccessMenu retry button
        public void Retry()
		{
			LoadingTXT.text = "Loading...";
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}


		//SuccessMenu exit button    
		public void Exit()
		{
			LoadingTXT.text = "Loading...";
			SceneManager.LoadScene(garageName);
		}
	}
}