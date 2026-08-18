

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace BilgeKorkmaz
{

	public enum ItemType
	{

		Ring, Horn, Guard, Roof, Exhaust

	}

	public class T_Shop : MonoBehaviour
	{

		public GameObject shopWindow;

		[HideInInspector] public int carID, itemID;

		[HideInInspector] public ItemType itemType;


		[Header("Horns")]
		public int[] hornPrice;
		public GameObject[] hornLocks;
		public Text[] hornPriceTexts;

		public AudioSource boxSound;

		[Header("Rings")]
		public int[] ringPrice;
		public GameObject[] ringLocks;
		public Text[] ringPriceTexts;

		[Header("Guards")]
		public int[] guardPrice;
		public GameObject[] guardLocks;
		public Text[] guardPriceTexts;

		[Header("Roof")]
		public int[] roofPrice;
		public GameObject[] roofLocks;
		public Text[] roofPriceTexts;

		[Header("Exhaust")]
		public int[] exhaustPrice;
		public GameObject[] exhaustLocks;
		public Text[] exhaustPriceTexts;


		T_Money tMoney;

		void Awake()
		{

			tMoney = GameObject.FindFirstObjectByType<T_Money>();

		}

		public void ShowWindow(bool state)
		{
			shopWindow.SetActive(state);

		}

		public void BuyItem()
		{

			if (itemType == ItemType.Ring)
			{
				// Check player money is more than ring price
				if (tMoney.GetMoney() >= ringPrice[itemID])
				{

					// Reduce current ring price from player total money
					tMoney.ChangeMoney(-ringPrice[itemID]);

					// Open current ring
					PlayerPrefs.SetInt(carID.ToString() + "Ring" + itemID.ToString(), 3);// 3=>true , 0=>false

					PlayerPrefs.SetInt(carID.ToString() + "RingID", itemID);

					//Deactivate current lock icon
					ringLocks[itemID].SetActive(false);

					// play item buy sound clip
					tMoney.PlayAudio(0);

					boxSound.Play();

				}
				else
				{
					// play item buy error sound clip (becouse money is not enough
					tMoney.PlayAudio(1);

					// Display shop offer window
					tMoney.showOfferWindow.SetActive(true);
				}

			}

			if (itemType == ItemType.Guard)
			{
				// Check player money is more than guard price
				if (tMoney.GetMoney() >= guardPrice[itemID])
				{

					// Reduce current guard price from player total money
					tMoney.ChangeMoney(-guardPrice[itemID]);

					// Open current guard
					PlayerPrefs.SetInt(carID.ToString() + "Guard" + itemID.ToString(), 3);// 3=>true , 0=>false

					PlayerPrefs.SetInt(carID.ToString() + "GuardID", itemID);

					//Deactivate current lock icon
					guardLocks[itemID].SetActive(false);

					// play item buy sound clip
					tMoney.PlayAudio(0);

					boxSound.Play();

				}
				else
				{
					// play item buy error sound clip (becouse money is not enough
					tMoney.PlayAudio(1);

					// Display shop offer window
					tMoney.showOfferWindow.SetActive(true);
				}

			}

			if (itemType == ItemType.Roof)
			{
				// Check player money is more than roof price
				if (tMoney.GetMoney() >= roofPrice[itemID])
				{

					// Reduce current roof price from player total money
					tMoney.ChangeMoney(-roofPrice[itemID]);

					// Open current roof
					PlayerPrefs.SetInt(carID.ToString() + "Roof" + itemID.ToString(), 3);// 3=>true , 0=>false

					PlayerPrefs.SetInt(carID.ToString() + "RoofID", itemID);

					//Deactivate current lock icon
					roofLocks[itemID].SetActive(false);

					// play item buy sound clip
					tMoney.PlayAudio(0);

					boxSound.Play();

				}
				else
				{
					// play item buy error sound clip (becouse money is not enough
					tMoney.PlayAudio(1);

					// Display shop offer window
					tMoney.showOfferWindow.SetActive(true);
				}

			}

			if (itemType == ItemType.Exhaust)
			{
				// Check player money is more than exhaust price
				if (tMoney.GetMoney() >= exhaustPrice[itemID])
				{

					// Reduce current exhaust price from player total money
					tMoney.ChangeMoney(-exhaustPrice[itemID]);

					// Open current exhaust
					PlayerPrefs.SetInt(carID.ToString() + "Exhaust" + itemID.ToString(), 3);// 3=>true , 0=>false

					PlayerPrefs.SetInt(carID.ToString() + "ExhaustID", itemID);

					//Deactivate current lock icon
					exhaustLocks[itemID].SetActive(false);

					// play item buy sound clip
					tMoney.PlayAudio(0);

					boxSound.Play();

				}
				else
				{
					// play item buy error sound clip (becouse money is not enough
					tMoney.PlayAudio(1);

					// Display shop offer window
					tMoney.showOfferWindow.SetActive(true);
				}

			}

			if (itemType == ItemType.Horn)
			{
				// Check player money is more than horn price
				if (tMoney.GetMoney() >= hornPrice[itemID])
				{

					// Reduce current horn price from player total money
					tMoney.ChangeMoney(-hornPrice[itemID]);

					// Open current horn
					PlayerPrefs.SetInt(carID.ToString() + "Horn" + itemID.ToString(), 3);// 3=>true , 0=>false

					PlayerPrefs.SetInt(carID.ToString() + "HornID", itemID);

					//Deactivate current lock icon
					hornLocks[itemID].SetActive(false);

					// play item buy sound clip
					tMoney.PlayAudio(0);

					boxSound.Play();

				}
				else
				{
					// play item buy error sound clip (becouse money is not enough
					tMoney.PlayAudio(1);

					// Display shop offer window
					tMoney.showOfferWindow.SetActive(true);
				}

			}



			shopWindow.SetActive(false);

		}

	}
}