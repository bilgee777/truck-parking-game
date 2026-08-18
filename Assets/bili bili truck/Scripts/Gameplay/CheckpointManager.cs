
//Oyuncunun sıradaki hedef noktasını (checkpoint) belirleyen ve GPS/ışık sistemiyle o noktayı göstermeyi sağlayan yönlendirme yöneticisidir

using UnityEngine;
using System.Collections;
namespace BilgeKorkmaz
{

	public class CheckpointManager : MonoBehaviour
	{


		public int currentCheckpoint;

		public GameObject[] checkpoints;

		// Vehicle GPS (used flash lights to find target)  
		FlashLight fLight;

		IEnumerator Start()
		{



			for (int a = 0; a < checkpoints.Length; a++)
			{

				checkpoints[a].SetActive(false);
			}

			// Wait a frame to vehicle had spawned
			yield return new WaitForEndOfFrame();

			fLight = GameObject.FindGameObjectWithTag("Player").GetComponent<FlashLight>();

			fLight.SetTarget(checkpoints[currentCheckpoint].transform);

			fLight.isActive = true;

		}

		public void NextCheckpoint()
		{

			currentCheckpoint++;

			for (int a = 0; a < checkpoints.Length; a++)
			{

				checkpoints[a].SetActive(false);
			}

			if (checkpoints.Length > currentCheckpoint)
				checkpoints[currentCheckpoint].SetActive(true);

			fLight.SetTarget(checkpoints[currentCheckpoint].transform);

		}
	}
}