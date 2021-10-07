using System;
using System.Collections.Generic;
using System.Text;

namespace Utils.Generators
{
    public class UniqueNamesGenerator
    {
        private String nameStart;
        private int number = 1;

        public UniqueNamesGenerator(string nameStart)
        {
            this.nameStart = nameStart;
        }
        public string generate()
        {
            return nameStart + number++;
        }
    }
}
