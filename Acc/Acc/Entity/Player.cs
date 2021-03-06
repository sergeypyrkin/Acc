﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Acc.Entity
{
    [Serializable()]
    public class Player
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int StartBalance { get; set; }
        public bool FlagDel { get; set; }
        public string Description { get; set; }
        public static string _NAME = "players";

        private static object lofile = new object();


        public static String LogfileName()
        {
            String path = new System.IO.FileInfo(Assembly.GetEntryAssembly().Location).DirectoryName;
            return path + @"\db\db-" + _NAME + "";
        }

        public static String LogFileDir()
        {
            return new System.IO.FileInfo(Assembly.GetEntryAssembly().Location).DirectoryName + @"\db";
        }

        public static void SaveToFile(List<Player> accounts)
        {
            lock (lofile)
            {
                try
                {
                    String dir_name = LogFileDir();
                    if (!Directory.Exists(dir_name))
                    {
                        Directory.CreateDirectory(dir_name);
                    } //создать директорию если не существует
                }
                catch (Exception ex)
                {
                }
                try
                {
                    using (Stream serializationStream =
                        new FileStream(LogfileName(), FileMode.Create, FileAccess.Write))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        formatter.Serialize(serializationStream, accounts);
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Ошибка при записи в локальную базу данных!");
                }

                lofile = new object();
            }
        }


        public static List<Player> LoadFromFile()
        {
            lock (lofile)
            {
                String fn = LogfileName();


                if (!File.Exists(fn))
                    return new List<Player>();


                try
                {
                    using (Stream serializationStream = new FileStream(fn, FileMode.Open, FileAccess.Read))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        List<Player> sav = (List<Player>)formatter.Deserialize(serializationStream);
                        List<Player> res = new List<Player>();
                        foreach (Player acc in sav)
                            if (acc != null)
                            {

                                res.Add(acc);
                            }

                        return res;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return new List<Player>();
                }
            }


        }
    }
}
