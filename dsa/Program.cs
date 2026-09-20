using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.AccessControl;
using System.Collections.Generic;
using System.Linq;
class program
{
   public static int LengthofLongestSubstring(string s)
    {
        HashSet<char> list = new HashSet<char>();

        int start = 0;
        int maxlengtH = 0;

        for (int end = 0; end < s.Length; end++)
        {
            while (list.Contains(s[end]))
            {
                list.Remove(s[start]);
                start++;
            }
            list.Add(s[end]);
            maxlengtH = Math.Max(maxlengtH, end - start + 1);
        }
        return maxlengtH;       
    }
}