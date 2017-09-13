using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.IO;
using System.Diagnostics;

namespace finalpro
{
    public partial class Panel : Form
    {
        int i;
        //Declararation of all variables, vectors and haarcascades
        Image<Bgr, Byte> currentFrame;
        Capture grabber;
        HaarCascade face;
        HaarCascade eye;
        MCvFont font = new MCvFont(FONT.CV_FONT_HERSHEY_TRIPLEX, 0.5d, 0.5d);
        Image<Gray, byte> result, TrainedFace = null;
        Image<Gray, byte> gray = null;
        List<Image<Gray, byte>> trainingImages = new List<Image<Gray, byte>>();
        List<string> labels = new List<string>();
        List<string> NamePersons = new List<string>();
        int ContTrain, NumLabels, t;
        string name, names = null;
        DatabaseConnection objConnect;
        string conString;
        DataSet ds;
        DataRow dRow;
        int MaxRows;
        int inc = 0;

        public Panel()
        {
            InitializeComponent();
            //Load haarcascades for face detection
            face = new HaarCascade("haarcascade_frontalface_default.xml");
            //eye = new HaarCascade("haarcascade_eye.xml");
            try
            {
                //Load of previus trainned faces and labels for each image
                string Labelsinfo = File.ReadAllText(Application.StartupPath + "/TrainedFaces/TrainedLabels.txt");
                string[] Labels = Labelsinfo.Split('%');
                NumLabels = Convert.ToInt16(Labels[0]);
                ContTrain = NumLabels;
                string LoadFaces;

                for (int tf = 1; tf < NumLabels + 1; tf++)
                {
                    LoadFaces = "face" + tf + ".bmp";
                    trainingImages.Add(new Image<Gray, byte>(Application.StartupPath + "/TrainedFaces/" + LoadFaces));
                    labels.Add(Labels[tf]);
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString());
                MessageBox.Show("No enrolled persons", "Please enroll face", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = (DateTime.Now.ToString("dd  MMM  yyyy"));
            toolStripStatusLabel4.Text = (DateTime.Now.ToString("hh : mm : ss - tt"));

            i++;
        }

        void FrameGrabber(object sender, EventArgs e)
        {
            label3.Text = "0";
            //label4.Text = "";
            NamePersons.Add("");


            //Get the current frame form capture device
            currentFrame = grabber.QueryFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);

            //Convert it to Grayscale
            gray = currentFrame.Convert<Gray, Byte>();

            //Face Detector
            MCvAvgComp[][] facesDetected = gray.DetectHaarCascade(
          face,
          1.2,
          10,
          Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
          new Size(20, 20));

            //Action for each element detected
            foreach (MCvAvgComp f in facesDetected[0])
            {
                t = t + 1;
                result = currentFrame.Copy(f.rect).Convert<Gray, byte>().Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                //draw the face detected in the 0th (gray) channel with blue color
                currentFrame.Draw(f.rect, new Bgr(Color.Red), 2);


                if (trainingImages.ToArray().Length != 0)
                {
                    //TermCriteria for face recognition with numbers of trained images like maxIteration
                    MCvTermCriteria termCrit = new MCvTermCriteria(ContTrain, 0.001);

                    //Eigen face recognizer
                    EigenObjectRecognizer recognizer = new EigenObjectRecognizer(
                       trainingImages.ToArray(),
                       labels.ToArray(),
                       3000,
                       ref termCrit);

                    name = recognizer.Recognize(result);

                    //Draw the label for each face detected and recognized
                    currentFrame.Draw(name, ref font, new Point(f.rect.X - 2, f.rect.Y - 2), new Bgr(Color.LightGreen));

                    /*DataRow row = ds2.Tables["tbl_Attend"].NewRow();

                    row[1] = label4.Text;
                    row[2] = Date.Text;
                    row[3] = Time.Text;
                        

                    ds .Tables["tbl_Attend"].Rows.Add(row);

                    try
                    {
                        objConnect.UpdateDatabase(ds );
                        MaxRows = MaxRows  + 1;
                        inc = MaxRows  - 1;
                        MessageBox.Show("Database updated");

                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(err.Message);
                    }*/

                }

                NamePersons[t - 1] = name;
                NamePersons.Add("");


                //Set the number of faces detected on the scene
                label3.Text = facesDetected[0].Length.ToString();

                /*
                //Set the region of interest on the faces
                        
                gray.ROI = f.rect;
                MCvAvgComp[][] eyesDetected = gray.DetectHaarCascade(
                   eye,
                   1.1,
                   10,
                   Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
                   new Size( 0, 20));
                gray.ROI = Rectangle.Empty;

                foreach (MCvAvgComp ey in eyesDetected[0])
                {
                    Rectangle eyeRect = ey.rect;
                    eyeRect.Offset(f.rect.X, f.rect.Y);
                    currentFrame.Draw(eyeRect, new Bgr(Color.Blue), 2);
                }
                 */

            }
            t = 0;

            //Names concatenation of persons recognized
            for (int nnn = 0; nnn < facesDetected[0].Length; nnn++)
            {
                names = names + NamePersons[nnn] + ", ";
            }
            //Show the faces procesed and recognized
            imageBoxFrameGrabber.Image = currentFrame;
            label4.Text = names;
            names = "";
            //Clear the list(vector) of names
            NamePersons.Clear();

        }

        private void Panel_Load(object sender, EventArgs e)
        {
            try
            {
                /* objConnect  = new DatabaseConnection();
                 conString  = Properties.Settings.Default.FinalProConnectionString;

                 objConnect .connection_string = conString ;
                 objConnect .Sql = Properties.Settings.Default.SQL;

                 ds  = objConnect .GetConnection;
                 MaxRows  = ds .Tables[0].Rows.Count;*/

                // NavigateRecords();

                //Load haarcascades for face detection
                face = new HaarCascade("haarcascade_frontalface_default.xml");

                //Initialize the capture device
                grabber = new Capture();
                grabber.QueryFrame();
                //Initialize the FrameGraber event
                Application.Idle += new EventHandler(FrameGrabber);
                //  button1.Enabled = false;

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void employeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form enr = new Enroll();
            enr.Show();
        }

        private void editEmployeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form editemp = new Edit_emp();
            editemp.Show();
        }

        private void deleteEmployeeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form editemp = new Edit_emp();
            editemp.Show();
        }

        private void leaveRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form leave = new leave();
            leave.Show();
        }

        private void attendanceToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Form repattend = new Rep_Attend();
            repattend.Show();
        }

        private void payslipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form payform = new PayForm();
            payform.Show();
        }

        private void markToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form attend = new Attendance();
            attend.Show();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();

            Form log = new Login();
            log.Show();
        }
    }
}
