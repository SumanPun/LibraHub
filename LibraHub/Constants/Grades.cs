using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraHub.Constants
{
    public static class Grades
    {
        public static List<string> Value = new(){
            "Grade 1",
            "Grade 2",
            "Grade 3",
            "Grade 4",
            "Grade 5",
            "Grade 6",
            "Grade 7",
            "Grade 8",
            "Grade 9",
            "Grade 10"
        };
        public static SelectList GetSelectLists(string selectedValue) => new SelectList(Value, selectedValue);
    }
}
