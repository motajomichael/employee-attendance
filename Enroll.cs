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
    
    public partial class Enroll : Form
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
        string sql;
        DataRow row = null;
        //connect to database
           

        public Enroll()
        {
            
            InitializeComponent();
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

        private void Enroll_Load(object sender, EventArgs e)
        {
            try
            {




                //Load haarcascades for face detection
                face = new HaarCascade("haarcascade_frontalface_default.xml");

                //Initialize the capture device
                grabber = new Capture();
                grabber.QueryFrame();
                //Initialize the FrameGraber event
                Application.Idle += new EventHandler(FrameGrabber);

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

            

    
        
        
        

       

        private void AddBtn_Click(object sender, EventArgs e)
        {
             //Get a gray frame from capture device
             gray = grabber.QueryGrayFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);

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
                 TrainedFace = currentFrame.Copy(f.rect).Convert<Gray, byte>();
                 break;
             }

             //Show face added in gray scale
             imageBox1.Image = TrainedFace;
             
        }



        private void SaveBtn_Click(object sender, EventArgs e)
        {






           /* using (System.Data.SqlClient.SqlConnection sqlConn = new System.Data.SqlClient.SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\Resources\FinalPro.mdf;Integrated Security=True"))
            {
                string query = "INSERT INTO tbl_Enroll VALUES (@Enroll_Id, @Emp_Id, @Admin_Id, @Img_Model, @Enroll_Date)";
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, sqlConn);
                cmd.Parameters.Add("@Enroll_Id", SqlDbType.VarChar, 50).Value = "Enr1";
                cmd.Parameters.Add("@Emp_Id", SqlDbType.NVarChar, 50).Value = comboBox3.Text;
                cmd.Parameters.Add("@Admin_Id", SqlDbType.NVarChar, 50).Value = "ADM1";
                cmd.Parameters.Add("@Img_Model", SqlDbType.NVarChar, 50).Value = "we3";
                cmd.Parameters.Add("@Enroll_Date", SqlDbType.Date, 12).Value = 02 / 02 / 1992;

                cmd.Connection.Open();

            */
           /* using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\Resources\FinalPro.mdf;Integrated Security=True")) 
                {
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("INSERT INTO tbl_Admin VALUES ('""', 'chrs', 'assed', 'em')");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection; 
                   // cmd.Parameters.AddWithValue("@Id", "name");
                    //cmd.Parameters.AddWithValue("@uname", "ope");
                    //cmd.Parameters.AddWithValue("@pass", "pass");
                    //cmd.Parameters.AddWithValue("@name", "yemi");
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }*/


                try
                {
                   // cmd.ExecuteNonQuery();



                    //Trained face counter
                    ContTrain = ContTrain + 1;

                    //Get a gray frame from capture device
                    gray = grabber.QueryGrayFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);

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
                        TrainedFace = currentFrame.Copy(f.rect).Convert<Gray, byte>();
                        break;
                    }

                    //resize face detected image for force to compare the same size with the 
                    //test image with cubic interpolation type method
                    TrainedFace = result.Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                    trainingImages.Add(TrainedFace);
                    labels.Add(comboBox2.Text);

                    //Show face added in gray scale
                    // imageBox1.Image = TrainedFace;

                    //Write the number of triained faces in a file text for further load
                    File.WriteAllText(Application.StartupPath + "/TrainedFaces/TrainedLabels.txt", trainingImages.ToArray().Length.ToString() + "%");

                    //Write the labels of triained faces in a file text for further load
                    for (int i = 1; i < trainingImages.ToArray().Length + 1; i++)
                    {
                        trainingImages.ToArray()[i - 1].Save(Application.StartupPath + "/TrainedFaces/face" + i + ".bmp");
                        File.AppendAllText(Application.StartupPath + "/TrainedFaces/TrainedLabels.txt", labels.ToArray()[i - 1] + "%");
                    }

                    MessageBox.Show(comboBox2.Text + "´s face detected and added :)", "Training OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("Enable the face detection first", "Training Fail", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        
        

            void FrameGrabber(object sender, EventArgs e)
            {
           // label3.Text = "0";
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
                        // currentFrame.Draw(name, ref font, new Point(f.rect.X - 2, f.rect.Y - 2), new Bgr(Color.LightGreen));

                        }

                            NamePersons[t-1] = name;
                            NamePersons.Add("");


                        //Set the number of faces detected on the scene
                     //   label3.Text = facesDetected[0].Length.ToString();
                       
                        /*
                        //Set the region of interest on the faces
                        
                        gray.ROI = f.rect;
                        MCvAvgComp[][] eyesDetected = gray.DetectHaarCascade(
                           eye,
                           1.1,
                           10,
                           Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
                           new Size(20, 20));
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
                   // for (int nnn = 0; nnn < facesDetected[0].Length; nnn++)
                   // {
                    //    names = names + NamePersons[nnn] + ", ";
                   // }
                    //Show the faces procesed and recognized
                    imageBoxFrameGrabber.Image = currentFrame;
                   // label4.Text = names;
                   // names = "";
                    //Clear the list(vector) of names
                   // NamePersons.Clear();

                


        }

            private void label6_Click(object sender, EventArgs e)
            {
                if (comboBox1.Text == "IT")
                {
                    label6.Text = "D1";
                }
                else if (comboBox1.Text == "ENG")
                {
                    label6.Text = "D2";
                }
                else if (comboBox1.Text == "BBA")
                {
                    label6.Text = "D3";
                }
                else label6.Text = "";

            }

            private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
            {
                if (comboBox1.Text == "IT")
                {
                    sql = "select Emp_Id, Emp_Name from tbl_Emp where Dept_Id ='D1'";
                }
                else if (comboBox1.Text == "ENG")
                {
                    sql = "select Emp_Id, Emp_Name from tbl_Emp where Dept_Id ='D2'";
                }
                else if (comboBox1.Text == "BBA")
                {
                    sql = "select Emp_Id, Emp_Name from tbl_Emp where Dept_Id ='D3'";
                }

                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\Resources\FinalPro.mdf;Integrated Security=True");
                conn.Open();
                System.Data.SqlClient.SqlCommand sc = new System.Data.SqlClient.SqlCommand(sql, conn);
                System.Data.SqlClient.SqlDataReader reader;

                reader = sc.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("Emp_Id", typeof(string));
                dt.Columns.Add("Emp_Name", typeof(string));
                dt.Load(reader);

                comboBox2.ValueMember = "Emp_Id";
                comboBox2.DisplayMember = "Emp_Name";
                comboBox2.DataSource = dt;

                conn.Close();
            }

            private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
            {
                string sql2 = "select Emp_Id, Emp_Name from tbl_Emp where Emp_Name ='" + comboBox2.Text + "'";
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\Resources\FinalPro.mdf;Integrated Security=True");
                conn.Open();
                System.Data.SqlClient.SqlCommand sc = new System.Data.SqlClient.SqlCommand(sql2, conn);
                System.Data.SqlClient.SqlDataReader reader;

                reader = sc.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("Emp_Id", typeof(string));
                dt.Columns.Add("Emp_Name", typeof(string));
                dt.Load(reader);

                comboBox3.ValueMember = "Emp_Id";
                comboBox3.DisplayMember = "Emp_Id";
                comboBox3.DataSource = dt;

                conn.Close();
            }

            private void timer1_Tick(object sender, EventArgs e)
            {
                toolStripLabel2.Text = (DateTime.Now.ToString("ddd - dd - MM - yyyy"));
                toolStripLabel4.Text = (DateTime.Now.ToString("hh : mm : ss - tt"));

                i++;
            }

            private void timer1_Tick_1(object sender, EventArgs e)
            {
                toolStripLabel2.Text = (DateTime.Now.ToString("ddd - dd - MM - yyyy"));
                toolStripLabel4.Text = (DateTime.Now.ToString("hh : mm : ss - tt"));

                i++;
            }

            private void toolStripLabel6_Click(object sender, EventArgs e)
            {
                toolStripLabel6.Text = null;
            }

        

          


    }
}
