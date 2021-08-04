/*
 * Created by SharpDevelop.
 * User: TheRedLord
 * Date: 9/8/2017
 * Time: 13:07
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using DataGridViewAutoFilter;
using MySql.Data.MySqlClient;

namespace TransAuto
{
	/// <summary>
	/// Description of CurseForm.
	/// </summary>
	public partial class CurseForm : Form
	{
		public String baza="";
		public String vesiune="";
		public String host="";
		public String cod="";
		public String locatie="";
		public String flag="";
		public DateTime Start;
		public DateTime End;
		public static String myStringCon="";
		public String loadTableQuery="";
		public String loadTableQueryAll="";
		public String id_auto="";
		public String loadtipTabel="";
		public String LoadTipActeDetali="";
		public String LoadTipActe="";
		public String ID_sofer="";
		public String ID_Comanda="";
		public static String ID_ComandaForHarta="";
		
		
		
		
		
		
		
		
		
		ToolStripStatusLabel filterStatusLabel4 = new ToolStripStatusLabel();
        ToolStripStatusLabel showAllLabel4 = new ToolStripStatusLabel("show all");
        ToolStripStatusLabel custom4 = new ToolStripStatusLabel("custom");
        StatusStrip statusStrip4 = new StatusStrip();
		
		public static  System.Windows.Forms.DataGridView dataGridView4;
		
		public CurseForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			GetString();
			LoadComandaTable();
			LoadComboBoxes();
			GetIDFromLastComand();
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
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
		public void LoadComandaTable()
		{
			String dataStart=dateTimePicker3.ToString();
			String dataEnd=dateTimePicker4.ToString();
			
			
			
			DataSet ds = new DataSet("DataSet_ListaTir");
		DataTable dt = new DataTable("DataTable_ListaTir");
			using (MySqlConnection con = new MySqlConnection(myStringCon))
    {
        MySqlCommand command = new MySqlCommand("Select_Comenzi;", con);
    command.CommandType = System.Data.CommandType.StoredProcedure;

    // Add your parameters here if you need them
    //command.Parameters.Add(new MySqlParameter("data_start_i", dataStart));
    //command.Parameters.Add(new MySqlParameter("Data_end_i", dataEnd));
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
            
          	panel2.Controls.AddRange(new Control[] {
                dataGridView4, statusStrip4 });
		}
		public void GetIDFromLastComand()
		{
			foreach (DataGridViewRow row in dataGridView4.Rows)
			{
  				 ID_Comanda += row.Cells["ID"].Value;
  				 ID_sofer += row.Cells["ID_Sofer"].Value;
 				  //More code here
			}
		}
		private void showAllLabel_Click(object sender, EventArgs e)
        {
        	//MessageBox.Show("MESAJ");
            //DataGridViewAutoFilterColumnHeaderCell.RemoveFilter(dataGridView1);
        }
		public void LoadComboBoxes()
		{
			//combobox clienti
using(MySqlConnection conn = new MySqlConnection(myStringCon))
{
    MySqlCommand command = new MySqlCommand("Select_clinet", conn);
    command.CommandType = System.Data.CommandType.StoredProcedure;
    conn.Open();

      using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboBox2.Items.Add(reader["nume"].ToString());
                            comboBox2.Sorted=true;
                        }
                    }
}
			//combobox Masina
			using(MySqlConnection conn = new MySqlConnection(myStringCon))
{
    MySqlCommand command = new MySqlCommand("Select_masina", conn);
    command.CommandType = System.Data.CommandType.StoredProcedure;
    conn.Open();

      using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            String nume=reader["nr_auto"].ToString();
                            comboBox1.Items.Add(nume);
                            comboBox1.Sorted=true;
                        }
                    }
}
			//combobox transportatori
			using(MySqlConnection conn = new MySqlConnection(myStringCon))
{
    MySqlCommand command = new MySqlCommand("Select_transportatori", conn);
    command.CommandType = System.Data.CommandType.StoredProcedure;
    conn.Open();

      using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            String nume=reader["nume"].ToString();
                            comboBox3.Items.Add(nume);
                            comboBox3.Sorted=true;
                        }
                    }
}
			//combobox Moneda
			using(MySqlConnection conn = new MySqlConnection(myStringCon))
{
    MySqlCommand command = new MySqlCommand("Select_moneda", conn);
    command.CommandType = System.Data.CommandType.StoredProcedure;
    conn.Open();

      using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            String nume=reader["nume"].ToString();
                            comboBox4.Items.Add(nume);
                            comboBox4.Sorted=true;
                           
                        }
                         comboBox4.SelectedIndex=0;
                    }
}
		}
		private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			
			int b = dataGridView4.CurrentCellAddress.Y;
			String datatip=dataGridView4.Rows[b].Cells[0].Value.ToString();
			using(MySqlConnection conn = new MySqlConnection(myStringCon))
{
    MySqlCommand command = new MySqlCommand("Select_comanda_specific", conn);
    command.CommandType = System.Data.CommandType.StoredProcedure;

    // Add your parameters here if you need them
    command.Parameters.Add(new MySqlParameter("nr_crt_i", datatip));

    conn.Open();

      using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            textBox2.Text=reader["Nume"].ToString();
                            String id_client=reader["ID_Client"].ToString();
                            comboBox2.Text=GetIDClient(id_client);
                            comboBox3.Text=GetIDClient(reader["ID_Transportator"].ToString());
                            DateTime date1;
                            DateTime.TryParse(reader["Data_comanda"].ToString(), out date1);
                            dateTimePicker1.Value = date1;
                            DateTime date2;
                            DateTime.TryParse(reader["Data_incarcare"].ToString(), out date2);
                            dateTimePicker2.Value = date2;
                            comboBox1.Text=findbyName("Trans_auto","nr_auto",reader["ID_Masina"].ToString(),"id");
                            textBox1.Text=reader["KM_Comanda"].ToString();
                            textBox4.Text=reader["Pret_Unitar"].ToString();
                            textBox9.Text=reader["KM_Harta"].ToString();
                            textBox8.Text=reader["Greutate"].ToString();
                            comboBox4.Text=findbyName("Moneda","Nume",reader["ID_Moneda"].ToString(),"id");
                        }
                    }
}
    		
    	using(MySqlConnection conn = new MySqlConnection(myStringCon))
{
    MySqlCommand command = new MySqlCommand("Select_traseu", conn);
    command.CommandType = System.Data.CommandType.StoredProcedure;

    // Add your parameters here if you need them
    command.Parameters.Add(new MySqlParameter("nr_crt_i", datatip));

    conn.Open();
      MySqlDataAdapter Da = new MySqlDataAdapter(command);
          						 
                						
                						DataTable dt = new DataTable("DataTable_ListaTir");
                						Da.Fill(dt);
                						 BindingSource dataSource = new BindingSource(dt, null);
            dataGridView1.DataSource=dataSource;
}
			
			
			
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
		public string GetIDClient(string id_client_string)
		{
		//combobox clienti
		String ComboboxItem="";
using(MySqlConnection conn = new MySqlConnection(myStringCon))
{
	
    MySqlCommand command = new MySqlCommand("Select_clinet_specific", conn);
    command.CommandType = System.Data.CommandType.StoredProcedure;
    
    command.Parameters.Add(new MySqlParameter("nr_crt_i", id_client_string));
    conn.Open();

      using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ComboboxItem=reader["nume"].ToString();
              	
                        }
                    }
}
              return ComboboxItem;
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
		void DateTimePicker3TabIndexChanged(object sender, EventArgs e)
		{
				
			//MessageBox.Show(dateTimePicker3.ToString());
		}
		void DateTimePicker3ValueChanged(object sender, EventArgs e)
		{
			if(dateTimePicker3.Value<=dateTimePicker4.Value)
			{
			if(Start!=dateTimePicker3.Value.Date)
			{
				LoadComandaTable();
			}
			}
			else
			{
			MessageBox.Show("Data de start Nu poate fi mai mare ca data de sfarsit");
			dateTimePicker3.Value=Start;
			}
		}
		void DateTimePicker4ValueChanged(object sender, EventArgs e)
		{
			if(dateTimePicker3.Value<=dateTimePicker4.Value){
			if(End!=dateTimePicker4.Value.Date)
			{
				LoadComandaTable();
			}
			}
			else
			{
			MessageBox.Show("Data de sfarsit Nu poate fi mai mare ca data de start");
			dateTimePicker4.Value=End;
			}
		}
		void DateTimePicker3Enter(object sender, EventArgs e)
		{
			Start=dateTimePicker3.Value;
		}
		void DateTimePicker4Enter(object sender, EventArgs e)
		{
			End=dateTimePicker3.Value;
		}
		void Button3Click(object sender, EventArgs e)
		{
			
			int b = dataGridView4.CurrentCellAddress.Y;
			ID_Comanda=dataGridView4.Rows[b].Cells[0].Value.ToString();
			if(dataGridView1.Rows.Count == 0){
			//simple open
			
			
			
			ID_ComandaForHarta=ID_Comanda;
			if(ID_ComandaForHarta=="")
			{
				GetIDFromLastComand();
				ID_ComandaForHarta=ID_ComandaForHarta+1;
			}
			
	// Create a new instance of the Form2 class
    Curse settingsForm = new Curse();

    // Show the settings form
    settingsForm.Show();
			}else
			{
				//load map open
				
				
				
				
				
				
			ID_ComandaForHarta=ID_Comanda;
			if(ID_ComandaForHarta=="")
			{
				GetIDFromLastComand();
				ID_ComandaForHarta=ID_ComandaForHarta+1;
			}
			
	// Create a new instance of the Form2 class
    Curse settingsForm = new Curse();

    // Show the settings form
    settingsForm.Show();
    
   // Curse.LoadFromAnother();
			}
		}
		void ComboBox4SelectedIndexChanged(object sender, EventArgs e)
		{
			float y = Int32.Parse(textBox4.Text);
			float x = Int32.Parse(textBox1.Text);
			float z = Int32.Parse(findbyName("Moneda","TVA",comboBox4.Text,"Nume"));
			float result=(x*y*z)/100;
			textBox5.Text=result.ToString();
			float suma=(x*y)+result;
			textBox6.Text=suma.ToString();
		}
		void TextBox1TextChanged(object sender, EventArgs e)
		{
			float y = Int32.Parse(textBox4.Text);
			float x = Int32.Parse(textBox1.Text);
			float z = Int32.Parse(findbyName("Moneda","TVA",comboBox4.Text,"Nume"));
			float result=(x*y*z)/100;
			textBox5.Text=result.ToString();
			float suma=x*y+result;
			textBox6.Text=suma.ToString();
		}
		void TextBox4TextChanged(object sender, EventArgs e)
		{
			float y = Int32.Parse(textBox4.Text);
			float x = Int32.Parse(textBox1.Text);
			float z = Int32.Parse(findbyName("Moneda","TVA",comboBox4.Text,"Nume"));
			float result=(x*y*z)/100;
			textBox5.Text=result.ToString();
			float suma=x*y+result;
			textBox6.Text=suma.ToString();
		}
		void TextBox5TextChanged(object sender, EventArgs e)
		{
			float y = Int32.Parse(textBox4.Text);
			float x = Int32.Parse(textBox1.Text);
			float z = Int32.Parse(findbyName("Moneda","TVA",comboBox4.Text,"Nume"));
			float result=(x*y*z)/100;
			textBox5.Text=result.ToString();
			float suma=x*y+result;
			textBox6.Text=suma.ToString();
		}
		void Button4Click(object sender, EventArgs e)
		{
				//get datas
				//numar Comanda
				String Comanda=textBox2.Text.ToString();
				//dataComanda
				String DataComanda=dateTimePicker1.Value.ToString("yyyy-MM-dd");
				//clinet
				String Client=findbyName("soc1","nr_crt",comboBox2.ToString(),"nume");
				//dataIncarcare
				String DataIncarcare=dateTimePicker2.Value.ToString("yyyy-MM-dd");
				//transportator
				String Transportator=findbyName("soc1","nr_crt",comboBox3.ToString(),"nume");
				//masina
				String Masina=findbyName("trans_auto","id",comboBox1.ToString(),"nr_auto");
				//kilometri comanda
				String KMComanda=textBox1.Text.ToString();
				//pret f TVA
				String PretFTVA=textBox4.Text.ToString();
				//TVA
				String TVA=textBox5.Text.ToString();
				//Valoare
				String Valoare=textBox6.Text.ToString();
				//Greutate Transport
				String Greutate=textBox8.Text.ToString();
				//kilometri harta
				String KMHarta=textBox9.Text.ToString();
				//valoare harta
				String VHart=textBox10.Text.ToString();
				//Moneda
				String Moneda=findbyName("moneda","ID",comboBox4.ToString(),"Nume");
			using (MySqlConnection con = new MySqlConnection(myStringCon))
    {
        MySqlCommand command = new MySqlCommand("Comanda_insert;", con);
    command.CommandType = System.Data.CommandType.StoredProcedure;

    // Add your parameters here if you need them
    //nume comanda placeholder
    command.Parameters.Add(new MySqlParameter("Nume_i", Comanda));
    command.Parameters.Add(new MySqlParameter("ID_Traseu_i", Comanda));
    
    
    command.Parameters.Add(new MySqlParameter("Numar_Comanda_i", Comanda));
  command.Parameters.Add(new MySqlParameter("Data_comanda_i", DataComanda));
    command.Parameters.Add(new MySqlParameter("ID_Client_i", Client));
      command.Parameters.Add(new MySqlParameter("Data_Incarcare_i", DataIncarcare));
        command.Parameters.Add(new MySqlParameter("ID_Transportator_i", Transportator));
      command.Parameters.Add(new MySqlParameter("ID_Masina_i", Masina));
      command.Parameters.Add(new MySqlParameter("KM_Comanda_i", KMComanda));
        command.Parameters.Add(new MySqlParameter("Pret_Unitar_i", PretFTVA));
     // command.Parameters.Add(new MySqlParameter("TVA_i", TVA));
      //  command.Parameters.Add(new MySqlParameter("Valoare_i", Valoare));
      command.Parameters.Add(new MySqlParameter("Greutate_i", Greutate));
        command.Parameters.Add(new MySqlParameter("KM_Harta_i", KMHarta));
     // command.Parameters.Add(new MySqlParameter("VHart_i", VHart));
      command.Parameters.Add(new MySqlParameter("ID_Moneda_i", Moneda));
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
			int flag=1;
			int b = dataGridView4.CurrentCellAddress.Y;
			id_auto=dataGridView4.Rows[b].Cells[0].Value.ToString();
			//string test=dataGridView3.Rows[0].ToString;
			
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
			//RefreshDataGrdid2();
			}catch(Exception)
			{
				
			}
		}
		void Button1Click(object sender, EventArgs e)
		{
	
		}
		void Button6Click(object sender, EventArgs e)
		{
	
		}
	}
}
