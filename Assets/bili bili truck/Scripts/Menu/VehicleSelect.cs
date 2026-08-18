

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace BilgeKorkmaz
{
    public class VehicleSelect : MonoBehaviour
    {
        [Header("Vehicle Selection")]
        public GameObject[] vehicles;
        public Transform point;
        public int ID;

        [Header("Scene")]
        public GameObject Loading;
        public string levelName = "BiliBili_Truck";

        [Header("Menus")]
        public GameObject mainMenu;
        public GameObject customizeMenu;
        public GameObject selectButtons;

        [Header("Character Info UI")]
        public Text characterNameText;
        public Text characterInfoText;

        private GameObject currentVehicle;

        private string[] characterNames =
        {
            "Raven",
            "Jack",
            "Sarah",
            "Mike",
            "Emma"
        };

        private string[] characterInfos =
        {
            "Balanced driving. Good for beginners.",
            "Best steering control. Good for narrow parking areas.",
            "Strong brake system. Stops faster and safer.",
            "Balanced performance. Stable and reliable driving.",
            "Highest speed. Good for time-based missions."
        };

        void Start()
        {
           
            ID = PlayerPrefs.GetInt("TruckID", 0);
            ID = Mathf.Clamp(ID, 0, vehicles.Length - 1);

            SpawnVehicle();
            UpdateCharacterInfo();
        }

        void SpawnVehicle()
        {
            if (currentVehicle != null)
                Destroy(currentVehicle);

            GameObject oldPlayer = GameObject.FindGameObjectWithTag("Player");

            if (oldPlayer != null)
                Destroy(oldPlayer);

            currentVehicle = Instantiate(vehicles[ID], point.position, point.rotation);

            currentVehicle.tag = "Player";
        }

        public void NextCar()
        {
            if (ID < vehicles.Length - 1)
                ID++;
            else
                ID = 0;

            PlayerPrefs.SetInt("TruckID", ID);

            SpawnVehicle();
            UpdateCharacterInfo();
        }

        public void PrevCar()
        {
            if (ID > 0)
                ID--;
            else
                ID = vehicles.Length - 1;

            PlayerPrefs.SetInt("TruckID", ID);

            SpawnVehicle();
            UpdateCharacterInfo();
        }

        public void SelectCar()
        {
            PlayerPrefs.SetInt("TruckID", ID);

            if (Loading != null)
                Loading.SetActive(true);

            SceneManager.LoadSceneAsync(levelName);
        }

        public void OpenCustomizeMenu()
        {
            if (mainMenu != null)
                mainMenu.SetActive(false);

            if (customizeMenu != null)
                customizeMenu.SetActive(true);

            if (selectButtons != null)
                selectButtons.SetActive(false);
        }

        public void GoToCustomize(GameObject target)
        {
            if (target != null)
                target.SetActive(false);
        }

        void UpdateCharacterInfo()
        {
            int characterID = PlayerPrefs.GetInt("CharacterID", 0);
            characterID = Mathf.Clamp(characterID, 0, characterNames.Length - 1);

            if (characterNameText != null)
                characterNameText.text = characterNames[characterID];

            if (characterInfoText != null)
                characterInfoText.text = characterInfos[characterID];
        }
    }
}