/*
 * Created by SharpDevelop.
 * User: TheRedLord
 * Date: 9/6/2017
 * Time: 14:51
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Demo.WindowsForms;
using Demo.WindowsForms.CustomMarkers;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using MySql.Data.MySqlClient;

namespace TransAuto
{
	/// <summary>
	/// Description of Curse.
	/// </summary>
	public partial class Curse : Form
	{
		// layers
      	readonly GMapOverlay top = new GMapOverlay();
      	internal readonly GMapOverlay objects = new GMapOverlay("objects");
      	internal readonly GMapOverlay routes = new GMapOverlay("routes");
      	internal readonly GMapOverlay polygons = new GMapOverlay("polygons");

      	
		// marker
      	GMapMarker currentMarker;
      	GMapRoute currentRoute = null;
      	GMapPolygon currentPolygon = null;
      	public String City="";
      	public String CityStart="";
      	public String CityEnd="";
      	// etc
      	GMapMarkerRect CurentRectMarker = null;
      	
      	GMapMarker currentTransport;
      	
      	PointLatLng lastPosition;
      	PointLatLng start;
        PointLatLng end;
        static Curse mystaticOutput;
     	bool isMouseDown = false;
      	int lastZoom;
      	
      	readonly List<FlightRadarData> flights = new List<FlightRadarData>();
      	
		public Curse()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			mystaticOutput=this;
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			try
			{
   				System.Net.IPHostEntry e =
        			System.Net.Dns.GetHostEntry("www.google.com");
			}
			catch
			{
   				gMapControl1.Manager.Mode = AccessMode.CacheOnly;
   					MessageBox.Show("No internet connection avaible, going to CacheOnly mode.", 
         				"GMap.NET - Demo.WindowsForms", MessageBoxButtons.OK,
         					MessageBoxIcon.Warning);
			}
				 gMapControl1.MapProvider = GMapProviders.OpenStreetMap;
            gMapControl1.Position = new PointLatLng(54.6961334816182, 25.2985095977783);
            gMapControl1.MinZoom = 0;
            gMapControl1.MaxZoom = 24;
            gMapControl1.Zoom = 9;  
				
            	


  	gMapControl1.OnPositionChanged += new PositionChanged(MainMap_OnPositionChanged);

  	gMapControl1.OnTileLoadStart += new TileLoadStart(MainMap_OnTileLoadStart);
  	gMapControl1.OnTileLoadComplete += new TileLoadComplete(MainMap_OnTileLoadComplete);


  	gMapControl1.OnMapZoomChanged += new MapZoomChanged(MainMap_OnMapZoomChanged);
  	gMapControl1.OnMapTypeChanged += new MapTypeChanged(MainMap_OnMapTypeChanged);

  	gMapControl1.OnMarkerClick += new MarkerClick(MainMap_OnMarkerClick);
  	gMapControl1.OnMarkerEnter += new MarkerEnter(MainMap_OnMarkerEnter);
  	gMapControl1.OnMarkerLeave += new MarkerLeave(MainMap_OnMarkerLeave);

  	gMapControl1.OnPolygonEnter += new PolygonEnter(MainMap_OnPolygonEnter);
  	gMapControl1.OnPolygonLeave += new PolygonLeave(MainMap_OnPolygonLeave);

  	gMapControl1.OnRouteEnter += new RouteEnter(MainMap_OnRouteEnter);
  	gMapControl1.OnRouteLeave += new RouteLeave(MainMap_OnRouteLeave);
  	gMapControl1.MouseDown += new MouseEventHandler(MainMap_MouseDown);

  	gMapControl1.Manager.OnTileCacheComplete += new TileCacheComplete(OnTileCacheComplete);
  	gMapControl1.Manager.OnTileCacheStart += new TileCacheStart(OnTileCacheStart);
  	gMapControl1.Manager.OnTileCacheProgress += new TileCacheProgress(OnTileCacheProgress);
  
  			addOverlays();
  			populateComboBoxWihtMaps();
  			StartPozition();
  			ChangeMap();
  			LoadMapFromTraseu();
		}
		public static void  LoadFromAnother()
		{
			mystaticOutput.LoadMapFromTraseu();
		}
		public void LoadMapFromTraseu()
		{
			try{
			using(MySqlConnection conn = new MySqlConnection(CurseForm.myStringCon))
{
    MySqlCommand command = new MySqlCommand("Select_traseu_Cordonation", conn);
    command.CommandType = System.Data.CommandType.StoredProcedure;
    
    command.Parameters.Add(new MySqlParameter("nr_crt_i", CurseForm.ID_ComandaForHarta));
    conn.Open();

      using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PointLatLng startFromTable=start;
                            PointLatLng endFromTable=end;
                            startFromTable.Lat=double.Parse(reader["LatStart"].ToString(), System.Globalization.CultureInfo.InvariantCulture);
                            startFromTable.Lng=double.Parse(reader["LongStart"].ToString(), System.Globalization.CultureInfo.InvariantCulture);
                            endFromTable.Lat=double.Parse(reader["LatEnd"].ToString(), System.Globalization.CultureInfo.InvariantCulture);
                            endFromTable.Lng=double.Parse(reader["LongEnd"].ToString(), System.Globalization.CultureInfo.InvariantCulture);
            double distance;
			String Nume;
			RoutingProvider rp = gMapControl1.MapProvider as RoutingProvider;
         if(rp == null)
         {
            rp = GMapProviders.GoogleMap; // use OpenStreetMap if provider does not implement routing
         }
         //routa
			GDirections ss;
			var xx = GMapProviders.GoogleMap.GetDirections(out ss, startFromTable, endFromTable, false, false, false, false, false);
			
			
         //MapRoute route = rp.GetRoute(start, end, false, false, (int) gMapControl1.Zoom);
         //MapRoute route = rp.GetRoute(start, end, false, false, 15);
         if(xx != null)
         {
            // add route
            
            GMapRoute r = new GMapRoute(ss.Route, "My route");
            distance = r.Distance;
            r.IsHitTestVisible = true;
            r.Stroke.Color=System.Drawing.Color.Blue;
            routes.Routes.Add(r);
            

            // add route start/end marks
            GMapMarker m1 = new GMarkerGoogle(startFromTable, GMarkerGoogleType.green_big_go);
            //m1.ToolTipText = "Start: " + "my route";
            m1.ToolTipMode = MarkerTooltipMode.Always;

            GMapMarker m2 = new GMarkerGoogle(endFromTable, GMarkerGoogleType.red_big_stop);
            //m2.ToolTipText = "End: " + end.ToString();
            m2.ToolTipMode = MarkerTooltipMode.Always;

            objects.Markers.Add(m1);
            objects.Markers.Add(m2);

            gMapControl1.ZoomAndCenterRoute(r);
                        }
                    }
}
			}
			}catch(Exception e)
		{
		}
			addOverlays();
		}
		public void StartPozition()
		{
			 GeoCoderStatusCode status = GeoCoderStatusCode.Unknow;
               {
                  PointLatLng? pos = GMapProviders.GoogleMap.GetPoint("Romania, Bucuresti", out status);
                  if(pos != null && status == GeoCoderStatusCode.G_GEO_SUCCESS)
                  {
                     currentMarker.Position = pos.Value;

                     //GMapMarker myCity = new GMarkerGoogle(pos.Value, GMarkerGoogleType.green_small);
                     //myCity.ToolTipMode = MarkerTooltipMode.Always;
                     //myCity.ToolTipText = "test";
                     //objects.Markers.Add(myCity);
                  }
               }
		}
		void MainMap_MouseDown(object sender, MouseEventArgs e)
      {
         if(e.Button == MouseButtons.Left)
         {
         	List<Placemark> plc = null;
        var st = GMapProviders.GoogleMap.GetPlacemarks(gMapControl1.FromLocalToLatLng(e.X, e.Y), out plc);
        if (st == GeoCoderStatusCode.G_GEO_SUCCESS && plc != null)
        {
        	City =plc[0].Address.ToString();
            //foreach (var pl in plc)
            //{
           // 	City=pl.Address.ToString();
                
            
        }
            isMouseDown = true;

            if(currentMarker.IsVisible)
            {
               currentMarker.Position = gMapControl1.FromLocalToLatLng(e.X, e.Y);

               var px = gMapControl1.MapProvider.Projection.FromLatLngToPixel(currentMarker.Position.Lat, currentMarker.Position.Lng, (int) gMapControl1.Zoom);
               var tile = gMapControl1.MapProvider.Projection.FromPixelToTileXY(px);
 
               //Debug.WriteLine("MouseDown: geo: " + currentMarker.Position + " | px: " + px + " | tile: " + tile);
            }
         }
         
      }
		void OnTileCacheProgress(int left)
      {
         if(!IsDisposed)
         {
            MethodInvoker m = delegate
            {
               //textBoxCacheStatus.Text = left + " tile to save...";
            };
            Invoke(m);
         }
      }
		void OnTileCacheStart()
      {
         //Debug.WriteLine("OnTileCacheStart");

         if(!IsDisposed)
         {
            MethodInvoker m = delegate
            {
               //textBoxCacheStatus.Text = "saving tiles...";
            };
            Invoke(m);
         }
      }
		void OnTileCacheComplete()
      {
         //Debug.WriteLine("OnTileCacheComplete");
         long size = 0;
         int db = 0;
         try
         {
            DirectoryInfo di = new DirectoryInfo(gMapControl1.CacheLocation);
            var dbs = di.GetFiles("*.gmdb", SearchOption.AllDirectories);
            foreach(var d in dbs)
            {
               size += d.Length;
               db++;
            }
         }
         catch
         {
         }

         if(!IsDisposed)
         {
            MethodInvoker m = delegate
            {
               //textBoxCacheSize.Text = string.Format(CultureInfo.InvariantCulture, "{0} db in {1:00} MB", db, size / (1024.0 * 1024.0));
               //textBoxCacheStatus.Text = "all tiles saved!";
            };

            if (!IsDisposed)
            {
               try
               {
                    Invoke(m);
               }
               catch(Exception)
               {
               }
            }
         }
      }
 		void MainMap_OnRouteEnter(GMapRoute item)
      {
         currentRoute = item;
         item.Stroke.Color = Color.Red;
         //Debug.WriteLine("OnRouteEnter: " + item.Name);
      }
 		void MainMap_OnRouteLeave(GMapRoute item)
      {
         currentRoute = null;
         item.Stroke.Color = Color.MidnightBlue;
        // Debug.WriteLine("OnRouteLeave: " + item.Name);
      }
	 void MainMap_OnPolygonLeave(GMapPolygon item)
      {
         currentPolygon = null;
         item.Stroke.Color = Color.MidnightBlue;
         //Debug.WriteLine("OnPolygonLeave: " + item.Name);
      }
		  void MainMap_OnPolygonEnter(GMapPolygon item)
      {
         currentPolygon = item;
         item.Stroke.Color = Color.Red;
         //Debug.WriteLine("OnPolygonEnter: " + item.Name);
      }
		void MainMap_OnMarkerEnter(GMapMarker item)
      {
         if(item is GMapMarkerRect)
         {
            GMapMarkerRect rc = item as GMapMarkerRect;
            rc.Pen.Color = Color.Red;

            CurentRectMarker = rc;
         }         
         //Debug.WriteLine("OnMarkerEnter: " + item.Position); 
      }
void MainMap_OnMarkerLeave(GMapMarker item)
      {
         if(item is GMapMarkerRect)
         {
            CurentRectMarker = null;

            GMapMarkerRect rc = item as GMapMarkerRect;
            rc.Pen.Color = Color.Blue;

			//Debug.WriteLine("OnMarkerLeave: " + item.Position);
         }
      }
		 void MainMap_OnMapTypeChanged(GMapProvider type)
      {
         comboBox1.SelectedItem = type;

         //trackBar1.Minimum = MainMap.MinZoom * 100;
         //trackBar1.Maximum = MainMap.MaxZoom * 100;

         //centermarkers
         //if(radioButtonFlight.Checked)
         //{
         //   MainMap.ZoomAndCenterMarkers("objects");
         //}
      }

		 // MapZoomChanged
      void MainMap_OnMapZoomChanged()
      {
         //trackBar1.Value = (int) (MainMap.Zoom * 100.0);
         //textBoxZoomCurrent.Text = MainMap.Zoom.ToString();
      }
		void MainMap_OnTileLoadStart()
      {
         MethodInvoker m = delegate()
         {
         	// loading text pentru utilizator
            //panelMenu.Text = "Menu: loading tiles...";
         };
         try
         {
            BeginInvoke(m);
         }
         catch
         {
         }
      }
		// loader end loading tiles
      void MainMap_OnTileLoadComplete(long ElapsedMilliseconds)
      {
        //text load time 
      	//MainMap.ElapsedMilliseconds = ElapsedMilliseconds;

         MethodInvoker m = delegate()
         {
           // panelMenu.Text = "Menu, last load in " + MainMap.ElapsedMilliseconds + "ms";

           // textBoxMemory.Text = string.Format(CultureInfo.InvariantCulture, "{0:0.00} MB of {1:0.00} MB", MainMap.Manager.MemoryCache.Size, MainMap.Manager.MemoryCache.Capacity);
         };
         try
         {
            BeginInvoke(m);
         }
         catch
         {
         }
      }
		void MainMap_OnPositionChanged(PointLatLng point)
      {
		 //textbox latitudine longitudine
         //textBoxLatCurrent.Text = point.Lat.ToString(CultureInfo.InvariantCulture);
         //textBoxLngCurrent.Text = point.Lng.ToString(CultureInfo.InvariantCulture);

         lock(flights)
         {
            lastPosition = point;
            lastZoom = (int) gMapControl1.Zoom;
         }
      }
		public void addOverlays()
		{
			currentMarker = new GMarkerGoogle(gMapControl1.Position, GMarkerGoogleType.arrow);          
            currentMarker.IsHitTestVisible = false; 
            top.Markers.Add(currentMarker);
			gMapControl1.Overlays.Add(routes);
            gMapControl1.Overlays.Add(polygons);
            gMapControl1.Overlays.Add(objects);
           	gMapControl1.Overlays.Add(top);
           	try
               {
                  GMapOverlay overlay = DeepClone<GMapOverlay>(objects);
                  //Debug.WriteLine("ISerializable status for markers: OK");

                  GMapOverlay overlay2 = DeepClone<GMapOverlay>(polygons);
                  //Debug.WriteLine("ISerializable status for polygons: OK");

                  GMapOverlay overlay3 = DeepClone<GMapOverlay>(routes);
                  //Debug.WriteLine("ISerializable status for routes: OK");
               }
               catch(Exception ex)
               {
                  //Debug.WriteLine("ISerializable failure: " + ex.Message);

#if DEBUG
                  //if(Debugger.IsAttached)
                  //{
                  //   Debugger.Break();
                  //}
#endif
               }
		}
		 public T DeepClone<T>(T obj)
      {
         using(var ms = new System.IO.MemoryStream())
         {
            var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

            formatter.Serialize(ms, obj);

            ms.Position = 0;

            return (T) formatter.Deserialize(ms);
         }
      }

		void MainMap_OnMarkerClick(GMapMarker item, MouseEventArgs e)
      {
			
         if(e.Button == System.Windows.Forms.MouseButtons.Left)
         {
            if(item is GMapMarkerRect)
            {
               GeoCoderStatusCode status;
               var pos = GMapProviders.GoogleMap.GetPlacemark(item.Position, out status);
               if(status == GeoCoderStatusCode.G_GEO_SUCCESS && pos != null)
               {
                  GMapMarkerRect v = item as GMapMarkerRect;
                  {
                     v.ToolTipText = pos.Value.Address;
                  }
                  gMapControl1.Invalidate(false);
               }
            }
            else
            {
               if(item.Tag != null)
               {
                  if(currentTransport != null)
                  {
                     currentTransport.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                     currentTransport = null;
                  }
                  currentTransport = item;
                  currentTransport.ToolTipMode = MarkerTooltipMode.Always;
               }
            }
         }
        
      }
		public void populateComboBoxWihtMaps()
		{
			comboBox1.DataSource=GMapProviders.List;
			comboBox1.SelectedItem=GMapProviders.GoogleMap;
		}
		void ComboBox1SelectedIndexChanged(object sender, EventArgs e)
		{
			 gMapControl1.MapProvider = comboBox1.SelectedItem as GMapProvider;
		}
		void Button1Click(object sender, EventArgs e)
		{
	 			start = currentMarker.Position;
	 			CityStart=City;
		}
		void Button2Click(object sender, EventArgs e)
		{
			end = currentMarker.Position;
			CityEnd=City;
		}
		void GMapControl1Click(object sender, EventArgs e)
		{
	
		}
		void Button3Click(object sender, EventArgs e)
		{
			double distance;
			String Nume;
			RoutingProvider rp = gMapControl1.MapProvider as RoutingProvider;
         if(rp == null)
         {
            rp = GMapProviders.GoogleMap; // use OpenStreetMap if provider does not implement routing
         }
         //routa
			GDirections ss;
			var xx = GMapProviders.GoogleMap.GetDirections(out ss, start, end, false, false, false, false, false);
			
			
         //MapRoute route = rp.GetRoute(start, end, false, false, (int) gMapControl1.Zoom);
         //MapRoute route = rp.GetRoute(start, end, false, false, 15);
         if(xx != null)
         {
            // add route
            
            GMapRoute r = new GMapRoute(ss.Route, "My route");
            distance = r.Distance;
            r.IsHitTestVisible = true;
            routes.Routes.Add(r);

            // add route start/end marks
            GMapMarker m1 = new GMarkerGoogle(start, GMarkerGoogleType.green_big_go);
            m1.ToolTipText = "Start: " + "my route";
            m1.ToolTipMode = MarkerTooltipMode.Always;

            GMapMarker m2 = new GMarkerGoogle(end, GMarkerGoogleType.red_big_stop);
            m2.ToolTipText = "End: " + end.ToString();
            m2.ToolTipMode = MarkerTooltipMode.Always;

            objects.Markers.Add(m1);
            objects.Markers.Add(m2);

            gMapControl1.ZoomAndCenterRoute(r);
            
            
             using (MySqlConnection con = new MySqlConnection(CurseForm.myStringCon))
    {
        MySqlCommand command = new MySqlCommand("insert_traseu;", con);
    command.CommandType = System.Data.CommandType.StoredProcedure;
    	// Add your parameters here if you need them
    	command.Parameters.Add(new MySqlParameter("ID_Comanda_i", CurseForm.ID_ComandaForHarta));
    	command.Parameters.Add(new MySqlParameter("CityStart_i", CityStart.ToString()));
    	command.Parameters.Add(new MySqlParameter("LatStart_i", start.Lat));
    	command.Parameters.Add(new MySqlParameter("LongStart_i", start.Lng.ToString()));
        command.Parameters.Add(new MySqlParameter("CityEnd_i", CityEnd));
        command.Parameters.Add(new MySqlParameter("LatEnd_i", end.Lat.ToString()));
        command.Parameters.Add(new MySqlParameter("LongEnd_i", end.Lng.ToString()));
        command.Parameters.Add(new MySqlParameter("Distance_i", distance));
    con.Open();
    command.ExecuteNonQuery();
    //int result = (int)command.ExecuteScalar();
    //MessageBox.Show("Date Introduse",
    //"Trans-auto");
    }
         }
         
        
		}
		public void ChangeMap()
		{
			GeoCoderStatusCode status = gMapControl1.SetPositionByKeywords(textBox1.Text);
            if(status != GeoCoderStatusCode.G_GEO_SUCCESS)
            {
               MessageBox.Show("Geocoder can't find: '" + textBox1.Text + "', reason: " + status.ToString(), "GMap.NET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
		}
		void Button4Click(object sender, EventArgs e)
		{
			GeoCoderStatusCode status = gMapControl1.SetPositionByKeywords(textBox1.Text);
            if(status != GeoCoderStatusCode.G_GEO_SUCCESS)
            {
               MessageBox.Show("Geocoder can't find: '" + textBox1.Text + "', reason: " + status.ToString(), "GMap.NET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
		}
		void Button5Click(object sender, EventArgs e)
		{
			gMapControl1.Zoom=gMapControl1.Zoom+1;
		}
		void Button6Click(object sender, EventArgs e)
		{
			gMapControl1.Zoom=gMapControl1.Zoom-1;
		}
	}
}
