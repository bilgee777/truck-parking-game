

// Atach this script to guard selection menu and call SelectGuard(int id) function on each guard button

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace BilgeKorkmaz
{

	public class T_GuardSelection : MonoBehaviour
	{

		// To acces current car guards class
		[HideInInspector] public T_Guard guards;
		T_Shop tShop;

		void Start()
		{

			tShop = GameObject.FindFirstObjectByType<T_Shop>();


			for (int a = 0; a < tShop.guardPriceTexts.Length; a++)
				tShop.guardPriceTexts[a].text = tShop.guardPrice[a].ToString();

			// update locks on start
			UpdateLocks();

		}


		// public function to use in guard button OnClick() in his inspector         
		public void SelectGuard(int id)
		{
			if (PlayerPrefs.GetInt(guards.carID.ToString() + "Guard" + id.ToString()) == 3)
			{  // 3=>true , 0=>false

				// Find current car T_guards component
				guards = GameObject.FindFirstObjectByType<T_Guard>();

				// And then activate current selected guard:
				guards.SetGuard(id, true);

				PlayerPrefs.SetInt(guards.carID.ToString() + "GuardID", id);

				tShop.ShowWindow(false);

			}
			else
			{// The current selected guard is not unlocked, We unlock it with reducing money from player total moneys and unlock guard

				// Find current car T_guards component
				guards = GameObject.FindFirstObjectByType<T_Guard>();

				// And then activate current selected guard:
				guards.SetGuard(id, true);

				tShop.carID = guards.carID;
				tShop.itemID = id;
				//			tShop.currentItemPrice.text = tShop.guardPrice [id].ToString();
				tShop.itemType = ItemType.Guard;
				tShop.ShowWindow(true);
			}
		}



		// public function to use in car selection ui manu
		public void UpdateLocks()
		{
			guards = GameObject.FindFirstObjectByType<T_Guard>();

			for (int a = 0; a < tShop.guardPrice.Length; a++)
			{

				tShop.guardLocks[a].SetActive(true);

				if (PlayerPrefs.GetInt(guards.carID.ToString() + "Guard" + a.ToString()) == 3)
				{

					// Deactivate pre unlocked guards icon
					tShop.guardLocks[a].SetActive(false);
				}
			}
		}
	}
}