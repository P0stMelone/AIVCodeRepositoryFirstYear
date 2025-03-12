using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lezione_20250310 {
    public class Box<T> where T: IComparable<T> {

        T variable;

        //più corretto con .FullName
        public override string ToString() {
            return $"Variable Type: {typeof(T).FullName} Value: {variable}";
        }
        
        
    }
}
