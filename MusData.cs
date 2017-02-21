using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyOfWpfApplication1
{
    public class MusData
    {
        private string name = "";
        private string alb = "";
        private string time = "";
        public MusData(){}
        public MusData(string name,string alb, string time)
        {
            this.name = name;
            this.alb = alb;
            this.time = time;
        }
        //一般记得初始化，否则可能出问题
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Alb
        {
            get { return alb; }
            set { alb = value; }
        }
        public string Time
        {
            get { return time; }
            set { time = value; }
        }
    }
}
