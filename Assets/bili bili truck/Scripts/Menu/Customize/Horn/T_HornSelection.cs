

// Atach this script to horn selection menu and call SelectHorn(int id) function on each horn button

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace BilgeKorkmaz
{

	public class T_HornSelection : MonoBehaviour
	{

		// To acces current car horns class
		[HideInInspector] public T_Horn horns;
		T_Shop tShop;

		void Start()
		{

			tShop = GameObject.FindFirstObjectByType<T_Shop>();


			for (int a = 0; a < tShop.hornPriceTexts.Length; a++)
				tShop.hornPriceTexts[a].text = tShop.hornPrice[a].ToString();

			// update locks on start
			UpdateLocks();

		}


		// public function to use in horn button OnClick() in his inspector         
		public void SelectHorn(int id)
		{
			if (PlayerPrefs.GetInt(horns.carID.ToString() + "Horn" + id.ToString()) == 3)
			{  // 3=>true , 0=>false

				// Find current car T_Horn component
				horns = GameObject.FindFirstObjectByType<T_Horn>();

				// And then activate current selected horn:
				horns.SetHorn(id, true);

				PlayerPrefs.SetInt(horns.carID.ToString() + "HornID", id);

				tShop.ShowWindow(false);
			}
			else
			{// The current selected horn is not unlocked, We unlock it with reducing money from player total moneys and unlock horn

				// Find current car T_Horn component
				horns = GameObject.FindFirstObjectByType<T_Horn>();

				// And then activate current selected horn:
				horns.SetHorn(id, true);


				tShop.carID = horns.carID;
				tShop.itemID = id;
				//			tShop.currentItemPrice.text = tShop.hornPrice [id].ToString();
				tShop.itemType = ItemType.Horn;
				tShop.ShowWindow(true);
			}
		}



		// public function to use in car selection ui manu
		public void UpdateLocks()
		{
			horns = GameObject.FindFirstObjectByType<T_Horn>();

			for (int a = 0; a < tShop.hornPrice.Length; a++)
			{

				tShop.hornLocks[a].SetActive(true);

				if (PlayerPrefs.GetInt(horns.carID.ToString() + "Horn" + a.ToString()) == 3)
				{

					// Deactivate pre unlocked horns icon
					tShop.hornLocks[a].SetActive(false);
				}
			}
		}
	}
}