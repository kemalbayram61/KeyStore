using KeyStore.Abstract;
using KeyStore.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace KeyStore.DataAccess
{
    [Serializable]
    public class ImageDataAccess : IImageDataAccess
    {
        private string image_db_path = @"C:\\Users\\Melih\\Desktop\\KeyStore-master\\KeyStore\\KeyStore\\DataAccess\\Database\\DBImage.txt";

        public string ByteArrayToString(byte[,] image_array)
        {
            string result = "";
            for(int i = 0; i < image_array.GetLength(0); i++)
            {
                for(int j = 0; j < image_array.GetLength(1); j++)
                {
                    result = result + ":" + image_array[i, j];
                }
                result = result + ",";
            }
            return result;
        }

        public byte[,] StringToByteArray(string image_string)
        {
            string[] rows = image_string.Split(',');
            int height = rows.Length;
            int weight = rows[0].Split(':').Length;

            byte[,] image_array = new byte[height,weight];

            for(int i = 0; i < height; i++)
            {
                string[] cols = rows[i].Split(':');
                for(int j = 0; j < cols.Length; j++)
                {
                    image_array[i, j] = (byte)Convert.ToInt32(cols[j]);
                }
            }

            return image_array;
        }

        public Imagee AddImage(Imagee image)
        {
            List<Imagee> image_list = GetAllImage();
            if (image_list != null)
            {
                foreach (Imagee element in image_list)
                {
                    if (element.id == image.id)
                    {
                        return new Imagee();
                    }
                }
            }

            if (File.Exists(image_db_path))
            {
                File.Delete(image_db_path);
            }

            if (!File.Exists(image_db_path))
            {
                using (StreamWriter sw = File.CreateText(image_db_path))
                {
                    if (image_list != null)
                    {
                        foreach (Imagee element in image_list)
                        {
                            sw.WriteLine(element.id.ToString() + ";" + element.user_id.ToString() + ";"+ ByteArrayToString(element.image_values));
                        }
                    }

                    sw.WriteLine(image.id.ToString() + ";" + image.user_id.ToString() + ";" + ByteArrayToString(image.image_values));
                }
            }
            else
            {
                return new Imagee();
            }
            return image;
        }

        public bool DeleteImage(int image_id)
        {
            bool is_element_find = false;

            List<Imagee> image_list = GetAllImage();
            if (File.Exists(image_db_path))
            {
                File.Delete(image_db_path);
            }
            using (StreamWriter sw = File.CreateText(image_db_path))
            {
                foreach (Imagee element in image_list)
                {
                    if (element.id != image_id)
                    {
                        sw.WriteLine(element.id.ToString() + ";" + element.user_id.ToString() + ";" + ByteArrayToString(element.image_values));
                    }
                    else
                    {
                        is_element_find = true;
                    }
                }
            }

            return is_element_find;
        }

        public List<Imagee> GetAllImage()
        {
            List<Imagee> image_list = new List<Imagee>();
            if (File.Exists(image_db_path))
            {
                using (StreamReader sr = File.OpenText(image_db_path))
                {
                    string line = "";
                    string[] line_element;
                    while ((line = sr.ReadLine()) != null)
                    {
                        line_element = line.Split(';');
                        Imagee image = new Imagee();
                        image.id = Convert.ToInt32(line_element[0]);
                        image.user_id = Convert.ToInt32(line_element[1]);
                        image.image_values = StringToByteArray(line_element[2]);
                        image_list.Add(image);
                    }
                }
                return image_list;
            }
            else
            {
                return null;
            }
        }

        public Imagee GetImageById(int image_id)
        {
            Imagee image = new Imagee();
            List<Imagee> image_list = GetAllImage();
            if (image_list != null)
            {
                if (image_list != null)
                {
                    foreach (Imagee element in image_list)
                    {
                        if (element.id == image_id)
                        {
                            image.id = image_id;
                            image.user_id = element.user_id;
                            image.image_values = element.image_values;
                            return image;
                        }
                    }
                }
                return image;
            }
            else
            {
                return null;
            }
        }

        public Imagee UpdateImage(Imagee image)
        {
            if (DeleteImage(image.id) == true)
            {
                AddImage(image);
                return image;
            }
            return new Imagee();
        }
    }
}