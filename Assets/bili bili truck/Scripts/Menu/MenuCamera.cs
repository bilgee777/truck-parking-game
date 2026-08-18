
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
namespace BilgeKorkmaz
{

	// Deactivate vehicle cameras in garage scene
	public class MenuCamera : MonoBehaviour
	{


		void Start()
		{
			if (SceneManager.GetActiveScene().name.Contains("Garage"))
				gameObject.SetActive(false);
		}

	}
}