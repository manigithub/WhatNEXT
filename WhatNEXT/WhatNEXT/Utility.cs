using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace WhatNEXT
{
    public static class Utility
    {
        public static int GetSizeOfObject(object obj)
        {
            //object v = null;
            //int size = 0;
            //Type type = obj.GetType();
            //PropertyInfo[] info = type.GetProperties();
            //foreach (PropertyInfo property in info)
            //{
            //    v = property.GetValue(obj, null);
            //    unsafe
            //    {
            //        size += sizeof(v);
            //    }
            //}
            //return size;

            return 100000;
            

        }

        public static byte[] GetByteArray(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
