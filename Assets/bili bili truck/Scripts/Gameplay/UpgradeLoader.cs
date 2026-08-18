

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
namespace BilgeKorkmaz
{

	public class UpgradeLoader : MonoBehaviour
	{

		// Upgrade level list
		public float[] enginePower, maxSpeed, brakeUpgrade;


		VehicleController truck;

		void Start()
		{

			if (SceneManager.GetActiveScene().name.Contains("Garage") ||
			   SceneManager.GetActiveScene().name.Contains("Menu"))
				return;

			truck = GetComponent<VehicleController>();

			// Set truck motor power based on upgrade value on upgrade menu
			truck.enginePower = enginePower[PlayerPrefs.GetInt("Engine" + PlayerPrefs.GetInt("TruckID").ToString())];
			truck.maxSpeed = maxSpeed[PlayerPrefs.GetInt("Speed" + PlayerPrefs.GetInt("TruckID").ToString())];
			truck.brakePower = brakeUpgrade[PlayerPrefs.GetInt("Brake" + PlayerPrefs.GetInt("TruckID").ToString())];
		}

	}
}