

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BilgeKorkmaz
{
    public class Submit : MonoBehaviour
    {
        public GameObject lockJack;
        public GameObject lockSarah;
        public GameObject lockMike;
        public GameObject lockEmma;

        public GameObject Loading;

        public Image[] avatarImage;
        public Sprite[] avatars;

        public InputField nameInput;
        public Text statusTXT;

        public string garageSceneName = "Garage";

        public GameObject skipButton;

        void Start()
        {
            
            if (PlayerPrefs.GetInt("StartMenu") == 3)
                LoadLevel(garageSceneName);
            else
                skipButton.SetActive(false);

            nameInput.text = PlayerPrefs.GetString("PlayerName");

            if (PlayerPrefs.HasKey("AvatarID"))
                avatarImage[0].sprite = avatars[PlayerPrefs.GetInt("AvatarID")];

            int level = PlayerPrefs.GetInt("TruckLevelNum", 1);

            lockJack.SetActive(level < 2);
            lockSarah.SetActive(level < 3);
            lockMike.SetActive(level < 4);
            lockEmma.SetActive(level < 5);

        }

        public void Exit()
        {
            Application.Quit();
        }

        public void SetTrue(GameObject target)
        {
            target.SetActive(true);
        }

        public void SetFalse(GameObject target)
        {
            target.SetActive(false);
        }

        public void ToggleObject(GameObject target)
        {
            target.SetActive(!target.activeSelf);
        }

        public void LoadLevel(string name)
        {
            PlayerPrefs.SetInt("StartMenu", 3);
            Loading.SetActive(true);
            SceneManager.LoadSceneAsync(name);
        }

        public void OpenURL(string val)
        {
            Application.OpenURL(val);
        }

        public void SelectAvatar(int index)
        {
            int level = PlayerPrefs.GetInt("TruckLevelNum", 1);

            if (index >= level)
            {
                statusTXT.gameObject.SetActive(true);
                statusTXT.text = "Character locked. Complete more levels.";
                return;
            }

            PlayerPrefs.SetInt("AvatarID", index);
            PlayerPrefs.SetInt("CharacterID", index);

            avatarImage[0].sprite = avatars[index];

            statusTXT.gameObject.SetActive(false);
        }

        public void SubmitPlayer()
        {
            if (nameInput.text != "")
            {
                PlayerPrefs.SetString("PlayerName", nameInput.text);
                LoadLevel(garageSceneName);
            }
            else
            {
                statusTXT.gameObject.SetActive(true);
                statusTXT.text = "Please enter your name.";
            }
        }
    }
}