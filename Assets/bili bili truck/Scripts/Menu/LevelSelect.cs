// This script used for level selection and lock system in game menu

using UnityEngine;
using System.Collections;   
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace BilgeKorkmaz
{

	public class LevelSelect : MonoBehaviour
	{

		// Array of locks
		public GameObject[] Locks;

		// Temp
		int temp;

		// Next menu for activat it
		public GameObject currentMenu, nextMenu;

		public GameObject[] star1Level, star2Level, star3Level;

		public Text[] bestTime;

		public GameObject selectDialog;

		public GameObject loading;

		void Start()
		{

            //Level  num   is  :   3
            temp = PlayerPrefs.GetInt("TruckLevelNum", 1);
            temp = Mathf.Clamp(temp, 1, 5);

            for (int a = 0; a < Locks.Length; a++)
            {
                if (a < 5)
                {
                    Locks[a].SetActive(a >= temp);
                }
                else
                {
                    Locks[a].SetActive(true);
                }
            }

            for (int aa = 0; aa < bestTime.Length; aa++)
			{

				float min = PlayerPrefs.GetFloat("TruckMinutes" + aa.ToString());
				float secn = PlayerPrefs.GetFloat("TruckSeconds" + aa.ToString());

				string minS, secS;

				minS = min.ToString();
				secS = Mathf.Floor(secn).ToString();

				if (min < 10)
					minS = "0" + min.ToString();

				if (secn < 10)
					secS = "0" + Mathf.Floor(secn).ToString();


				bestTime[aa].text = (minS + ":" + secS)
					.ToString();

				if (PlayerPrefs.GetInt("Star" + aa.ToString()) == 3)
				{
					star1Level[aa].SetActive(true);
					star2Level[aa].SetActive(true);
					star3Level[aa].SetActive(true);
				}
				if (PlayerPrefs.GetInt("Star" + aa.ToString()) == 2)
				{
					star1Level[aa].SetActive(true);
					star2Level[aa].SetActive(true);
					star3Level[aa].SetActive(false);
				}
				if (PlayerPrefs.GetInt("Star" + aa.ToString()) == 1)
				{
					star1Level[aa].SetActive(true);
					star2Level[aa].SetActive(false);
					star3Level[aa].SetActive(false);
				}
				if (PlayerPrefs.GetInt("Star" + aa.ToString()) == 0)
				{
					star1Level[aa].SetActive(false);
					star2Level[aa].SetActive(false);
					star3Level[aa].SetActive(false);
				}





			}
		}

        public void SelectLevel(int id)
        {
            if (id < temp && id < 5)
            {
                tempID = id;
                selectDialog.SetActive(true);
            }
        }

        int tempID;

		public void SelectLevelNow()
		{
			loading.SetActive(true);

			GetComponentInParent<PauseMen>().Resume();

			PlayerPrefs.SetInt("TruckLevelID", tempID);
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

	}
}