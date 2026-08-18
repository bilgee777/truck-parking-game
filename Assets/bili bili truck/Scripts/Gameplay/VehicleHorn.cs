

using UnityEngine;
using System.Collections;
namespace BilgeKorkmaz
{

	public class VehicleHorn : MonoBehaviour
	{
		AudioSource hornSource;
		public AudioClip[] horn;
		public AudioClip airBrake;

		IEnumerator Start()
		{
			yield return new WaitForEndOfFrame();

			hornSource = GameObject.Find("HornSource").GetComponent<AudioSource>();
		}

		public void HornOn()
		{
			if (!hornSource.isPlaying)
			{
				hornSource.clip = horn[PlayerPrefs.GetInt(PlayerPrefs.GetInt("TruckID").ToString() + "HornID")];
				hornSource.Play();
			}
		}
		public void HornOff()
		{
			hornSource.clip = airBrake;
			hornSource.Play();
		}
	}
}