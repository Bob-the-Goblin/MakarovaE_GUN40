using Palmmedia.ReportGenerator.Core.Common;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace DefaultNamespace
{
	public class PositionSaver : MonoBehaviour
	{
		[Serializable]
		public struct Data
		{
			public Vector3 Position;
			public float Time;
		}

		[Tooltip("Create File"), ReadOnly]
		public TextAsset _json;

		[field: SerializeField, HideInInspector]
		public List<Data> Records { get; private set; }


		private void Awake()
		{
			//todo comment: Что будет, если в теле этого условия не сделать выход из метода?
			//метод Awake продолжит выполнение, а поскольку в следуюая строка использует _json /с null/, будет ошибка 
			if (_json == null)
			{
				gameObject.SetActive(false);
				Debug.LogError("Please, create TextAsset and add in field _json");
				return;
			}

			JsonUtility.FromJsonOverwrite(_json.text, this);
			//todo comment: Для чего нужна эта проверка (что она позволяет избежать)?
			//Проверят наличие ссылки в Records, и позволяет исбежать ошибок с Null
			if (Records == null)
				Debug.LogError(("is null"));
				Records = new List<Data>(10);
		}

		private void OnDrawGizmos()
		{
			//todo comment: Зачем нужны эти проверки (что они позволляют избежать)?
			//проверяют наличие самого Records и наличия в нём объектов, так же позволяет избегать ошибок с null 
			if (Records == null || Records.Count == 0) return;
			var data = Records;
			var prev = data[0].Position;
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(prev, 0.3f);
			//todo comment: Почему итерация начинается не с нулевого элемента?
			//операции с нулевым элементом производяться выше - в цикле они не нужны.
			for (int i = 1; i < data.Count; i++)
			{
				var curr = data[i].Position;
				Gizmos.DrawWireSphere(curr, 0.3f);
				Gizmos.DrawLine(prev, curr);
				prev = curr;
			}
		}

#if UNITY_EDITOR
		[ContextMenu("Create File")]
		private void CreateFile()
		{
			//todo comment: Что происходит в этой строке?
			//происходит создание файла по пути, который комбинируется из пути до папки Assets и самого названия файла
			var stream = File.Create(Path.Combine(Application.dataPath, "Path.txt"));
			//todo comment: Подумайте для чего нужна эта строка? (а потом проверьте догадку, закомментировав) 
			//освобождает неуправляемые ресурсы
			//stream.Dispose();
			UnityEditor.AssetDatabase.Refresh();
			//В Unity можно искать объекты по их типу, для этого используется префикс "t:"
			//После нахождения, Юнити возвращает массив гуидов (которые в мета-файлах задаются, например)
			var guids = UnityEditor.AssetDatabase.FindAssets("t:TextAsset");
			foreach (var guid in guids)
			{
				//Этой командой можно получить путь к ассету через его гуид
				var path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
				//Этой командой можно загрузить сам ассет
				var asset = UnityEditor.AssetDatabase.LoadAssetAtPath<TextAsset>(path);
				//todo comment: Для чего нужны эти проверки?
				//для проверки наличия ссылки в asset и корректного названия /тот ли ассет/
				if (asset != null && asset.name == "Path")
				{
					_json = asset;
					UnityEditor.EditorUtility.SetDirty(this);
					UnityEditor.AssetDatabase.SaveAssets();
					UnityEditor.AssetDatabase.Refresh();
					//todo comment: Почему мы здесь выходим, а не продолжаем итерироваться?
					//потому что нужные нам процессы выполнены и продолжение не имеет необходимости
					return;
				}
			}
		}

		private void OnDestroy()
		{

			

            string path = Path.Combine(Application.dataPath, "Path.txt");
			StreamWriter writer = new StreamWriter(path);
			/*foreach (var rec in Records)
			{
				string str = JsonUtility.ToJson(rec);
				Debug.Log(str);
				writer.WriteLine(str);
			}*/
			
			var str = JsonUtility.ToJson(Records);
			Debug.Log(str);
			writer.WriteLine(str);
			


            writer.Close();
			

        }
#endif
	}

}
