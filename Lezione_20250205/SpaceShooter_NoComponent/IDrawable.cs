using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter_NoComponent {
    public interface IDrawable {

        bool IsActive {
            get;
        }

        void Draw();

    }
}
