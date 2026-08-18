

using UnityEngine;
using System.Collections;
namespace BilgeKorkmaz
{

	public class ParkingTrigger : MonoBehaviour//Bu script, park alanındaki tetikleyicilerin işlevselliğini sağlar. Her bir tetikleyici, hangi tetikleyici olduğunu belirten bir tNum değişkenine sahiptir ve ParkingManager sınıfının bir örneğine referans tutar. OnTriggerStay ve OnTriggerExit fonksiyonları, oyuncu veya römork tetikleyiciye girdiğinde veya çıktığında tetikleyicinin durumunu günceller.
    {

		
		public int tNum;

		public ParkingManager tManager;//tManager değişkeni, ParkingManager sınıfının bir örneğine referans tutar ve tetikleyicinin durumunu güncellemek için kullanılır


        void OnTriggerStay(Collider col)//OnTriggerStay fonksiyonu, oyuncu veya römork tetikleyiciye girdiğinde tetiklenir ve tetikleyicinin durumunu günceller
        {


			if (col.tag == "Player" || col.tag == "Trailer")//Eğer tetikleyiciye giren nesnenin tag'i "Player" veya "Trailer" ise, tetikleyicinin durumunu günceller
            {

				if (tNum == 1)//Eğer tNum 1 ise, tManager'ın t0 değişkeni true olarak ayarlanır
                    tManager.t0 = true;
                else if (tNum == 2)
					tManager.t1 = true;
				else if (tNum == 3)
					tManager.t2 = true;
				else if (tNum == 4)
					tManager.t3 = true;


			}




		}

		void OnTriggerExit(Collider col)//OnTriggerExit fonksiyonu, oyuncu veya römork tetikleyiciden çıktığında tetiklenir ve tetikleyicinin durumunu günceller
        {


			if (col.tag == "Player" || col.tag == "Trailer")
			{
				if (tNum == 1)
					tManager.t0 = false;
				else if (tNum == 2)
					tManager.t1 = false;
				else if (tNum == 3)
					tManager.t2 = false;
				else if (tNum == 4)
					tManager.t3 = false;



			}
		}
	}
}