

// Attach this script to any car and drage his Guards

using UnityEngine;
using System.Collections;
namespace BilgeKorkmaz
{

	public class T_Guard : MonoBehaviour
	{

		public int carID;

		public GameObject[] Guards;

		void Start()
		{
			SetGuard(PlayerPrefs.GetInt(carID.ToString() + "GuardID"), true);
		}
		public void SetGuard(int id, bool state)
		{

			// We have 6 guards, and first de activate all guards
			for (int a = 0; a < Guards.Length; a++)
				Guards[a].SetActive(false);


			if (Guards[id])
				Guards[id].SetActive(state);



		}
	}
}