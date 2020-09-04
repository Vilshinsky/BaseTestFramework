using System;

namespace L1.Domain.CleanUp
{
	public class CleanupQueue
	{
		CleanupAction _head;
		CleanupAction _tail;

		public bool IsEmpty => _head == null;

		public void Enqueue(Action value)
		{
			if (IsEmpty)
			{
				_tail = _head = new CleanupAction { Method = value, Next = null };
			}
			else
			{
				var item = new CleanupAction {Method = value, Next = null};
				_tail.Next = item;
				_tail = item;
			}
		}

		private void Dequeue()
		{
			if (_head == null) throw new InvalidOperationException("Can not dequeue from empty queue.");

			_head = _head.Next;

			if (_head == null) _tail = null;
		}

		public void ExecuteQueue()
		{
			while (!IsEmpty)
			{
				_head.Method();
				Dequeue();
			}
		}
	}
}