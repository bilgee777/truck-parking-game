//Oyuncunun belirlenen güzergâh üzerinde ilerlemesini sağlamak amacıyla kontrol noktası (checkpoint) sistemi geliştirilmiştir. Checkpoint nesneleri tetikleyici (trigger) collider kullanılarak oluşturulmuş, oyuncu aracı kontrol noktasına ulaştığında bir sonraki hedef otomatik olarak aktif hale getirilmiştir. Böylece oyuncunun park alanına ulaşana kadar belirli bir rota izlemesi sağlanmıştır.

using UnityEngine;
using System.Collections;
namespace BilgeKorkmaz
{
	
	public class Checkpoint : MonoBehaviour
	{

		public float maxSize = 4f, minSize = 1.4f, delayTime = 0.3f, speed = 7f;

		float sizeTemp;

		bool state;

		Transform trans;

		CheckpointManager man;

		void Awake()
		{
			man = GetComponentInParent<CheckpointManager>();
		}

		void Start()
		{

			trans = GetComponent<Transform>();

			sizeTemp = trans.localScale.y;

			trans.localScale = new Vector3(trans.localScale.x,
				Mathf.Lerp(trans.localScale.y, sizeTemp, Time.deltaTime * speed), trans.localScale.z);

			StartCoroutine("ScaleChange");

		}

		IEnumerator ScaleChange()
		{
			while (true)
			{
				yield return new WaitForSeconds(delayTime);
				state = !state;
				if (state)
					sizeTemp = maxSize;
				else
					sizeTemp = minSize;
			}
		}

		void Update()
		{

			trans.localScale = new Vector3(trans.localScale.x,
				Mathf.Lerp(trans.localScale.y, sizeTemp, Time.deltaTime * speed), trans.localScale.z);
		}



		void OnTriggerEnter(Collider col)
		{

			if (col.CompareTag("Player"))
			{

				man.NextCheckpoint();

				gameObject.SetActive(false);

			}

		}
	}





}