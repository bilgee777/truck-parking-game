
// This script used for color picking system in game menu(garage)

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace BilgeKorkmaz
{

	public class ColorPicker : MonoBehaviour
	{

		// List of the colors
		public Color[] Colors;

		// Public function for changing color buttons
		public void SetColor(int id)
		{

			PlayerPrefs.SetInt("TruckColor" + PlayerPrefs.GetInt("TruckID").ToString(), id);

			GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<ColorLoader>().mat.color = Colors[id];

		}
	}
}