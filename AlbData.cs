using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyOfWpfApplication1
{
    public class AlbData
    {
        private string name = "";
        private string mus = "";
        private string time = "";
        public AlbData() { }
        public AlbData(string name, string mus,string time)
        {
            this.name = name;
            this.mus = mus;
            this.time = time;
        }
        //一般记得初始化，否则可能出问题
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Mus
        {
            get { return mus; }
            set { mus = value; }
        }
        public string Time
        {
            get { return time; }
            set { time = value; }
        }
    }
}
