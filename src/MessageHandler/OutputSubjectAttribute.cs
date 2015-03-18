using System;

namespace MessageHandler
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class OutputSubjectAttribute : Attribute
    {
        public OutputSubjectAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}