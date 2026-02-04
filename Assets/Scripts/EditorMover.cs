using UnityEngine;

namespace DefaultNamespace
{
	
	[RequireComponent(typeof(PositionSaver))]
	public class EditorMover : MonoBehaviour
	{
		private PositionSaver _save;
		private float _currentDelay;

        //todo comment: Что произойдёт, если _delay > _duration?
        // Если задержка будет больше, чем продолжительность, тогда в кадре не будет происходить никаких изменений,
        // так как они просто будут задержаны

        [Range(0.2f, 1f)]
        private float _delay = 0.5f;
        [Min(0.2f)]
        private float _duration = 5f;

		private void Start()
		{
			//todo comment: Почему этот поиск производится здесь, а не в начале метода Update?
			//Метод update производиться переодически, а нам достаточно произвести поиск только один раз
			_save = GetComponent<PositionSaver>();
			_save.Records.Clear();
            if (!(_duration > _delay))
            {
                _duration = _delay * 5;
            }
        }

		private void Update()
		{
			_duration -= Time.deltaTime;
			if (_duration <= 0f)
			{
				enabled = false;
				Debug.Log($"<b>{name}</b> finished", this);
				return;
			}
			
			//todo comment: Почему не написать (_delay -= Time.deltaTime;) по аналогии с полем _duration?
			//вводиться дополнительная переменная, чтобы в случае, если задержка будет слишком маленькой,
			//увеличить её до необходимого значения /позволяет нам контролировать скорость сохранений
			_currentDelay -= Time.deltaTime;
			if (_currentDelay <= 0f)
			{
				_currentDelay = _delay;
				_save.Records.Add(new PositionSaver.Data
				{
					Position = transform.position,
					//todo comment: Для чего сохраняется значение игрового времени?
					//чтобы использовать его при воспроизведении
					Time = Time.time,
				});
			}
		}
	}
}