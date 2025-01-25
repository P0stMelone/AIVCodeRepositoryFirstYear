using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lezione_20241111 {
    public enum HttpError {
        NotFound = 400,
        ServerError = 500,
        Ok = 200,
        Pupu
    }

    public enum Direction {
        North,
        South,
        West,
        Est
    }
}
