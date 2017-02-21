using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyOfWpfApplication1
{
    public class AllData
    {
        private string name = "";
        private string mus = "";
        private string alb = "";
        private string time = "";
        public AllData(){}
        public AllData(string name, string mus, string alb, string time)
        {
            this.name = name;
            this.mus = mus;
            this.alb = alb;
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
