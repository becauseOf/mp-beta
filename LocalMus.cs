using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyOfWpfApplication1
{
    public class LocalMus
    {
        private string name = "";
        private string path = "";
        public LocalMus(){}
        //一般记得初始化，否则可能出问题
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Path
        {
            get { return path; }
            set { path = value; }
        }
    }
}
