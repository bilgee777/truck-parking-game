
// Attach this script to any car and drage his horns

using UnityEngine;
using System.Collections;
namespace BilgeKorkmaz
{

	public class T_Horn : MonoBehaviour
	{

		public int carID;

		public GameObject[] Horns;


		void Start()
		{
			SetHorn(PlayerPrefs.GetInt(carID.ToString() + "HornID"), true);
		}

		public void SetHorn(int id, bool state)
		{
			// We have first de activate all horns
			for (int a = 0; a < Horns.Length; a++)
				Horns[a].SetActive(false);


			// Activate current selected
			if (Horns[id])
				Horns[id].SetActive(state);


		}
	}
}