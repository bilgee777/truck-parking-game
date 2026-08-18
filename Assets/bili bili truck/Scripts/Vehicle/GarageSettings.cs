

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
namespace BilgeKorkmaz
{

	public class GarageSettings : MonoBehaviour
	{

		public string garageSceneName = "Garage";

		public GameObject cameraParent;

		public GameObject helperArrow;

		void Start()
		{
			if (SceneManager.GetActiveScene().name.Contains(garageSceneName))
			{
				cameraParent.SetActive(false);

				if (helperArrow)
					helperArrow.SetActive(false);
			}
			else
			{
				cameraParent.SetActive(true);

				if (helperArrow)
					helperArrow.SetActive(true);
			}
		}
	}
}