using System.Collections.Generic;

namespace App.Domain
{
    public class Group
    {
        public Group()
        {
            Rights = new List<Right>();
        }

        public string Name { get; set; }
        public List<Right> Rights { get; set; }

    }
}