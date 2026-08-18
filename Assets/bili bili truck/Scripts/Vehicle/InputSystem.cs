

using UnityEngine;
using System.Collections;

namespace BilgeKorkmaz
{
    public class InputSystem : MonoBehaviour
    {
        VehicleController controller;//arabayı süren scripti tanımladım.
        CameraManager camManager;//kamerayı değiştiren scripti tanımladım.

        float motorInput;//motor gücü girişi.
        float steerInput;//direksiyon girişi.
        bool handBrake;// el freni girişi.
        bool reversing;//geri vites durumu.

       
     

        IEnumerator Start()
        {
            camManager = GetComponent<CameraManager>();//CameraManager scriptini alıyorum.

            yield return new WaitForEndOfFrame();//Bir frame bekleyerek sahnedeki tüm nesnelerin yüklenmesini sağlıyorum.

            controller = GameObject.FindFirstObjectByType<VehicleController>();//VehicleController scriptini sahnede bulup atıyorum.
        }

        void Update()//Her frame'de oyuncu girişi kontrol ediliyor.
        {
            if (controller == null)//Eğer controller bulunamazsa, girişi işleme ve hata vermemesi için fonksiyondan çıkıyorum.
                return;

            float vertical = Input.GetAxis("Vertical");//Dikey eksen girişi (ileri/geri) alınıyor.
            float horizontal = Input.GetAxis("Horizontal");//Yatay eksen girişi (sağ/sol) alınıyor.

            steerInput = horizontal;//Direksiyon girişi yatay eksen değerine atanıyor.

            if (vertical > 0.1f)//Eğer dikey eksen girişi ileri yönde ise, motor gücü pozitif olarak atanıyor.
                motorInput = reversing ? -1f : 1f;//Geri viteste ise motor gücü negatif, değilse pozitif oluyor.
            else
                motorInput = 0f;//Dikey eksen girişi ileri yönde değilse, motor gücü sıfırlanıyor.

            handBrake = Input.GetKey(KeyCode.Space) || vertical < -0.1f;//El freni, boşluk tuşuna basıldığında veya dikey eksen geri yönde olduğunda aktif oluyor.

            if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.JoystickButton1))//R tuşuna veya oyun kumandasında belirli bir butona basıldığında geri vites durumu değiştiriliyor.
            {
                reversing = !reversing;//Geri vites durumu tersine çevriliyor.

            }

            if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.JoystickButton3))//C tuşuna veya oyun kumandasında belirli bir butona basıldığında kamera değiştiriliyor.
            {
                if (camManager != null)//CameraManager scripti atanmışsa, kamera değiştirme fonksiyonunu çağırıyorum.
                    camManager.NextCam();//Kamera değiştirme fonksiyonu çağırılıyor.
            }

            controller.Move(motorInput, steerInput, handBrake);//VehicleController scriptine motor gücü, direksiyon girişi ve el freni durumunu gönderiyorum.
        }
    }
}