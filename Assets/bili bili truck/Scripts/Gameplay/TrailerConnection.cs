

using UnityEngine;
using System.Collections;
namespace BilgeKorkmaz
{

	public class TrailerConnection : MonoBehaviour
	{

		public GameObject cJoint;
		public string connectionName = "Connect_Trigger";
		public AudioSource connectionSound;
		Rigidbody player;
		public Light[] brakeLights, reverseLights, flashLights;

        IEnumerator Start()
        {
            yield return new WaitForSeconds(0.5f);

            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

            if (playerObj == null)
            {
                Debug.LogWarning("TrailerConnection: Player bulunamadı.");
                yield break;
            }

            player = playerObj.GetComponent<Rigidbody>();

            if (!connectionSound)
                connectionSound = GetComponent<AudioSource>();
        }
        void OnTriggerEnter(Collider col)
		{

			if (col.name == "Connect_Trigger")
			{

				GetComponentInParent<CheckpointManager>().NextCheckpoint();

				cJoint.GetComponent<ConfigurableJoint>().connectedBody = player;

				cJoint.transform.parent = player.transform;

				if (connectionSound)
					connectionSound.Play();

				GameObject.FindFirstObjectByType<SmoothFollow>().UpdateCameraMode(CameraMode.WithTrailer);

				cJoint.GetComponent<Trailer>().isActive = true;

				GameObject.FindFirstObjectByType<CameraManager>().canChangeCamera = true;

				GameObject.FindFirstObjectByType<CameraManager>().ActivateCam(0);

				GetComponentInParent<VehicleController>().brakeLights[0].intensity = 0;
				GetComponentInParent<VehicleController>().brakeLights[0] = brakeLights[0];

				GetComponentInParent<VehicleController>().brakeLights[1].intensity = 0;
				GetComponentInParent<VehicleController>().brakeLights[1] = brakeLights[1];

				GetComponentInParent<VehicleController>().reverseLights[0].intensity = 0;
				GetComponentInParent<VehicleController>().reverseLights[0] = reverseLights[0];

				GetComponentInParent<VehicleController>().reverseLights[1].intensity = 0;
				GetComponentInParent<VehicleController>().reverseLights[1] = reverseLights[1];

				col.GetComponentInParent<FlashLight>().flashLights[0].intensity = 0;
				col.GetComponentInParent<FlashLight>().flashLights[0] = flashLights[0];

				col.GetComponentInParent<FlashLight>().flashLights[1].intensity = 0;
				col.GetComponentInParent<FlashLight>().flashLights[1] = flashLights[1];

				Destroy(gameObject);

			}

		}

	}
}