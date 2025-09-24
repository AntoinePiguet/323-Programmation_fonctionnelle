using System;
using System.Drawing;        
using System.Windows.Forms; 

using System.IO;           
using System.Xml.Linq;        

using GMap.NET;              
using GMap.NET.MapProviders; 
using GMap.NET.WindowsForms; 
using GMap.NET.WindowsForms.Markers;

using System.Security.AccessControl;

using Aspose.Gis;
using Aspose.Gis.Geometries;


namespace Rando
{
    public partial class Rando : Form
    {
        public Rando()
        {
            InitializeComponent();
        }

        private void Rando_Form_Paint(object sender, PaintEventArgs e)
        {
            string path = "C:\\Users\\pn01kbc\\Documents\\GitHub\\323-Programmation_fonctionnelle\\PERSONNEL_APT\\exos\\rando\\gpx\\Running.gpx";
            Pen myPen = new Pen(Color.Red);
            myPen.Width = 2;
            getPointsFromGPX(path);
            System.Drawing.Point[] points = { new System.Drawing.Point(30,50), new System.Drawing.Point(50,10), new System.Drawing.Point(80,50), new System.Drawing.Point(111,400) };
            this.CreateGraphics().DrawLines(myPen, points);
        }
        private void getPointsFromGPX(string path)
        {
            var layer = Drivers.Gpx.OpenLayer(path);

            foreach (var feature in layer)
            {
                // Check for LineString geometry
                if (feature.Geometry.GeometryType == GeometryType.LineString)
                {
                    // Read Routs
                    LineString ls = (LineString)feature.Geometry;

                    foreach (var point in ls)
                    {
                        Console.WriteLine(" X: " + point.X + " Y: " + point.Y + " Z: " + point.Z);
                    }
                }
            }
        }
    }
}
