﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading;

namespace PPE4_Stars_up
{
    public partial class FormLogin : Form
    {
        private BindingSource bindingSource1 = new BindingSource();
        public int click = 0;
        public int click3 = 0;
        int countt = 0;

        string FichierLangue = "";
        List<string> LangueElement = new List<string>();

        string langue = "Francais";
        string fileName2 = @"C:\PPE4_DR\Preferences_PPE4_DR.txt";

        private string nomInspecteur;
        private string prenomInspecteur;
        private int idInspecteur;

        public int idINSP;

        // creation d’une liste des logins inspecteurs
        List<KeyValuePair<int, string>> FListLogin = new List<KeyValuePair<int, string>>();

        // creation d’une liste des mot de passe inspecteurs
        List<KeyValuePair<int, string>> FListMdp = new List<KeyValuePair<int, string>>();



        public FormLogin()
        {
            InitializeComponent();
        }

        private void creationFichier()
        {
            string curFile = @"C:\PPE4_DR\Preferences_PPE4_DR.txt";

            if(File.Exists(curFile))
            {
                // on fait rien
            }
            else
            {
                string folderName = @"c:\";
                string pathString = System.IO.Path.Combine(folderName, "PPE4_DR");
                System.IO.Directory.CreateDirectory(pathString);
                string fileName = "Preferences_PPE4_DR.txt";
                pathString = System.IO.Path.Combine(pathString, fileName);


                Console.WriteLine("Path to my file: {0}\n", pathString);


                using (System.IO.FileStream fs = new System.IO.FileStream(pathString, FileMode.Append))
                {

                    for (byte i = 0; i < 100; i++)
                    {
                        fs.WriteByte(i);
                    }

                }

                try
                {
                    byte[] readBuffer = System.IO.File.ReadAllBytes(pathString);
                    foreach (byte b in readBuffer)
                    {
                        Console.Write(b + " ");
                    }
                    Console.WriteLine();
                }
                catch (System.IO.IOException e)
                {
                    Console.WriteLine(e.Message);
                }

                List<string> listeElement = new List<string>();

                listeElement.Add(idINSP.ToString());
                listeElement.Add(langue);

                
                StreamWriter writer = new StreamWriter(fileName2);

                foreach (var item in listeElement)
                {
                    writer.WriteLine(item);
                }
                writer.Close();
            }
        }

