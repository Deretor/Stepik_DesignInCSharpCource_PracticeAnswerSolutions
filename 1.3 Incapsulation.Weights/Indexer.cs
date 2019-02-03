using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incapsulation.Weights
{
    public class Indexer {

        double[] arr = new double[0];
        int length =0;
        int skip=0;
        public Indexer(double[] array, int start, int length)
        {
            if (start < 0 || length < 0 || start > array.Length || length > array.Length || start + length > array.Length) {
                throw new ArgumentException();
            }

            this.arr = array;
            this.length = length;
            this.skip = start;
        }
        public int Length { get { return length; } }

        public double this[int key] {
            get {
                if (key >= Length || key < 0) {
                    throw new IndexOutOfRangeException();
                }
                var index = key + skip;
                return arr[index];
            }
            set {
                if (key >= Length || key < 0)
                {
                    throw new IndexOutOfRangeException();
                }
                var index = key + skip;
                arr[index]=value;
            }
        }
    }
}
