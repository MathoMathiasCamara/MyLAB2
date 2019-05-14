using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyLAB2.Model
{

    public class Triangle:INotifyPropertyChanged
    {
        private Coordinate _PointA;
        private Coordinate _PointB;
        private Coordinate _PointC;

        private String _Description;

        #region  Commands
        //my calculate command
        public MyICommand CalCulateCommand { get; set; }

        public Triangle()
        {
            CalCulateCommand = new MyICommand(OnCalculate, CanCalculate);
            _PointA = new Coordinate();
            _PointB = new Coordinate();
            _PointC = new Coordinate();

        }



        private bool CanCalculate()
        {
            return true;
        }

        private void OnCalculate()
        {
            if (CheckExist)
            {
                string Length = string.Format("Length: \n\tAB={0:G4}\n\tBC={1:G4}\n\tCA={2:G4}", this.LineAB, this.LineBC, this.LineCA);
                string Angles = string.Format("Angles: \n\ta={0:G4}\n\tb={1:G4}\n\tc={2:G4}", this.AngleA, this.AngleB, this.AngleC);
                string Perimeter = string.Format("Perimeter: \n\tP={0}", this.Perimeter);
                string Area = string.Format("Area: \n\tAr={0:G4}", this.Area);
                string exist = string.Format("Triangle exist ?={0}", this.CheckExist.ToString());
                var CheckIsIsoscele = new IsIsoscele();
                string IsIsos = string.Format("Is Isosceles={0}", CheckIsIsoscele.CheckIsIsoscele.ToString());

                //formatting the view
                _Description = string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}", Length, Angles, Perimeter, Area, IsIsos, exist);
                RaisePropertyChanged("Description");

                //AddCommand.RaiseCanExecuteChanged();
            }
            else
            {
                _Description = "No Triangle!!! :((";
                RaisePropertyChanged("Description");

            }
        }
       
        #endregion

        #region point's Coordinates 
        public Coordinate PointA
        {
            get
            {
                return _PointA;
            }
            set
            {
                if (_PointA != value)
                {
                    try
                    {
                        _PointA = value;

                        Description = "";
                        RaisePropertyChanged("PointA");
                        RaisePropertyChanged("LineAB");
                        RaisePropertyChanged("LineBC");
                        RaisePropertyChanged("LineCA");
                        RaisePropertyChanged("Perimeter");
                        RaisePropertyChanged("AngleA");
                        RaisePropertyChanged("AngleB");
                        RaisePropertyChanged("AngleC");
                        RaisePropertyChanged("Area");
                        RaisePropertyChanged("CheckExist");
                        RaisePropertyChanged("Description");

                        CalCulateCommand.RaiseCanExecuteChanged();
                    }
                    catch (Exception)
                    {

                        throw new Exception("Error when assigning values to PointA");
                    }
                                    
                }
            }
        }
        public Coordinate PointB
        {
            get
            {
                return _PointB;
            }
            set
            {
                if (_PointB != value)
                {
                    try
                    {
                        _PointB = value;
                        Description = "";
                        RaisePropertyChanged("PointB");
                        RaisePropertyChanged("LineAB");
                        RaisePropertyChanged("LineBC");
                        RaisePropertyChanged("LineCA");
                        RaisePropertyChanged("Perimeter");
                        RaisePropertyChanged("AngleA");
                        RaisePropertyChanged("AngleB");
                        RaisePropertyChanged("AngleC");
                        RaisePropertyChanged("Area");
                        RaisePropertyChanged("CheckExist");
                        RaisePropertyChanged("Description");

                        CalCulateCommand.RaiseCanExecuteChanged();
                    }
                    catch (Exception)
                    {

                        throw new Exception("Error when assigning values to PointB");
                    }
                                     
                }
            }
        }
        public Coordinate PointC
        {
            get
            {
                return _PointC;
            }
            set
            {
                if (_PointC != value)
                {
                    try
                    {
                        _PointC = value;
                        Description = "";

                        RaisePropertyChanged("PointC");
                        RaisePropertyChanged("LineAB");
                        RaisePropertyChanged("LineBC");
                        RaisePropertyChanged("LineCA");
                        RaisePropertyChanged("Perimeter");
                        RaisePropertyChanged("AngleA");
                        RaisePropertyChanged("AngleB");
                        RaisePropertyChanged("AngleC");
                        RaisePropertyChanged("Area");
                        RaisePropertyChanged("CheckExist");
                        RaisePropertyChanged("Description");

                        CalCulateCommand.RaiseCanExecuteChanged();
                    }
                    catch (Exception)
                    {

                        throw new Exception("Error when assigning values to Point C");
                    }
                                  
                }
            }
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
                Description = string.Empty;
            }
        }

        #region Check does the triangle exist
        public bool CheckExist
        {
            get
            {
                return ((_PointA != null && _PointB != null && _PointC != null)
               && ((_PointC._CoordinateX != _PointA._CoordinateX && _PointC._CoordinateY != _PointA._CoordinateY)
               && (_PointC._CoordinateX != _PointB._CoordinateX && _PointC._CoordinateY != _PointB._CoordinateY)
               && (_PointA._CoordinateX != _PointB._CoordinateX && _PointA._CoordinateY != _PointB._CoordinateY)
               && (_PointA._CoordinateX != _PointC._CoordinateX && _PointA._CoordinateY != _PointC._CoordinateY)
               && (_PointB._CoordinateX != _PointA._CoordinateX && _PointB._CoordinateY != _PointA._CoordinateY)
               && (_PointB._CoordinateX != _PointC._CoordinateX && _PointB._CoordinateY != _PointC._CoordinateY)));
            }                   
        }

        #endregion

        #region calculating the triangle side AB,BC,CA
        public double LineAB
        {
            get
            {
                var AB = (Math.Pow(PointB._CoordinateX - PointA._CoordinateX, 2)+ Math.Pow(PointB._CoordinateY- PointA._CoordinateY, 2));
                return Math.Sqrt(AB);
            }
        }
        public double LineBC
        {
            get
            {
                var BC = Math.Pow(PointC._CoordinateX - PointB._CoordinateX, 2) + Math.Pow(PointC._CoordinateY - PointB._CoordinateY, 2);
                return Math.Sqrt(BC);
            }
        }
        public double LineCA
        {
            get
            {
                var CA = Math.Pow(PointA._CoordinateX - PointC._CoordinateX, 2) + Math.Pow(PointA._CoordinateY - PointC._CoordinateY, 2);
                return Math.Sqrt(CA);
            }
        }

        #endregion

        #region method for Area,angles,perimeter

        //perimeter
        private double Perimeter
        {
            get
            {
                return (LineAB + LineBC + LineCA);
            }
        }
        
        //angles (A,B,C)
        private double AngleA
        {
            get
            {
                var cosA = (Math.Pow(LineBC, 2) + Math.Pow(LineCA, 2) - Math.Pow(LineAB, 2))/(2*LineBC*LineCA);
                return Math.Acos(cosA);
            }
        }
        private double AngleC
        {
            get
            {
                var cosC = (Math.Pow(LineAB, 2) + Math.Pow(LineBC, 2) - Math.Pow(LineCA, 2))/(2*LineAB*LineBC);
                return Math.Acos(cosC);
            }
        }
        private double AngleB
        {
            get
            {
                var cosB = (Math.Pow(LineCA, 2) + Math.Pow(LineAB, 2) - Math.Pow(LineBC, 2))/(2*LineCA*LineAB);
                return Math.Acos(cosB);
            }
        }

        //Area
        private double Area
        {
            get
            {
                var s = Perimeter / 2;
                return (Math.Sqrt(s*(s-LineAB)*(s-LineBC)*(s-LineCA)));
            }
        }


        #endregion

        #region Description of the triangle 
        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
            }
       }
        #endregion
    }
}