        private void ecrireFichier()
        {
            StreamReader reader = File.OpenText(fileName2);
            string ligne;

            List<string> listeElement = new List<string>();
            while (!reader.EndOfStream)
            {
                ligne = reader.ReadLine();
                listeElement.Add(ligne);
            }
            reader.Close();

            listeElement[0] = idINSP.ToString();

            StreamWriter writer = new StreamWriter(fileName2);
            foreach (var item in listeElement)
            {
                writer.WriteLine(item);
            }
            writer.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            InputBox(LangueElement[4], "");

            if (lbLogin.Items.Contains(tbNom.Text))
            {                
                // login correct 

                foreach(var o in lbLogin.Items)
                {
                    if(lbLogin.Items[countt].ToString() == tbNom.Text)
                    {

                    }
                    else
                    {
                        countt++;
                    }
                }

                InputBox(LangueElement[5], "");
                
                if (tbMdp.Text== lbMdp.Items[countt].ToString()) 
                {
                    // mdp correct

                    #region confidentiel
                    if (tbNom.Text == "NGrondin")
                    {
                        nomInspecteur = "Grondin";
                        prenomInspecteur = "Nicolas";
                        idInspecteur = 1;
                        idINSP = 1;
                    }

                    if (tbNom.Text == "NBouhours")
                    {
                        nomInspecteur = "Bouhours";
                        prenomInspecteur = "Natacha";
                        idInspecteur = 2;
                        idINSP = 2;
                    }

                    if (tbNom.Text == "NDSilva")
                    {
                        nomInspecteur = "Da Silva";
                        prenomInspecteur = "Nicolas";
                        idInspecteur = 3;
                        idINSP = 3;
                    }

                    if (tbNom.Text == "Drobin")
                    {
                        nomInspecteur = "Robin";
                        prenomInspecteur = "Dimitry";
                        idInspecteur = 4;
                        idINSP = 4;
                    }

                    if (tbNom.Text == "PHermange")
                    {
                        nomInspecteur = "Hermange";
                        prenomInspecteur = "Pierre";
                        idInspecteur = 5;
                        idINSP = 5;
                    }

                    if (tbNom.Text == "KMenant")
                    {
                        nomInspecteur = "Menant";
                        prenomInspecteur = "Kévin";
                        idInspecteur = 6;
                        idINSP = 6;
                    }
                    #endregion confidentiel

                    ecrireFichier();

                    FormIndex FI = new FormIndex(nomInspecteur, prenomInspecteur, idInspecteur);
                    FI.Show();
                    Visible = false;
                }
                else
                {
                    // mdp incorrect

                    MessageBox.Show(LangueElement[6], LangueElement[7], MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbMdp.Clear();

                    tbMdp.ForeColor = Color.Black;

                    // tbMdp.ForeColor = Color.DarkGray;
                    tbMdp.Clear();

                    // tbMdp.ForeColor = Color.DarkGray;
                    // tbMdp.Cursor = Default;
                    // tbMdp.Text = "Mot de passe";
                    // cbAfficherMdp.Checked = false;

                    if(cbAfficherMdp.Checked==true)
                    {
                        tbMdp.PasswordChar = '\0';
                    }
                    else
                    {
                        tbMdp.PasswordChar = '*';
                    }                    
                }
            }
            else
            {
                // login incorrect

                MessageBox.Show(LangueElement[8], LangueElement[7], MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbNom.Clear();

                tbNom.ForeColor = Color.DarkGray;
                tbNom.Clear();

                tbNom.ForeColor = Color.DarkGray;
                // tbMdp.Cursor = Default;
                tbNom.Text = LangueElement[0];
            }
        }

        private void FormLogin_Leave(object sender, EventArgs e)
        {
            
        }

        private void FormLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Application.Exit();

           // System.Diagnostics.Process monProcess = System.Diagnostics.Process.Start("CefSharp.BrowserSubprocess.exe");
            //monProcess.Kill();
        }

        private void tbNom_Click(object sender, EventArgs e)
        {
            tbNom.Clear();

            tbNom.Clear(); // Palatino Linotype; 12pt; style=Bold
            // tbNom.Cursor = IBeam;
            tbNom.ForeColor = Color.Black;

            if (tbMdp.Text == "")
            {
                tbMdp.ForeColor = Color.DarkGray;
                tbMdp.Clear();

                tbMdp.ForeColor = Color.DarkGray;
                // tbMdp.Cursor = Default;
                tbMdp.Text = LangueElement[1];
            }
        }

        private void tbNom_Enter(object sender, EventArgs e)
        {
            tbNom_Click(sender, e);
        }

        private void tbNom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.NumPad0 || e.KeyCode == Keys.NumPad1 || e.KeyCode == Keys.NumPad2 || e.KeyCode == Keys.NumPad3 || e.KeyCode == Keys.NumPad4 || e.KeyCode == Keys.NumPad5 || e.KeyCode == Keys.NumPad6 || e.KeyCode == Keys.NumPad7 || e.KeyCode == Keys.NumPad8 || e.KeyCode == Keys.NumPad9)
            {
                MessageBox.Show(LangueElement[9], LangueElement[7], MessageBoxButtons.OK, MessageBoxIcon.Stop);
                tbNom.Clear();
            }
        }

        private void tbNom_Leave(object sender, EventArgs e)
        {
            if (tbNom.Text == LangueElement[0] || tbNom.Text == "" || tbNom.Text == " ")
            {
                tbNom.ForeColor = Color.DarkGray;

               // tbNom.Cursor = Default;
                tbNom.Text = LangueElement[0];
            }
        }

        private void tbMdp_Click(object sender, EventArgs e)
        {
            tbMdp.Clear();

            tbMdp.Clear(); // Palatino Linotype; 12pt; style=Bold
            // tbNom.Cursor = IBeam;
            tbMdp.ForeColor = Color.Black;

            if(cbAfficherMdp.Checked==false)
            {
                tbMdp.PasswordChar = '*';
            }

            if (tbNom.Text == "")
            {
                tbNom.ForeColor = Color.DarkGray;
                tbNom.Clear();

                tbNom.ForeColor = Color.DarkGray;
                // tbMdp.Cursor = Default;
                tbNom.Text = LangueElement[0];
            }
        }

        private void tbMdp_Enter(object sender, EventArgs e)
        {
            tbMdp_Click(sender, e);
        }

