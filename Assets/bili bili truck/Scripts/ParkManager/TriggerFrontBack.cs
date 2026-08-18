

// This script used for car front and back trigger when parking

using UnityEngine;
using System.Collections;
namespace BilgeKorkmaz
{

	//Public enum for select trigger type in Inspector
	public enum TriggerType { Front, Back }

	public class TriggerFrontBack : MonoBehaviour
	{


		// ParkingManager handler
		public ParkingManager manager;

		// Is front trigger or back?
		public TriggerType triggerType;

		// On parking triggers enter
		void OnTriggerEnter(Collider col)
		{

			// Is front trigger
			if (triggerType == TriggerType.Front)
			{
				if (col.tag == "Front")
				{
					manager.tFront = true;
				}
			}
			else
			{// Or back trigger?
				if (col.tag == "Back")
				{
					manager.tBack = true;
				}

			}


		}
		// On parking triggers exit
		void OnTriggerExit(Collider col)
		{

			if (triggerType == TriggerType.Front)
			{
				if (col.tag == "Front")
				{
					manager.tFront = false;
				}

			}
			else
			{
				if (col.tag == "Back")
				{
					manager.tBack = false;
				}

			}
		}
	}
}