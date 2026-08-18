
//--------------------------------------------------------------

// Atach this script to roof selection menu and call SelectRoof(int id) function on each roof button

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace BilgeKorkmaz
{

	public class T_RoofSelection : MonoBehaviour
	{

		// To acces current car roofs class
		[HideInInspector] public T_Roof roofs;

		T_Shop tShop;

		void Start()
		{

			tShop = GameObject.FindFirstObjectByType<T_Shop>();


			for (int a = 0; a < tShop.roofPriceTexts.Length; a++)
				tShop.roofPriceTexts[a].text = tShop.roofPrice[a].ToString();

			// update locks on start
			UpdateLocks();

		}


		// public function to use in roof button OnClick() in his inspector         
		public void SelectRoof(int id)
		{
			if (PlayerPrefs.GetInt(roofs.carID.ToString() + "Roof" + id.ToString()) == 3)
			{  // 3=>true , 0=>false

				// Find current car T_Roof component
				roofs = GameObject.FindFirstObjectByType<T_Roof>();

				// And then activate current selected roof:
				roofs.SetRoof(id, true);

				PlayerPrefs.SetInt(roofs.carID.ToString() + "RoofID", id);

				tShop.ShowWindow(false);

			}
			else
			{// The current selected roof is not unlocked, We unlock it with reducing money from player total moneys and unlock roof


				// Find current car T_Roof component
				roofs = GameObject.FindFirstObjectByType<T_Roof>();

				// And then activate current selected roof:
				roofs.SetRoof(id, true);

				tShop.carID = roofs.carID;
				tShop.itemID = id;
				//			tShop.currentItemPrice.text = tShop.roofPrice[id].ToString();
				tShop.itemType = ItemType.Roof;
				tShop.ShowWindow(true);
			}
		}



		// public function to use in car selection ui manu
		public void UpdateLocks()
		{
			roofs = GameObject.FindFirstObjectByType<T_Roof>();

			for (int a = 0; a < tShop.roofPrice.Length; a++)
			{

				tShop.roofLocks[a].SetActive(true);

				if (PlayerPrefs.GetInt(roofs.carID.ToString() + "Roof" + a.ToString()) == 3)
				{

					// Deactivate pre unlocked roofs icon
					tShop.roofLocks[a].SetActive(false);
				}
			}
		}
	}
}