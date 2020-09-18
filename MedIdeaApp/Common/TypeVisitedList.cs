using System;
using System.Collections.Generic;
using System.Text;

namespace MedIdeaApp.Common
{
    class TypeVisitedList : List<string>
    {
        public TypeVisitedList()
        {
            Add("Первичный");
            Add("Повторный");
        }
    }
}
