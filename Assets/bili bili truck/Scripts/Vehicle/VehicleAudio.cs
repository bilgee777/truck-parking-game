

// This script used for truck audio system
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BilgeKorkmaz
{
    [RequireComponent(typeof(VehicleController))]
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(Rigidbody))]
    public class VehicleAudio : MonoBehaviour
    {
        [Header("Engine Sound")]
        public AudioClip EngineSound;
        public float pitchMultiplier = 1f;
        public float PitchMin = 0.43f;
        public float PitchMax = 1.7f;

        [Header("Reverse Sound")]
        public AudioSource vehicleBackingUp;

        [Header("Crash Sound")]
        public AudioSource crashSound;
        public float crashVelocity = 10f;

        [Header("Garage")]
        public string garageSceneName = "Garage";

        private AudioSource engineAudioSource;
        private VehicleController vehicleController;
        private Rigidbody rb;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            vehicleController = GetComponent<VehicleController>();
            engineAudioSource = GetComponent<AudioSource>();

            if (SceneManager.GetActiveScene().name == garageSceneName)
            {
                if (engineAudioSource != null)
                    engineAudioSource.Stop();

                enabled = false;
                return;
            }

            if (engineAudioSource != null && EngineSound != null)
            {
                engineAudioSource.clip = EngineSound;
                engineAudioSource.loop = true;
                engineAudioSource.playOnAwake = false;
                engineAudioSource.Play();
            }
        }

        void Update()
        {
            if (vehicleController == null || engineAudioSource == null)
                return;

            float pitch = Mathf.Lerp(PitchMin, PitchMax, vehicleController.Revs);
            pitch = Mathf.Min(PitchMax, pitch);

            engineAudioSource.pitch = pitch * pitchMultiplier;
        }

        void OnCollisionEnter(Collision collision)
        {
            if (crashSound == null || rb == null)
                return;

            if (rb.velocity.magnitude > crashVelocity)
            {
                if (!crashSound.isPlaying)
                    crashSound.Play();
            }
        }
    }
}