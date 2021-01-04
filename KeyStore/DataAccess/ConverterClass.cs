using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeyStore.DataAccess
{
    [Serializable]
    public class ConverterClass
    {
        public byte[] GetRandomByteArray(int array_size=1000)
        {
            Random rnd = new Random();
            byte[] array = new byte[array_size];
            for(int i = 0; i < array_size; i++)
            {
                array[i] = (byte)rnd.Next(0, 1);
            }
            return array;
        }

        public byte[] GetFalseArray(int array_size = 1000)
        {
            byte[] array = new byte[array_size];
            for (int i = 0; i < array_size; i++)
            {
                array[i] = 0;
            }
            return array;
        }

        public byte[] GetTrueArray(int array_size = 1000)
        {
            byte[] array = new byte[array_size];
            for (int i = 0; i < array_size; i++)
            {
                array[i] = 1;
            }
            return array;
        }

        public byte[] XORArrays(byte[] array1, byte[] array2)
        {
            byte[] result_array = new byte[array1.Length];
            for(int i = 0; i < array1.Length; i++)
            {
                if (array1[i] == array2[i])
                {
                    result_array[i] = 0;
                }
                else
                {
                    result_array[i] = 1;
                }
            }
            return result_array;
        }

    }
}