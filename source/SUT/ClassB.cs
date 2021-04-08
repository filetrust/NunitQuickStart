using SUT.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SUT
{
    public class ClassB
    {
        public string g(IMyInterface myInterface)
        {
            return myInterface.f();
        }
    }
}