        private void tbMdp_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void tbMdp_Leave(object sender, EventArgs e)
        {
            if (tbMdp.Text == LangueElement[1] || tbMdp.Text == "" || tbMdp.Text == " ")
            {
                tbMdp.ForeColor = Color.DarkGray;

                // tbNom.Cursor = Default;
                tbMdp.PasswordChar = '\0';
                tbMdp.Text = LangueElement[1];
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            // Gestion de la langue
            StreamReader reader = File.OpenText(fileName2);
            string ligne;

            List<string> listeElement = new List<string>();
            while (!reader.EndOfStream)
            {
                ligne = reader.ReadLine();
                listeElement.Add(ligne);
            }
            reader.Close();

            if(listeElement[1] == "Francais")
            {
                FichierLangue = "Francais.txt";
            }

            if (listeElement[1] == "Anglais")
            {
                FichierLangue = "Anglais.txt";
            }

            if (listeElement[1] == "Allemand")
            {
                FichierLangue = "Allemand.txt";
            }

            if (listeElement[1] == "Espagnol")
            {
                FichierLangue = "Espagne.txt";
            }

            StreamReader reader2 = File.OpenText(FichierLangue);
            string ligne2;

            while (!reader2.EndOfStream)
            {
                ligne2 = reader2.ReadLine();
                LangueElement.Add(ligne2);
            }
            reader.Close();


            // Fin Gestion langue

            pbCorrect1.Visible = false;
            pbCorrect2.Visible = false;
            pbIncorrect1.Visible = false;
            pbIncorrect2.Visible = false;

            creationFichier();
            Visible = true;
            this.ActiveControl = textBox1;

            controleur.init();
            controleur.Vmodele.seconnecter();

            controleur.Vmodele.import();
            controleur.Vmodele.sedeconnecter();

            chargedgv();


            tbNom.Text = LangueElement[0];
            tbMdp.Text = LangueElement[1];
            cbAfficherMdp.Text = LangueElement[2];
            this.Text = LangueElement[3];
            btnOK.Text = LangueElement[3];

        }

        public void chargedgv()
        {
            bindingSource1.DataSource = controleur.Vmodele.Dv_login;

            // on parcourt le dataView des inspecteurs Dv_login de la classe bdd pour compléter la FList
            for (int i = 0; i < controleur.Vmodele.Dv_login.ToTable().Rows.Count; i++)
            {
                FListLogin.Add(new KeyValuePair<int, string>((int)controleur.Vmodele.Dv_login.ToTable().Rows[i][0], controleur.Vmodele.Dv_login.ToTable().Rows[i][4].ToString()));
                // rtbLogin.Text += controleur.Vmodele.Dv_login.ToTable().Rows[i][4].ToString() + "\n";
                lbLogin.Items.Add(controleur.Vmodele.Dv_login.ToTable().Rows[i][4].ToString());
            }

            for (int i = 0; i < controleur.Vmodele.Dv_login.ToTable().Rows.Count; i++)
            {
                FListMdp.Add(new KeyValuePair<int, string>((int)controleur.Vmodele.Dv_login.ToTable().Rows[i][0], controleur.Vmodele.Dv_login.ToTable().Rows[i][5].ToString()));
                // rtbMdp.Text += controleur.Vmodele.Dv_login.ToTable().Rows[i][5].ToString() + "\n";
                lbMdp.Items.Add(controleur.Vmodele.Dv_login.ToTable().Rows[i][5].ToString());
            }
        }

        private void cbAfficherMdp_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cbAfficherMdp_Click(object sender, EventArgs e)
        {
            if (tbMdp.Text != LangueElement[1])
            {
                if (cbAfficherMdp.Checked == true)
                {
                    tbMdp.PasswordChar = '\0';
                }
                else
                {
                    tbMdp.PasswordChar = '*';
                }
            }
        }

        private void tbMdp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnOK_Click(sender, e);
            }
        }

        public static int InputBox(string title, string promptText)
        {
            Form form = new Form();
            LinkLabel texte = new LinkLabel();
            ProgressBar Progress = new ProgressBar();

            Progress.Minimum = 0;
            Progress.Maximum = 100;

            form.Text = title;
            texte.Text = promptText;
            texte.SetBounds(9, 20, 372, 13);
            Progress.SetBounds(9, 30, 372, 20);

            texte.AutoSize = true;
            Progress.Anchor = Progress.Anchor | AnchorStyles.Right;

            form.ClientSize = new Size(396, 91);
            form.Controls.AddRange(new Control[] { texte, Progress });
            form.ClientSize = new Size(Math.Max(300, texte.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;

            Progress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;

            form.Show();

            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(7); // --> Timer au tick

                Progress.Value += 1;
                form.Show();
            }

            int Res = Progress.Value;
            if (Res == 100)
                form.Close();

            return Res;
        }

        private void tbNom_TextChanged(object sender, EventArgs e)
        {
            if (tbNom.Text != "")
            {
                if (lbLogin.Items.Contains(tbNom.Text))
                {
                    pbCorrect1.Visible = true;
                    pbIncorrect1.Visible = false;
                }
                else
                {
                    pbCorrect1.Visible = false;
                    pbIncorrect1.Visible = true;
                }
            }
        }

        private void tbMdp_TextChanged(object sender, EventArgs e)
        {
            int countt2 = 0;

            foreach (var o in lbLogin.Items)
            {
                if (lbLogin.Items[countt2].ToString() == tbNom.Text)
                {
                    // recupérer la valeur count
                    // MessageBox.Show(countt.ToString());   
                }
                else
                {
                    if(countt2 == 5)
                    {
                        countt2 = 0;
                    }

                    countt2++;
                }
            }

            if (tbMdp.Text != "")
            {

                // MessageBox.Show(countt2.ToString());
                
                if (tbMdp.Text == lbMdp.Items[countt2].ToString())
                {
                    pbCorrect2.Visible = true;
                    pbIncorrect2.Visible = false;
                }
                else
                {
                    pbCorrect2.Visible = false;
                    pbIncorrect2.Visible = true;
                }
                
            }
        }
    }
}
