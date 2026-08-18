using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace BilgeKorkmaz
{
    public class ParkingManager : MonoBehaviour
    {
        public bool t1, t2, t3, t0, tFront, tBack;

        public GameObject FinishMenu;
        public GameObject GameCompletedMenu;
        public GameObject TimerCountMen;
        public GameObject Controller;
        public GameObject FailedMenu;

        private bool isFinish;
        private bool FinisheD;
        private bool Score;
        private bool checkingFinish;

        float endTime;

        public Text CountDownText;
        public Text CollistionCountText;

        [HideInInspector] public int CollisionCount;

        public MeshRenderer ParkRenderer;
        public GameObject Helper;

        public AudioSource AlarmSound;

        public GameObject star1, star2, star3;

        public AudioClip clipSucces, clipLost;
        AudioSource As;

        public bool timeLimit;
        public GameObject TimeDownMenu;

        public Text bestTime, currentTime;

        public Text _text;
        public float seconds = 59;
        public float minutes = 0;

        IEnumerator Start()
        {
            if (TimeDownMenu != null)
                TimeDownMenu.SetActive(timeLimit);

            endTime = Time.time + 4;

            if (CountDownText != null)
                CountDownText.text = "3";

            As = gameObject.AddComponent<AudioSource>();
            As.spatialBlend = 0;
            As.playOnAwake = false;
            As.loop = false;

            yield return new WaitForSeconds(.03f);
        }

        void Update()
        {
            if (FinisheD)
                return;

            if (t0 && t1 && t2 && t3 && tFront && tBack)
            {
                isFinish = true;

                if (ParkRenderer != null)
                    ParkRenderer.material.color = Color.green;

                if (TimerCountMen != null)
                    TimerCountMen.SetActive(true);

                if (CountDownText != null)
                    CountDownText.gameObject.SetActive(true);

                int timeLeft = (int)(endTime - Time.time);
                if (timeLeft < 0)
                    timeLeft = 0;

                if (CountDownText != null)
                    CountDownText.text = timeLeft.ToString();

                if (!checkingFinish)
                {
                    checkingFinish = true;
                    StartCoroutine(CheckTimeToFinished());
                }
            }
            else
            {
                isFinish = false;
                checkingFinish = false;

                if (TimerCountMen != null)
                    TimerCountMen.SetActive(false);

                endTime = Time.time + 4;

                if (CountDownText != null)
                    CountDownText.text = "3";

                if (ParkRenderer != null)
                    ParkRenderer.material.color = Color.white;
            }

            if (timeLimit)
                TimeDown();
        }

        IEnumerator CheckTimeToFinished()
        {
            yield return new WaitForSeconds(4f);

            checkingFinish = false;

            if (!isFinish)
                yield break;

            if (!Score)
            {
                Score = true;
                FinisheD = true;

                int earnedStar = 0;

                if (CollisionCount == 0)
                {
                    earnedStar = 3;

                    if (star1 != null) star1.SetActive(true);
                    if (star2 != null) star2.SetActive(true);
                    if (star3 != null) star3.SetActive(true);
                }
                else if (CollisionCount == 1)
                {
                    earnedStar = 2;

                    if (star1 != null) star1.SetActive(true);
                    if (star2 != null) star2.SetActive(true);
                    if (star3 != null) star3.SetActive(false);
                }
                else if (CollisionCount == 2)
                {
                    earnedStar = 1;

                    if (star1 != null) star1.SetActive(true);
                    if (star2 != null) star2.SetActive(false);
                    if (star3 != null) star3.SetActive(false);
                }
                else if (CollisionCount >= 3)
                {
                    earnedStar = 0;

                    if (star1 != null) star1.SetActive(false);
                    if (star2 != null) star2.SetActive(false);
                    if (star3 != null) star3.SetActive(false);
                }

                if (earnedStar > 0)
                    PlayerPrefs.SetInt("Star" + PlayerPrefs.GetInt("TruckLevelID"), earnedStar);

                if (As != null)
                {
                    As.clip = earnedStar > 0 ? clipSucces : clipLost;
                    if (As.clip != null)
                        As.Play();
                }

                SaveTime();

                if (bestTime != null)
                    bestTime.text = ReadBestTime();

                if (currentTime != null)
                    currentTime.text = ReadCurrentTime();

                StopPlayer();

                PlayerPrefs.SetInt("TotalPassed", PlayerPrefs.GetInt("TotalPassed") + 1);

                int currentLevel = PlayerPrefs.GetInt("TruckLevelID", 0);
                int openedLevel = PlayerPrefs.GetInt("TruckLevelNum", 1);

                if (currentLevel + 1 == openedLevel && openedLevel < 5)
                    PlayerPrefs.SetInt("TruckLevelNum", openedLevel + 1);

                PlayerPrefs.SetInt("PassedLevels", PlayerPrefs.GetInt("PassedLevels") + 1);
            }

            ShowFinishMenu();
        }

        void ShowFinishMenu()
        {
            int currentLevel = PlayerPrefs.GetInt("TruckLevelID", 0);

            if (TimerCountMen != null)
                TimerCountMen.SetActive(false);

            if (CountDownText != null)
                CountDownText.gameObject.SetActive(false);

            if (Controller != null)
                Controller.SetActive(false);

            if (currentLevel >= 4 && GameCompletedMenu != null)
            {
                GameCompletedMenu.SetActive(true);
            }
            else
            {
                if (FinishMenu != null)
                    FinishMenu.SetActive(true);
            }
        }

        void SaveTime()
        {
            string levelID = PlayerPrefs.GetInt("TruckLevelID").ToString();

            float oldMin = PlayerPrefs.GetFloat("TruckMinutes" + levelID, -1);
            float oldSec = PlayerPrefs.GetFloat("TruckSeconds" + levelID, -1);

            if (oldMin < 0 || minutes > oldMin || (minutes == oldMin && seconds > oldSec))
            {
                PlayerPrefs.SetFloat("TruckMinutes" + levelID, minutes);
                PlayerPrefs.SetFloat("TruckSeconds" + levelID, seconds);
            }
        }

        void StopPlayer()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player == null)
                return;

            Rigidbody rb = player.GetComponent<Rigidbody>();

            if (rb != null)
                rb.isKinematic = true;
        }

        public void TimeFailed()
        {
            if (As != null)
            {
                As.clip = clipLost;
                if (As.clip != null)
                    As.Play();
            }

            if (FailedMenu != null)
                FailedMenu.SetActive(true);

            PlayerPrefs.SetInt("TotalFailed", PlayerPrefs.GetInt("TotalFailed") + 1);

            if (TimerCountMen != null)
                TimerCountMen.SetActive(false);

            if (CountDownText != null)
                CountDownText.gameObject.SetActive(false);

            if (Controller != null)
                Controller.SetActive(false);

            enabled = false;

            if (_text != null)
                _text.text = "00:00";

            StopPlayer();
        }

        public void TimeDown()
        {
            if (seconds <= 0)
            {
                seconds = 59;

                if (minutes >= 1)
                    minutes--;
                else
                {
                    minutes = 0;
                    seconds = 0;

                    if (_text != null)
                        _text.text = "00:00";
                }
            }
            else
            {
                seconds -= Time.deltaTime;

                string min = minutes < 10 ? "0" + minutes.ToString("f0") : minutes.ToString("f0");
                string sec = seconds < 10 ? "0" + Mathf.FloorToInt(seconds).ToString() : Mathf.FloorToInt(seconds).ToString();

                if (_text != null)
                    _text.text = min + ":" + sec;
            }

            if (minutes <= 0 && seconds <= 0)
                TimeFailed();
        }

        string ReadBestTime()
        {
            string levelID = PlayerPrefs.GetInt("TruckLevelID").ToString();

            float min = PlayerPrefs.GetFloat("TruckMinutes" + levelID);
            float secn = PlayerPrefs.GetFloat("TruckSeconds" + levelID);

            string minS = min < 10 ? "0" + min.ToString("f0") : min.ToString("f0");
            string secS = secn < 10 ? "0" + Mathf.FloorToInt(secn).ToString() : Mathf.FloorToInt(secn).ToString();

            return "Best Time : " + minS + ":" + secS;
        }

        string ReadCurrentTime()
        {
            string min = minutes < 10 ? "0" + minutes.ToString("f0") : minutes.ToString("f0");
            string sec = seconds < 10 ? "0" + Mathf.FloorToInt(seconds).ToString() : Mathf.FloorToInt(seconds).ToString();

            return "Current Time : " + min + ":" + sec;
        }
    }
}