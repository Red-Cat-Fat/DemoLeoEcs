using System;

namespace Services
{
	public class PauseService
	{
		public event Action<bool> ChangeStateEvent;

		private bool _currentValue;

		public PauseService(bool startState)
		{
			_currentValue = startState;
		}

		public void SetPause()
		{
			SetState(true);
		}

		public void ResetPause()
		{
			SetState(false);
		}

		private void SetState(bool value)
		{
			if (_currentValue != value)
			{
				_currentValue = value;
				ChangeStateEvent?.Invoke(value);
			}
		}
	}
}