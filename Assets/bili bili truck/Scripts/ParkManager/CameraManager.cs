

using UnityEngine;
using System.Collections;
namespace BilgeKorkmaz
{

    public class CameraManager : MonoBehaviour
    {

        [HideInInspector] public Camera[] Cams;

        [Header("Enter the camera name")]
        [Space(7)]
        public string[] camerasName;//Inspector'da kamera isimlerini gireceğimiz dizi

        [HideInInspector] public bool canChangeCamera;//Camera değiştirebilme izni

        IEnumerator Start()//Start fonksiyonu IEnumerator olarak tanımlanır çünkü kameraların aktif edilmesi için bir frame beklememiz gerekiyor
        {

            Cams = new Camera[camerasName.Length];//Cams dizisi, camerasName dizisi kadar eleman içerecek şekilde tanımlanır

            canChangeCamera = true;//Camera değiştirebilme izni verilir

            yield return new WaitForEndOfFrame();//Bir frame beklenir

            for (int a = 0; a < Cams.Length; a++)//Cams dizisinin her bir elemanı için, camerasName dizisindeki isimle eşleşen GameObject bulunur ve bu GameObject'in Camera bileşeni Cams dizisine atanır
            {
                Cams[a] = GameObject.Find(camerasName[a]).GetComponent<Camera>();//Cams dizisinin a. elemanı, camerasName dizisinin a. elemanının ismiyle eşleşen GameObject'in Camera bileşeni olarak atanır
            }

            for (int a = 0; a < Cams.Length; a++)//Cams dizisinin her bir elemanı için, eğer a. eleman CameraID ile eşleşmiyorsa, bu Camera bileşeni devre dışı bırakılır ve CameraID ile eşleşen Camera bileşeni etkinleştirilir
            {
                Cams[a].enabled = (false);//Cams dizisinin a. elemanı devre dışı bırakılır
            }
            Cams[CameraID].enabled = (true);//CameraID ile eşleşen Camera bileşeni etkinleştirilir
        }



        int CameraID;//CameraID değişkeni, hangi kameranın aktif olduğunu belirler


        public void NextCam()//NextCam fonksiyonu, eğer canChangeCamera true ise, CameraID'yi bir sonraki kameraya geçirir ve tüm kameraları devre dışı bırakıp sadece CameraID ile eşleşen kamerayı etkinleştirir
        {
            if (canChangeCamera)//Eğer canChangeCamera true ise, CameraID'yi bir sonraki kameraya geçirir ve tüm kameraları devre dışı bırakıp sadece CameraID ile eşleşen kamerayı etkinleştirir
            {
                if (CameraID < Cams.Length - 1)//Eğer CameraID, Cams dizisinin son elemanından küçükse, CameraID bir sonraki kameraya geçirilir
                    CameraID++;//CameraID bir sonraki kameraya geçirilir
                else
                    CameraID = 0;//Eğer CameraID, Cams dizisinin son elemanına eşitse, CameraID sıfırlanır ve ilk kameraya geçilir

                for (int a = 0; a < Cams.Length; a++)//Cams dizisinin her bir elemanı için, eğer a. eleman CameraID ile eşleşmiyorsa, bu Camera bileşeni devre dışı bırakılır ve CameraID ile eşleşen Camera bileşeni etkinleştirilir
                    Cams[a].enabled = (false);//Cams dizisinin a. elemanı devre dışı bırakılır

                Cams[CameraID].enabled = (true);//CameraID ile eşleşen Camera bileşeni etkinleştirilir

            }

        }


        public void ActivateCam(int index)//ActivateCam fonksiyonu, eğer canChangeCamera true ise, tüm kameraları devre dışı bırakıp sadece index ile eşleşen kamerayı etkinleştirir
        {
            if (canChangeCamera)//Eğer canChangeCamera true ise, tüm kameraları devre dışı bırakıp sadece index ile eşleşen kamerayı etkinleştirir
            {

                for (int a = 0; a < Cams.Length; a++)//Cams dizisinin her bir elemanı için, eğer a. eleman index ile eşleşmiyorsa, bu Camera bileşeni devre dışı bırakılır ve index ile eşle��en Camera bileşeni etkinleştirilir
                    Cams[a].enabled = false;//Cams dizisinin a. elemanı devre dışı bırakılır

                Cams[index].enabled = true;//index ile eşleşen Camera bileşeni etkinleştirilir

            }
        }
    }
}