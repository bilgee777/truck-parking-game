//--------------------------------------------------------------
//
//                    Truck Parking kit
//        
//           Contact me : aliyeredon@gmail.com
//
//--------------------------------------------------------------

// Atach this script to Exhaust selection menu and call SelectExhaust(int id) function on each Exhaust button

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace BilgeKorkmaz
{

	public class T_ExhaustSelection : MonoBehaviour
	{

		// To acces current car exhausts class
		[HideInInspector] public T_Exhaust exhausts;

		T_Shop tShop;

		void Start()
		{

			tShop = GameObject.FindFirstObjectByType<T_Shop>();

			for (int a = 0; a < tShop.exhaustPriceTexts.Length; a++)
				tShop.exhaustPriceTexts[a].text = tShop.exhaustPrice[a].ToString();

			// update locks on start
			UpdateLocks();

		}


		// public function to use in Exhaust button OnClick() in his inspector         
		public void SelectExhaust(int id)
		{
			if (PlayerPrefs.GetInt(exhausts.carID.ToString() + "Exhaust" + id.ToString()) == 3)
			{  // 3=>true , 0=>false

				// Find current car T_exhausts component
				exhausts = GameObject.FindFirstObjectByType<T_Exhaust>();

				// And then activate current selected Exhaust:
				exhausts.SetExhaust(id, true);

				PlayerPrefs.SetInt(exhausts.carID.ToString() + "ExhaustID", id);

				tShop.ShowWindow(false);


			}
			else
			{// The current selected Exhaust is not unlocked, We unlock it with reducing money from player total moneys and unlock Exhaust


				// Find current car T_exhausts component
				exhausts = GameObject.FindFirstObjectByType<T_Exhaust>();

				// And then activate current selected Exhaust:
				exhausts.SetExhaust(id, true);

				tShop.carID = exhausts.carID;
				tShop.itemID = id;
				//	tShop.currentItemPrice.text = tShop.exhaustPrice [id].ToString();
				tShop.itemType = ItemType.Exhaust;
				tShop.ShowWindow(true);
			}
		}



		// public function to use in car selection ui manu
		public void UpdateLocks()
		{
			exhausts = GameObject.FindFirstObjectByType<T_Exhaust>();

			for (int a = 0; a < tShop.exhaustPrice.Length; a++)
			{

				tShop.exhaustLocks[a].SetActive(true);

				if (PlayerPrefs.GetInt(exhausts.carID.ToString() + "Exhaust" + a.ToString()) == 3)
				{

					// Deactivate pre unlocked exhausts icon
					tShop.exhaustLocks[a].SetActive(false);
				}
			}
		}
	}
}