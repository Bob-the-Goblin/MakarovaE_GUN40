using System;
using UnityEngine;

namespace DefaultNamespace
{
	[RequireComponent(typeof(PositionSaver))]
	public class ReplayMover : MonoBehaviour
	{
		private PositionSaver _save;

		private int _index;
		private PositionSaver.Data _prev;
		private float _duration;

		private void Start()
		{
			////todo comment: зачем нужны эти проверки?
			//проверяет наличие сохранения и записей в нём
			if (!TryGetComponent(out _save) || _save.Records.Count == 0)
			{
				Debug.LogError("Records incorrect value", this);
				//todo comment: Для чего выключается этот компонент?
				//чтобы не сломать код при дальнейшем выполнении с null или с отсутсвием объектов в List /не вызывать ошибок
				enabled = false;
			}
		}

		private void Update()
		{
			var curr = _save.Records[_index];
			//todo comment: Что проверяет это условие (с какой целью)? 
			//проверяет больше ли время с активации приложения, чем время объекта.
			//Нужна для того чтобы воиспроизведение можно было наблюдать
			if (Time.time > curr.Time)
			{
				_prev = curr;
				_index++;
				//todo comment: Для чего нужна эта проверка?
				//проверяет закончились ли сохранения, если да - выключает компонент
				if (_index >= _save.Records.Count)
				{
					enabled = false;
					Debug.Log($"<b>{name}</b> finished", this);
				}
			}
			//todo comment: Для чего производятся эти вычисления (как в дальнейшем они применяются)?
			//вычисляется delta, необходимую для создания линии между точками методом Lerp
			var delta = (Time.time - _prev.Time) / (curr.Time - _prev.Time);
			//todo comment: Зачем нужна эта проверка?
			//Проверка на NaN.
			//арифмитические действия с NaN всегда дают NaN, что может в дыльнейшем вызвать ошибки при
			//попытках использовать такие "значения"
			if (float.IsNaN(delta)) delta = 0f;
            //todo comment: Опишите, что происходит в этой строчке так подробно, насколько это возможно
            //происходит "перемещение" объекта по линейной интерполяции, через метод Lerp, который её вычислият.
            //линейная интерполяция  - метод оценки промежуточных значений фун-ции на основе уже известных точек:
            //_prev.Position, curr.Position /создаётся линия между точками
            transform.position = Vector3.Lerp(_prev.Position, curr.Position, delta);
		}
	}
}