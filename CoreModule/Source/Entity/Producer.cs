using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Entity
{
    public class Producer
    {
        protected Producer()
        {

        }
        public Producer(string name, string description)
        {
            FullName = name;
            Description = description;
         
        }
        public void Update(string name, string description)
        {
            FullName = name;
            Description = description;
        }
        public void SetImage(string image)
        {
            Image = image;
        }
        public int Id { get;private set; }
        public string FullName { get; protected set; }
        public string Description { get; protected set; }
        public string? Image { get; protected set; }
    }
}
