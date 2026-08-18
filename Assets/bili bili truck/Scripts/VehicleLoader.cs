

using UnityEngine;

namespace BilgeKorkmaz
{
    public class VehicleLoader : MonoBehaviour
    {
        public GameObject[] Vehicle;

        void Start()
        {
            
            int id = PlayerPrefs.GetInt("TruckID", 0);

            if (Vehicle == null || Vehicle.Length == 0)
            {
                Debug.LogError("Vehicle listesi boş!");
                return;
            }

            if (id < 0 || id >= Vehicle.Length)
            {
                Debug.LogWarning("Geçersiz TruckID: " + id + " yerine 0 kullanıldı.");
                id = 0;
                PlayerPrefs.SetInt("TruckID", 0);
            }

            if (!GameObject.FindGameObjectWithTag("Player"))
            {
                GameObject truck = Instantiate(Vehicle[id], transform.position, transform.rotation);
                truck.tag = "Player";
            }
        }
    }
}