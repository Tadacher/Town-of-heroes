using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Utilities
{
    public static class Mat
    {
        public static float DistanceBetweenPointsV3(Vector3 initial, Vector3 final) => (initial - final).magnitude;
    }
}
