using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_translator
{

    /*
    x=3
    def myFun(){
        print(x)
    }

     */
    public class Context
    {
        private Dictionary<string, object> localItems;
        private Context outerContext;
        public Context(Context outerContext)
        {
            this.outerContext = outerContext;
            this.localItems = new Dictionary<string, object>();

        }
        public void SetValue(string name, object value)
        {
            localItems[name] = value;

        }
        public (bool found, object value ) LookUp(string name)
        {
            if (localItems.ContainsKey(name))
            {
                return(true, localItems[name]);

            }
            else if (outerContext == null)
            {
                return(false, null);
            }
            else
            {
                return outerContext.LookUp(name);
            }
        }
    }

}
