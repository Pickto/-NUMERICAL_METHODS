using System;
using System.Collections.Generic;
using System.Text;

namespace NM1
{
    class Gauss
    {
        public static Vector GaussMethod(Matrix A, Vector F)
        {
            Vector RES;
            MatrixСonverting.LeadingElementSelection(A);
            MatrixСonverting.BringingMatrixToTopTriangle(A);
            RES = Substitutions.BackRowSubstitution(A, F);
            return RES;
        }
    }
}
