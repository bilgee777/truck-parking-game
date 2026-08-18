

using UnityEngine;

namespace BilgeKorkmaz
{
    public class LevelLoader : MonoBehaviour
    {
        public GameObject[] Levels;

        void Start()
        {
            for (int a = 0; a < Levels.Length; a++)
                Levels[a].SetActive(false);

            int levelID = PlayerPrefs.GetInt("TruckLevelID", 0);

            if (levelID < 0 || levelID >= Levels.Length)
            {
                Debug.LogWarning("Geçersiz TruckLevelID: " + levelID + " yerine 0 açıldı.");
                levelID = 0;
                PlayerPrefs.SetInt("TruckLevelID", 0);
            }

            Levels[levelID].SetActive(true);

            Debug.Log("Aktif level: Level" + levelID);
        }
    }
}