using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DShooter
{
    class ImageNotFoundException : Exception
    {
        public ImageNotFoundException()
        {
        }

        public ImageNotFoundException(string message)
            : base(message)
        {
        }

        public ImageNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
