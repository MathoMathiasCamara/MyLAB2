using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyLAB2.Model
{
   public class ListTriangle
    {
   
        //My file stream
        //private static string path

        public ObservableCollection<Triangle> Triangles { get; set; }

        private Triangle RndmTriangle { get; set; }
        private Random rnd;

        public MyICommand AddCommand { get; set; }
        public MyICommand SaveCommand { get; set; }
        public MyICommand LoadCommand { get; set; }
        public MyICommand CheckTrianglesCommand { get; set; }

        public ListTriangle()
        {
            rnd = new Random();

            CheckTrianglesCommand = new MyICommand(CheckingTriangles, CanCheckTriangles);
            AddCommand = new MyICommand(AddTriangle, CanAdd);
            //SaveCommand = new MyICommand(Save, CanSave);
            //LoadCommand = new MyICommand(LoadTriangle, CanLoad);

            Triangles = new ObservableCollection<Triangle>();

            //Initializing my file path and the filestream
            //path = Directory.GetCurrentDirectory() + @"\Triangles.bin";
            //fs= new FileStream(path, FileMode.OpenOrCreate);
        }
        private bool CanSave()
        {
           
            return Triangles.Count > 0;
        }

        private bool CanLoad()
        {
            return true;
        }
        private void LoadTriangle()
        {
            Microsoft.Win32.OpenFileDialog sfd = new Microsoft.Win32.OpenFileDialog();
            sfd.CheckPathExists = true;
            if (sfd.ShowDialog() == true)
            {
                FileStream fs = new FileStream(sfd.FileName, FileMode.Open);
                BinaryReader br = new BinaryReader(fs, Encoding.Default);
                try
                {
                    int count = br.ReadInt32();
                    for (int i = 0; i < count; i++)
                    {
                        Triangle item = new Triangle();
                        item.PointA._CoordinateX =br.ReadInt32();
                        item.PointA._CoordinateY = br.ReadInt32();

                        item.PointB._CoordinateX = br.ReadInt32();
                        item.PointB._CoordinateY = br.ReadInt32();

                        item.PointC._CoordinateX = br.ReadInt32();
                        item.PointC._CoordinateY = br.ReadInt32();

                        Triangles.Add(item);
                    }

                    fs.Close();
                    br.Close();
                    MessageBox.Show("Triangles Loaded!");
                    LoadCommand.RaiseCanExecuteChanged();
                    CheckTrianglesCommand.RaiseCanExecuteChanged();
                }
                catch (Exception ex)
                {

                    throw new Exception("No data found in the file.\nException:" + ex.Message);
                }
            }    
        }

        // My save command
        public void  Save()
        {
               
                    Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
                    sfd.CheckPathExists = true;
                    if (sfd.ShowDialog() == true)
                        SaveAfile(sfd.FileName);

        }

        private void SaveAfile(string paths)
        {
            FileStream fs = new FileStream(paths, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs, Encoding.Default);
            // writing triangle count first
            bw.Write(Triangles.Count);
            for (int i = 0; i < Triangles.Count; i++)
            {
                //point A
                bw.Write(Triangles[i].PointA._CoordinateX);
                bw.Write(Triangles[i].PointA._CoordinateY);

                //point B
                bw.Write(Triangles[i].PointB._CoordinateX);
                bw.Write(Triangles[i].PointB._CoordinateY);

                //point C
                bw.Write(Triangles[i].PointC._CoordinateX);
                bw.Write(Triangles[i].PointC._CoordinateY);
            }

            fs.Close();
            bw.Close();
            SaveCommand.RaiseCanExecuteChanged();
            MessageBox.Show("Triangles saved!");
        }
        //Methode to check if the Add command should be Activated
        private bool CanAdd()
        {
            return true;
        }

        private Triangle CreateRandomTriangle()
        {
            RndmTriangle = new Triangle();
            RndmTriangle.PointA = new Coordinate() { _CoordinateX = rnd.Next(10), _CoordinateY = rnd.Next(10) };
            RndmTriangle.PointB = new Coordinate() { _CoordinateX = rnd.Next(10), _CoordinateY = rnd.Next(10) };
            RndmTriangle.PointC = new Coordinate() { _CoordinateX = rnd.Next(10), _CoordinateY = rnd.Next(10) };

            return RndmTriangle;
        }

        //My Add command should be Activated
        public void AddTriangle()
        {
            try
            {
                Triangle temp;
                do
                {
                    temp = CreateRandomTriangle();
                    if (temp.CheckExist)
                    {
                        Triangles.Add(temp);
                        CheckTrianglesCommand.RaiseCanExecuteChanged();
                    }
                } while (temp.CheckExist == false);

                SaveCommand.RaiseCanExecuteChanged();

            }
            catch (Exception)
            {

                throw new Exception("Error when adding the triangle.");
            }

        }

        #region My Command Check triangles
        private bool CanCheckTriangles()
        {
            if (Triangles != null)
                return Triangles.Count > 0;
            else
                return false;
        }

        
        public void CheckingTriangles()
        {
            if (Triangles != null && Triangles.Count > 1)
            {
                int countSameTriangle = 0;

                for (int i = 0; i < Triangles.Count; i++)
                {
                    for (int j = i + 1; j < Triangles.Count; j++)
                    {
                        //if (Triangles[i] == Triangles[j])
                        if(Triangles[i].LineAB==Triangles[j].LineBC 
                            && Triangles[i].LineBC == Triangles[j].LineCA
                            && Triangles[i].LineCA == Triangles[j].LineAB
                            && Triangles[i].LineAB == Triangles[j].LineCA
                            && Triangles[i].LineBC == Triangles[j].LineAB
                            && Triangles[i].LineCA == Triangles[j].LineBC
                            && Triangles[i].LineAB == Triangles[j].LineAB
                            && Triangles[i].LineBC == Triangles[j].LineBC
                            && Triangles[i].LineCA == Triangles[j].LineCA)
                            countSameTriangle += 1;
                    }
                }

                string Message = "";
                if (countSameTriangle > 0)
                    Message = " identical triangles found.";
                else
                    Message = "No identical triangles found.";
                MessageBox.Show(Message, "Identical Triangles", MessageBoxButton.OK);
            }
            else
            {
                string Message = "Error, There is no/or just 1 Triangle in the list.";
                MessageBox.Show(Message, "Error in the list.", MessageBoxButton.OK);
            }

        }
        #endregion
    }
}
