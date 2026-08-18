using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace BilgeKorkmaz
{
    public class PauseMen : MonoBehaviour
    {
        [Header("Pause Menu Manager")]
        public GameObject PauseMenu;

        public Text LoadingText;

        public string garageName = "Garage";

        void Start()
        {
            Time.timeScale = PlayerPrefs.GetFloat("TimeScale", 1f);
        }

        public void Pause()
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }

        public void Resume()
        {
            PauseMenu.SetActive(false);
            Time.timeScale = PlayerPrefs.GetFloat("TimeScale", 1f);
        }

        public void Retry()
        {
            if (LoadingText != null)
                LoadingText.text = "Please Wait...";

            PauseMenu.SetActive(false);
            Time.timeScale = PlayerPrefs.GetFloat("TimeScale", 1f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void Exit()
        {
            if (LoadingText != null)
            {
                LoadingText.gameObject.SetActive(true);
                LoadingText.text = "Please Wait...";
            }

            Time.timeScale = PlayerPrefs.GetFloat("TimeScale", 1f);
            SceneManager.LoadScene(garageName);
        }

        public void SetTrue(GameObject target)
        {
            if (target != null)
                target.SetActive(true);
        }

        public void SetFalse(GameObject target)
        {
            if (target != null)
                target.SetActive(false);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Pause();
        }
    }
}