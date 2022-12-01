using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOManager.Domain.DomainModel;

namespace TODOManager.Helpers
{
    public static class PriorityHelper
    {

        public static string PriorityToString(Priority priority)
        {
            return priority.ToString();
        }

        public static Priority StringToPriority(string priority)
        {
            Priority data = Priority.NONE;

            switch(priority)
            {
                case "HIGH":
                    data = Priority.HIGH;
                    break;
                case "MEDIUM":
                    data = Priority.MEDIUM;
                    break;
                case "LOW":
                    data = Priority.LOW;
                    break;
                case "NONE":
                    data = Priority.NONE;
                    break;
            }

            return data;
        }
    }
}
