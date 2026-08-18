

// Attach this script to any car and drage his Exhausts

using UnityEngine;
using System.Collections;
namespace BilgeKorkmaz
{

	public class T_Exhaust : MonoBehaviour
	{

		public int carID;

		public GameObject[] Exhausts;

		void Start()
		{
			SetExhaust(PlayerPrefs.GetInt(carID.ToString() + "ExhaustID"), true);
		}

		public void SetExhaust(int id, bool state)
		{

			// We have 6 exhausts, and first de activate all exhausts
			for (int a = 0; a < Exhausts.Length; a++)
				Exhausts[a].SetActive(false);


			if (Exhausts[id])
				Exhausts[id].SetActive(state);




		}
	}
}