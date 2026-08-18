using UnityEngine;

namespace BilgeKorkmaz
{
    public class CharacterVehicleStats : MonoBehaviour
    {
        void Start()
        {
            VehicleController controller = GetComponent<VehicleController>();

            if (controller == null)
            {
                Debug.LogWarning("CharacterVehicleStats: VehicleController bulunamadý.");
                return;
            }

            int characterID = PlayerPrefs.GetInt("CharacterID", 0);

            switch (characterID)
            {
                case 0: // Raven - Balanced
                    controller.enginePower = 1400;
                    controller.brakePower = 1400;
                    controller.maxSteer = 43;
                    controller.maxSpeed = 74;
                    break;

                case 1: // Jack - Steering
                    controller.enginePower = 1300;
                    controller.brakePower = 1400;
                    controller.maxSteer = 55;
                    controller.maxSpeed = 70;
                    break;

                case 2: // Sarah - Brake
                    controller.enginePower = 1350;
                    controller.brakePower = 2000;
                    controller.maxSteer = 43;
                    controller.maxSpeed = 72;
                    break;

                case 3: // Mike - Power
                    controller.enginePower = 1800;
                    controller.brakePower = 1500;
                    controller.maxSteer = 40;
                    controller.maxSpeed = 76;
                    break;

                case 4: // Emma - Speed
                    controller.enginePower = 1500;
                    controller.brakePower = 1300;
                    controller.maxSteer = 38;
                    controller.maxSpeed = 90;
                    break;
            }

            Debug.Log("Character stats applied. CharacterID: " + characterID);
        }
    }
}