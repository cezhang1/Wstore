using LE_STORE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lenovo_store
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FormControl FC = new FormControl();
            LeStore LS = new LeStore();
            TestTool TT=new TestTool();
            LS.LaunchLeStore(FC);
            TT.SetToolClass();
            TT.LaunchTestTool();
            LS.SetClass(FC);
            LS.GetSoftForIdx(FC);

            LS.InstallSoftForIdx(3,FC);
            LS.LaunchlSoftForIdx(3, FC);
            TT.WaitToTest(FC);
            LS.CloseSoftByIdx(3);

            LS.LaunchLeStore(FC);
            LS.InstallSoftForIdx(4, FC);
            LS.LaunchlSoftForIdx(4, FC);
            TT.WaitToTest(FC);
            LS.CloseSoftByIdx(4);
        }
    }
}
