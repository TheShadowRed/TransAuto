/*
 * Created by SharpDevelop.
 * User: TheRedLord
 * Date: 8/31/2017
 * Time: 17:32
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DataGridViewAutoFilter;
using MySql.Data.MySqlClient;

namespace TransAuto
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public String baza="";
		public String vesiune="";
		public String host="";
		public String cod="";
		public String locatie="";
		public String flag="";
		public String myStringCon="";
		public String loadTableQuery="";
		public String loadTableQueryAll="";
		public String id_auto="";
		public String loadtipTabel="";
		public String LoadTipActeDetali="";
		public String LoadTipActe="";
		
		public static  System.Windows.Forms.DataGridView dataGridView2;
        public static  System.Windows.Forms.DataGridView dataGridView3;
		public static  System.Windows.Forms.DataGridView dataGridView5;
		public static  System.Windows.Forms.DataGridView dataGridView4;
		
		StatusStrip statusStrip1 = new StatusStrip();
        StatusStrip statusStrip3 = new StatusStrip();
        StatusStrip statusStrip4 = new StatusStrip();
        StatusStrip statusStrip5 = new StatusStrip();
		ToolStripStatusLabel filterStatusLabel = new ToolStripStatusLabel();
        ToolStripStatusLabel showAllLabel = new ToolStripStatusLabel("show all");
        ToolStripStatusLabel custom = new ToolStripStatusLabel("custom");
		ToolStripStatusLabel filterStatusLabel3 = new ToolStripStatusLabel();
        ToolStripStatusLabel showAllLabel3 = new ToolStripStatusLabel("show all");
        ToolStripStatusLabel custom3 = new ToolStripStatusLabel("custom");
		ToolStripStatusLabel filterStatusLabel4 = new ToolStripStatusLabel();
        ToolStripStatusLabel showAllLabel4 = new ToolStripStatusLabel("show all");
        ToolStripStatusLabel custom4 = new ToolStripStatusLabel("custom");
		ToolStripStatusLabel filterStatusLabel5 = new ToolStripStatusLabel();
        ToolStripStatusLabel showAllLabel5 = new ToolStripStatusLabel("show all");
        ToolStripStatusLabel custom5 = new ToolStripStatusLabel("custom");


        
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			GetString();
			LoadTableQueryInit();
			comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
			SetStareInterfata();
			//comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		public void SetStareInterfata()
		{
			button2.Enabled=false;
			//combobox proprietari
using(MySqlConnection conn = new MySqlConnection(myStringCon))
{
    MySqlCommand command = new MySqlCommand("TransAuto_S_Furnizori", conn);
    command.CommandType = System.Data.CommandType.StoredProcedure;
    conn.Open();

      using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboBox4.Items.Add(reader["nume"].ToString());
                            comboBox4.Sorted=true;
                        }
                    }
}
		}
		public void GetString()
		{
		 baza=System.Configuration.ConfigurationManager.AppSettings["baza"].ToString();
		 vesiune=System.Configuration.ConfigurationManager.AppSettings["vesiune"].ToString();
		 host=System.Configuration.ConfigurationManager.AppSettings["host"].ToString();
		 cod=System.Configuration.ConfigurationManager.AppSettings["cod"].ToString();
		 locatie=System.Configuration.ConfigurationManager.AppSettings["locatie"].ToString();
		 flag=System.Configuration.ConfigurationManager.AppSettings["flag"].ToString();
		
        myStringCon = "SERVER=" + host + ";" +
                 "DATABASE=" + baza + ";" +
                 "UID=user; PASSWORD=pass" + ";pooling=true;Allow Zero Datetime=True;Min Pool Size=1; Max Pool Size=100; default command timeout=120";
            
        loadTableQuery=System.Configuration.ConfigurationManager.AppSettings["loadTableQuery"].ToString();
               		
        //LoadDetaliQuery=System.Configuration.ConfigurationManager.AppSettings["LoadDetaliQuery"].ToString();
               		 
               
		}
		public void LoadTableQueryInit()
		{
			String query="";
				using (var connP = new MySqlConnection(myStringCon))
						{
     					connP.Open();
     					using (MySqlCommand cmdP = connP.CreateCommand())
     						{
          						//cmdP.Parameters.Add(new MySqlParameter("@an_lucru", id_raport));
          						cmdP.CommandText = "select query from trans_query";
          						using (MySqlDataReader readerP = cmdP.ExecuteReader())
          						{
               						while (readerP.Read())
               							{
               							//elementele de la header
               							query=readerP["query"].ToString();
               							
               }	
          }
     }
}
				string[] lines = query.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
				loadTableQuery=lines[0];
				loadTableQueryAll=lines[1];
				loadtipTabel=lines[2];
				LoadTipActeDetali=lines[3];
				LoadTipActe=lines[4];
				LoadTableInit();
		}
		private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up))
            {
                DataGridViewAutoFilterColumnHeaderCell filterCell =
                    dataGridView2.CurrentCell.OwningColumn.HeaderCell as
                    DataGridViewAutoFilterColumnHeaderCell;
                if (filterCell != null)
                {
                    filterCell.ShowDropDownList();
                    e.Handled = true;
                }
            }
        }
		public void LoadTableInit()
		{
			using (var connP = new MySqlConnection(myStringCon))
						{
     					connP.Open();
     					using (MySqlCommand cmdP = connP.CreateCommand())
     						{
          						//cmdP.Parameters.Add(new MySqlParameter("@an_lucru", id_raport));
          						cmdP.CommandText = LoadTipActe + " where tip = 'M'";
          						using (MySqlDataReader readerP = cmdP.ExecuteReader())
          						{
          							
          							while (readerP.Read())
               							{
          									comboBox3.Items.Add(readerP["nume"].ToString());
          									
               							}	
          							
          						}
     						}
						}
		DataSet ds = new DataSet("DataSet_ListaTir");
		DataTable dt = new DataTable("DataTable_ListaTir");
			using (MySqlConnection con = new MySqlConnection(myStringCon))
    {
        MySqlCommand command = new MySqlCommand("select_tabelAuto;", con);
    command.CommandType = System.Data.CommandType.StoredProcedure;

    // Add your parameters here if you need them
    //command.Parameters.Add(new MySqlParameter("id_auto_i", id_auto));
    con.Open();
    MySqlDataAdapter Da = new MySqlDataAdapter(command);
          						 
                						Da.Fill(dt);
    
    }
			dataGridView2 = new DataGridView();
			dataGridView2.Name = "dataGridView2";
            dataGridView2.Dock = DockStyle.Fill;
            dataGridView2.RowHeadersVisible=false;
            dataGridView2.AllowUserToAddRows=false;
            dataGridView2.ReadOnly=true;
            dataGridView2.CellClick +=dataGridView1_CellClick;
            //dataGridView2.CellPainting+=dataGridView2_CellPainting;
            //asta adauga alea sus
            dataGridView2.BindingContextChanged += new EventHandler(dataGridView2_BindingContextChanged);
            dataGridView2.KeyDown += new KeyEventHandler(dataGridView2_KeyDown);
            dataGridView2.DataBindingComplete +=
                new DataGridViewBindingCompleteEventHandler(
                dataGridView2_DataBindingComplete);
            showAllLabel.Visible = false;
            showAllLabel.IsLink = true;
            showAllLabel.LinkBehavior = LinkBehavior.HoverUnderline;
            showAllLabel.Click += new EventHandler(showAllLabel_Click);
 			custom.Visible = false;
            custom.IsLink = true;
            custom.LinkBehavior = LinkBehavior.HoverUnderline;
            custom.Click += new EventHandler(showAllLabel_Click);
            statusStrip1.Cursor = Cursors.Default;
            statusStrip1.Items.AddRange(new ToolStripItem[] { 
                filterStatusLabel, showAllLabel });


            //this.Text = "Raportare";
            this.Width *= 3;
            this.Height *= 2;
            panel1.Controls.AddRange(new Control[] {
                dataGridView2, statusStrip1 });
            BindingSource dataSource = new BindingSource(dt, null);
            dataGridView2.DataSource=dataSource;
            dataGridView2.Columns[0].MinimumWidth=3;
       
            //dataGridView2.Columns[1].MinimumWidth=220;
            //dataGridView2.Columns[2].MinimumWidth=320;
            //dataGridView2.Columns[3].MinimumWidth=220;
            //dataGridView2.Columns[0].Visible = false;
            //dataGridView2.Columns[5].Visible = false;
            //dataGridView2.Columns[6].Visible = false;
            //dataGridView2.Columns[7].Visible = false;
            //dataGridView2.Columns[8].Visible = false;
            //dataGridView2.Columns[9].Visible = false;
            //dataGridView2.Columns[10].Visible = false;
         
				using (var connP = new MySqlConnection(myStringCon))
						{
     					connP.Open();
     					using (MySqlCommand cmdP = connP.CreateCommand())
     						{
          						//cmdP.Parameters.Add(new MySqlParameter("@an_lucru", id_raport));
          						cmdP.CommandText = loadtipTabel;
          						using (MySqlDataReader readerP = cmdP.ExecuteReader())
          						{
          							
          							while (readerP.Read())
               							{
          									comboBox1.Items.Add(readerP["nume"].ToString());
          									
               							}	
          							
          						}
     						}
						}
				
				dataGridView2.Columns[4].Visible = false;
				dataGridView2.Columns[5].Visible = false;
				loadTableSoferi();
		}
		private void dataGridView4_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
		 	foreach (DataGridViewRow row in dataGridView4.Rows)
            {

                //if (row.Cells["id"].Value.ToString() == "0") //if check ==0
                //{
                    //row.DefaultCellStyle.BackColor = Color.Red;
					//row.DefaultCellStyle.ForeColor = Color.Blue;                    //then change row color to red
                //} 

            }
            String filterStatus4 = DataGridViewAutoFilterColumnHeaderCell
                .GetFilterStatus(dataGridView4);
            if (String.IsNullOrEmpty(filterStatus4))
            {
                showAllLabel4.Visible = false;
                filterStatusLabel4.Visible = false;
            }
            else
            {
                showAllLabel4.Visible = true;
                filterStatusLabel4.Visible = true;
                filterStatusLabel4.Text = filterStatus4;
            }
        }
		public void loadTableSoferi()
		{
			using (var connP = new MySqlConnection(myStringCon))
						{
     					connP.Open();
     					using (MySqlCommand cmdP = connP.CreateCommand())
     						{
          						//cmdP.Parameters.Add(new MySqlParameter("@an_lucru", id_raport));
          						cmdP.CommandText = LoadTipActe + " where tip = 'S'";
          						using (MySqlDataReader readerP = cmdP.ExecuteReader())
          						{
          							
          							while (readerP.Read())
               							{
          									comboBox2.Items.Add(readerP["nume"].ToString());
          									
               							}	
          							
          						}
     						}
						}
			
			
			
			
			
			DataSet ds = new DataSet("DataSet_ListaTir");
		DataTable dt = new DataTable("DataTable_ListaTir");
			using (MySqlConnection con = new MySqlConnection(myStringCon))
    {
        MySqlCommand command = new MySqlCommand("Select_angajati;", con);
    command.CommandType = System.Data.CommandType.StoredProcedure;

    // Add your parameters here if you need them
    //command.Parameters.Add(new MySqlParameter("id_auto_i", id_auto));
    con.Open();
    MySqlDataAdapter Da = new MySqlDataAdapter(command);
          						 
                						Da.Fill(dt);
    
    }
			dataGridView4 = new DataGridView();
			  BindingSource dataSource = new BindingSource(dt, null);
            dataGridView4.DataSource=dataSource;
			dataGridView4.Name = "dataGridView4";
            dataGridView4.Dock = DockStyle.Fill;
            dataGridView4.RowHeadersVisible=false;
            dataGridView4.ReadOnly=true;
            dataGridView4.AllowUserToAddRows=false;
            dataGridView4.CellClick +=dataGridView4_CellClick;
            //dataGridView2.CellPainting+=dataGridView2_CellPainting;
            //asta adauga alea sus
            dataGridView4.BindingContextChanged += new EventHandler(dataGridView4_BindingContextChanged);
            dataGridView4.KeyDown += new KeyEventHandler(dataGridView4_KeyDown);
            dataGridView4.DataBindingComplete +=
                new DataGridViewBindingCompleteEventHandler(
                dataGridView4_DataBindingComplete);
            showAllLabel4.Visible = false;
            showAllLabel4.IsLink = true;
            showAllLabel4.LinkBehavior = LinkBehavior.HoverUnderline;
            showAllLabel4.Click += new EventHandler(showAllLabel_Click);
 			custom4.Visible = false;
            custom4.IsLink = true;
            custom4.LinkBehavior = LinkBehavior.HoverUnderline;
            custom4.Click += new EventHandler(showAllLabel_Click);
            statusStrip4.Cursor = Cursors.Default;
            statusStrip4.Items.AddRange(new ToolStripItem[] { 
                filterStatusLabel4, showAllLabel4 });


            //this.Text = "Raportare";
            this.Width *= 3;
            this.Height *= 2;
            
          	panel3.Controls.AddRange(new Control[] {
                dataGridView4, statusStrip4 });
            //ModTableSoferi();
            
		}
		public void ModTableSoferi()
		{
			dataGridView4.Columns[0].Width=105;
			dataGridView4.Columns[2].Visible = false;
			dataGridView4.Columns[3].Visible = false;
			dataGridView4.Columns[4].Visible = false;
			dataGridView4.Columns[5].Visible = false;
			dataGridView4.Columns[6].Visible = false;
			dataGridView4.Columns[7].Visible = false;
			dataGridView4.Columns[8].Visible = false;
			dataGridView4.Columns[9].Visible = false;
			dataGridView4.Columns[10].Visible = false;
			dataGridView4.Columns[11].Visible = false;
			dataGridView4.Columns[12].Visible = false;
			dataGridView4.Columns[13].Visible = false;
			dataGridView4.Columns[14].Visible = false;
			dataGridView4.Columns[15].Visible = false;
			dataGridView4.Columns[16].Visible = false;
			dataGridView4.Columns[17].Visible = false;
			dataGridView4.Columns[18].Visible = false;
			dataGridView4.Columns[19].Visible = false;
			dataGridView4.Columns[20].Visible = false;
			dataGridView4.Columns[21].Visible = false;
			dataGridView4.Columns[22].Visible = false;
			dataGridView4.Columns[23].Visible = false;
			dataGridView4.Columns[24].Visible = false;
			dataGridView4.Columns[25].Visible = false;
			dataGridView4.Columns[26].Visible = false;
			dataGridView4.Columns[27].Visible = false;
			dataGridView4.Columns[28].Visible = false;
			
		}
		private void dataGridView4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up))
            {
                DataGridViewAutoFilterColumnHeaderCell filterCell =
                    dataGridView4.CurrentCell.OwningColumn.HeaderCell as
                    DataGridViewAutoFilterColumnHeaderCell;
                if (filterCell != null)
                {
                    filterCell.ShowDropDownList();
                    e.Handled = true;
                }
            }
        }
		private void dataGridView4_BindingContextChanged(object sender, EventArgs e)
        {
            // Continue only if the data source has been set.
            if (dataGridView4.DataSource == null)
            {
                return;
            }

            // Add the AutoFilter header cell to each column.
            foreach (DataGridViewColumn col in dataGridView4.Columns)
            {
                col.HeaderCell = new
                    DataGridViewAutoFilterColumnHeaderCell(col.HeaderCell);
            }

            // Format the OrderTotal column as currency. 
            //dataGridView2.Columns["OrderTotal"].DefaultCellStyle.Format = "c";

            // Resize the columns to fit their contents.
            dataGridView4.AutoResizeColumns();
        }
		private void showAllLabel_Click(object sender, EventArgs e)
        {
        	MessageBox.Show("MESAJ");
            //DataGridViewAutoFilterColumnHeaderCell.RemoveFilter(dataGridView1);
        }
		public void loadsecoundTAbleAngajati()
		{
			int b = dataGridView4.CurrentCellAddress.Y;
			 id_auto=dataGridView4.Rows[b].Cells[16].Value.ToString();
			panel4.Controls.Clear();
		DataSet ds = new DataSet("DataSet_ListaTir");
		DataTable dt = new DataTable("DataTable_ListaTir");
		using (MySqlConnection con = new MySqlConnection(myStringCon))
    {
        MySqlCommand command = new MySqlCommand("select_angajati_specific;", con);
    command.CommandType = System.Data.CommandType.StoredProcedure;

    // Add your parameters here if you need them
    command.Parameters.Add(new MySqlParameter("nr_crt_i", id_auto));
    con.Open();
    MySqlDataAdapter Da = new MySqlDataAdapter(command);
          						 
                						Da.Fill(dt);
    
    }
			
			dataGridView5 = new DataGridView();
			 BindingSource dataSource = new BindingSource(dt, null);
            dataGridView5.DataSource=dataSource;
			dataGridView5.Name = "dataGridView5";
            dataGridView5.Dock = DockStyle.Fill;
            dataGridView5.RowHeadersVisible=false;
            dataGridView5.ReadOnly=true;
            dataGridView5.AllowUserToAddRows=false;
            dataGridView5.CellClick +=dataGridView5_CellClick;
            //asta adauga alea sus
            dataGridView5.BindingContextChanged += new EventHandler(dataGridView5_BindingContextChanged);
            dataGridView5.KeyDown += new KeyEventHandler(dataGridView5_KeyDown);
            dataGridView5.DataBindingComplete +=
                new DataGridViewBindingCompleteEventHandler(
                dataGridView5_DataBindingComplete);
            showAllLabel5.Visible = false;
            showAllLabel5.IsLink = true;
            showAllLabel5.LinkBehavior = LinkBehavior.HoverUnderline;
            showAllLabel5.Click += new EventHandler(showAllLabel_Click);
 			custom5.Visible = false;
            custom5.IsLink = true;
            custom5.LinkBehavior = LinkBehavior.HoverUnderline;
            custom5.Click += new EventHandler(showAllLabel_Click);
            statusStrip5.Cursor = Cursors.Default;
            statusStrip5.Items.AddRange(new ToolStripItem[] { 
                filterStatusLabel5, showAllLabel5 });


            //this.Text = "Raportare";
            this.Width *= 3;
            this.Height *= 2;
            
           
            textBox12.Text=dataGridView4.Rows[b].Cells[0].Value.ToString();
            textBox13.Text=dataGridView4.Rows[b].Cells[1].Value.ToString();
            textBox14.Text=dataGridView4.Rows[b].Cells[2].Value.ToString();
			textBox15.Text=dataGridView4.Rows[b].Cells[3].Value.ToString();
			textBox16.Text=dataGridView4.Rows[b].Cells[6].Value.ToString();
			textBox17.Text=dataGridView4.Rows[b].Cells[9].Value.ToString();
			textBox18.Text=dataGridView4.Rows[b].Cells[10].Value.ToString();
			textBox19.Text=dataGridView4.Rows[b].Cells[15].Value.ToString();
			textBox20.Text=dataGridView4.Rows[b].Cells[27].Value.ToString();
			textBox21.Text=dataGridView4.Rows[b].Cells[28].Value.ToString();
		
            //dataGridView5.Columns[1].Visible = false;
			//dataGridView5.Columns[2].Visible = false;
			panel4.Controls.AddRange(new Control[] {
                dataGridView5, statusStrip5 });
		}
		private void dataGridView5_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
		 	foreach (DataGridViewRow row in dataGridView5.Rows)
            {

                //if (row.Cells["id"].Value.ToString() == "0") //if check ==0
                //{
                    //row.DefaultCellStyle.BackColor = Color.Red;
					//row.DefaultCellStyle.ForeColor = Color.Blue;                    //then change row color to red
                //} 

            }
            String filterStatus5 = DataGridViewAutoFilterColumnHeaderCell
                .GetFilterStatus(dataGridView5);
            if (String.IsNullOrEmpty(filterStatus5))
            {
                showAllLabel5.Visible = false;
                filterStatusLabel5.Visible = false;
            }
            else
            {
                showAllLabel5.Visible = true;
                filterStatusLabel5.Visible = true;
                filterStatusLabel5.Text = filterStatus5;
            }
        }
		private void dataGridView5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up))
            {
                DataGridViewAutoFilterColumnHeaderCell filterCell5 =
                    dataGridView5.CurrentCell.OwningColumn.HeaderCell as
                    DataGridViewAutoFilterColumnHeaderCell;
                if (filterCell5 != null)
                {
                    filterCell5.ShowDropDownList();
                    e.Handled = true;
                }
            }
        }
		private void dataGridView5_BindingContextChanged(object sender, EventArgs e)
        {
            // Continue only if the data source has been set.
            if (dataGridView5.DataSource == null)
            {
                return;
            }

            // Add the AutoFilter header cell to each column.
            foreach (DataGridViewColumn col in dataGridView5.Columns)
            {
                col.HeaderCell = new
                    DataGridViewAutoFilterColumnHeaderCell(col.HeaderCell);
            }

            // Format the OrderTotal column as currency. 
            //dataGridView2.Columns["OrderTotal"].DefaultCellStyle.Format = "c";

            // Resize the columns to fit their contents.
            dataGridView5.AutoResizeColumns();
        }
		private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			
			//panoul cu butoane de la acte soferi
				//panel11.Enabled= true;
				loadsecoundTAbleAngajati();
				//button5.Enabled=true;
				//drawColorsIntable();
		}
		private void dataGridView5_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			//button6.Enabled=true;
			int b = dataGridView5.CurrentCellAddress.Y;
			textBox22.Text=dataGridView5.Rows[b].Cells[5].Value.ToString();
			comboBox2.Text=findbyName("trans_tip_acte","nume",dataGridView5.Rows[b].Cells[2].Value.ToString(),"id");
			dateTimePicker3.Value = Convert.ToDateTime(dataGridView5.Rows[b].Cells[3].Value.ToString());   
			dateTimePicker4.Value = Convert.ToDateTime(dataGridView5.Rows[b].Cells[4].Value.ToString());
			
		}
			public String findbyName(String tableName,String searchThis,String searchForThis,String searchInThis)
		{
				using (var connP = new MySqlConnection(myStringCon))
						{
     					connP.Open();
     					using (MySqlCommand cmdP = connP.CreateCommand())
     						{
          						//cmdP.Parameters.Add(new MySqlParameter("@an_lucru", id_raport));
          						cmdP.CommandText = "select "+searchThis+" from "+tableName+" where " +searchInThis+" = '"+searchForThis+"'";
          						using (MySqlDataReader readerP = cmdP.ExecuteReader())
          						{
               						while (readerP.Read())
               							{
               							//elementele de la header
               							searchThis=readerP[searchThis].ToString();
               							}	
          						}
     						}
						}
				return searchThis;
		}
		private void dataGridView2_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
		 	foreach (DataGridViewRow row in dataGridView2.Rows)
            {

                //if (row.Cells["id"].Value.ToString() == "0") //if check ==0
                //{
                    //row.DefaultCellStyle.BackColor = Color.Red;
					//row.DefaultCellStyle.ForeColor = Color.Blue;                    //then change row color to red
                //} 

            }
            String filterStatus = DataGridViewAutoFilterColumnHeaderCell
                .GetFilterStatus(dataGridView2);
            if (String.IsNullOrEmpty(filterStatus))
            {
                showAllLabel.Visible = false;
                filterStatusLabel.Visible = false;
            }
            else
            {
                showAllLabel.Visible = true;
                filterStatusLabel.Visible = true;
                filterStatusLabel.Text = filterStatus;
            }
        }
		private void dataGridView2_BindingContextChanged(object sender, EventArgs e)
        {
            // Continue only if the data source has been set.
            if (dataGridView2.DataSource == null)
            {
                return;
            }

            // Add the AutoFilter header cell to each column.
            foreach (DataGridViewColumn col in dataGridView2.Columns)
            {
                col.HeaderCell = new
                    DataGridViewAutoFilterColumnHeaderCell(col.HeaderCell);
            }

            // Format the OrderTotal column as currency. 
            //dataGridView2.Columns["OrderTotal"].DefaultCellStyle.Format = "c";

            // Resize the columns to fit their contents.
            dataGridView2.AutoResizeColumns();
        }
		public void LoadTableDetaliForSpecificID(String idAuto)
		{
			//metoda doi care nu a mers
			//DataSet ds = new DataSet("DataSet_ListaActe");
			//DataTable dt = new DataTable("DataTable_Master");
			//using (var connP = new MySqlConnection(myStringCon))
			//			{
     		//			connP.Open();
     		//			using (MySqlCommand cmdP = connP.CreateCommand())
     		//				{
     		//					
          	//					cmdP.CommandText = LoadTipActeDetali + " where id_auto = ' "+idAuto+" ' ";
          	/*					//cmdP.Parameters.AddWithValue("@schimb", flag);
          						//cmdP.Parameters.AddWithValue("@flag", flag);
          						MySqlDataAdapter Da = new MySqlDataAdapter(cmdP);
          						 
                						Da.Fill(dt);
     						}
						}
			DataTable dtDetail = new DataTable("DataTable_ListaTir");
			using (var connP = new MySqlConnection(myStringCon))
						{
     					connP.Open();
     					using (MySqlCommand cmdP = connP.CreateCommand())
     						{
     							
          						cmdP.CommandText = LoadTipActeDetali + " where id_auto = ' "+idAuto+" ' ";
          						//cmdP.Parameters.AddWithValue("@schimb", flag);
          						//cmdP.Parameters.AddWithValue("@flag", flag);
          						MySqlDataAdapter Da = new MySqlDataAdapter(cmdP);
          						 
                						Da.Fill(dtDetail);
     						}
						}
			ds.Tables.Add(dt);  
    		ds.Tables.Add(dtDetail);    
			DataRelation Datatablerelation = new DataRelation("DetailsMarks", ds.Tables[0].Columns[0], ds.Tables[1].Columns[0], true); 
			ds.Relations.Add(Datatablerelation);  
			dataGrid1.DataSource = ds.Tables[1];
			*/
			
			//tabPage2.Controls.Clear();
			//tabPage2.Controls.Add(panel11);
			// To bind the Master data to List 
			
			//metoda pentru tabel in tabel
			
			
            ///Master_BindData(idAuto);
			
            // To bind the Detail data to List 
            //Detail_BindData();
            
            //MasterGrid_Initialize(idAuto);

            //DetailGrid_Initialize();
            secoundTableWithTabs(idAuto);
		}
		public void secoundTableWithTabs(String idauto)
		{
			panel2.Controls.Clear();
			int b = dataGridView2.CurrentCellAddress.Y;
			 id_auto=dataGridView2.Rows[b].Cells[4].Value.ToString();
			//panel5.Controls.Clear();
		DataSet ds = new DataSet("DataSet_ListaTir");
		DataTable dt = new DataTable("DataTable_ListaTir");
		using (MySqlConnection con = new MySqlConnection(myStringCon))
    {
        MySqlCommand command = new MySqlCommand("select_acte_auto;", con);
    command.CommandType = System.Data.CommandType.StoredProcedure;

    // Add your parameters here if you need them
    command.Parameters.Add(new MySqlParameter("id_auto_i", id_auto));
    con.Open();
    MySqlDataAdapter Da = new MySqlDataAdapter(command);
          						 
                						Da.Fill(dt);
    
    }
			/*using (var connP = new MySqlConnection(myStringCon))
						{
     					connP.Open();
     					using (MySqlCommand cmdP = connP.CreateCommand())
     						{
     							
          						cmdP.CommandText = LoadTipActeDetali+" where id_auto = '"+idauto+"' and flag = '0'";
          						//cmdP.Parameters.AddWithValue("@schimb", flag);
          						//cmdP.Parameters.AddWithValue("@flag", flag);
          						MySqlDataAdapter Da = new MySqlDataAdapter(cmdP);
          						 
                						Da.Fill(dt);
     						}
						}*/
			dataGridView3 = new DataGridView();
			BindingSource dataSource3 = new BindingSource(dt, null);
			dataGridView3.DataSource=dataSource3;
			dataGridView3.Name = "dataGridView3";
            dataGridView3.Dock = DockStyle.Fill;
            dataGridView3.RowHeadersVisible=false;
            dataGridView3.ReadOnly=true;
            dataGridView3.AllowUserToAddRows=false;
            dataGridView3.CellClick +=dataGridView2_CellClick;
            //asta adauga alea sus
            dataGridView3.BindingContextChanged += new EventHandler(dataGridView3_BindingContextChanged);
            dataGridView3.KeyDown += new KeyEventHandler(dataGridView3_KeyDown);
            dataGridView3.DataBindingComplete +=
                new DataGridViewBindingCompleteEventHandler(
                dataGridView3_DataBindingComplete);
            showAllLabel3.Visible = false;
            showAllLabel3.IsLink = true;
            showAllLabel3.LinkBehavior = LinkBehavior.HoverUnderline;
            showAllLabel3.Click += new EventHandler(showAllLabel_Click);
 			custom3.Visible = false;
            custom3.IsLink = true;
            custom3.LinkBehavior = LinkBehavior.HoverUnderline;
            custom3.Click += new EventHandler(showAllLabel_Click);
            statusStrip3.Cursor = Cursors.Default;
            statusStrip3.Items.AddRange(new ToolStripItem[] { 
                filterStatusLabel3, showAllLabel3 });


            //this.Text = "Raportare";
            this.Width *= 3;
            this.Height *= 2;
            panel2.Controls.AddRange(new Control[] {
                dataGridView3, statusStrip3 });
            
            
            
			dataGridView3.Columns[1].Visible = false;
			dataGridView3.Columns[2].Visible = false;
			button2.Enabled=true;
			button2.BringToFront();
		}
		private void dataGridView3_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
		 	foreach (DataGridViewRow row in dataGridView3.Rows)
            {

                //if (row.Cells["id"].Value.ToString() == "0") //if check ==0
                //{
                    //row.DefaultCellStyle.BackColor = Color.Red;
					//row.DefaultCellStyle.ForeColor = Color.Blue;                    //then change row color to red
                //} 

            }
            String filterStatus3 = DataGridViewAutoFilterColumnHeaderCell
                .GetFilterStatus(dataGridView3);
            if (String.IsNullOrEmpty(filterStatus3))
            {
                showAllLabel3.Visible = true;
                filterStatusLabel3.Visible = true;
            }
            else
            {
                showAllLabel3.Visible = true;
                filterStatusLabel3.Visible = true;
                filterStatusLabel3.Text = filterStatus3;
            }
        }
			private void dataGridView3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up))
            {
                DataGridViewAutoFilterColumnHeaderCell filterCell3 =
                    dataGridView3.CurrentCell.OwningColumn.HeaderCell as
                    DataGridViewAutoFilterColumnHeaderCell;
                if (filterCell3 != null)
                {
                    filterCell3.ShowDropDownList();
                    e.Handled = true;
                }
            }
        }
		private void dataGridView3_BindingContextChanged(object sender, EventArgs e)
        {
            // Continue only if the data source has been set.
            if (dataGridView3.DataSource == null)
            {
                return;
            }

            // Add the AutoFilter header cell to each column.
            foreach (DataGridViewColumn col in dataGridView3.Columns)
            {
                col.HeaderCell = new
                    DataGridViewAutoFilterColumnHeaderCell(col.HeaderCell);
            }

            // Format the OrderTotal column as currency. 
            //dataGridView2.Columns["OrderTotal"].DefaultCellStyle.Format = "c";

            // Resize the columns to fit their contents.
            dataGridView3.AutoResizeColumns();
        }
		private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			//button4.Enabled=true;
			int b = dataGridView3.CurrentCellAddress.Y;
			textBox10.Text=dataGridView3.Rows[b].Cells[6].Value.ToString();
			comboBox3.Text=dataGridView3.Rows[b].Cells[3].Value.ToString();
			dateTimePicker1.Value = Convert.ToDateTime(dataGridView3.Rows[b].Cells[4].Value.ToString());   
			dateTimePicker2.Value = Convert.ToDateTime(dataGridView3.Rows[b].Cells[5].Value.ToString());

		}
		public void drawColorsIntable()
		{
			foreach (DataGridViewRow row in dataGridView3.Rows)
			{
				string enddate = row.Cells["data_stop"].Value.ToString();
				DateTime enteredDate = DateTime.Parse(enddate);
				//var parameterDate = DateTime.ParseExact("03/26/2015", "MM/dd/yyyy", CultureInfo.InvariantCulture);
var todaysDate = DateTime.Today;
DateTime MyNewDateValue = todaysDate.AddDays(3);

if(enteredDate <= MyNewDateValue)
{
				//row.DefaultCellStyle.BackColor = Color.Blue;
				row.DefaultCellStyle.ForeColor= Color.Blue;
}
if(enteredDate <= todaysDate)
{
				row.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 225);;
				row.DefaultCellStyle.ForeColor= Color.Red;
}
   			}
		}
		private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			foreach (DataGridViewRow row in dataGridView2.Rows)
            {
            	row.DefaultCellStyle.BackColor = Color.White;
				//row.DefaultCellStyle.ForeColor = Color.Blue;                 
            }
			int b = dataGridView2.CurrentCellAddress.Y;
			String datatip=dataGridView2.Rows[b].Cells[0].Value.ToString();
				using (var connP = new MySqlConnection(myStringCon))
						{
     					connP.Open();
     					using (MySqlCommand cmdP = connP.CreateCommand())
     						{
          						//cmdP.Parameters.Add(new MySqlParameter("@an_lucru", id_raport));
          						cmdP.CommandText = loadTableQueryAll+"where id = '"+datatip+"'";
          						using (MySqlDataReader readerP = cmdP.ExecuteReader())
          						{
               						while (readerP.Read())
               							{
               							//elementele de la header
               							textBox1.Text=readerP["marca"].ToString();
               							textBox2.Text=readerP["serie"].ToString();
               							textBox3.Text=readerP["greutate"].ToString();
               							textBox4.Text=readerP["culoare"].ToString();
               							textBox5.Text=readerP["proprietar"].ToString();
               							textBox6.Text=readerP["nr_auto"].ToString();
               							textBox7.Text=readerP["serie_certificat"].ToString();
               							textBox8.Text=readerP["capacitate"].ToString();
               							textBox9.Text=readerP["combustibil"].ToString();
               							textBox11.Text=readerP["consum_normat"].ToString();
               							String id_tir=readerP["tip"].ToString();
               							comboBox1.Text=findbyName("trans_tip_auto","nume",id_tir,"id");
               							
               }	
          }
     }
}
				
				//detali rapoarte
				//get id
				panel4.Enabled= true;
				LoadTableDetaliForSpecificID(datatip);
				//button3.Enabled=true;
				drawColorsIntable();
		}
		public void RefreshDataGrdid3()
		{
			int columnIndex = dataGridView3.CurrentCell.ColumnIndex;
			int rowIndex = dataGridView3.CurrentCell.RowIndex;
			dataGridView1_CellClick(dataGridView2, new DataGridViewCellEventArgs(0, 0));
			dataGridView3.CurrentCell=dataGridView3.Rows[rowIndex].Cells[columnIndex];
			
		}
		void Button1Click(object sender, EventArgs e)
		{
	if(comboBox3.SelectedIndex > -1){
			String tip_auto=findbyName("trans_tip_acte","id",comboBox3.SelectedItem.ToString(),"nume");
			string StartDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
			string StartEnd = dateTimePicker2.Value.ToString("yyyy-MM-dd");
			string observatii=textBox10.Text.ToString();
			int b = dataGridView2.CurrentCellAddress.Y;
			 id_auto=dataGridView2.Rows[b].Cells[4].Value.ToString();
			
			using (MySqlConnection con = new MySqlConnection(myStringCon))
    {
        MySqlCommand command = new MySqlCommand("insert_update_acteAuto;", con);
    command.CommandType = System.Data.CommandType.StoredProcedure;

    // Add your parameters here if you need them
    command.Parameters.Add(new MySqlParameter("id_auto_i", id_auto));
  command.Parameters.Add(new MySqlParameter("id_tip_act_i", tip_auto));
    command.Parameters.Add(new MySqlParameter("data_start_i", StartDate));
      command.Parameters.Add(new MySqlParameter("data_stop_i", StartEnd));
        command.Parameters.Add(new MySqlParameter("observatii_i", observatii));
        
      command.Parameters.Add(new MySqlParameter("data_stop_nou_i", StartEnd));
        command.Parameters.Add(new MySqlParameter("observatii_nou_i", observatii));
    con.Open();
    command.ExecuteNonQuery();
    //int result = (int)command.ExecuteScalar();
    MessageBox.Show("Date Introduse",
    "Trans-auto");
    }
			}else
			{
				MessageBox.Show("Nu ai introdus Tip-ul de Act",
    "Trans-Auto");
			}
			RefreshDataGrdid3();
		}
		void Button2Click(object sender, EventArgs e)
		{
			try{
			int flag=1;
			int b = dataGridView3.CurrentCellAddress.Y;
			id_auto=dataGridView3.Rows[b].Cells[1].Value.ToString();
			//string test=dataGridView3.Rows[0].ToString;
				using (MySqlConnection con = new MySqlConnection(myStringCon))
    {
        MySqlCommand command = new MySqlCommand("delete_dateAct;", con);
    command.CommandType = System.Data.CommandType.StoredProcedure;

    // Add your parameters here if you need them
    command.Parameters.Add(new MySqlParameter("id_act_i", id_auto));
  command.Parameters.Add(new MySqlParameter("flag_i", flag));
 
    con.Open();
    command.ExecuteNonQuery();
    //int result = (int)command.ExecuteScalar();
    MessageBox.Show("Date Sterse",
    "Trans-auto");
    }
			
			RefreshDataGrdid3();
			}catch(Exception)
			{
				
			}
		}
		public void RefreshDataGrdid2()
		{
			using (var connP = new MySqlConnection(myStringCon))
						{
     					connP.Open();
     					using (MySqlCommand cmdP = connP.CreateCommand())
     						{
          						//cmdP.Parameters.Add(new MySqlParameter("@an_lucru", id_raport));
          						cmdP.CommandText = LoadTipActe + " where tip = 'M'";
          						using (MySqlDataReader readerP = cmdP.ExecuteReader())
          						{
          							
          							while (readerP.Read())
               							{
          									comboBox2.Items.Add(readerP["nume"].ToString());
          									
               							}	
          							
          						}
     						}
						}
		DataSet ds = new DataSet("DataSet_ListaTir");
		DataTable dt = new DataTable("DataTable_ListaTir");
				using (MySqlConnection con = new MySqlConnection(myStringCon))
    {
        MySqlCommand command = new MySqlCommand("select_tabelAuto;", con);
    command.CommandType = System.Data.CommandType.StoredProcedure;

    // Add your parameters here if you need them
    //command.Parameters.Add(new MySqlParameter("id_auto_i", id_auto));
    con.Open();
    MySqlDataAdapter Da = new MySqlDataAdapter(command);
          						 
                						Da.Fill(dt);
    
    }

            BindingSource dataSource = new BindingSource(dt, null);
            dataGridView2.DataSource=dataSource;
            
		}
		void Button3Click(object sender, EventArgs e)
		{
		try{
			int flag=1;
			int b = dataGridView2.CurrentCellAddress.Y;
			id_auto=dataGridView2.Rows[b].Cells[4].Value.ToString();
			//string test=dataGridView3.Rows[0].ToString;
			if(dataGridView3.Rows.Count==0)
			{
				using (MySqlConnection con = new MySqlConnection(myStringCon))
    {
        MySqlCommand command = new MySqlCommand("delete_dateAuto;", con);
    command.CommandType = System.Data.CommandType.StoredProcedure;

    // Add your parameters here if you need them
    command.Parameters.Add(new MySqlParameter("id_auto_i", id_auto));
  command.Parameters.Add(new MySqlParameter("flag_i", flag));
 
    con.Open();
    command.ExecuteNonQuery();
    //int result = (int)command.ExecuteScalar();
    MessageBox.Show("Date Sterse",
    "Trans-auto");
    }
			}else
			{
				    MessageBox.Show("Tirul are Acte Valide",
    "Trans-auto");
			}
			RefreshDataGrdid2();
			}catch(Exception)
			{
				
			}
		}
		void Button4Click(object sender, EventArgs e)
		{
			String tip_auto=findbyName("trans_tip_auto","id",comboBox1.SelectedItem.ToString(),"nume");
			String serie_certificat=textBox7.Text.ToString();
			String numar_inmatriculare=textBox6.Text.ToString();
			String marca=textBox1.Text.ToString();
			String serie_sasiu=textBox2.Text.ToString();
			String culoare=textBox4.Text.ToString();
			String proprietar=textBox5.Text.ToString();
			String capacitate=textBox8.Text.ToString();
			String combustibil=textBox9.Text.ToString();
			String consum_normal=textBox11.Text.ToString();
			String greutate=textBox3.Text.ToString();
			String Propietar=comboBox4.SelectedText.ToString();
			using (MySqlConnection con = new MySqlConnection(myStringCon))
    {
        MySqlCommand command = new MySqlCommand("insert_update_DateAuto;", con);
    command.CommandType = System.Data.CommandType.StoredProcedure;

    // Add your parameters here if you need them
    command.Parameters.Add(new MySqlParameter("tip_i", tip_auto));
  command.Parameters.Add(new MySqlParameter("serie_certificat_i", serie_certificat));
    command.Parameters.Add(new MySqlParameter("nr_auto_i", numar_inmatriculare));
      command.Parameters.Add(new MySqlParameter("marca_i", marca));
        command.Parameters.Add(new MySqlParameter("serie_i", serie_sasiu));
      command.Parameters.Add(new MySqlParameter("greutate_i", greutate));
      command.Parameters.Add(new MySqlParameter("culoare_i", culoare));
        command.Parameters.Add(new MySqlParameter("proprietar_i", proprietar));
      command.Parameters.Add(new MySqlParameter("capacitate_i", capacitate));
        command.Parameters.Add(new MySqlParameter("combustibil_i", combustibil));
      command.Parameters.Add(new MySqlParameter("consum_normat_i", consum_normal));
      command.Parameters.Add(new MySqlParameter("proprietar_furnizor_i", Propietar));
    con.Open();
    command.ExecuteNonQuery();
    //int result = (int)command.ExecuteScalar();
    MessageBox.Show("Date Introduse",
    "Trans-auto");
    }
		}
		void Button5Click(object sender, EventArgs e)
		{
	try{
			int b = dataGridView4.CurrentCellAddress.Y;
			id_auto=dataGridView4.Rows[b].Cells[16].Value.ToString();
			//string test=dataGridView3.Rows[0].ToString;
			if(dataGridView5.Rows.Count==0)
			{
				using (MySqlConnection con = new MySqlConnection(myStringCon))
    {
        MySqlCommand command = new MySqlCommand("delete_sofer;", con);
    command.CommandType = System.Data.CommandType.StoredProcedure;

    // Add your parameters here if you need them
    command.Parameters.Add(new MySqlParameter("id_auto_i", id_auto));
 
    con.Open();
    command.ExecuteNonQuery();
    //int result = (int)command.ExecuteScalar();
    MessageBox.Show("Date Sterse",
    "Trans-auto");
    }
			}else
			{
				    MessageBox.Show("Soferul are Acte Valide",
    "Trans-auto");
			}
			RefreshDataGrdid2();
			}catch(Exception)
			{
				
			}
		}
		void Button6Click(object sender, EventArgs e)
		{
	if(comboBox2.SelectedIndex > -1){
			String tip_auto=findbyName("trans_tip_acte","id",comboBox2.SelectedItem.ToString(),"nume");
			string StartDate = dateTimePicker3.Value.ToString("yyyy-MM-dd");
			string StartEnd = dateTimePicker4.Value.ToString("yyyy-MM-dd");
			string observatii=textBox22.Text.ToString();
			int b = dataGridView4.CurrentCellAddress.Y;
			 id_auto=dataGridView4.Rows[b].Cells[16].Value.ToString();
			
			using (MySqlConnection con = new MySqlConnection(myStringCon))
    {
        MySqlCommand command = new MySqlCommand("insert_update_acteSofer;", con);
    command.CommandType = System.Data.CommandType.StoredProcedure;

    // Add your parameters here if you need them
    command.Parameters.Add(new MySqlParameter("id_auto_i", id_auto));
  command.Parameters.Add(new MySqlParameter("id_tip_act_i", tip_auto));
    command.Parameters.Add(new MySqlParameter("data_start_i", StartDate));
      command.Parameters.Add(new MySqlParameter("data_stop_i", StartEnd));
        command.Parameters.Add(new MySqlParameter("observatii_i", observatii));
        
      command.Parameters.Add(new MySqlParameter("data_stop_nou_i", StartEnd));
        command.Parameters.Add(new MySqlParameter("observatii_nou_i", observatii));
    con.Open();
    command.ExecuteNonQuery();
    //int result = (int)command.ExecuteScalar();
    MessageBox.Show("Date Introduse",
    "Trans-auto");
    }
			}else
			{
				MessageBox.Show("Nu ai introdus Tip-ul de Act",
    "Trans-Auto");
			}
			int columnIndex=0 ;
			int rowIndex=0;
			try{
			
			columnIndex = dataGridView5.CurrentCell.ColumnIndex;
			rowIndex = dataGridView5.CurrentCell.RowIndex;
			}catch(Exception){}
			dataGridView4_CellClick(dataGridView2, new DataGridViewCellEventArgs(0, 0));
			try{
			
			dataGridView5.CurrentCell=dataGridView5.Rows[rowIndex].Cells[columnIndex];
			}catch(Exception){}
			
		}
		void Button7Click(object sender, EventArgs e)
		{
		try{
			int flag=1;
			int b = dataGridView4.CurrentCellAddress.Y;
			id_auto=dataGridView4.Rows[b].Cells[16].Value.ToString();
			//string test=dataGridView3.Rows[0].ToString;
				using (MySqlConnection con = new MySqlConnection(myStringCon))
    {
        MySqlCommand command = new MySqlCommand("delete_dateSofer;", con);
    command.CommandType = System.Data.CommandType.StoredProcedure;

    // Add your parameters here if you need them
    command.Parameters.Add(new MySqlParameter("id_act_i", id_auto));
  command.Parameters.Add(new MySqlParameter("flag_i", flag));
 
    con.Open();
    command.ExecuteNonQuery();
    //int result = (int)command.ExecuteScalar();
    MessageBox.Show("Date Sterse",
    "Trans-auto");
    }
			
			int columnIndex=0;
			int rowIndex=0;
			try{
			
			columnIndex = dataGridView5.CurrentCell.ColumnIndex;
			rowIndex = dataGridView5.CurrentCell.RowIndex;
			}catch(Exception){}
			dataGridView4_CellClick(dataGridView2, new DataGridViewCellEventArgs(0, 0));
			try{
			
			dataGridView5.CurrentCell=dataGridView5.Rows[rowIndex].Cells[columnIndex];
			}catch(Exception){}
			}catch(Exception)
			{
				
			}
		}
		void TabControl1SelectedIndexChanged(object sender, EventArgs e)
		{
			if(tabControl1.SelectedIndex==1)
				{
				ModTableSoferi();
				}
			
		}
	}
}
