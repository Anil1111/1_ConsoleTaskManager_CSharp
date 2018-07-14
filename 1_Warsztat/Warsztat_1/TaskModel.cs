using System;
using System.Collections.Generic;
using System.Text;

namespace Warsztat_1
{
	public class TaskModel
	{
		public string TaskDescription { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public bool? IsAllDay { get; set; }
		public bool? IsImportant { get; set; }


		public TaskModel(string taskDescription, DateTime startDate, DateTime? endDate, bool? isAllDay, bool? isImportant)
		{
			TaskDescription = taskDescription;
			StartDate = startDate;
			EndDate = endDate;
			IsAllDay = isAllDay;
			IsImportant = isImportant;
		}

	}
}
