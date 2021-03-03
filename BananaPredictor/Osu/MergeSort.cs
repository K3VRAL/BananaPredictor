using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BananaPredictor.Osu
{
    public class MergeSort
    {
        public List<GetObjectInfo> Merge(List<GetObjectInfo> list)
        {
            if (list.Count <= 1)
                return null;

            int mid = list.Count / 2;

            List<GetObjectInfo> left = new();
            List<GetObjectInfo> right = new();

            for (int i = 0; i < mid; i++)
            {
                left.Add(list[0]);
                list.RemoveAt(0);
            }

            while (list.Count != 0)
            {
                right.Add(list[0]);
                list.RemoveAt(0);
            }

            MergeSort doingleft = new();
            MergeSort doingright = new();

            doingleft.Merge(left);
            doingleft.Merge(right);

            while (left.Count != 0 && right.Count != 0)
            {
                if (Int32.Parse(left[0].Object.Split(",")[2]) <= Int32.Parse(right[0].Object.Split(",")[2]))
                {
                    list.Add(left[0]);
                    left.RemoveAt(0);
                }
                else
                {
                    list.Add(right[0]);
                    right.RemoveAt(0);
                }
            }

            while (left.Count > 0)
            {
                list.Add(left[0]);
                left.RemoveAt(0);
            }

            while (right.Count > 0)
            {
                list.Add(right[0]);
                right.RemoveAt(0);
            }

            return list;
        }
    }
}
